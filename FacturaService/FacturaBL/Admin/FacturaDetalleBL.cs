using Modelos.Models;
using Modelos.Utils;
using SISCOL.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace FacturaBL.Admin
{
	public class FacturaDetalleBL
	{
	  ModelEntities db = new ModelEntities();
	  public JResult jresult = new JResult();

	  public JResult GetFacturaDetalle(int id)
	  {
	  	try
	  	{
	  		var queryable = getQueriableBase();
	  		var dbItem = queryable.Where(w => w.Id == id).FirstOrDefault();
	  		jresult.Data = new FacturaDetalleViewModel
	  		{
	  			Id = dbItem.Id,
	  			FacturaId = dbItem.FacturaId,
	  			//Factura = dbItem.FACT_FACTURA.,
	  			InventarioId = dbItem.InventarioId,
	  			Producto = dbItem.FACT_INVENTARIO.FACT_PRODUCTO.Nombre,
	  			Cant = dbItem.Cant,
	  			Subtotal = dbItem.Subtotal,
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

	  public JResult InsFacturaDetalle( FacturaDetalleViewModel model)
	  {
	  	try
	  	{
            ProductoBL productoBL = new ProductoBL();
            var producto = productoBL.GetProducto(model.InventarioId);

	  		FACT_FACTURA_DETALLE dbItem = new FACT_FACTURA_DETALLE
	  		{
	  			Id = model.Id,
	  			FacturaId = model.FacturaId,
	  			InventarioId = model.InventarioId,
	  			Cant = model.Cant,
	  			Subtotal = producto.Data.Valor * model.Cant,
	  			Estregistro = 1,
	  		};

	  		db.FACT_FACTURA_DETALLE.Attach(dbItem);
	  		db.Entry(dbItem).State = EntityState.Added;
	  		db.SaveChanges();

            var result = UpdTotalFactura(model.FacturaId);
            if (!result.Success)
            {
                return jresult.SetError(null, "Error actualizando total factura", this.GetType().Name);
            }

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

	  public JResult UpdFacturaDetalle( FacturaDetalleViewModel model)
	  {
	  	try
	  	{
            ProductoBL productoBL = new ProductoBL();
            var producto = productoBL.GetProducto(model.InventarioId);

            var dbItem = db.FACT_FACTURA_DETALLE.Find(model.Id);
	  		dbItem.Id = model.Id;
	  		dbItem.FacturaId = model.FacturaId;
	  		dbItem.InventarioId = model.InventarioId;
	  		dbItem.Cant = model.Cant;
            dbItem.Subtotal = producto.Data.Valor * model.Cant;
	  		dbItem.Estregistro = model.Estregistro;

	  		db.FACT_FACTURA_DETALLE.Attach(dbItem);
	  		db.Entry(dbItem).State = EntityState.Modified;
            
	  		db.SaveChanges();

            var result = UpdTotalFactura(model.FacturaId);
            if (!result.Success)
            {
                return jresult.SetError( null, "Error actualizando total factura", this.GetType().Name);
            }

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

        // Actualizar total factura
        public JResult UpdTotalFactura(int facturaId)
        {
            var total = db.FACT_FACTURA_DETALLE.Where(w => w.FacturaId == facturaId).Sum(f => f.Subtotal);
            FacturaGoBL BLFactura = new FacturaGoBL();
            return  BLFactura.UpdTotalFactura(facturaId, total > 0 ? total : 0);
        }

        public JResult DelFacturaDetalle ( int id)
	  {
	  	try
	  	{
	  		var dbItem = db.FACT_FACTURA_DETALLE.Find(id);
	  		dbItem.Estregistro = 4;
	  		db.FACT_FACTURA_DETALLE.Attach(dbItem);
	  		db.Entry(dbItem).State = EntityState.Modified;
	  		db.SaveChanges();

                var result = UpdTotalFactura( dbItem.FacturaId );
                if (!result.Success)
                {
                    return jresult.SetError(null, "Error actualizando total factura", this.GetType().Name);
                }

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
	  	var lista = db.FACT_FACTURA_DETALLE.Where(w => w.Estregistro == 1).Select(dbItem => new FacturaDetalleViewModel
          {
              Id = dbItem.Id,
              FacturaId = dbItem.FacturaId,
              //Factura = dbItem.FACT_FACTURA.,
              InventarioId = dbItem.InventarioId,
              Producto = dbItem.FACT_INVENTARIO.FACT_PRODUCTO.Nombre,
              Cant = dbItem.Cant,
              Subtotal = dbItem.Subtotal,
              Estregistro = dbItem.Estregistro
          }).ToList<FacturaDetalleViewModel>();
	  	return jresult.SetOk(lista, "Datos consultados correctamente");
	  }

	  public IQueryable<FACT_FACTURA_DETALLE> getQueriableBase()
	  {
	  	return db.FACT_FACTURA_DETALLE.Include( i => i.FACT_FACTURA).Include( i => i.FACT_INVENTARIO.FACT_PRODUCTO).AsQueryable();
	  }

	  public IQueryable<FACT_FACTURA_DETALLE> getQueriableBaseActive()
	  {
	  	return this.getQueriableBase().Where(w => w.Estregistro <= 2).AsQueryable();
	  }

	  #region Validaciones

	  public bool ValidacionUnique(FacturaDetalleViewModel model )
	  {
	  	var query = getQueriableBaseActive().Where(w => w.FacturaId == model.FacturaId && w.InventarioId == model.InventarioId && w.Estregistro == 1 );
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

