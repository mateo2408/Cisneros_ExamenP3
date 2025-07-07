using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cisneros_ExamenP3.Models;
using Cisneros_ExamenP3.Repository;
using System.Collections.ObjectModel;

namespace Cisneros_ExamenP3.ViewModels;

public partial class ClienteViewModel : ObservableObject
{
    [ObservableProperty] string nombre = string.Empty;
    [ObservableProperty] string empresa = string.Empty;
    [ObservableProperty] int antiguedadMeses;
    [ObservableProperty] bool activo;
    [ObservableProperty] string? error;

    [ObservableProperty]
    ObservableCollection<Cliente> clientes = new();

    [ObservableProperty]
    ObservableCollection<string> logs = new();

    readonly ClienteRepository _repository;

    public ClienteViewModel(ClienteRepository repository)
    {
        _repository = repository;
        FechaNacimiento = "07/07/2000"; // Cambia por tu fecha real
        NombreUsuario = "Mateo Cisneros"; // Cambia por tu nombre real
    }

    public string NombreUsuario { get; }
    public string FechaNacimiento { get; }

    [RelayCommand]
    public async Task ObtenerClientesAsync()
    {
        var lista = await _repository.GetClientesAsync();
        Clientes.Clear();
        foreach (var c in lista)
            Clientes.Add(c);
    }

    [RelayCommand]
    public async Task ObtenerLogsAsync()
    {
        await Task.Run(() =>
        {
            var lines = _repository.GetLogs();
            Logs.Clear();
            foreach (var l in lines)
                Logs.Add(l);
        });
    }

    [RelayCommand]
    public async Task GuardarAsync()
    {
        var cliente = new Cliente
        {
            Nombre = Nombre,
            Empresa = Empresa,
            AntiguedadMeses = AntiguedadMeses,
            Activo = Activo
        };
        if (!await _repository.AddClienteAsync(cliente))
            Error = "No se puede guardar: la antigüedad supera el límite.";
        else
        {
            Error = null;
            await ObtenerClientesAsync(); // Refresh the list
        }
    }

    [RelayCommand]
    public void LimpiarCampos()
    {
        Nombre = string.Empty;
        Empresa = string.Empty;
        AntiguedadMeses = 0;
        Activo = false;
        Error = null;
    }
}