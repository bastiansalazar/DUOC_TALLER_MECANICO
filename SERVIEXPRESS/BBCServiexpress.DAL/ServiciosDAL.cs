using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class ServiciosDAL
    {
        public List<ServiciosVIEW> ListarTodosServicios()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _servicios = (from a in con.SERVICIO
                                 join b in con.ESTADO_SERVICIO on a.ESTADO_SERVICIO_ID equals b.ID
                                 join c in con.TIPO_SERVICIO on a.TIPO_SERVICIO_ID equals c.ID
                                 join d in con.SUCURSAL on a.SUCURSAL_ID equals d.ID
                                  orderby a.ID ascending
                                  select new ServiciosVIEW
                                 {
                                     ID = a.ID,
                                     TIPO_SERVICIO = c.NOMBRE,
                                     ESTADO_SERVICIO = b.NOMBRE,
                                     SUCURSAL = d.NOMBRE,
                                     COSTO = a.COSTO,
                                     CALCULO_X_PROD = "SI",
                                     VALOR_CALCULO = 0.0
                                 }).ToList();
                return _servicios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ServiciosVIEW> ListarTodosServiciosActivos()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _servicios = (from a in con.SERVICIO
                                  join b in con.ESTADO_SERVICIO on a.ESTADO_SERVICIO_ID equals b.ID
                                  join c in con.TIPO_SERVICIO on a.TIPO_SERVICIO_ID equals c.ID
                                  join d in con.SUCURSAL on a.SUCURSAL_ID equals d.ID
                                  where a.ESTADO_SERVICIO_ID == 1
                                  orderby a.ID ascending
                                  select new ServiciosVIEW
                                  {
                                      ID = a.ID,
                                      TIPO_SERVICIO = c.NOMBRE,
                                      ESTADO_SERVICIO = b.NOMBRE,
                                      SUCURSAL = d.NOMBRE,
                                      COSTO = a.COSTO,
                                      CALCULO_X_PROD = "SI",
                                      VALOR_CALCULO = 0.0
                                  }).ToList();
                return _servicios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ServiciosVIEW CargarServicio(int idServicio)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _servicio = (from a in con.SERVICIO
                                 join b in con.ESTADO_SERVICIO on a.ESTADO_SERVICIO_ID equals b.ID
                                 join c in con.TIPO_SERVICIO on a.TIPO_SERVICIO_ID equals c.ID
                                 join d in con.SUCURSAL on a.SUCURSAL_ID equals d.ID
                                 where a.ID==idServicio
                                 select new ServiciosVIEW
                                {
                                    ID = a.ID,
                                    Estado_Servicio_Id = a.ESTADO_SERVICIO_ID,
                                    Sucursal_Id = a.SUCURSAL_ID,
                                    Tipo_Servicio_Id = a.TIPO_SERVICIO_ID,
                                    COSTO = a.COSTO,
                                    CALCULO_X_PROD = "SI",
                                    VALOR_CALCULO = 0.0,
                                    TIPO_SERVICIO = c.NOMBRE
                                }).FirstOrDefault();
                return _servicio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ServiciosVIEW> FiltrarServicios(string tipo, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "TIPO")
                {
                    string filtro = valor.ToUpper();
                    var _servicios = (from a in con.SERVICIO
                                      join b in con.ESTADO_SERVICIO on a.ESTADO_SERVICIO_ID equals b.ID
                                      join c in con.TIPO_SERVICIO on a.TIPO_SERVICIO_ID equals c.ID
                                      join d in con.SUCURSAL on a.SUCURSAL_ID equals d.ID
                                      where c.NOMBRE.Contains(filtro)
                                      orderby a.ID ascending
                                      select new ServiciosVIEW
                                      {
                                          ID = a.ID,
                                          TIPO_SERVICIO = c.NOMBRE,
                                          ESTADO_SERVICIO = b.NOMBRE,
                                          SUCURSAL = d.NOMBRE,
                                          COSTO = a.COSTO,
                                          CALCULO_X_PROD = "SI",
                                          VALOR_CALCULO = 0.0
                                      }).ToList();
                    return _servicios;
                }
                else if (tipo == "SUCURSAL")
                {
                    string filtro = valor.ToUpper();
                    var _servicios = (from a in con.SERVICIO
                                      join b in con.ESTADO_SERVICIO on a.ESTADO_SERVICIO_ID equals b.ID
                                      join c in con.TIPO_SERVICIO on a.TIPO_SERVICIO_ID equals c.ID
                                      join d in con.SUCURSAL on a.SUCURSAL_ID equals d.ID
                                      where d.NOMBRE.Contains(filtro)
                                      orderby a.ID ascending
                                      select new ServiciosVIEW
                                      {
                                          ID = a.ID,
                                          TIPO_SERVICIO = c.NOMBRE,
                                          ESTADO_SERVICIO = b.NOMBRE,
                                          SUCURSAL = d.NOMBRE,
                                          COSTO = a.COSTO,
                                          CALCULO_X_PROD = "SI",
                                          VALOR_CALCULO = 0.0
                                      }).ToList();
                    return _servicios;
                }
                else if (tipo == "ID SUCURSAL")
                {
                    int filtro = int.Parse(valor);
                    var _servicios = (from a in con.SERVICIO
                                      join b in con.ESTADO_SERVICIO on a.ESTADO_SERVICIO_ID equals b.ID
                                      join c in con.TIPO_SERVICIO on a.TIPO_SERVICIO_ID equals c.ID
                                      join d in con.SUCURSAL on a.SUCURSAL_ID equals d.ID
                                      where a.SUCURSAL_ID == filtro
                                      orderby a.ID ascending
                                      select new ServiciosVIEW
                                      {
                                          ID = a.ID,
                                          TIPO_SERVICIO = c.NOMBRE,
                                          ESTADO_SERVICIO = b.NOMBRE,
                                          SUCURSAL = d.NOMBRE,
                                          COSTO = a.COSTO,
                                          CALCULO_X_PROD = "SI",
                                          VALOR_CALCULO = 0.0
                                      }).ToList();
                    return _servicios;
                }
                else
                {
                    return new List<ServiciosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ServiciosVIEW> FiltrarServicios(string valor1, int tipoServicio, string id_sucursal)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (valor1 == "ID TIPO")
                {
                    string filtro = valor1.ToUpper();
                    int sucursal = int.Parse(id_sucursal);
                    var _servicios = (from a in con.SERVICIO
                                      join b in con.ESTADO_SERVICIO on a.ESTADO_SERVICIO_ID equals b.ID
                                      join c in con.TIPO_SERVICIO on a.TIPO_SERVICIO_ID equals c.ID
                                      join d in con.SUCURSAL on a.SUCURSAL_ID equals d.ID
                                      where a.TIPO_SERVICIO_ID == tipoServicio &&
                                      a.SUCURSAL_ID == sucursal
                                      orderby a.ID ascending
                                      select new ServiciosVIEW
                                      {
                                          ID = a.ID,
                                          TIPO_SERVICIO = c.NOMBRE,
                                          ESTADO_SERVICIO = b.NOMBRE,
                                          SUCURSAL = d.NOMBRE,
                                          COSTO = a.COSTO,
                                          CALCULO_X_PROD = "SI",
                                          VALOR_CALCULO = 0.0
                                      }).ToList();
                    return _servicios;
                }                
                else
                {
                    return new List<ServiciosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearServicio(SERVICIO servicio)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exServicio = (from a in con.SERVICIO
                                  where a.SUCURSAL_ID == servicio.SUCURSAL_ID &&
                                  a.TIPO_SERVICIO_ID == servicio.TIPO_SERVICIO_ID
                                  select a).FirstOrDefault();

                if (_exServicio == null)
                {
                    con.SERVICIO.Add(servicio);
                    con.SaveChanges();
                    return "creado";
                }
                else
                {
                    return "El servicio ya existe en los registros";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ActualizarServicio(SERVICIO servicio)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exServicio = (from a in con.SERVICIO
                                   where a.ID == servicio.ID 
                                   select a).FirstOrDefault();

                if (_exServicio != null)
                {
                    //actualizar servicio
                    _exServicio.SUCURSAL_ID = servicio.SUCURSAL_ID;
                    _exServicio.TIPO_SERVICIO_ID = servicio.TIPO_SERVICIO_ID;
                    _exServicio.ESTADO_SERVICIO_ID = servicio.ESTADO_SERVICIO_ID;
                    _exServicio.FECHA_ULTIMO_UPDATE = servicio.FECHA_ULTIMO_UPDATE;
                    _exServicio.COSTO = servicio.COSTO;
                    con.SaveChanges();
                    return "actualizado";
                }
                else
                {
                    return "El servicio no existe en los registros para la sucursal seleccionada";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }//
}
