namespace MauiApp1;

public partial class SigninPage : ContentPage
{
	public SigninPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
    }

	public void Tap_Register(object sender, TappedEventArgs e)
	{
		
        Navigation.PushAsync(new SignupPage());
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NavigationPage(new AppShell()));
    }

}