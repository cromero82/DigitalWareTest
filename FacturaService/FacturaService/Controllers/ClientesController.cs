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
    public class ClientesController : ApiController
    {
        ClienteBL BL = new ClienteBL();

        // GET: api/Cliente
        public IHttpActionResult Get()
        {
            var jresult = BL.GetListActivos();
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // GET: api/Cliente/5
        public IHttpActionResult Get(int id)
        {
            var jresult = BL.GetCliente((int)id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // PUT: api/Cliente
        public IHttpActionResult Put([FromBody] ClienteViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.UpdCliente(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // POST: api/Cliente
        public IHttpActionResult Post( [FromBody] ClienteViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                return Json(BL.jresult);
            }

            // Normal fluent
            var jresult = BL.InsCliente(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // DELETE: api/Cliente/5
        public IHttpActionResult delete([FromUri]int id)
        {
            var jresult = BL.DelCliente(id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

        // GET: api/Clientes/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Clientes
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Clientes/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Clientes/5
        //public void Delete(int id)
        //{
        //}




    }
}
