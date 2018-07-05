using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class UsuarioDAL
    {
        public string ValidarUsuario(string usuario, string clave)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _usuario = (from a in con.USUARIO
                                where a.NOMBRE == usuario
                                select a).FirstOrDefault();
                if (_usuario != null)
                {
                    if (_usuario.CONTRACENA.ToString() == clave)
                    {
                        int tipo = int.Parse(_usuario.TIPO_USUARIO_ID.ToString());
                        if (tipo == 1 | tipo == 2)
                        {
                            return "valido";
                        }
                        else
                        {
                            return "Su usuario no cuenta con el perfil de acceso correspondiente para este sistema";
                        }
                       
                    }
                    else
                    {
                        return "Contraseña inválida!";
                    }                    
                }
                else
                {
                    return "Usuario no existe!";
                }

            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public int TipoUsuario(string usuario)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _usuario = (from a in con.USUARIO
                                where a.NOMBRE == usuario
                                select a).FirstOrDefault();
                if (_usuario != null)
                {
                    return _usuario.TIPO_USUARIO_ID;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ObtenerTipoEmpleado(string usuario)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _usuario = (from a in con.USUARIO
                                join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                join c in con.EMPLEADO on b.ID equals c.PERSONA_ID
                                join d in con.TIPO_EMPLEADO on c.TIPO_EMPLEADO_ID equals d.ID
                                where a.NOMBRE == usuario
                                select d).FirstOrDefault();
                if (_usuario != null)
                {
                    return _usuario.ID;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuariosVIEW> ListarTodosUsuarios()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _usuarios = (from a in con.USUARIO
                                  join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                  join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                  join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                  join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                  join f in con.ESTADO_USUARIO on a.ESTADO_USUARIO_ID equals f.ID
                                  join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                  join h in con.TIPO_USUARIO on a.TIPO_USUARIO_ID equals h.ID
                                 orderby a.NOMBRE ascending
                                 select new UsuariosVIEW
                                  {
                                      ID = a.ID,
                                      USUARIO = a.NOMBRE,
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
                                      ESTADO_USUARIO = f.NOMBRE,
                                      TIPO_USUARIO = h.NOMBRE,
                                      NOMBRE_SUCURSAL = g.NOMBRE
                                  }).ToList();
                return _usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuariosVIEW CargarUsuario(int idUsuario)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _usuario = (from a in con.USUARIO
                                 join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                 join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                 join d in con.PROVINCIA on c.PROVINCIA_ID equals d.ID
                                 join e in con.REGION on d.REGION_ID equals e.ID
                                 where a.ID == idUsuario
                                 select new UsuariosVIEW
                                 {
                                     ID = a.ID,
                                     USUARIO = a.NOMBRE,
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
                                     IdEstadoUsuario = a.ESTADO_USUARIO_ID,
                                     IdTipoUsuario = a.TIPO_USUARIO_ID,
                                     IdSucursal = a.SUCURSAL_ID,
                                     IdPais = e.PAIS_ID,
                                     IdRegion = d.REGION_ID,
                                     IdProvincia = c.PROVINCIA_ID
                                 }).FirstOrDefault();
                return _usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<UsuariosVIEW> FiltrarUsuarios(string tipo, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "NOMBRE")
                {
                    string filtro = valor.ToUpper();
                    var _usuarios = (from a in con.USUARIO
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_USUARIO on a.ESTADO_USUARIO_ID equals f.ID
                                     join h in con.TIPO_USUARIO on a.TIPO_USUARIO_ID equals h.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                     where b.NOMBRE.Contains(filtro)
                                     orderby a.NOMBRE ascending
                                     select new UsuariosVIEW
                                     {
                                         ID = a.ID,
                                         USUARIO = a.NOMBRE,                                         
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
                                         ESTADO_USUARIO = f.NOMBRE,
                                         TIPO_USUARIO = h.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _usuarios;
                }
                else if (tipo == "APELLIDO")
                {
                    string filtro = valor.ToUpper();
                    var _usuarios = (from a in con.USUARIO
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_USUARIO on a.ESTADO_USUARIO_ID equals f.ID
                                     join h in con.TIPO_USUARIO on a.TIPO_USUARIO_ID equals h.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                     where b.APELLIDO.Contains(filtro)
                                     orderby a.NOMBRE ascending
                                     select new UsuariosVIEW
                                     {
                                         ID = a.ID,
                                         USUARIO = a.NOMBRE,
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
                                         ESTADO_USUARIO = f.NOMBRE,
                                         TIPO_USUARIO = h.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _usuarios;
                }
                else if (tipo == "RUT")
                {
                    int _valor = 0;
                    int.TryParse(valor, out _valor);
                    var _usuarios = (from a in con.USUARIO
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_USUARIO on a.ESTADO_USUARIO_ID equals f.ID
                                     join h in con.TIPO_USUARIO on a.TIPO_USUARIO_ID equals h.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                     where b.NUM_ID == _valor
                                     orderby a.NOMBRE ascending
                                     select new UsuariosVIEW
                                     {
                                         ID = a.ID,
                                         USUARIO = a.NOMBRE,
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
                                         ESTADO_USUARIO = f.NOMBRE,
                                         TIPO_USUARIO = h.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _usuarios;
                }
                else
                {
                    return new List<UsuariosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string CrearUsuario(USUARIO usuario, PERSONA persona)
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
                    con.PERSONA.Add(persona);
                    con.SaveChanges();
                    var _id = (from a in con.PERSONA
                               where a.NUM_ID == persona.NUM_ID &&
                               a.DIV_ID == persona.DIV_ID
                               select a).FirstOrDefault();
                    var _ultimo = (from a in con.USUARIO
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    usuario.ID = _ultimo + 1;
                    usuario.PERSONA_ID = _id.ID;
                    con.USUARIO.Add(usuario);
                    con.SaveChanges();

                    return "creado";
                }
                else
                {
                    usuario.PERSONA_ID = _exPersona.ID;
                    var _exUsuario2 = (from a in con.USUARIO
                                      where a.NOMBRE == usuario.NOMBRE
                                      select a).FirstOrDefault();
                    if (_exUsuario2 == null)
                    {
                        var _ultimo = (from a in con.USUARIO
                                       orderby a.ID descending
                                       select a.ID).FirstOrDefault();
                        usuario.ID = _ultimo + 1;
                        con.USUARIO.Add(usuario);
                        con.SaveChanges();
                        return "creado";
                    }
                    else
                    {
                        return "Ya existe un usuario con ese nombre de usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarUsuario(USUARIO usuario, PERSONA persona)
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

                    return "El usuario no existe en los registros";
                }
                else
                {
                    usuario.PERSONA_ID = _exPersona.ID;
                    var _exUsuario = (from a in con.USUARIO
                                       where a.PERSONA_ID == usuario.PERSONA_ID
                                       select a).FirstOrDefault();
                    if (_exUsuario == null)
                    {
                        return "El usuario no existe en los registros";

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
                        _exUsuario.SUCURSAL_ID = usuario.SUCURSAL_ID;
                        _exUsuario.ESTADO_USUARIO_ID = usuario.ESTADO_USUARIO_ID;
                        _exUsuario.TIPO_USUARIO_ID = usuario.TIPO_USUARIO_ID;
                        _exUsuario.FECHA_ULTIMO_UPDATE = usuario.FECHA_ULTIMO_UPDATE;
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
