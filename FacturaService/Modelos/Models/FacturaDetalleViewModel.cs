
namespace SISCOL.Model
{
    public class FacturaDetalleViewModel
    {
    public int Id { get; set; }

    public int FacturaId { get; set; }
    public string Factura { get; set; }

    public int ProductoId { get; set; }
    public string Producto { get; set; }

    public int Cant { get; set; }

    public int Subtotal { get; set; }

    public int Estregistro { get; set; }

    }
}
