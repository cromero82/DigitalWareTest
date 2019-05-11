using Modelos.Models;
using Modelos.Utils;
using System;
using System.Data.Entity;
using System.Linq;

namespace FacturaBL.Admin
{
    public class TestBL
    {
        ModelEntities db = new ModelEntities();
        public JResult jresult = new JResult();

        #region Funciones CRUD
        public IQueryable<DOM_TEST> getQueriableBase()
        {
            return db.DOM_TEST.AsQueryable();
        }

        public IQueryable<DOM_TEST> getQueriableBaseActive()
        {
            return this.getQueriableBase();
        }

        public TestViewModel getModelFromDbItem(DOM_TEST dbItem)
        {
            return new TestViewModel
            {
                id = dbItem.id,
                nombre = dbItem.nombre
              
            };
        }

        public DOM_TEST getDbItemFromViewModel(TestViewModel model)
        {
            return new DOM_TEST
            {
                id = model.id,
                nombre = model.nombre
            };
        }

        public JResult UpdTest(TestViewModel model)
        {
            try
            {
                var dbItem = getDbItemFromViewModel(model);
                //db.DOM_TEST.Attach(dbItem);
                db.Entry(dbItem).State = EntityState.Modified;
                db.SaveChanges();

                #region Salida Generica OK                
                return jresult.SetOk("Datos modificados correctamente");
                #endregion
            }
            #region Salida generica para errores
            catch (Exception ex)
            {
                return jresult.SetError(ex, "Error registrando datos");
            }
            #endregion
        }

        public JResult DelTest(int id)
        {
            try
            {
                var dbItem = db.DOM_TEST.Find(id);
                //dbItem.Estregistro = 4;
                db.DOM_TEST.Attach(dbItem);
                // Pendiente estado eliminado del rol
                //db.Entry(dbItem).State = EntityState.Modified;
                db.Entry(dbItem).State = EntityState.Deleted;
                db.SaveChanges();

                #region Salida Generica OK                
                return jresult.SetOk("Registro eliminado correctamente");
                #endregion
            }
            #region Salida generica para errores
            catch (Exception ex)
            {
                return jresult.SetError(ex, "Error eliminando registro");
            }
            #endregion
        }


        public JResult InsTest(TestViewModel model)
        {
            try
            {
                DOM_TEST dbItem = getDbItemFromViewModel(model);
                db.DOM_TEST.Attach(dbItem);
                db.Entry(dbItem).State = EntityState.Added;
                db.SaveChanges();
                // Salida success              
                return jresult.SetOk("Registro creado correctamente");
            }
            #region Salida generica para errores
            catch (Exception ex)
            {
                return jresult.SetError(ex, "Error registrando datos");
            }
            #endregion
        }

        public JResult GetListActivos()
        {
            //var listaTipos = db.DOM_TEST.Where(w => w.Estregistro == 1).ToList();

            var lista = getQueriableBase().Select(dbItem =>
            new AspGeneralViewModel
            {
                Id = dbItem.id,
                Nombre = dbItem.nombre
            }
            ).ToList<AspGeneralViewModel>();

            return jresult.SetOk(lista, "Datos consultados correctamente");
        }
        #endregion

        #region Validaciones

        public bool ValidacionUnique(TestViewModel model)
        {
            var query = getQueriableBaseActive().Where(w => w.nombre == model.nombre);
            if (model.id != null)
            {
                query = query.Where(w => w.id != model.id);
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
