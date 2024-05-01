using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Passworld.Data;
using Passworld.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlowFishCS;

namespace Passworld.ViewModels
{
    public partial class PasswordViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;

        public PasswordViewModel(DatabaseContext context)
        {
            _context = context;
        }

        [ObservableProperty]
        private ObservableCollection<Password> _passwords = new();

        [ObservableProperty]
        private Password _operatingPassword = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        [ObservableProperty]
        private string _pwdKey;

        public async Task LoadPasswordsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var passwords = await _context.GetAllAsync<Password>();
                if (passwords is not null && passwords.Any())
                {
                    Passwords ??= new ObservableCollection<Password>();

                    foreach (var password in passwords)
                    {
                        Passwords.Add(password);
                    }
                }
            }, "Fetching passwords...");
        }

        [RelayCommand]
        private void SetOperatingPassword(Password? password) => OperatingPassword = password ?? new();

        [RelayCommand]
        private async Task Decrypt(Password? password)
        {
            OperatingPassword = password ?? new();

            await ExecuteAsync(async () =>
            {
                if (OperatingPassword.Pwd != null)
                {
                    string pwd = OperatingPassword.Pwd;
                    BlowFish bf = new BlowFish(PwdKey);
                    OperatingPassword.Pwd = bf.Decrypt_CBC(pwd);
                    SetOperatingPasswordCommand.Execute(new());
                }
            });
        }

        [RelayCommand]
        private async Task SavePasswordAsync()
        {
            
            if (OperatingPassword is null)
                return;

            if (PwdKey is null)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Encryption key required.", "OK");
            }

            var (isValid, errorMessage) = OperatingPassword.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "OK");
                return;
            }

            var busyText = OperatingPassword.PId == 0 ? "Creating password..." : "Updating product...";
            

            await ExecuteAsync(async () =>
            {
                if (OperatingPassword.PId == 0)
                {
                    string pwd = OperatingPassword.Pwd;
                    
                    BlowFish bf = new BlowFish(PwdKey);
                    string enc = bf.Encrypt_CBC(pwd);

                    OperatingPassword.Pwd = enc;

                    await _context.AddItemAsync<Password>(OperatingPassword);
                    Passwords.Add(OperatingPassword);
                }
                else
                {
                    // Updating pass
                    if (await _context.UpdateItemAsync<Password>(OperatingPassword))
                    {
                        var passwordCopy = OperatingPassword.Clone();
                        BlowFish bf = new BlowFish(PwdKey);

                        string pwd = passwordCopy.Pwd;

                        string enc = bf.Encrypt_CBC(pwd);

                        passwordCopy.Pwd = enc;

                        var index = Passwords.IndexOf(OperatingPassword);
                        Passwords.RemoveAt(index);

                        Passwords.Insert(index, passwordCopy);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Password updation error", "OK");
                        return;
                    }
                }
                SetOperatingPasswordCommand.Execute(new());
            }, busyText);
        }

        [RelayCommand]
        private async Task DeletePasswordAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByKeyAsync<Password>(id))
                {
                    var password = Passwords.FirstOrDefault(p => p.PId == id);
                    Passwords.Remove(password);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Password was not deleted.", "OK");
                }
            }, "Deleting password...");
        }

        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
            try
            {
                await operation?.Invoke();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }
    }
}
