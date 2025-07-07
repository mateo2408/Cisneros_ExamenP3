using Cisneros_ExamenP3.ViewModels;

namespace Cisneros_ExamenP3.Pages;

public partial class RegistroPage : ContentPage
{
    public RegistroPage(ClienteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
