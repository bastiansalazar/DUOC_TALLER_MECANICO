using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class SucursalNEG
    {
        public List<SUCURSAL> ListarSucuralesActivas()
        {
            try
            {
                SucursalDAL sucursalDAL = new SucursalDAL();
                return sucursalDAL.ListarSusursalesActivas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SUCURSAL CargarSucursal(int idSucursal)
        {
            try
            {
                SucursalDAL sucursalDAL = new SucursalDAL();
                return sucursalDAL.CargarSucursal(idSucursal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SUCURSAL> ListarTodasSucursales()
        {
            try
            {
                SucursalDAL sucursalDAL = new SucursalDAL();
                return sucursalDAL.ListarTodasSucursales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SUCURSAL> FiltrarSucursal(string valor)
        {
            try
            {
                SucursalDAL sucursalDAL = new SucursalDAL();
                return sucursalDAL.FiltrarSucursal(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearSucursal(string nombre, string direccion, int numero, int estado , int empresa)
        {
            try
            {
                SUCURSAL sucursal = new SUCURSAL();
                SucursalDAL sucursalDAL = new SucursalDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    if (direccion != "" & nombre.Trim().Length > 1)
                    {
                        if (numero.ToString().Length > 4)
                        {
                            if (estado > 0)
                            {
                                if (empresa > 0)
                                {
                                    sucursal.NOMBRE = nombre.ToUpper();
                                    sucursal.FECHA_CREACION = DateTime.Now;
                                    sucursal.ESTADO_SUCURSAL_ID = estado;
                                    sucursal.MULTI_EMPRESA_ID = empresa;
                                    sucursal.NUMERO_TELEFONO = numero;
                                    sucursal.DIRECCION = direccion;
                                    sucursal.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                    return sucursalDAL.CrearSucursal(sucursal);
                                }
                                else { return "Indique una empresa"; }
                            }
                            else { return "Indique un estado"; }
                        }
                        else { return "Debe ingresar un número de telefono"; }
                    }
                    else { return "La direccion debe tener al menos 2 caracteres"; }
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarSucursal(string nombre, string direccion, int numero, int estado, int empresa, int id)
        {
            try
            {
                SUCURSAL sucursal = new SUCURSAL();
                SucursalDAL sucursalDAL = new SucursalDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    if (direccion != "" & nombre.Trim().Length > 1)
                    {
                        if (numero.ToString().Length > 4)
                        {
                            if (estado > 0)
                            {
                                if (empresa > 0)
                                {
                                    if (id > 0)
                                    {
                                        sucursal.NOMBRE = nombre.ToUpper();
                                        sucursal.ESTADO_SUCURSAL_ID = estado;
                                        sucursal.MULTI_EMPRESA_ID = empresa;
                                        sucursal.NUMERO_TELEFONO = numero;
                                        sucursal.DIRECCION = direccion;
                                        sucursal.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                        sucursal.ID = id;
                                        return sucursalDAL.ActualizarSucursal(sucursal);
                                    }
                                    else { return "Seleccione una sucursal"; }
                                }
                                else { return "Indique una empresa"; }
                            }
                            else { return "Indique un estado"; }
                        }
                        else { return "Debe ingresar un número de telefono"; }
                    }
                    else { return "La direccion debe tener al menos 2 caracteres"; }
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
