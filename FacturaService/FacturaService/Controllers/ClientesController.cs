using FacturaBL.Admin;
using Modelos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacturaService.Controllers
{
    public class ClientesController : ApiController
    {
        TestBL BL = new TestBL();
        // GET: api/Clientes
        public IHttpActionResult Get()
        {
            IHttpActionResult ret = null;
            //return new string[] { "value1", "value2" };
            var jresult = BL.GetListActivos();
            if (jresult.Success)
                return Ok(jresult.Data);
                //return Json(jresult);
            else
                return Json(jresult);
            //return StatusCode(HttpStatusCode.InternalServerError);
        }

        // GET: api/Clientes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Clientes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clientes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clientes/5
        public void Delete(int id)
        {
        }
    }
}
