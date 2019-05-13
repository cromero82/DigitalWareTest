using FacturaBL.Admin;
using SISCOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacturaService.Controllers
{
    public class InventarioController : ApiController
    {
        InventarioBL BL = new InventarioBL();

        // GET: api/Inventario
        public IHttpActionResult Get()
        {
            var jresult = BL.GetListActivos();
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // GET: api/Inventario/5
        public IHttpActionResult Get([FromUri] int id)
        {
            var jresult = BL.GetInventario((int)id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // PUT: api/Inventario
        public IHttpActionResult Put([FromBody] InventarioViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.UpdInventario(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // POST: api/Inventario
        public IHttpActionResult Post([FromBody] InventarioViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.InsInventario(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // DELETE: api/Inventario/5
        public IHttpActionResult delete([FromUri] int id)
        {
            var jresult = BL.GetInventario(id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }
    }
}
