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
    public class FacturaDetalleController : ApiController
    {
	  FacturaDetalleBL BL = new FacturaDetalleBL();

        // GET: api/FacturaDetalle
        public IHttpActionResult Get()
        {
            var jresult = BL.GetListActivos();
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // GET: api/FacturaDetalle/5
        public IHttpActionResult Get([FromUri] int id)
        {
            var jresult = BL.GetFacturaDetalle((int)id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // PUT: api/FacturaDetalle
        public IHttpActionResult Put([FromBody] FacturaDetalleViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.UpdFacturaDetalle(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // POST: api/FacturaDetalle
        public IHttpActionResult Post([FromBody] FacturaDetalleViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.InsFacturaDetalle(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // DELETE: api/FacturaDetalle/5
        public IHttpActionResult delete([FromUri] int id)
        {
            var jresult = BL.GetFacturaDetalle(id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

    }
}
