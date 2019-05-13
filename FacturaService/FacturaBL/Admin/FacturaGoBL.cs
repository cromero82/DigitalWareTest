using Modelos.Models;
using Modelos.Utils;
using System;
using System.Data.Entity;
using System.Linq;

namespace FacturaBL.Admin
{
    public class FacturaGoBL
    {
        ModelEntities db = new ModelEntities();
        public JResult jresult = new JResult();

        public JResult GetFactura(int id)
        {
            try
            {
                var queryable = getQueriableBase();
                var dbItem = queryable.Where(w => w.Id == id).FirstOrDefault();
                jresult.Data = new FacturaViewModel
                {
                    Id = dbItem.Id,
                    ClienteId = dbItem.ClienteId,
                    //Cliente = dbItem.FACT_CLIENTE.Documento,
                    Fecha = dbItem.Fecha,//utilidades.ObtenerStringDesdeFecha(dbItem.Fecha),
                    Total = dbItem.Total,
                    Estregistro = dbItem.Estregistro,
                };

                #region Salida Generica OK
                return jresult.SetOk();
                #endregion
            }
            #region Salida generica para errores
            catch (Exception ex)
            {
                return jresult.SetError(ex, "Error registrando datos", this.GetType().Name);
            }
            #endregion
        }
        

        public JResult InsFactura(FacturaViewModel model)
        {
            try
            {
                FACT_FACTURA dbItem = new FACT_FACTURA
                {
                    Id = model.Id,
                    ClienteId = model.ClienteId,
                    Fecha = model.Fecha, //utilidades.ObtenerFechaDesdeString(model.Fecha),
                    Total = model.Total,
                    Estregistro = 1,
                };

                db.FACT_FACTURA.Attach(dbItem);
                db.Entry(dbItem).State = EntityState.Added;
                db.SaveChanges();
                model.Id = dbItem.Id;
                model.Estregistro = 1;
                jresult.Data = model;

                #region Salida Generica OK
                return jresult.SetOk("Registro creado correctamente");
                #endregion
            }
            #region Salida generica para errores
            catch (Exception ex)
            {
                return jresult.SetError(ex, "Error registrando datos", this.GetType().Name);
            }
            #endregion
        }

        public JResult UpdFactura(FacturaViewModel model)
        {
            try
            {
                var dbItem = db.FACT_FACTURA.Find(model.Id);
                dbItem.Id = model.Id;
                dbItem.ClienteId = model.ClienteId;
                dbItem.Fecha = model.Fecha;
                dbItem.Total = model.Total;
                dbItem.Estregistro = model.Estregistro;

                db.FACT_FACTURA.Attach(dbItem);
                db.Entry(dbItem).State = EntityState.Modified;
                db.SaveChanges();

                #region Salida Generica OK
                return jresult.SetOk();
                #endregion
            }
            #region Salida generica para errores
            catch (Exception ex)
            {
                return jresult.SetError(ex, "Error registrando datos", this.GetType().Name);
            }
            #endregion
        }

        public JResult UpdTotalFactura(int facturaId, int totalFactua)
        {
            try
            {
                var dbItem = db.FACT_FACTURA.Find(facturaId);
                dbItem.Total =totalFactua;

                db.FACT_FACTURA.Attach(dbItem);
                db.Entry(dbItem).State = EntityState.Modified;
                db.SaveChanges();

                #region Salida Generica OK
                return jresult.SetOk();
                #endregion
            }
            #region Salida generica para errores
            catch (Exception ex)
            {
                return jresult.SetError(ex, "Error registrando datos", this.GetType().Name);
            }
            #endregion
        }


        public JResult DelFactura(int id)
        {
            try
            {
                var dbItem = db.FACT_FACTURA.Find(id);
                dbItem.Estregistro = 4;
                db.FACT_FACTURA.Attach(dbItem);
                db.Entry(dbItem).State = EntityState.Modified;
                db.SaveChanges();

                #region Salida Generica OK
                return jresult.SetOk();
                #endregion
            }
            #region Salida generica para errores
            catch (Exception ex)
            {
                return jresult.SetError(ex, "Error registrando datos", this.GetType().Name);
            }
            #endregion
        }

        public JResult GetListActivos()
        {
            var lista = db.FACT_FACTURA.Where(w => w.Estregistro == 1).Select(dbItem => new FacturaViewModel
            {
                Id = dbItem.Id,
                ClienteId = dbItem.ClienteId,
                //Cliente = dbItem.FACT_CLIENTE.Documento,
                Fecha = dbItem.Fecha,//utilidades.ObtenerStringDesdeFecha(dbItem.Fecha),
                Total = dbItem.Total,
                Estregistro = dbItem.Estregistro,
            }).ToList<FacturaViewModel>();
            return jresult.SetOk(lista, "Datos consultados correctamente");
        }

        public IQueryable<FACT_FACTURA> getQueriableBase()
        {
            return db.FACT_FACTURA.Include(i => i.FACT_CLIENTE);
        }

        public IQueryable<FACT_FACTURA> getQueriableBaseActive()
        {
            return this.getQueriableBase().Where(w => w.Estregistro <= 2);
        }

        #region Validaciones

        public bool ValidacionUnique(FacturaViewModel model)
        {
            //var query = getQueriableBaseActive().Where(w => w.ClienteId == model.ClienteId && w.f && w.Estregistro == 1 );
            /*if (model.Id != null)
            {
                query = query.Where(w => w.Id != model.Id);
            }
            var data = query.FirstOrDefault();
            if (data != null)
            {
                jresult.Errores.addError("General", "Ya existe un registro con datos similares");
                return false;
            }*/
            return true;
        }

        #endregion
    }
}
