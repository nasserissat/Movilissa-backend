namespace Movilissa_api.Models;

public class BusStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
}
// Ejemplo de estados predefinidos en la base de datos:
   // 1 - EnServicio
   // 2 - EnMantenimiento
   // 3 - Disponible
   // 4 - Reservado
   // 5 - FueradeServicio