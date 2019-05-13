using Modelos.Models;
using Modelos.Utils;
using System;
using System.Data.Entity;
using System.Linq;

namespace FacturaBL.Admin
{
	public class ClienteBL
	{
        ModelEntities db = new ModelEntities();
	  public JResult jresult = new JResult();

	  public JResult GetCliente(int id)
	  {
	  	try
	  	{
	  		var queryable = getQueriableBase();
	  		var dbItem = queryable.Where(w => w.Id == id).FirstOrDefault();
	  		jresult.Data = new ClienteViewModel
	  		{
	  			Id = dbItem.Id,
	  			Nombres = dbItem.Nombres,
	  			Documento = dbItem.Documento,
                FechaNacimiento = dbItem.FechaNacimiento,
	  			Telefono = dbItem.Telefono,
	  			Direccion = dbItem.Direccion,
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

	  public JResult InsCliente( ClienteViewModel model)
	  {
	  	try
	  	{
	  		FACT_CLIENTE dbItem = new FACT_CLIENTE
	  		{
	  			Id = model.Id,
	  			Documento = model.Documento.ToUpper(),
	  			Nombres = model.Nombres.ToUpper(),
                FechaNacimiento = model.FechaNacimiento,
                Telefono = model.Telefono,
	  			Direccion = model.Direccion.ToUpper(),
	  			Estregistro = 1,
	  		};

	  		db.FACT_CLIENTE.Attach(dbItem);
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

	  public JResult UpdCliente( ClienteViewModel model)
	  {
	  	try
	  	{
	  		var dbItem = db.FACT_CLIENTE.Find(model.Id);
	  		dbItem.Id = model.Id;
	  		dbItem.Documento = model.Documento.ToUpper();
	  		dbItem.Nombres = model.Nombres.ToUpper();
            dbItem.FechaNacimiento = model.FechaNacimiento;
	  		dbItem.Telefono = model.Telefono.ToUpper();
	  		dbItem.Direccion = model.Direccion.ToUpper();
	  		dbItem.Estregistro = model.Estregistro;

	  		db.FACT_CLIENTE.Attach(dbItem);
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

	  public JResult DelCliente ( int id)
	  {
	  	try
	  	{
	  		var dbItem = db.FACT_CLIENTE.Find(id);
	  		dbItem.Estregistro = 4;
	  		db.FACT_CLIENTE.Attach(dbItem);
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
	  	var lista = db.FACT_CLIENTE.Where(w => w.Estregistro == 1).Select(dbItem => new ClienteViewModel
          {
              Id = dbItem.Id,
              Nombres = dbItem.Nombres,
              Documento = dbItem.Documento,
              FechaNacimiento = dbItem.FechaNacimiento,
              Telefono = dbItem.Telefono,
              Direccion = dbItem.Direccion,
              Estregistro = dbItem.Estregistro,
          }).ToList<ClienteViewModel>();
	  	return jresult.SetOk(lista, "Datos consultados correctamente");
	  }	 

	  public IQueryable<FACT_CLIENTE> getQueriableBase()
	  {
	  	return db.FACT_CLIENTE.Include( i => i.FACT_FACTURA);
	  }

        public IQueryable<FACT_CLIENTE> getQueriableBaseActive()
        {
            return this.getQueriableBase().Where(w => w.Estregistro <= 2);
        }

        #region Validaciones

        public bool ValidacionUnique(ClienteViewModel model )
	  {
	  	var query = getQueriableBaseActive().Where(w => w.Documento == model.Documento && w.Estregistro == 1 );
	  	if (model.Id != null)
	  	{
	  		query = query.Where(w => w.Id != model.Id);
	  	};
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

