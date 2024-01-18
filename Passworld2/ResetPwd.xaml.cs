namespace Passworld2;

public partial class ResetPwd : ContentPage
{
	public ResetPwd()
	{
		InitializeComponent();
	}

	private async void OnResetPwd(object sender, EventArgs e)
	{
        if (NewPwd1.Text == NewPwd2.Text && NewPwd1 != null && NewPwd2 != null)
        {
            if (await this.DisplayAlert(
                "Change your main password",
                "Are you sure you want to change your main password?",
                "Yes", "No"))
            {
                try
                {
                    await DisplayAlert(
                        "Success",
                        "Your password has been changed successfully!",
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
        else if (NewPwd1.Text == null)
        {
            await DisplayAlert(
                "Error",
                "Field one is empty!\nPlease enter the same password in both fields.",
                "OK");
        }
        else if (NewPwd2.Text == null)
        {
            await DisplayAlert(
                "Error",
                "Field two is empty!\nPlease enter the same password in both fields.",
                "OK");
        }
        else if (NewPwd1.Text != NewPwd2.Text)
        {
            await DisplayAlert(
                "Error",
                "Your passwords don't match!\nPlease enter the same password in both fields.",
                "OK");
        }
    }
}