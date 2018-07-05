using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBCServiexpress.DAL;

namespace BBCServiexpress.NEG
{
    public class MultiEmpresaNEG
    {
        public List<MULTI_EMPRESA> ListarEmpresas()
        {
            try
            {
                MultiEmpresaDAL multiEmpresaDAL = new MultiEmpresaDAL();
                return multiEmpresaDAL.ListarEmpresas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MULTI_EMPRESA CargarEmpresa(int id)
        {
            try
            {
                MultiEmpresaDAL multiEmpresaDAL = new MultiEmpresaDAL();
                return multiEmpresaDAL.CargarMultiEmpresa(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MULTI_EMPRESA> FiltrarEmpresa(string valor)
        {
            try
            {
                MultiEmpresaDAL multiEmpresaDAL = new MultiEmpresaDAL();
                return multiEmpresaDAL.FiltrarMultiempresa(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearEmpresa(string razon,string direccion,int telefono,string rut,int estado)
        {
            try
            {
                MULTI_EMPRESA multiempresa = new MULTI_EMPRESA();
                MultiEmpresaDAL multiEmpresaDAL = new MultiEmpresaDAL();

                if (razon != "" & razon.Trim().Length > 1)
                {
                    if (direccion != "" & direccion.Trim().Length > 1)
                    {
                        if (telefono.ToString().Length > 5)
                        {
                            multiempresa.RAZON_SOCIAL = razon.ToUpper();
                            multiempresa.DIRECCION = direccion.ToUpper();
                            multiempresa.NUMERO_TELEFONO = telefono;
                            multiempresa.RUT = rut.ToUpper();
                            multiempresa.ESTADO_EMPRESA_ID = estado;
                            multiempresa.FECHA_CREACION = DateTime.Now;
                            multiempresa.FECHA_ULTIMO_UPDATE = DateTime.Now;
                            return multiEmpresaDAL.CrearMultiempresa(multiempresa);
                        }
                        else { return "Debe ingresar un número de teléfono"; }
                    }
                    else { return "La dirección debe tener al menos 2 caracteres"; }
                }
                else { return "La razón social debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarEmpresa(int id, string direccion, int telefono, int estado)
        {
            try
            {
                MULTI_EMPRESA multiempresa = new MULTI_EMPRESA();
                MultiEmpresaDAL multiEmpresaDAL = new MultiEmpresaDAL();

                if (direccion != "" & direccion.Trim().Length > 1)
                {
                    if (telefono.ToString().Length > 5)
                    {
                        if (id>0)
                        {
                            multiempresa.DIRECCION = direccion.ToUpper();
                            multiempresa.NUMERO_TELEFONO = telefono;
                            multiempresa.ESTADO_EMPRESA_ID = estado;
                            multiempresa.FECHA_ULTIMO_UPDATE = DateTime.Now;
                            multiempresa.ID = id;
                            return multiEmpresaDAL.ActualizarMultiempresa(multiempresa);
                        }
                        else { return "Debe seleccionar una empresa"; }
                    }
                    else { return "Debe ingresar un número de teléfono"; }
                }
                else { return "La dirección debe tener al menos 2 caracteres"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
