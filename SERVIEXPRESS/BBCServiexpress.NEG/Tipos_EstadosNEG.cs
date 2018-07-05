using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class Tipos_EstadosNEG
    {
        public List<TIPO_PERSONA> ListarTPersonas()
        {
            try
            {
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarTPersonas();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarTProductos();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarTEmpleados();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarTProveedores();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarTUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_EMPRESA> ListasEEmpresa()
        {
            try
            {
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEEmpresa();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarESucursal();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarTServicios();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarTVehiculos();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEPersonas();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarTVentas();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEOrdenesPedidos();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEControlRecepcion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_PRODUCTO> ListarEProductos()
        {
            try
            {
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEProducto();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarECliente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESTADO_EMPLEADO> ListarEEmpleados()
        {
            try
            {
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEEmpleado();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEProveedor();
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
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEUsuario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ESTADO_SERVICIO> ListarEServicios()
        {
            try
            {
                Tipos_EstadosDAL tipoDAL = new Tipos_EstadosDAL();
                return tipoDAL.ListarEServicio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }//
}
