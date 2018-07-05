using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class Tipos_EstadosDAL
    {
        public List<TIPO_PERSONA> ListarTPersonas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_PERSONA
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_PRODUCTO> ListarTProductos()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_PRODUCTO
                              orderby a.NOMBRE ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_EMPLEADO> ListarTEmpleados()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_EMPLEADO
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_PROVEEDOR> ListarTProveedores()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_PROVEEDOR
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_USUARIO> ListarTUsuarios()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_USUARIO
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_SUCURSAL> ListarESucursal()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_SUCURSAL
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_EMPRESA> ListarEEmpresa()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_EMPRESA
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_SERVICIO> ListarTServicios()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_SERVICIO
                              orderby a.NOMBRE ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TIPO_VEHICULO> ListarTVehiculos()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_VEHICULO
                              orderby a.NOMBRE ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TIPO_VENTA> ListarTVentas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.TIPO_VENTA
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_PERSONA> ListarEPersonas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_PERSONA
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_CONTROL_RECEPCION> ListarEControlRecepcion()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_CONTROL_RECEPCION
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_ORDEN_PEDIDO> ListarEOrdenesPedidos()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_ORDEN_PEDIDO
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_CLIENTE> ListarECliente()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_CLIENTE
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_PRODUCTO> ListarEProducto()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_PRODUCTO
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_EMPLEADO> ListarEEmpleado()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_EMPLEADO
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_PROVEEDOR> ListarEProveedor()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_PROVEEDOR
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_USUARIO> ListarEUsuario()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_USUARIO
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ESTADO_SERVICIO> ListarEServicio()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.ESTADO_SERVICIO
                              orderby a.ID ascending
                              select a).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }//
}
