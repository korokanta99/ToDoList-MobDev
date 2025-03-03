namespace MauiApp1;

public partial class SigninPage : ContentPage
{
	public SigninPage()
	{
		InitializeComponent();
	}

	public void Tap_Register(object sender, TappedEventArgs e)
	{
		
        Navigation.PushAsync(new SignupPage());
    }

}