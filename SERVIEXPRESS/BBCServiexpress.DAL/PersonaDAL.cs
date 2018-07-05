using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class PersonaDAL
    {        
        public List<PersonaVIEW> ListarTodosPersonas()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _persona = (from a in con.PERSONA
                                 join c in con.COMUNA on a.COMUNA_ID equals c.ID
                                 join d in con.ESTADO_PERSONA on a.ESTADO_PERSONA_ID equals d.ID
                                 join e in con.TIPO_PERSONA on a.TIPO_PERSONA_ID equals e.ID
                                 orderby a.NUM_ID ascending
                                 select new PersonaVIEW
                                 {
                                     ID = a.ID,
                                     NOMBRE = a.NOMBRE,
                                     APELLIDO = a.APELLIDO,
                                     NUM_ID = a.NUM_ID,
                                     DIV_ID = a.DIV_ID,
                                     DIRECCION = a.DIRECCION,
                                     COMUNA = c.NOMBRE,
                                     TELEFONO_CELULAR = a.TELEFONO_CELULAR,
                                     TELEFONO_FIJO = a.TELEFONO_FIJO,
                                     ESTADO_PERSONA = d.NOMBRE,
                                     TIPO_PERSONA = e.NOMBRE,
                                     FECHA_NACIMIENTO = a.FECHA_NACIMIENTO
                                 }).ToList();
                return _persona;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PersonaVIEW CargarPersona(int idPersona)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _persona = (from a in con.PERSONA
                                join c in con.COMUNA on a.COMUNA_ID equals c.ID
                                join d in con.PROVINCIA on c.PROVINCIA_ID equals d.ID
                                join e in con.REGION on d.REGION_ID equals e.ID
                                where a.ID == idPersona
                                select new PersonaVIEW
                                {
                                    ID = a.ID,
                                    NOMBRE = a.NOMBRE,
                                    APELLIDO = a.APELLIDO,
                                    NUM_ID = a.NUM_ID,
                                    DIV_ID = a.DIV_ID,
                                    DIRECCION = a.DIRECCION,
                                    IdComuna = a.COMUNA_ID,
                                    TELEFONO_CELULAR = a.TELEFONO_CELULAR,
                                    TELEFONO_FIJO = a.TELEFONO_FIJO,
                                    FECHA_NACIMIENTO = a.FECHA_NACIMIENTO,
                                    CORREO = a.CORREO,
                                    IdEstadoPersona = a.ESTADO_PERSONA_ID,
                                    IdTipoPersona = a.TIPO_PERSONA_ID,
                                    IdPais = e.PAIS_ID,
                                    IdRegion = d.REGION_ID,
                                    IdProvincia = c.PROVINCIA_ID
                                }).FirstOrDefault();
                return _persona;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<PersonaVIEW> FiltrarPersona(string tipo, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "NOMBRE")
                {
                    string filtro = valor.ToUpper();
                    var _persona = (from a in con.PERSONA
                                    join c in con.COMUNA on a.COMUNA_ID equals c.ID
                                    join d in con.PROVINCIA on c.PROVINCIA_ID equals d.ID
                                    join e in con.REGION on d.REGION_ID equals e.ID
                                    where a.NOMBRE.Contains(filtro)
                                    orderby a.NUM_ID ascending
                                    select new PersonaVIEW
                                    {
                                        ID = a.ID,
                                        NOMBRE = a.NOMBRE,
                                        APELLIDO = a.APELLIDO,
                                        NUM_ID = a.NUM_ID,
                                        DIV_ID = a.DIV_ID,
                                        DIRECCION = a.DIRECCION,
                                        COMUNA = c.NOMBRE,
                                        TELEFONO_CELULAR = a.TELEFONO_CELULAR,
                                        TELEFONO_FIJO = a.TELEFONO_FIJO,
                                        ESTADO_PERSONA = d.NOMBRE,
                                        TIPO_PERSONA = e.NOMBRE,
                                        FECHA_NACIMIENTO = a.FECHA_NACIMIENTO
                                    }).ToList();
                    return _persona;
                }
                else if (tipo == "APELLIDO")
                {
                    string filtro = valor.ToUpper();
                    var _persona = (from a in con.PERSONA
                                    join c in con.COMUNA on a.COMUNA_ID equals c.ID
                                    join d in con.PROVINCIA on c.PROVINCIA_ID equals d.ID
                                    join e in con.REGION on d.REGION_ID equals e.ID
                                    where a.APELLIDO.Contains(filtro)
                                    orderby a.NUM_ID ascending
                                    select new PersonaVIEW
                                    {
                                        ID = a.ID,
                                        NOMBRE = a.NOMBRE,
                                        APELLIDO = a.APELLIDO,
                                        NUM_ID = a.NUM_ID,
                                        DIV_ID = a.DIV_ID,
                                        DIRECCION = a.DIRECCION,
                                        IdComuna = a.COMUNA_ID,
                                        TELEFONO_CELULAR = a.TELEFONO_CELULAR,
                                        TELEFONO_FIJO = a.TELEFONO_FIJO,
                                        FECHA_NACIMIENTO = a.FECHA_NACIMIENTO,
                                        CORREO = a.CORREO,
                                        IdEstadoPersona = a.ESTADO_PERSONA_ID,
                                        IdTipoPersona = a.TIPO_PERSONA_ID,
                                        IdPais = e.PAIS_ID,
                                        IdRegion = d.REGION_ID,
                                        IdProvincia = c.PROVINCIA_ID
                                    }).ToList();
                    return _persona;
                }
                else if (tipo == "RUT")
                {
                    int _valor = 0;
                    int.TryParse(valor, out _valor);
                    var _persona = (from a in con.PERSONA
                                    join c in con.COMUNA on a.COMUNA_ID equals c.ID
                                    join d in con.PROVINCIA on c.PROVINCIA_ID equals d.ID
                                    join e in con.REGION on d.REGION_ID equals e.ID
                                    where a.NUM_ID == _valor
                                    orderby a.NUM_ID ascending
                                    select new PersonaVIEW
                                    {
                                        ID = a.ID,
                                        NOMBRE = a.NOMBRE,
                                        APELLIDO = a.APELLIDO,
                                        NUM_ID = a.NUM_ID,
                                        DIV_ID = a.DIV_ID,
                                        DIRECCION = a.DIRECCION,
                                        IdComuna = a.COMUNA_ID,
                                        TELEFONO_CELULAR = a.TELEFONO_CELULAR,
                                        TELEFONO_FIJO = a.TELEFONO_FIJO,
                                        FECHA_NACIMIENTO = a.FECHA_NACIMIENTO,
                                        CORREO = a.CORREO,
                                        IdEstadoPersona = a.ESTADO_PERSONA_ID,
                                        IdTipoPersona = a.TIPO_PERSONA_ID,
                                        IdPais = e.PAIS_ID,
                                        IdRegion = d.REGION_ID,
                                        IdProvincia = c.PROVINCIA_ID
                                    }).ToList();
                    return _persona;
                }
                else
                {
                    return new List<PersonaVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string CrearPersona(PERSONA persona)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exPersona = (from a in con.PERSONA
                                  where a.NUM_ID == persona.NUM_ID &&
                                  a.DIV_ID == persona.DIV_ID
                                  select a).FirstOrDefault();

                if (_exPersona !=null)
                {
                    con.PERSONA.Add(persona);
                    con.SaveChanges();
                    return "creado";
                }
                else
                {
                    return "La persona ya existe en los registros";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarPersona(PERSONA persona)
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

                    return "La persona no existe en los registros";
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
                    con.SaveChanges();
                    return "actualizado";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
