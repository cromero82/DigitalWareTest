using Microsoft.AspNetCore.Mvc;
using SISCOL.BL;
using SISCOL.Model;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SISCOL.Areas.General.Controllers
{
    [Area("General")]
    public class ClienteController : Controller
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
        public IHttpActionResult PutCliente(ClienteViewModel model)
        {
            // Validation logic errors
            BL.ValidacionUnique(model);
            if (BL.jresult.hasError())
            {
                ViewBag.Errores = BL.jresult.getListErrores();
                return PartialView("ClienteViewEdit", model);
            }

            // Normal fluent
            var jresult = BL.UpdPersona(model);
            if (jresult.Success)
                return Ok(jresult);
            else
            {
                // Internal errors
                return Json(jresult);
            }
        }

        // POST: api/Cliente
        public IHttpActionResult Post(ClienteViewModel model)
        {
                // Validation logic errors
                BL.ValidacionUnique(model);
                if (BL.jresult.hasError())
                {
                    ViewBag.Errores = BL.jresult.getListErrores();
                    return PartialView("ClienteViewEdit", model);
                }

                // Normal fluent
                var jresult = BL.InsPersona(model);
                if (jresult.Success)
                    return Ok(jresult);
                else
                {
                    // Internal errors
                    return Json(jresult);
                }
        }

        // DELETE: api/Cliente/5
        public IHttpActionResult delete(int id)
        {
            var jresult = BL.GetDelCliente(id);
            if (jresult.Success)
                return Ok(jresult);
            else
                return Json(jresult);
        }

    }
}

