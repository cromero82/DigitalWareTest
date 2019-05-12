using Modelos.Model;
using Modelos.Models;
using Modelos.Utils;
using System;
using System.Data.Entity;
using System.Linq;

namespace FacturaBL.Admin
{
    public class ProductoBL
    {
        ModelEntities db = new ModelEntities();
        public JResult jresult = new JResult();

        public JResult GetProducto(int id)
        {
            try
            {
                var queryable = getQueriableBase();
                var dbItem = queryable.Where(w => w.Id == id).FirstOrDefault();
                jresult.Data = new ProductoViewModel
                {
                    Id = dbItem.Id,
                    Codigo = dbItem.Codigo,
                    Nombre = dbItem.Nombre,
                    Valor = dbItem.Valor,
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

        public JResult InsProducto(ProductoViewModel model)
        {
            try
            {
                FACT_PRODUCTO dbItem = new FACT_PRODUCTO
                {
                    Id = model.Id,
                    Codigo = model.Codigo.ToUpper(),
                    Nombre = model.Nombre.ToUpper(),
                    Valor = model.Valor,
                    Estregistro = 1
                };

                db.FACT_PRODUCTO.Attach(dbItem);
                db.Entry(dbItem).State = EntityState.Added;
                db.SaveChanges();
                model.Id = dbItem.Id;
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

        public JResult UpdProducto(ProductoViewModel model)
        {
            try
            {
                var dbItem = db.FACT_PRODUCTO.Find(model.Id);
                dbItem.Id = model.Id;
                dbItem.Codigo = model.Codigo.ToUpper();
                dbItem.Nombre = model.Nombre.ToUpper();
                dbItem.Valor = model.Valor;
                dbItem.Estregistro = model.Estregistro;

                db.FACT_PRODUCTO.Attach(dbItem);
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

        public JResult DelProducto(int id)
        {
            try
            {
                var dbItem = db.FACT_PRODUCTO.Find(id);
                dbItem.Estregistro = 4;
                db.FACT_PRODUCTO.Attach(dbItem);
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
            var lista = db.FACT_PRODUCTO.Where(w => w.Estregistro == 1).Select(dbItem => new GeneralViewModel
            {
                Id = dbItem.Id,
                Estregistro = dbItem.Estregistro,
                Nombre = dbItem.Nombre,
            }).ToList<GeneralViewModel>();
            return jresult.SetOk(lista, "Datos consultados correctamente");
        }

        public IQueryable<FACT_PRODUCTO> getQueriableBase()
        {
            return db.FACT_PRODUCTO.AsQueryable();
        }

        public IQueryable<FACT_PRODUCTO> getQueriableBaseActive()
        {
            return this.getQueriableBase().Where(w => w.Estregistro <= 2).AsQueryable();
        }

        #region Validaciones

        public bool ValidacionUnique(ProductoViewModel model)
        {
            var query = getQueriableBaseActive().Where(w => w.Nombre == model.Nombre && w.Estregistro == 1);
            if (model.Id != null)
            {
                query = query.Where(w => w.Id != model.Id);
            }
            var data = query.FirstOrDefault();
            if (data != null)
            {
                jresult.Errores.addError("General", "Ya existe un registro con datos similares");
                return false;
            }
            return true;
        }

        #endregion
    }
}

