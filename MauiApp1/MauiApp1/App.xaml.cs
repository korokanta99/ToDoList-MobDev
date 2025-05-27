using Microsoft.Maui.Storage;
using System;


namespace MauiApp1
{
    public partial class App : Application
    {
        public static NavigationPage NavPage;

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            string lastPage = Preferences.Get("LastPage", "SigninPage");
            Page startPage = lastPage switch
            {
                "TodoListPage" => new TodoList(),
                "CompleteListPage" => new CompleteList(),
                "SignupPage" => new SignupPage(),
                "SigninPage" => new SigninPage(),
                _ => new SigninPage(),
            };

            NavPage = new NavigationPage(startPage);
            return new Window(NavPage);
        }

        protected override void OnSleep()
        {
            base.OnSleep();

            if (NavPage?.CurrentPage is Page currentPage)
            {
                string pageName = currentPage switch
                {
                    TodoList => "TodoListPage",
                    CompleteList => "CompleteListPage",
                    SignupPage => "SignupPage",
                    SigninPage => "SigninPage",
                    _ => "SigninPage"
                };

                Preferences.Set("LastPage", pageName);
            }
        }
    }
}
