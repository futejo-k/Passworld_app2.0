namespace Passworld2;

public partial class NewPwd : ContentPage
{
	public NewPwd()
	{
		InitializeComponent();
	}

	private async void OnAddPwd(object sender, EventArgs e)
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
                await Navigation.PushAsync(new MainPage());
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
}