
namespace Modelos.Models
{
    public class FacturaViewModel
    {
    public int Id { get; set; }    

    public int ClienteId { get; set; }

        //public string Fecha { get; set; }
        public System.DateTime Fecha { get; set; }

        public int Total { get; set; }

    public int Estregistro { get; set; }

    }
}
