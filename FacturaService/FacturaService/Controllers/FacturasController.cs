using FacturaBL.Admin;
using Modelos.Models;
using Modelos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacturaService.Controllers
{
    public class FacturasController : ApiController
    {
        FacturaGoBL BL = new FacturaGoBL();
         
        // GET: api/Factura
        public IHttpActionResult Get()
        {
            var jresult = BL.GetListActivos();
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // GET: api/Factura/5
        public IHttpActionResult Get([FromUri] int id)
        {
            var jresult = BL.GetFactura((int)id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // PUT: api/Factura
        public IHttpActionResult Put([FromBody] FacturaViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.UpdFactura(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // POST: api/Factura
        public IHttpActionResult Post([FromBody] FacturaViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.InsFactura(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // DELETE: api/Factura/5
        public IHttpActionResult delete([FromUri] int id)
        {
            var jresult = BL.GetFactura(id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }
    }
}
