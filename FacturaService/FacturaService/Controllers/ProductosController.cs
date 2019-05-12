using FacturaBL.Admin;
using Modelos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacturaService.Controllers
{
    public class ProductosController : ApiController
    {
        ProductoBL BL = new ProductoBL();

        // GET: api/Producto
        public IHttpActionResult Get()
        {
            var jresult = BL.GetListActivos();
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // GET: api/Producto/5
        public IHttpActionResult Get([FromUri] int id)
        {
            var jresult = BL.GetProducto((int)id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // PUT: api/Producto
        public IHttpActionResult Put([FromBody] ProductoViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }
                // Normal fluent
                var jresult = BL.UpdProducto(model);
                if (jresult.Success)
                    return Ok(jresult);
                else
                {
                    // Internal errors
                    return Json(jresult);
                }
            }
        
// POST: api/Producto
        public IHttpActionResult Post([FromBody] ProductoViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.InsProducto(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // DELETE: api/Producto/5
        public IHttpActionResult delete([FromUri] int id)
        {
            var jresult = BL.GetProducto(id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

    }

}
