using MySql.Data.MySqlClient;
using System.Data;

public class DatabaseService
{
    private readonly MySqlConnection _connection;

    public DatabaseService(MySqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<Producto?> GetProducto()
    {
        Producto? producto = null;

        await _connection.OpenAsync();

        var query = "SELECT id, nombre, descripcion, precio FROM productos WHERE id = 1";

        using var cmd = new MySqlCommand(query, _connection);
        using var reader = await cmd.ExecuteReaderAsync();

        if(await reader.ReadAsync())
        {
            producto = new Producto
            {
                id = reader.GetInt32("id"),
                nombre = reader.GetString("nombre"),
                descripcion = reader.GetString("descripcion"),
                precio = reader.GetInt32("precio")

            };
        }

        await _connection.CloseAsync();

        return producto;
    }

}