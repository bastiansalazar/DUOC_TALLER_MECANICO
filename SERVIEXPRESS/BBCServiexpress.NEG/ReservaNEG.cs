using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class ReservaNEG
    {
        public ReservaVIEW CargarReserva(int idReserva)
        {
            try
            {
                ReservaDAL vista = new ReservaDAL();
                return vista.CargarReservaView(idReserva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReservaVIEW> ListarTodosRequerimientos()
        {
            try
            {
                ReservaDAL datos = new ReservaDAL();
                return datos.ListarTodosRequerimientos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReservaVIEW> FiltrarRequerimientos(string tipo, string valor)
        {
            try
            {
                ReservaDAL datos = new ReservaDAL();
                return datos.FiltrarOrdenesPedidos(tipo, valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReservaVIEW> FiltrarRequerimientos2(string tipo, string valor)
        {
            try
            {
                ReservaDAL datos = new ReservaDAL();
                return datos.FiltrarOrdenesPedidos2(tipo, valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReservaVIEW> FiltrarRequerimientos3(string tipo, int sucursal, string valor)
        {
            try
            {
                ReservaDAL datos = new ReservaDAL();
                return datos.FiltrarOrdenesPedidos3(tipo,sucursal, valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
