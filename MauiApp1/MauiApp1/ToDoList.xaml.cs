namespace MauiApp1;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

public partial class ToDoList : ContentPage
{
	ObservableCollection<tasklist> taskview = new ObservableCollection<tasklist>();
	public ToDoList()
	{
		InitializeComponent();

		taskview.Add(new tasklist() { id = 1, taskname = "Eating", description = "I want to eat something." });
		taskview.Add(new tasklist() { id = 2, taskname = "Playing", description = "I want to play something." });
		taskview.Add(new tasklist() { id = 3, taskname = "Sleeping", description = "I want to sleep later." });
		taskview.Add(new tasklist() { id = 4, taskname = "Watching", description = "I want to watch a movie." });
		taskLV.ItemsSource = taskview;

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