using System.Data.Common;

internal class ModelPrizes : DB {
    public ModelPrizes() : base () {
        this.table = "PRIZES";
    }

    public bool AddPrize(Prizes prize)
    {
        try
        {
            // Construir la cadena de columnas y valores para el query
            string columns = "Name, Price, Amount";
            string values = $"'{prize.Name}', {prize.Price}, {prize.Amount}";

            // Usar el método Create de la clase base para insertar el premio
            Create(columns, values);

            // Si la inserción es exitosa, retornamos true
            return true;
        }
        catch (Exception ex)
        {
            // Si ocurre un error, lo mostramos y retornamos false
            Console.WriteLine($"Error al agregar el premio: {ex.Message}");
            return false;
        }
    }
        // Método para agregar una cantidad a un premio existente
    public bool AddPrizeAmount(int prizeId, int additionalAmount)
    {
        try
        {
            // Verificamos que la cantidad adicional sea positiva
            if (additionalAmount <= 0)
            {
                Console.WriteLine("La cantidad a agregar debe ser mayor que cero.");
                return false;
            }

            // Construir el query para actualizar la cantidad
            string setColumns = $"AMOUNT = AMOUNT + {additionalAmount}";
            string condition = $"ID = {prizeId}";

            // Usar el método Update de la clase base para actualizar la cantidad
            Update(setColumns, condition);

            Console.WriteLine($"Cantidad de premio con ID {prizeId} aumentada en {additionalAmount}.");
            return true;
        }
        catch (Exception ex)
        {
            // Si ocurre un error, lo mostramos y retornamos false
            Console.WriteLine($"Error al agregar la cantidad de premios: {ex.Message}");
            return false;
        }
    }
        public bool DeletePrizeById(int prizeId)
    {
        try
        {
            // Condición para eliminar el premio con el ID proporcionado
            string condition = $"ID = {prizeId}";

            // Usar el método Delete de la clase base para ejecutar el query de eliminación
            Delete(condition);

            Console.WriteLine($"Premio con ID {prizeId} ha sido eliminado.");
            return true;
        }
        catch (Exception ex)
        {
            // Si ocurre un error, lo mostramos y retornamos false
            Console.WriteLine($"Error al eliminar el premio: {ex.Message}");
            return false;
        }
    }
        public bool EditPrize(int prizeId, string newName, float newPrice, int newAmount)
    {
        try
        {
            // Verificar si el premio con el ID existe
            var existingPrize = Read($"ID = {prizeId}");
            if (existingPrize.Count == 0)
            {
                Console.WriteLine($"No se encontró un premio con el ID {prizeId}.");
                return false;
            }

            // Construir las columnas a actualizar y sus nuevos valores
            string setColumns = $"NAME = '{newName}', PRICE = {newPrice}, AMOUNT = {newAmount}";
            string condition = $"ID = {prizeId}";

            // Usar el método Update de la clase base para actualizar el premio
            Update(setColumns, condition);

            Console.WriteLine($"El premio con ID {prizeId} ha sido actualizado.");
            return true;
        }
        catch (Exception ex)
        {
            // Si ocurre un error, lo mostramos y retornamos false
            Console.WriteLine($"Error al editar el premio: {ex.Message}");
            return false;
        }
    }
    public List<Prizes> GetAllPrizes()
{
    List<Prizes> prizesList = new List<Prizes>(); // Creamos la lista para almacenar los premios

    try
    {
        // Obtenemos todos los premios desde la base de datos
        var prizesData = Read();

        // Iteramos sobre los resultados obtenidos y creamos objetos de tipo Prizes
        foreach (var prize in prizesData)
        {
            // Comprobamos si los valores son nulos antes de asignarlos
            int id = Convert.ToInt32(prize["ID"]);
            string name = prize["NAME"]?.ToString() ?? "Sin nombre"; // Si es null, asigna "Sin nombre"
            float price = prize["PRICE"] != DBNull.Value ? Convert.ToSingle(prize["PRICE"]) : 0f; // Si es null, asigna 0f
            int amount = prize["AMOUNT"] != DBNull.Value ? Convert.ToInt32(prize["AMOUNT"]) : 0; // Si es null, asigna 0

            // Agregamos el objeto Prizes a la lista
            prizesList.Add(new Prizes(id, name, price, amount));
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al leer los premios: {ex.Message}");
    }

    return prizesList; // Devolvemos la lista completa de premios
}
}



public class Prizes
{
    private int id;
    private string name;
    private float price;
    private int amount;

    public Prizes(int id, string name, float price, int amount)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.amount = amount;
    }
    public int Id => id;
    public string Name => name;
    public float Price => price;
    public int Amount => amount;
    
}