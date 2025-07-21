using ContactMap.Presentation.Maui.ViewModels;

namespace ContactMap.Presentation.Maui.Views;

public partial class PeoplePage : ContentPage
{
    public PeoplePage()
    {
        InitializeComponent();
        BindingContext = new MainMapViewModel();
    }
}
