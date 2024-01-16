namespace Passworld2;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

	private async void OnChngInfo(object sender, EventArgs e)
	{
        await DisplayAlert("Change your personal information", "Are you sure you want to change your personal information?", "Yes", "No");
    }

	private async void OnChngPwd(object sender, EventArgs e)
	{
		await DisplayAlert("Change your main password", "Are you sure you want to change your main password?", "Yes", "No");
	}

	private async void OnDltAcc(object sender, EventArgs e)
	{
		await DisplayAlert("Delelte your account", "Are you sure you want to delete your account? \nThis action cannot be taken back!", "Yes", "No");
	}
}