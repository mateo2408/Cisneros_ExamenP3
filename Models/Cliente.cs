using SQLite;
namespace Cisneros_ExamenP3.Models;

public class Cliente
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;
    public int AntiguedadMeses { get; set; }
    public bool Activo { get; set; }
}