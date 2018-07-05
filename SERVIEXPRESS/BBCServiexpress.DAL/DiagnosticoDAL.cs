using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class DiagnosticoDAL
    {
        public List<ServiciosXDiagnosticoVIEW> ListarServiciosXDiagnostico(int idDiagnostico)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.SERVICIOS_X_DIAGNOSTICO
                              join b in con.SERVICIO on a.ID_SERVICIO equals b.ID
                              join c in con.TIPO_SERVICIO on b.TIPO_SERVICIO_ID equals c.ID
                              join d in con.ESTADO_SERVICIO_X_DIAGNOSTICO on a.ID_ESTADO equals d.ID_ESTADO_SXD
                              where a.ID_DIAGNOSTICO == idDiagnostico
                              select new ServiciosXDiagnosticoVIEW
                              {
                                  IdServicio = a.ID_SERVICIO,
                                  NombreServicio = c.NOMBRE,
                                  CostoServicio = b.COSTO,
                                  EstadoServicio = d.NOMBRE
                              }).ToList();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductosXDiagnostico> ListarProductosXDiagnostico(int idDiagnostico)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.PRODUCTOS_X_DIAGNOSTICO
                              join b in con.PRODUCTO on a.ID_PRODUCTO equals b.ID
                              where a.ID_DIAGNOSTICO == idDiagnostico
                              select new ProductosXDiagnostico
                              {
                                  IdProducto = a.ID_PRODUCTO,
                                  NombreProdcuto = b.NOMBRE,
                                  Cantidad = a.CANTIDAD_PROD,
                                  PrecioUni = b.PRECIO_VENTA,
                                  PrecioTot = b.PRECIO_VENTA * a.CANTIDAD_PROD
                              }).ToList();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ServiciosXDiagnosticoVIEW CargarServicioXDiagnostico(int diagnostico, int servicio)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _lista = (from a in con.SERVICIOS_X_DIAGNOSTICO
                              join b in con.SERVICIO on a.ID_SERVICIO equals b.ID
                              join c in con.TIPO_SERVICIO on b.TIPO_SERVICIO_ID equals c.ID
                              join d in con.ESTADO_SERVICIO_X_DIAGNOSTICO on a.ID_ESTADO equals d.ID_ESTADO_SXD
                              where a.ID_DIAGNOSTICO == diagnostico &&
                              a.ID_SERVICIO == servicio
                              select new ServiciosXDiagnosticoVIEW
                              {
                                  IdServicio = a.ID_SERVICIO,
                                  NombreServicio = c.NOMBRE,
                                  CostoServicio = b.COSTO,
                                  EstadoServicio = d.NOMBRE
                              }).FirstOrDefault();
                return _lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
