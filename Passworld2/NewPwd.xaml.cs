namespace Passworld2;

public partial class NewPwd : ContentPage
{
	public NewPwd()
	{
		InitializeComponent();
	}

	private async void OnAddPwd(object sender, EventArgs e)
	{
        if (NewUsnInput.Text != "" || NewPwdInput.Text != "" || NewWebInput.Text != "" || NewTypePick.ItemsSource != null)
        {
            if (await this.DisplayAlert(
            "Add password?",
            "Are you sure about that?",
            "Yes",
            "No"))
            {
                try
                {
                    NewUsnInput.Text = "NUHUH";
                    await DisplayAlert(
                        "Success",
                        "New password has been added successfully!",
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
        else
        {
            await DisplayAlert(
                "Error",
                "Please fill out all fields.",
                "OK"
                );
        }
	}
}