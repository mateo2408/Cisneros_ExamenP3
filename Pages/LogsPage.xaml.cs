using Cisneros_ExamenP3.ViewModels;

namespace Cisneros_ExamenP3.Pages;

public partial class LogsPage : ContentPage
{
    public LogsPage(ClienteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ClienteViewModel viewModel)
        {
            await viewModel.ObtenerLogsCommand.ExecuteAsync(null);
        }
    }
}
