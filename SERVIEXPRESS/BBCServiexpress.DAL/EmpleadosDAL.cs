using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class EmpleadosDAL
    {
        public List<EmpleadosVIEW> ListarTodosEmpleados()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _empleados = (from a in con.EMPLEADO
                                 join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                 join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                 join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                 join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                 join f in con.ESTADO_EMPLEADO on a.ESTADO_EMPLEADO_ID equals f.ID
                                 join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                 join h in con.TIPO_EMPLEADO on a.TIPO_EMPLEADO_ID equals h.ID
                                  orderby b.NUM_ID ascending
                                  select new EmpleadosVIEW
                                 {
                                     ID = a.ID,
                                     NOMBRE = b.NOMBRE,
                                     APELLIDO = b.APELLIDO,
                                     NUM_ID = b.NUM_ID,
                                     DIV_ID = b.DIV_ID,
                                     DIRECCION = b.DIRECCION,
                                     COMUNA = c.NOMBRE,
                                     TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                     TELEFONO_FIJO = b.TELEFONO_FIJO,
                                     ESTADO_PERSONA = d.NOMBRE,
                                     TIPO_PERSONA = e.NOMBRE,
                                     ESTADO_EMPLEADO = f.NOMBRE,
                                     TIPO_EMPLEADO = h.NOMBRE,
                                     NOMBRE_SUCURSAL = g.NOMBRE,
                                     NombreEmpleadoComboBox = b.NOMBRE+" "+b.APELLIDO
                                 }).ToList();
                return _empleados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmpleadosVIEW> ListarTodosEmpleadosSucursal(int sucursal)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _empleados = (from a in con.EMPLEADO
                                  join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                  join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                  join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                  join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                  join f in con.ESTADO_EMPLEADO on a.ESTADO_EMPLEADO_ID equals f.ID
                                  join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                  join h in con.TIPO_EMPLEADO on a.TIPO_EMPLEADO_ID equals h.ID
                                  where a.SUCURSAL_ID == sucursal
                                  orderby b.NUM_ID ascending
                                  select new EmpleadosVIEW
                                  {
                                      ID = a.ID,
                                      NOMBRE = b.NOMBRE,
                                      APELLIDO = b.APELLIDO,
                                      NUM_ID = b.NUM_ID,
                                      DIV_ID = b.DIV_ID,
                                      DIRECCION = b.DIRECCION,
                                      COMUNA = c.NOMBRE,
                                      TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                      TELEFONO_FIJO = b.TELEFONO_FIJO,
                                      ESTADO_PERSONA = d.NOMBRE,
                                      TIPO_PERSONA = e.NOMBRE,
                                      ESTADO_EMPLEADO = f.NOMBRE,
                                      TIPO_EMPLEADO = h.NOMBRE,
                                      NOMBRE_SUCURSAL = g.NOMBRE,
                                      NombreEmpleadoComboBox = b.NOMBRE + " " + b.APELLIDO
                                  }).ToList();
                return _empleados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EmpleadosVIEW CargarEmpleado(int idEmpleado)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _empleado = (from a in con.EMPLEADO
                                 join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                 join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                 join d in con.PROVINCIA on c.PROVINCIA_ID equals d.ID
                                 join e in con.REGION on d.REGION_ID equals e.ID
                                 where a.ID == idEmpleado
                                 select new EmpleadosVIEW
                                 {
                                     ID = a.ID,
                                     NOMBRE = b.NOMBRE,
                                     APELLIDO = b.APELLIDO,
                                     NUM_ID = b.NUM_ID,
                                     DIV_ID = b.DIV_ID,
                                     DIRECCION = b.DIRECCION,
                                     IdComuna = b.COMUNA_ID,
                                     TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                     TELEFONO_FIJO = b.TELEFONO_FIJO,
                                     FECHA_NACIMIENTO = b.FECHA_NACIMIENTO,
                                     CORREO = b.CORREO,
                                     IdEstadoPersona = b.ESTADO_PERSONA_ID,
                                     IdTipoPersona = b.TIPO_PERSONA_ID,
                                     IdEstadoEmpleado = a.ESTADO_EMPLEADO_ID,
                                     IdTipoEmpleado = a.TIPO_EMPLEADO_ID,
                                     IdSucursal = a.SUCURSAL_ID,
                                     IdPais = e.PAIS_ID,
                                     IdRegion = d.REGION_ID,
                                     IdProvincia = c.PROVINCIA_ID
                                 }).FirstOrDefault();
                return _empleado;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EmpleadosVIEW> FiltrarEmpleados(string tipo, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "NOMBRE")
                {
                    string filtro = valor.ToUpper();
                    var _empleado = (from a in con.EMPLEADO
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_EMPLEADO on a.ESTADO_EMPLEADO_ID equals f.ID
                                     join h in con.TIPO_EMPLEADO on a.TIPO_EMPLEADO_ID equals h.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID                                     
                                     where b.NOMBRE.Contains(filtro)
                                     orderby b.NUM_ID ascending
                                     select new EmpleadosVIEW
                                     {
                                         ID = a.ID,
                                         NOMBRE = b.NOMBRE,
                                         APELLIDO = b.APELLIDO,
                                         NUM_ID = b.NUM_ID,
                                         DIV_ID = b.DIV_ID,
                                         DIRECCION = b.DIRECCION,
                                         COMUNA = c.NOMBRE,
                                         TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                         TELEFONO_FIJO = b.TELEFONO_FIJO,
                                         ESTADO_PERSONA = d.NOMBRE,
                                         TIPO_PERSONA = e.NOMBRE,
                                         ESTADO_EMPLEADO = f.NOMBRE,
                                         TIPO_EMPLEADO = h.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _empleado;
                }
                else if (tipo == "APELLIDO")
                {
                    string filtro = valor.ToUpper();
                    var _empleado = (from a in con.EMPLEADO
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_EMPLEADO on a.ESTADO_EMPLEADO_ID equals f.ID
                                     join h in con.TIPO_EMPLEADO on a.TIPO_EMPLEADO_ID equals h.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                     where b.APELLIDO.Contains(filtro)
                                     select new EmpleadosVIEW
                                     {
                                         ID = a.ID,
                                         NOMBRE = b.NOMBRE,
                                         APELLIDO = b.APELLIDO,
                                         NUM_ID = b.NUM_ID,
                                         DIV_ID = b.DIV_ID,
                                         DIRECCION = b.DIRECCION,
                                         COMUNA = c.NOMBRE,
                                         TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                         TELEFONO_FIJO = b.TELEFONO_FIJO,
                                         ESTADO_PERSONA = d.NOMBRE,
                                         TIPO_PERSONA = e.NOMBRE,
                                         ESTADO_EMPLEADO = f.NOMBRE,
                                         TIPO_EMPLEADO = h.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _empleado;
                }
                else if (tipo == "RUT")
                {
                    int _valor = 0;
                    int.TryParse(valor, out _valor);
                    var _empleado = (from a in con.EMPLEADO
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_EMPLEADO on a.ESTADO_EMPLEADO_ID equals f.ID
                                     join h in con.TIPO_EMPLEADO on a.TIPO_EMPLEADO_ID equals h.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                     where b.NUM_ID == _valor
                                     select new EmpleadosVIEW
                                     {
                                         ID = a.ID,
                                         NOMBRE = b.NOMBRE,
                                         APELLIDO = b.APELLIDO,
                                         NUM_ID = b.NUM_ID,
                                         DIV_ID = b.DIV_ID,
                                         DIRECCION = b.DIRECCION,
                                         COMUNA = c.NOMBRE,
                                         TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                         TELEFONO_FIJO = b.TELEFONO_FIJO,
                                         ESTADO_PERSONA = d.NOMBRE,
                                         TIPO_PERSONA = e.NOMBRE,
                                         ESTADO_EMPLEADO = f.NOMBRE,
                                         TIPO_EMPLEADO = h.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _empleado;
                }
                else
                {
                    return new List<EmpleadosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string CrearEmpleado(EMPLEADO empleado, PERSONA persona)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exPersona = (from a in con.PERSONA
                                  where a.NUM_ID == persona.NUM_ID &&
                                  a.DIV_ID == persona.DIV_ID
                                  select a).FirstOrDefault();

                if (_exPersona ==null)
                {
                    con.PERSONA.Add(persona);
                    con.SaveChanges();
                    var _id = (from a in con.PERSONA
                               where a.NUM_ID == persona.NUM_ID &&
                               a.DIV_ID == persona.DIV_ID
                               select a).FirstOrDefault();
                    var _ultimo = (from a in con.EMPLEADO
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    empleado.ID = _ultimo + 1;
                    empleado.PERSONA_ID = _id.ID;
                    con.EMPLEADO.Add(empleado);
                    con.SaveChanges();

                    return "creado";
                }
                else
                {
                    empleado.PERSONA_ID = _exPersona.ID;
                    var _exEmpleado = (from a in con.EMPLEADO
                                      where a.PERSONA_ID == empleado.PERSONA_ID
                                      select a).FirstOrDefault();
                    if (_exEmpleado==null)
                    {
                        var _ultimo = (from a in con.EMPLEADO
                                       orderby a.ID descending
                                       select a.ID).FirstOrDefault();
                        empleado.ID = _ultimo + 1;
                        con.EMPLEADO.Add(empleado);
                        con.SaveChanges();
                        return "creado";
                    }
                    else
                    {
                        return "El empleado ya existe en los registros";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarEmpleado(EMPLEADO empleado, PERSONA persona)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exPersona = (from a in con.PERSONA
                                  where a.NUM_ID == persona.NUM_ID &&
                                  a.DIV_ID == persona.DIV_ID
                                  select a).FirstOrDefault();

                if (_exPersona == null)
                {

                    return "El empleado no existe en los registros";
                }
                else
                {
                    empleado.PERSONA_ID = _exPersona.ID;
                    var _exEmpleado = (from a in con.EMPLEADO
                                      where a.PERSONA_ID == empleado.PERSONA_ID
                                      select a).FirstOrDefault();
                    if (_exEmpleado == null)
                    {
                        return "El empleado no existe en los registros";

                    }
                    else
                    {
                        //actualizar persona
                        _exPersona.NOMBRE = persona.NOMBRE;
                        _exPersona.APELLIDO = persona.APELLIDO;
                        _exPersona.DIRECCION = persona.DIRECCION;
                        _exPersona.TELEFONO_FIJO = persona.TELEFONO_FIJO;
                        _exPersona.TELEFONO_CELULAR = persona.TELEFONO_CELULAR;
                        _exPersona.COMUNA_ID = persona.COMUNA_ID;
                        _exPersona.TIPO_PERSONA_ID = persona.TIPO_PERSONA_ID;
                        _exPersona.ESTADO_PERSONA_ID = persona.ESTADO_PERSONA_ID;
                        _exPersona.FECHA_NACIMIENTO = persona.FECHA_NACIMIENTO;
                        _exPersona.FECHA_ULTIMO_UPDATE = persona.FECHA_ULTIMO_UPDATE;
                        _exPersona.CORREO = persona.CORREO;

                        //actualizar empleado
                        _exEmpleado.SUCURSAL_ID = empleado.SUCURSAL_ID;
                        _exEmpleado.ESTADO_EMPLEADO_ID = empleado.ESTADO_EMPLEADO_ID;
                        _exEmpleado.TIPO_EMPLEADO_ID = empleado.TIPO_EMPLEADO_ID;
                        _exEmpleado.FECHA_ULTIMO_UPDATE = empleado.FECHA_ULTIMO_UPDATE;
                        con.SaveChanges();
                        return "actualizado";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
