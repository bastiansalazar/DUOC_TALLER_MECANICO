using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBCServiexpress.DAL.Vistas;

namespace BBCServiexpress.DAL
{
    public class ReservaDAL
    {
        public ReservaVIEW CargarReservaView(int idReserva)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.RESERVA_HORA
                              join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                              join e in con.PERSONA on d.PERSONA_ID equals e.ID
                              join x in con.COMUNA on c.COMUNA_ID equals x.ID
                              join f in con.ESTADO_RESERVA on a.ESTADO_RESERVA_ID equals f.ID
                              join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                              join h in con.TIPO_RESERVA on a.TIPO_RESERVA_ID equals h.ID
                              join i in con.VEHICULO on a.VEHICULO_ID equals i.ID
                              join k in con.TIPO_VEHICULO on i.TIPO_VEHICULO_ID equals k.ID
                              join l in con.MARCA_VEHICULO on i.MARCA_VEHICULO_ID equals l.ID
                              join j in con.DIAGNOSTICO on a.ID equals j.RESERVA_HORA_ID
                              join m in con.MULTI_EMPRESA on g.ID equals m.ID
                              where a.ID == idReserva
                                 select new ReservaVIEW
                                 {
                                     ID = a.ID,
                                     FECHA_CREACION = a.FECHA_CREACION,
                                     FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                     CLIENTE_ID = a.CLIENTE_ID,
                                     EMPLEADO_ID = a.EMPLEADO_ID,
                                     ESTADO_RESERVA_ID = a.ESTADO_RESERVA_ID,
                                     SUCURSAL_ID = a.SUCURSAL_ID,
                                     TIPO_RESERVA_ID = a.TIPO_RESERVA_ID,
                                     VEHICULO_ID = a.VEHICULO_ID,
                                     ORSERVACION_FINAL = a.ORSERVACION_FINAL,
                                     FECHA_RESERVA = a.FECHA_RESERVA,
                                     NOMBRE_CLIENTE = c.NOMBRE,
                                     NOMBRE_APELLIDO = c.APELLIDO,
                                     NUM_ID_CLIENTE = c.NUM_ID,
                                     DIV_CLIENTE = c.DIV_ID,
                                     TELEFONO_CLIENTE = c.TELEFONO_CELULAR,
                                     TELEFONO_CLIENTE2 = c.TELEFONO_FIJO,
                                     DIRECCION_CLIENTE = c.DIRECCION,
                                     COMUNA_CLIENTE = x.NOMBRE,
                                     NOMBRE_EMPLEADO = e.NOMBRE,
                                     APELLIDO_EMPLEADO = e.APELLIDO,
                                     ESTADO_RESERVA = f.NOMBRE,
                                     ESTADO_DIAGNOSTICO = j.ESTADO_DIAGNOSTICO,
                                     NOMBRE_SUCURSAL = g.NOMBRE,
                                     DIRECCION_SUCURSAL = g.DIRECCION,
                                     TELEFONO_SUCURSAL = g.NUMERO_TELEFONO,
                                     TIPO_RESERVA = h.NOMBRE,
                                     TIPO_VEHICULO = k.NOMBRE,
                                     MARCA_VEHICULO = l.NOMBRE,
                                     PATENTE_VEHICULO = i.PATENTE,
                                     TOTAL = j.VALOR_FINAL,
                                     ID_DIAGNOTICO = j.ID,
                                     CORREO_CLIENTE = c.CORREO
                                 }).FirstOrDefault();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReservaVIEW> FiltrarOrdenesPedidos3(string tipo, int sucursal, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                int cliente = int.Parse(valor);
                var _query = (from a in con.RESERVA_HORA
                              join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                              join e in con.PERSONA on d.PERSONA_ID equals e.ID
                              join x in con.COMUNA on e.COMUNA_ID equals x.ID
                              join f in con.ESTADO_RESERVA on a.ESTADO_RESERVA_ID equals f.ID
                              join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                              join h in con.TIPO_RESERVA on a.TIPO_RESERVA_ID equals h.ID
                              join i in con.VEHICULO on a.VEHICULO_ID equals i.ID
                              join k in con.TIPO_VEHICULO on i.TIPO_VEHICULO_ID equals k.ID
                              join l in con.MARCA_VEHICULO on i.MARCA_VEHICULO_ID equals l.ID
                              join j in con.DIAGNOSTICO on a.ID equals j.RESERVA_HORA_ID
                              where a.CLIENTE_ID == cliente &&
                              j.ESTADO_DIAGNOSTICO == "COMPLETADO" &&
                              a.SUCURSAL_ID == sucursal
                              orderby a.ID ascending
                              select new ReservaVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  CLIENTE_ID = a.CLIENTE_ID,
                                  EMPLEADO_ID = a.EMPLEADO_ID,
                                  ESTADO_RESERVA_ID = a.ESTADO_RESERVA_ID,
                                  SUCURSAL_ID = a.SUCURSAL_ID,
                                  TIPO_RESERVA_ID = a.TIPO_RESERVA_ID,
                                  VEHICULO_ID = a.VEHICULO_ID,
                                  ORSERVACION_FINAL = a.ORSERVACION_FINAL,
                                  FECHA_RESERVA = a.FECHA_RESERVA,
                                  NOMBRE_CLIENTE = c.NOMBRE,
                                  NOMBRE_APELLIDO = c.APELLIDO,
                                  NUM_ID_CLIENTE = c.NUM_ID,
                                  DIV_CLIENTE = c.DIV_ID,
                                  TELEFONO_CLIENTE = c.TELEFONO_CELULAR,
                                  TELEFONO_CLIENTE2 = c.TELEFONO_FIJO,
                                  DIRECCION_CLIENTE = c.DIRECCION,
                                  COMUNA_CLIENTE = x.NOMBRE,
                                  NOMBRE_EMPLEADO = e.NOMBRE,
                                  APELLIDO_EMPLEADO = e.APELLIDO,
                                  ESTADO_RESERVA = f.NOMBRE,
                                  ESTADO_DIAGNOSTICO = j.ESTADO_DIAGNOSTICO,
                                  NOMBRE_SUCURSAL = g.NOMBRE,
                                  DIRECCION_SUCURSAL = g.DIRECCION,
                                  TELEFONO_SUCURSAL = g.NUMERO_TELEFONO,
                                  TIPO_RESERVA = h.NOMBRE,
                                  TIPO_VEHICULO = k.NOMBRE,
                                  MARCA_VEHICULO = l.NOMBRE,
                                  PATENTE_VEHICULO = i.PATENTE,
                                  TOTAL = j.VALOR_FINAL,
                                  ID_DIAGNOTICO = j.ID
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReservaVIEW> FiltrarOrdenesPedidos2(string tipo, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                int cliente = int.Parse(valor);
                var _query = (from a in con.RESERVA_HORA
                              join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                              join e in con.PERSONA on d.PERSONA_ID equals e.ID
                              join x in con.COMUNA on e.COMUNA_ID equals x.ID
                              join f in con.ESTADO_RESERVA on a.ESTADO_RESERVA_ID equals f.ID
                              join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                              join h in con.TIPO_RESERVA on a.TIPO_RESERVA_ID equals h.ID
                              join i in con.VEHICULO on a.VEHICULO_ID equals i.ID
                              join k in con.TIPO_VEHICULO on i.TIPO_VEHICULO_ID equals k.ID
                              join l in con.MARCA_VEHICULO on i.MARCA_VEHICULO_ID equals l.ID
                              join j in con.DIAGNOSTICO on a.ID equals j.RESERVA_HORA_ID
                              where a.CLIENTE_ID == cliente &&
                              j.ESTADO_DIAGNOSTICO == "COMPLETADO"
                              orderby a.ID ascending
                              select new ReservaVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  CLIENTE_ID = a.CLIENTE_ID,
                                  EMPLEADO_ID = a.EMPLEADO_ID,
                                  ESTADO_RESERVA_ID = a.ESTADO_RESERVA_ID,
                                  SUCURSAL_ID = a.SUCURSAL_ID,
                                  TIPO_RESERVA_ID = a.TIPO_RESERVA_ID,
                                  VEHICULO_ID = a.VEHICULO_ID,
                                  ORSERVACION_FINAL = a.ORSERVACION_FINAL,
                                  FECHA_RESERVA = a.FECHA_RESERVA,
                                  NOMBRE_CLIENTE = c.NOMBRE,
                                  NOMBRE_APELLIDO = c.APELLIDO,
                                  NUM_ID_CLIENTE = c.NUM_ID,
                                  DIV_CLIENTE = c.DIV_ID,
                                  TELEFONO_CLIENTE = c.TELEFONO_CELULAR,
                                  TELEFONO_CLIENTE2 = c.TELEFONO_FIJO,
                                  DIRECCION_CLIENTE = c.DIRECCION,
                                  COMUNA_CLIENTE = x.NOMBRE,
                                  NOMBRE_EMPLEADO = e.NOMBRE,
                                  APELLIDO_EMPLEADO = e.APELLIDO,
                                  ESTADO_RESERVA = f.NOMBRE,
                                  ESTADO_DIAGNOSTICO = j.ESTADO_DIAGNOSTICO,
                                  NOMBRE_SUCURSAL = g.NOMBRE,
                                  DIRECCION_SUCURSAL = g.DIRECCION,
                                  TELEFONO_SUCURSAL = g.NUMERO_TELEFONO,
                                  TIPO_RESERVA = h.NOMBRE,
                                  TIPO_VEHICULO = k.NOMBRE,
                                  MARCA_VEHICULO = l.NOMBRE,
                                  PATENTE_VEHICULO = i.PATENTE,
                                  TOTAL = j.VALOR_FINAL,
                                  ID_DIAGNOTICO = j.ID
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReservaVIEW> FiltrarOrdenesPedidos(string tipo, string valor)
        {

            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "ESTADO")
                {
                    var _query = (from a in con.RESERVA_HORA
                                  join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                                  join c in con.PERSONA on b.PERSONA_ID equals c.ID
                                  join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                                  join e in con.PERSONA on d.PERSONA_ID equals e.ID
                                  join x in con.COMUNA on e.COMUNA_ID equals x.ID
                                  join f in con.ESTADO_RESERVA on a.ESTADO_RESERVA_ID equals f.ID
                                  join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                  join h in con.TIPO_RESERVA on a.TIPO_RESERVA_ID equals h.ID
                                  join i in con.VEHICULO on a.VEHICULO_ID equals i.ID
                                  join k in con.TIPO_VEHICULO on i.TIPO_VEHICULO_ID equals k.ID
                                  join l in con.MARCA_VEHICULO on i.MARCA_VEHICULO_ID equals l.ID
                                  join j in con.DIAGNOSTICO on a.ID equals j.RESERVA_HORA_ID
                                   where j.ESTADO_DIAGNOSTICO == valor
                                  orderby a.ID ascending
                                  select new ReservaVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      CLIENTE_ID = a.CLIENTE_ID,
                                      EMPLEADO_ID = a.EMPLEADO_ID,
                                      ESTADO_RESERVA_ID = a.ESTADO_RESERVA_ID,
                                      SUCURSAL_ID = a.SUCURSAL_ID,
                                      TIPO_RESERVA_ID = a.TIPO_RESERVA_ID,
                                      VEHICULO_ID = a.VEHICULO_ID,
                                      ORSERVACION_FINAL = a.ORSERVACION_FINAL,
                                      FECHA_RESERVA = a.FECHA_RESERVA,
                                      NOMBRE_CLIENTE = c.NOMBRE,
                                      NOMBRE_APELLIDO = c.APELLIDO,
                                      NUM_ID_CLIENTE = c.NUM_ID,
                                      DIV_CLIENTE = c.DIV_ID,
                                      TELEFONO_CLIENTE = c.TELEFONO_CELULAR,
                                      TELEFONO_CLIENTE2 = c.TELEFONO_FIJO,
                                      DIRECCION_CLIENTE = c.DIRECCION,
                                      COMUNA_CLIENTE = x.NOMBRE,
                                      NOMBRE_EMPLEADO = e.NOMBRE,
                                      APELLIDO_EMPLEADO = e.APELLIDO,
                                      ESTADO_RESERVA = f.NOMBRE,
                                      ESTADO_DIAGNOSTICO = j.ESTADO_DIAGNOSTICO,
                                      NOMBRE_SUCURSAL = g.NOMBRE,
                                      DIRECCION_SUCURSAL = g.DIRECCION,
                                      TELEFONO_SUCURSAL = g.NUMERO_TELEFONO,
                                      TIPO_RESERVA = h.NOMBRE,
                                      TIPO_VEHICULO = k.NOMBRE,
                                      MARCA_VEHICULO = l.NOMBRE,
                                      PATENTE_VEHICULO = i.PATENTE,
                                      TOTAL = j.VALOR_FINAL,
                                      ID_DIAGNOTICO = j.ID
                                  }).ToList();
                    return _query;
                }
                else if (tipo == "SUCURSAL")
                {
                    int sucursal = int.Parse(valor);
                    var _query = (from a in con.RESERVA_HORA
                                  join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                                  join c in con.PERSONA on b.PERSONA_ID equals c.ID
                                  join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                                  join e in con.PERSONA on d.PERSONA_ID equals e.ID
                                  join x in con.COMUNA on e.COMUNA_ID equals x.ID
                                  join f in con.ESTADO_RESERVA on a.ESTADO_RESERVA_ID equals f.ID
                                  join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                  join h in con.TIPO_RESERVA on a.TIPO_RESERVA_ID equals h.ID
                                  join i in con.VEHICULO on a.VEHICULO_ID equals i.ID
                                  join k in con.TIPO_VEHICULO on i.TIPO_VEHICULO_ID equals k.ID
                                  join l in con.MARCA_VEHICULO on i.MARCA_VEHICULO_ID equals l.ID
                                  join j in con.DIAGNOSTICO on a.ID equals j.RESERVA_HORA_ID
                                  where a.SUCURSAL_ID == sucursal
                                  orderby a.ID ascending
                                  select new ReservaVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      CLIENTE_ID = a.CLIENTE_ID,
                                      EMPLEADO_ID = a.EMPLEADO_ID,
                                      ESTADO_RESERVA_ID = a.ESTADO_RESERVA_ID,
                                      SUCURSAL_ID = a.SUCURSAL_ID,
                                      TIPO_RESERVA_ID = a.TIPO_RESERVA_ID,
                                      VEHICULO_ID = a.VEHICULO_ID,
                                      ORSERVACION_FINAL = a.ORSERVACION_FINAL,
                                      FECHA_RESERVA = a.FECHA_RESERVA,
                                      NOMBRE_CLIENTE = c.NOMBRE,
                                      NOMBRE_APELLIDO = c.APELLIDO,
                                      NUM_ID_CLIENTE = c.NUM_ID,
                                      DIV_CLIENTE = c.DIV_ID,
                                      TELEFONO_CLIENTE = c.TELEFONO_CELULAR,
                                      TELEFONO_CLIENTE2 = c.TELEFONO_FIJO,
                                      DIRECCION_CLIENTE = c.DIRECCION,
                                      COMUNA_CLIENTE = x.NOMBRE,
                                      NOMBRE_EMPLEADO = e.NOMBRE,
                                      APELLIDO_EMPLEADO = e.APELLIDO,
                                      ESTADO_RESERVA = f.NOMBRE,
                                      ESTADO_DIAGNOSTICO = j.ESTADO_DIAGNOSTICO,
                                      NOMBRE_SUCURSAL = g.NOMBRE,
                                      DIRECCION_SUCURSAL = g.DIRECCION,
                                      TELEFONO_SUCURSAL = g.NUMERO_TELEFONO,
                                      TIPO_RESERVA = h.NOMBRE,
                                      TIPO_VEHICULO = k.NOMBRE,
                                      MARCA_VEHICULO = l.NOMBRE,
                                      PATENTE_VEHICULO = i.PATENTE,
                                      TOTAL = j.VALOR_FINAL,
                                      ID_DIAGNOTICO = j.ID
                                  }).ToList();
                    return _query;
                }
                else if (tipo == "CLIENTES")
                {
                    int cliente = int.Parse(valor);
                    var _query = (from a in con.RESERVA_HORA
                                  join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                                  join c in con.PERSONA on b.PERSONA_ID equals c.ID
                                  join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                                  join e in con.PERSONA on d.PERSONA_ID equals e.ID
                                  join x in con.COMUNA on e.COMUNA_ID equals x.ID
                                  join f in con.ESTADO_RESERVA on a.ESTADO_RESERVA_ID equals f.ID
                                  join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                  join h in con.TIPO_RESERVA on a.TIPO_RESERVA_ID equals h.ID
                                  join i in con.VEHICULO on a.VEHICULO_ID equals i.ID
                                  join k in con.TIPO_VEHICULO on i.TIPO_VEHICULO_ID equals k.ID
                                  join l in con.MARCA_VEHICULO on i.MARCA_VEHICULO_ID equals l.ID
                                  join j in con.DIAGNOSTICO on a.ID equals j.RESERVA_HORA_ID
                                  where a.CLIENTE_ID == cliente
                                  orderby a.ID ascending
                                  select new ReservaVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      CLIENTE_ID = a.CLIENTE_ID,
                                      EMPLEADO_ID = a.EMPLEADO_ID,
                                      ESTADO_RESERVA_ID = a.ESTADO_RESERVA_ID,
                                      SUCURSAL_ID = a.SUCURSAL_ID,
                                      TIPO_RESERVA_ID = a.TIPO_RESERVA_ID,
                                      VEHICULO_ID = a.VEHICULO_ID,
                                      ORSERVACION_FINAL = a.ORSERVACION_FINAL,
                                      FECHA_RESERVA = a.FECHA_RESERVA,
                                      NOMBRE_CLIENTE = c.NOMBRE,
                                      NOMBRE_APELLIDO = c.APELLIDO,
                                      NUM_ID_CLIENTE = c.NUM_ID,
                                      DIV_CLIENTE = c.DIV_ID,
                                      TELEFONO_CLIENTE = c.TELEFONO_CELULAR,
                                      TELEFONO_CLIENTE2 = c.TELEFONO_FIJO,
                                      DIRECCION_CLIENTE = c.DIRECCION,
                                      COMUNA_CLIENTE = x.NOMBRE,
                                      NOMBRE_EMPLEADO = e.NOMBRE,
                                      APELLIDO_EMPLEADO = e.APELLIDO,
                                      ESTADO_RESERVA = f.NOMBRE,
                                      ESTADO_DIAGNOSTICO = j.ESTADO_DIAGNOSTICO,
                                      NOMBRE_SUCURSAL = g.NOMBRE,
                                      DIRECCION_SUCURSAL = g.DIRECCION,
                                      TELEFONO_SUCURSAL = g.NUMERO_TELEFONO,
                                      TIPO_RESERVA = h.NOMBRE,
                                      TIPO_VEHICULO = k.NOMBRE,
                                      MARCA_VEHICULO = l.NOMBRE,
                                      PATENTE_VEHICULO = i.PATENTE,
                                      TOTAL = j.VALOR_FINAL,
                                      ID_DIAGNOTICO = j.ID
                                  }).ToList();
                    return _query;
                }
                else
                {
                    return new List<ReservaVIEW>();
                }
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                List<ReservaVIEW> lista = new List<ReservaVIEW>();
                var _query = (from a in con.RESERVA_HORA
                              join b in con.CLIENTE on a.CLIENTE_ID equals b.ID
                              join c in con.PERSONA on b.PERSONA_ID equals c.ID
                              join d in con.EMPLEADO on a.EMPLEADO_ID equals d.ID
                              join e in con.PERSONA on d.PERSONA_ID equals e.ID
                              join x in con.COMUNA on e.COMUNA_ID equals x.ID
                              join f in con.ESTADO_RESERVA on a.ESTADO_RESERVA_ID equals f.ID
                              join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                              join h in con.TIPO_RESERVA on a.TIPO_RESERVA_ID equals h.ID
                              join i in con.VEHICULO on a.VEHICULO_ID equals i.ID
                              join k in con.TIPO_VEHICULO on i.TIPO_VEHICULO_ID equals k.ID
                              join l in con.MARCA_VEHICULO on i.MARCA_VEHICULO_ID equals l.ID
                              join j in con.DIAGNOSTICO on a.ID equals j.RESERVA_HORA_ID
                              orderby a.ID ascending
                              select new ReservaVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  CLIENTE_ID = a.CLIENTE_ID,
                                  EMPLEADO_ID = a.EMPLEADO_ID,
                                  ESTADO_RESERVA_ID = a.ESTADO_RESERVA_ID,
                                  SUCURSAL_ID = a.SUCURSAL_ID,
                                  TIPO_RESERVA_ID = a.TIPO_RESERVA_ID,
                                  VEHICULO_ID = a.VEHICULO_ID,
                                  ORSERVACION_FINAL = a.ORSERVACION_FINAL,
                                  FECHA_RESERVA = a.FECHA_RESERVA,
                                  NOMBRE_CLIENTE = c.NOMBRE,
                                  NOMBRE_APELLIDO = c.APELLIDO,
                                  NUM_ID_CLIENTE = c.NUM_ID,
                                  DIV_CLIENTE = c.DIV_ID,
                                  TELEFONO_CLIENTE = c.TELEFONO_CELULAR,
                                  TELEFONO_CLIENTE2 = c.TELEFONO_FIJO,
                                  DIRECCION_CLIENTE = c.DIRECCION,
                                  COMUNA_CLIENTE = x.NOMBRE,
                                  NOMBRE_EMPLEADO = e.NOMBRE,
                                  APELLIDO_EMPLEADO = e.APELLIDO,
                                  ESTADO_RESERVA = f.NOMBRE,
                                  ESTADO_DIAGNOSTICO = j.ESTADO_DIAGNOSTICO,
                                  NOMBRE_SUCURSAL = g.NOMBRE,
                                  DIRECCION_SUCURSAL = g.DIRECCION,
                                  TELEFONO_SUCURSAL = g.NUMERO_TELEFONO,
                                  TIPO_RESERVA = h.NOMBRE,
                                  TIPO_VEHICULO = k.NOMBRE,
                                  MARCA_VEHICULO = l.NOMBRE,
                                  PATENTE_VEHICULO = i.PATENTE,
                                  TOTAL = j.VALOR_FINAL,
                                  ID_DIAGNOTICO = j.ID
                              }).ToList();
                lista = _query;
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
