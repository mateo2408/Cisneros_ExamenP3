using Cisneros_ExamenP3.ViewModels;

namespace Cisneros_ExamenP3;

public partial class MainPage : TabbedPage
{
    public MainPage(ClienteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}