namespace Passworld2;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

	private async void OnChngInfo(object sender, EventArgs e)
	{
		if (await this.DisplayAlert(
			"Change your personal information",
			"Are you sure you want to change your personal information?",
			"Yes", "No"))
		{
            try
            {
				StngsFNEdit.Text = "NUHUH";
                await DisplayAlert(
                    "Success",
                    "Your personal information has been changed successfully!",
                    "OK");
            }
            catch (Exception)
            {
                //Other error has occured
                await DisplayAlert(
                    "Error",
                    "An error has occured, please try again.",
                    "OK");
            }
        }
    }

	private async void OnChngPwd(object sender, EventArgs e)
	{
		if (await this.DisplayAlert(
            "Change your main password",
            "Are you sure you want to change your main password?",
            "Yes", "No"))
		{
            try
            {
                StngsLNEdit.Text = "NUHUH";
                await Navigation.PushAsync(new ResetPwd());
            }
            catch (Exception)
            {
                //Other error has occured
                await DisplayAlert(
                    "Error",
                    "An error has occured, please try again.",
                    "OK");
            }
        }
	}

	private async void OnDltAcc(object sender, EventArgs e)
	{
		await DisplayAlert(
            "Delelte your account",
            "Are you sure you want to delete your account? \nThis action cannot be taken back!",
            "Yes",
            "No");
	}
}