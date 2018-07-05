using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class ComunaNEG
    {
        public List<COMUNA> ListarComunas(int provincia)
        {
            try
            {
                ComunaDAL comunaDAL = new ComunaDAL();
                return comunaDAL.ListarComunas(provincia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<COMUNA> ListarTodasComunas()
        {
            try
            {
                ComunaDAL comunaDAL = new ComunaDAL();
                return comunaDAL.ListarTodasComunas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public COMUNA CargarComuna(int id)
        {
            try
            {
                ComunaDAL comunaDAL = new ComunaDAL();
                return comunaDAL.CargarComuna(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<COMUNA> FiltrarComuna(string valor)
        {
            try
            {
                ComunaDAL comunaDAL = new ComunaDAL();
                return comunaDAL.FiltrarComuna(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearComuna(string nombre, int provincia)
        {
            try
            {
                COMUNA comuna = new COMUNA();
                ComunaDAL comunaDAL = new ComunaDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    comuna.NOMBRE = nombre.ToUpper();
                    comuna.FECHA_CREACION = DateTime.Now;
                    comuna.PROVINCIA_ID = provincia;
                    comuna.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return comunaDAL.CrearComuna(comuna);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarComuna(string nombre, int id, int provincia)
        {
            try
            {
                COMUNA comuna = new COMUNA();
                ComunaDAL comunaDAL = new ComunaDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        if (provincia > 0)
                        {
                            comuna.ID = id;
                            comuna.FECHA_ULTIMO_UPDATE = DateTime.Now;
                            comuna.PROVINCIA_ID = provincia;
                            comuna.NOMBRE = nombre;
                            return comunaDAL.ActualizarComuna(comuna);
                        }
                        else { return "Seleccione una region"; }
                    }
                    else { return "Seleccione un registro de la tabla"; }
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
