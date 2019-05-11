using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Utils
{
    public class ErrorBuffer
    {
        public List<ErrorBuferItem> Errores { get; set; }
        public ErrorBuffer()
        {
            Errores = new List<ErrorBuferItem>();
        }

        public void addError(string nombre, string descripcion)
        {
            this.Errores.Add(new ErrorBuferItem
            {
                Nombre = nombre,
                Descripcion = descripcion
            });
        }

        //public List<Dictionary<string, string>> getErrores()
        //{
        //    var result = new List<Dictionary<string, string>>();
        //    foreach (var item in this.Errores)
        //    {
        //        result.Add(item.Nombre, item.Descripcion);
        //    }
        //    return result;
        //}
    }

    public class ErrorBuferItem
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
