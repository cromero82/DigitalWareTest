using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Utils
{
    public class JResult
    {
        public bool Success { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public ErrorBuffer Errores { get; set; }
        public JResult()
        {
            Success = false;
            Errores = new ErrorBuffer();
        }

        public JResult SetOk(string _message_ok = "Transacción ejecutada correctamente")
        {
            Success = true;
            Message = _message_ok;
            return this;
        }

        public JResult SetOk(dynamic data, string _message_ok = "Transacción ejecutada correctamente")
        {
            Data = data;
            Success = true;
            Message = _message_ok;
            return this;
        }

        public JResult SetError(Exception ex, string _message_error = "Error ejecutando transacción")
        {
            Message = _message_error;
            // Pendiente log personalizado (traceId)
            Success = false;
            return this;
        }

        public JResult SetError(Exception ex, string _message_error = "Error ejecutando transacción", string origen = "")
        {
            Message = _message_error;
            // Pendiente log personalizado (traceId)
            Success = false;
            return this;
        }

        public JResult SetErrorLogico()
        {
            Message = "Error ejecutando transacción";
            Success = false;
            // Pendiente log personalizado (traceId) foreach errores
            return this;
        }

        /// <summary>
        /// Determina si se detectaron errores de logica de negocio
        /// </summary>
        /// <returns></returns>
        public bool hasError()
        {
            if (Errores.Errores.Count > 0)
            {
                return true;
            }
            return false;
        }


        public List<string> getListErrores()
        {
            var errores = new List<string>();
            foreach (var itemError in Errores.Errores)
            {
                errores.Add(itemError.Nombre + "<@> " + itemError.Descripcion);
            }
            return errores;
        }
    }
}
