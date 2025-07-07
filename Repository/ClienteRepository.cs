using SQLite;
using System.IO;

namespace Cisneros_ExamenP3.Repository;

public class ClienteService
{
    readonly SQLiteAsyncConnection _db;
    readonly string _logPath;

    public ClienteService(string dbPath, string apellido)
    {
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<Cliente>().Wait();
        _logPath = Path.Combine(FileSystem.AppDataDirectory, $"Logs_{apellido}.txt");
    }

    public async Task<bool> AddClienteAsync(Cliente cliente)
    {
        if (cliente.AntiguedadMeses > 150) // 1500 días / 10 días por mes
            return false;

        await _db.InsertAsync(cliente);
        var log = $"Se incluyó el registro [{cliente.Nombre}] el {DateTime.Now:dd/MM/yyyy HH:mm}\n";
        File.AppendAllText(_logPath, log);
        return true;
    }

    public Task<List<Cliente>> GetClientesAsync() => _db.Table<Cliente>().ToListAsync();

    public string[] GetLogs() => File.Exists(_logPath) ? File.ReadAllLines(_logPath) : Array.Empty<string>();
}