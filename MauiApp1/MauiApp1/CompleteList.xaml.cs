using Microsoft.UI.Xaml.Controls;

namespace MauiApp1;

public partial class CompleteList : ContentPage
{
	public CompleteList()
	{
		InitializeComponent();
	}

    public void Tap_Delete(object sender, TappedEventArgs e)
    {
        if (sender is Image image)
        {
            if (image.BindingContext is tasklist itemToDelete)
            {
                Console.WriteLine($"Deleting task: {itemToDelete.taskname} (ID: {itemToDelete.id})");
                taskview.Remove(itemToDelete);
            }
        }
    }
}

