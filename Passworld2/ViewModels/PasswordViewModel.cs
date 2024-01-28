using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Passworld.Data;
using Passworld.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private ObservableCollection<Password> _password;

        [ObservableProperty]
        private Password _operatingPassword = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        [RelayCommand]
        private async Task LoadPasswordsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var passwords = await _context.GetAllAsync<Password>();
                if (passwords is not null && passwords.Any())
                {
                    if (Password is null)
                    {
                        Password ??= new ObservableCollection<Password>();

                        foreach (var password in passwords)
                        {
                            Password.Add(password);
                        }
                    }
                }
            }, "Fetching passwords...");
        }

        [RelayCommand]
        private void SetOperatingPassword(Password? password) => OperatingPassword = password ?? new();

        [RelayCommand]
        private async Task SavePasswordAsync()
        {
            if (OperatingPassword is null)
            {
                return;
            }

            var busyText = OperatingPassword.PId == 0 ? "Creating password..." : "Updating product...";

            await ExecuteAsync(async () =>
            {
                if (OperatingPassword.PId == 0)
                {
                    // Creating pass
                    await _context.AddItemAsync<Password>(OperatingPassword);
                    Password.Add(OperatingPassword);
                }
                else
                {
                    // Updating pass
                    await _context.UpdateItemAsync<Password>(OperatingPassword);

                    var passwordCopy = OperatingPassword.Clone();

                    var index = Password.IndexOf(OperatingPassword);
                    Password.RemoveAt(index);

                    Password.Insert(index, passwordCopy);
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
                    var password = Password.FirstOrDefault(p => p.PId == id);
                    Password.Remove(password);
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
                await operation.Invoke();
            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }
    }
}
