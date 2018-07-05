using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class VehiculosDAL
    {
        public List<VehiculosVIEW> ListarTodosVehiculos()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _vehiculos = (from a in con.VEHICULO
                                  join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                                  join c in con.MARCA_VEHICULO on a.MARCA_VEHICULO_ID equals c.ID
                                  join d in con.TIPO_VEHICULO on a.TIPO_VEHICULO_ID equals d.ID
                                  join e in con.PERSONA on b.PERSONA_ID equals e.ID
                                  orderby a.PATENTE ascending
                                  select new VehiculosVIEW
                                  {
                                      ID = a.ID,
                                      PATENTE = a.PATENTE,
                                      NOMBRE_CLIENTE = e.NOMBRE + " "+e.APELLIDO,
                                      RUT_CLIENTE = e.NUM_ID,
                                      DIV_CLIENTE = e.DIV_ID,
                                      MARCA = c.NOMBRE,
                                      TIPO = d.NOMBRE
                                  }).ToList();
                return _vehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public VehiculosVIEW CargarVehiculo(int idVehiculo)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _servicio = (from a in con.VEHICULO
                                 where a.ID == idVehiculo
                                 select new VehiculosVIEW
                                 {
                                     ID = a.ID,
                                     PATENTE = a.PATENTE,
                                     CLIENTE_ID = a.CLIENTE_ID,
                                     MARCA_VEHICULO_ID = a.MARCA_VEHICULO_ID,
                                     TIPO_VEHICULO_ID = a.TIPO_VEHICULO_ID
                                 }).FirstOrDefault();
                return _servicio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VehiculosVIEW> FiltrarVehiculos(string tipo, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "TIPO")
                {
                    string filtro = valor.ToUpper();
                    var vehiculos = (from a in con.VEHICULO
                                     join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                                     join c in con.MARCA_VEHICULO on a.MARCA_VEHICULO_ID equals c.ID
                                     join d in con.TIPO_VEHICULO on a.TIPO_VEHICULO_ID equals d.ID
                                     join e in con.PERSONA on b.PERSONA_ID equals e.ID
                                     where d.NOMBRE.Contains(valor)
                                     orderby a.PATENTE ascending
                                     select new VehiculosVIEW
                                     {
                                         ID = a.ID,
                                         PATENTE = a.PATENTE,
                                         NOMBRE_CLIENTE = e.NOMBRE + " " + e.APELLIDO,
                                         RUT_CLIENTE = e.NUM_ID,
                                         DIV_CLIENTE = e.DIV_ID,
                                         MARCA = c.NOMBRE,
                                         TIPO = d.NOMBRE
                                     }).ToList();
                    return vehiculos;
                }
                else if (tipo == "MARCA")
                {
                    string filtro = valor.ToUpper();
                    var vehiculos = (from a in con.VEHICULO
                                     join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                                     join c in con.MARCA_VEHICULO on a.MARCA_VEHICULO_ID equals c.ID
                                     join d in con.TIPO_VEHICULO on a.TIPO_VEHICULO_ID equals d.ID
                                     join e in con.PERSONA on b.PERSONA_ID equals e.ID
                                     where c.NOMBRE.Contains(valor)
                                     orderby a.PATENTE ascending
                                     select new VehiculosVIEW
                                     {
                                         ID = a.ID,
                                         PATENTE = a.PATENTE,
                                         NOMBRE_CLIENTE = e.NOMBRE + " " + e.APELLIDO,
                                         RUT_CLIENTE = e.NUM_ID,
                                         DIV_CLIENTE = e.DIV_ID,
                                         MARCA = c.NOMBRE,
                                         TIPO = d.NOMBRE
                                     }).ToList();
                    return vehiculos;
                }
                else if (tipo == "PATENTE")
                {
                    string filtro = valor.ToUpper();
                    var vehiculos = (from a in con.VEHICULO
                                     join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                                     join c in con.MARCA_VEHICULO on a.MARCA_VEHICULO_ID equals c.ID
                                     join d in con.TIPO_VEHICULO on a.TIPO_VEHICULO_ID equals d.ID
                                     join e in con.PERSONA on b.PERSONA_ID equals e.ID
                                     where a.PATENTE.Contains(valor)
                                     orderby a.PATENTE ascending
                                     select new VehiculosVIEW
                                     {
                                         ID = a.ID,
                                         PATENTE = a.PATENTE,
                                         NOMBRE_CLIENTE = e.NOMBRE + " " + e.APELLIDO,
                                         RUT_CLIENTE = e.NUM_ID,
                                         DIV_CLIENTE = e.DIV_ID,
                                         MARCA = c.NOMBRE,
                                         TIPO = d.NOMBRE
                                     }).ToList();
                    return vehiculos;
                }
                else if (tipo == "ID CLIENTE")
                {
                    int filtro = int.Parse(valor);
                    var vehiculos = (from a in con.VEHICULO
                                     join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                                     join c in con.MARCA_VEHICULO on a.MARCA_VEHICULO_ID equals c.ID
                                     join d in con.TIPO_VEHICULO on a.TIPO_VEHICULO_ID equals d.ID
                                     join e in con.PERSONA on b.PERSONA_ID equals e.ID
                                     where a.CLIENTE_ID==filtro
                                     orderby a.PATENTE ascending
                                     select new VehiculosVIEW
                                     {
                                         ID = a.ID,
                                         PATENTE = a.PATENTE,
                                         NOMBRE_CLIENTE = e.NOMBRE + " " + e.APELLIDO,
                                         RUT_CLIENTE = e.NUM_ID,
                                         DIV_CLIENTE = e.DIV_ID,
                                         MARCA = c.NOMBRE,
                                         TIPO = d.NOMBRE
                                     }).ToList();
                    return vehiculos;
                }
                else
                {
                    return new List<VehiculosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CrearVehiculo(VEHICULO vehiculo)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exVehiculo = (from a in con.VEHICULO
                                   where a.PATENTE == vehiculo.PATENTE 
                                   select a).FirstOrDefault();

                if (_exVehiculo == null)
                {
                    con.VEHICULO.Add(vehiculo);
                    con.SaveChanges();
                    return "creado";
                }
                else
                {
                    return "La patente indicada, ya figura en el sistema para otro vehiculo";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ActualizarVehiculo(VEHICULO vehiculo)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exVehiculo = (from a in con.VEHICULO
                                   where a.ID == vehiculo.ID
                                   select a).FirstOrDefault();

                if (_exVehiculo != null)
                {
                    //actualizar servicio
                    _exVehiculo.CLIENTE_ID = vehiculo.CLIENTE_ID;
                    _exVehiculo.MARCA_VEHICULO_ID = vehiculo.MARCA_VEHICULO_ID;
                    _exVehiculo.TIPO_VEHICULO_ID = vehiculo.TIPO_VEHICULO_ID;
                    _exVehiculo.FECHA_ULTIMO_UPDATE = vehiculo.FECHA_ULTIMO_UPDATE;
                    con.SaveChanges();
                    return "actualizado";
                }
                else
                {
                    return "El vehiculo no existe en los registros, o no se ha podido localizar la información";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
