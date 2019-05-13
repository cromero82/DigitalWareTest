using Modelos.Models;
using Modelos.Model;
using Modelos.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using SISCOL.Model;

namespace FacturaBL.Admin
{
	public class InventarioBL
	{
	  ModelEntities db = new ModelEntities();
	  public JResult jresult = new JResult();

	  public JResult GetInventario(int id)
	  {
	  	try
	  	{
	  		var queryable = getQueriableBase();
	  		var dbItem = queryable.Where(w => w.Id == id).FirstOrDefault();
	  		jresult.Data = new InventarioViewModel
	  		{
	  			Id = dbItem.Id,
	  			ProductoId = dbItem.ProductoId,
	  			Producto = dbItem.FACT_PRODUCTO.Nombre,
	  			Existencias = dbItem.Existencias,
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

	  public JResult InsInventario( InventarioViewModel model)
	  {
	  	try
	  	{
	  		FACT_INVENTARIO dbItem = new FACT_INVENTARIO
	  		{
	  			Id = model.Id,
	  			ProductoId = model.ProductoId,
	  			Existencias = model.Existencias,
	  			Estregistro = 1,
	  		};

	  		db.FACT_INVENTARIO.Attach(dbItem);
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

	  public JResult UpdInventario( InventarioViewModel model)
	  {
	  	try
	  	{
	  		var dbItem = db.FACT_INVENTARIO.Find(model.Id);
	  		dbItem.Id = model.Id;
	  		dbItem.ProductoId = model.ProductoId;
	  		dbItem.Existencias = model.Existencias;
	  		dbItem.Estregistro = model.Estregistro;

	  		db.FACT_INVENTARIO.Attach(dbItem);
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

	  public JResult DelInventario ( int id)
	  {
	  	try
	  	{
	  		var dbItem = db.FACT_INVENTARIO.Find(id);
	  		dbItem.Estregistro = 4;
	  		db.FACT_INVENTARIO.Attach(dbItem);
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
	  	var lista = db.FACT_INVENTARIO.Where(w => w.Estregistro == 1).Select(dbItem => new InventarioViewModel
          {
              Id = dbItem.Id,
              ProductoId = dbItem.ProductoId,
              Producto = dbItem.FACT_PRODUCTO.Nombre,
              Existencias = dbItem.Existencias,
              Estregistro = dbItem.Estregistro,
          }).ToList<InventarioViewModel>();
	  	return jresult.SetOk(lista, "Datos consultados correctamente");
	  }

	  public IQueryable<FACT_INVENTARIO> getQueriableBase()
	  {
	  	return db.FACT_INVENTARIO.Include( i => i.FACT_PRODUCTO).Include( i => i.FACT_PRODUCTO).AsQueryable();
	  }

	  public IQueryable<FACT_INVENTARIO> getQueriableBaseActive()
	  {
	  	return this.getQueriableBase().Where(w => w.Estregistro <= 2).AsQueryable();
	  }

	  #region Validaciones

	  public bool ValidacionUnique(InventarioViewModel model )
	  {
	  	var query = getQueriableBaseActive().Where(w => w.ProductoId == model.ProductoId && w.Estregistro == 1 );
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

