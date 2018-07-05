using BBCServiexpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class PaisNEG
    {
        public List<PAIS> ListarPaises()
        {
            try
            {
                PaisDAL paisDAL = new PaisDAL();
                return paisDAL.ListarPaises();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PAIS CargarPais(int id)
        {
            try
            {
                PaisDAL paisDAL = new PaisDAL();
                return paisDAL.CargarPais(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PAIS> FiltrarPais(string valor)
        {
            try
            {
                PaisDAL paisDAL = new PaisDAL();
                return paisDAL.FiltrarPais(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearPais(string nombre)
        {
            try
            {
                PAIS pais = new PAIS();
                PaisDAL paisDAL = new PaisDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    pais.NOMBRE = nombre.ToUpper();
                    pais.FECHA_CREACION = DateTime.Now;
                    pais.FECHA_ULTIMO_UPDATE = DateTime.Now;
                    return paisDAL.CrearPais(pais);
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarPais(string nombre, int id)
        {
            try
            {
                PAIS pais = new PAIS();
                PaisDAL paisDAL = new PaisDAL();

                if (nombre.Trim().Length > 1)
                {
                    if (id > 0)
                    {
                        pais.ID = id;
                        pais.FECHA_ULTIMO_UPDATE = DateTime.Now;
                        pais.NOMBRE = nombre;
                        return paisDAL.ActualizarPais(pais);
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
