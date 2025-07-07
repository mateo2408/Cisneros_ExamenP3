using Cisneros_ExamenP3.ViewModels;

namespace Cisneros_ExamenP3.Pages;

public partial class ClientesPage : ContentPage
{
    public ClientesPage(ClienteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is ClienteViewModel viewModel)
        {
            await viewModel.ObtenerClientesCommand.ExecuteAsync(null);
        }
    }
}
