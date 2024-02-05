using Passworld.ViewModels;

namespace Passworld2
{
    public partial class MainPage : ContentPage
    {
        private readonly PasswordViewModel _viewModel;
        public MainPage(PasswordViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadPasswordsAsync();
        }

        private void OnSettings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());
        }

        private async void OnSearch(object sender, EventArgs e)
        {
            await DisplayAlert("Good job!", "You clicked the search button!", "OK");
        }
    }
}
