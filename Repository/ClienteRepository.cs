using SQLite;
using Cisneros_ExamenP3.Models;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cisneros_ExamenP3.Repository;

public class ClienteRepository
{
    readonly SQLiteAsyncConnection _db;
    readonly string _logPath;

    public ClienteRepository(string dbPath, string apellido)
    {
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<Cliente>().Wait();
        
        // Use AppData directory for runtime logs (this is the standard approach)
        _logPath = Path.Combine(FileSystem.AppDataDirectory, $"Logs_Cisneros.txt");
        
        // Initialize log file if it doesn't exist
        InitializeLogFile();
    }

    private void InitializeLogFile()
    {
        if (!File.Exists(_logPath))
        {
            var header = $"# Log File - Logs_{Path.GetFileNameWithoutExtension(_logPath)}.txt\n" +
                        $"# Created on {DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                        $"# Client registration logs\n\n";
            File.WriteAllText(_logPath, header);
        }
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

    public string[] GetLogs() => File.Exists(_logPath) ? File.ReadAllLines(_logPath) : System.Array.Empty<string>();
}