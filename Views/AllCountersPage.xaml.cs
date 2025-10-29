using CommunityToolkit.Mvvm.Input;
using Counter_Lekhkun_Andrii.Models;
using System.Collections.ObjectModel;

namespace Counter_Lekhkun_Andrii.Views;

public partial class AllCountersPage : ContentPage
{
    public AllCountersPage()
    {
        InitializeComponent();
    }

    private void ContentPage_NavigateTo(object sender, NavigatedToEventArgs e)
    {
        countersCollection.SelectedItem = null;
    }
}