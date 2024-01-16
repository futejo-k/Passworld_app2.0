namespace Passworld2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSettings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());
        }

        private async void OnSearch(object sender, EventArgs e)
        {
            await DisplayAlert("Good job!", "You clicked the search button!", "OK");
        }
        
        private void OnNewPwd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewPwd());
        }
    }
}
