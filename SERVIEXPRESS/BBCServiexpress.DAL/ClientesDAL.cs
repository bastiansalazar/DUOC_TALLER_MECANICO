using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class ClientesDAL
    {

        public ClienteVIEW CargarCliente(int idCliente)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _clientes = (from a in con.CLIENTE
                                 join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                 join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                 join d in con.PROVINCIA on c.PROVINCIA_ID equals d.ID
                                 join e in con.REGION on d.REGION_ID equals e.ID
                                 where a.ID == idCliente
                                 select new ClienteVIEW
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
                                     IdEstadoCliente = a.ESTADO_CLIENTE_ID,
                                     IdSucursal = a.SUCURSAL_ID,
                                     IdPais = e.PAIS_ID,
                                     IdRegion = d.REGION_ID,
                                     IdProvincia = c.PROVINCIA_ID
                                 }).FirstOrDefault();
                return _clientes;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public string CrearCliente(CLIENTE cliente, PERSONA persona)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exPersona = (from a in con.PERSONA
                                  where a.NUM_ID == persona.NUM_ID &&
                                  a.DIV_ID == persona.DIV_ID
                                  select a).FirstOrDefault();

                if (_exPersona==null)
                {
                    con.PERSONA.Add(persona);
                    con.SaveChanges();
                    var _id = (from a in con.PERSONA
                               where a.NUM_ID == persona.NUM_ID &&
                               a.DIV_ID == persona.DIV_ID
                               select a).FirstOrDefault();
                    var _ultimo = (from a in con.CLIENTE
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    cliente.ID = _ultimo + 1;
                    cliente.PERSONA_ID = _id.ID;
                    con.CLIENTE.Add(cliente);
                    con.SaveChanges();

                    return "creado";
                }
                else
                {
                    cliente.PERSONA_ID = _exPersona.ID;
                    var _exCliente = (from a in con.CLIENTE
                                      where a.PERSONA_ID == cliente.PERSONA_ID
                                      select a).FirstOrDefault();
                    var _ultimo = (from a in con.CLIENTE
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    cliente.ID = _ultimo + 1;
                    if (_exCliente ==null)
                    {
                        con.CLIENTE.Add(cliente);
                        con.SaveChanges();
                        return "creado";
                    }
                    else
                    {
                        return "El cliente ya existe en los registros";
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarCliente(CLIENTE cliente, PERSONA persona)
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
                    
                    return "El cliente no existe en los registros";
                }
                else
                {
                    cliente.PERSONA_ID = _exPersona.ID;
                    var _exCliente = (from a in con.CLIENTE
                                      where a.PERSONA_ID == cliente.PERSONA_ID
                                      select a).FirstOrDefault();
                    if (_exCliente == null)
                    {                        
                        return "El cliente no existe en los registros";
                        
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

                        //actualizar cliente
                        _exCliente.SUCURSAL_ID = cliente.SUCURSAL_ID;
                        _exCliente.ESTADO_CLIENTE_ID = cliente.ESTADO_CLIENTE_ID;
                        _exCliente.FECHA_ULTIMO_UPDATE = cliente.FECHA_ULTIMO_UPDATE;
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
        public List<ClienteVIEW> ListarTodosClientes()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _clientes = (from a in con.CLIENTE 
                                 join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                 join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                 join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                 join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                 join f in con.ESTADO_CLIENTE on a.ESTADO_CLIENTE_ID equals f.ID
                                 join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                 orderby b.NUM_ID ascending
                                 select new ClienteVIEW
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
                                     ESTADO_CLIENTE = f.NOMBRE,
                                     NOMBRE_SUCURSAL = g.NOMBRE,
                                     NombreCombox = b.NOMBRE+" "+b.APELLIDO+" "
                                 }).ToList();
                return _clientes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ClienteVIEW> FiltrarClientes(string tipo, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "NOMBRE")
                {
                    string filtro = valor.ToUpper();
                    var _clientes = (from a in con.CLIENTE
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_CLIENTE on a.ESTADO_CLIENTE_ID equals f.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                     where b.NOMBRE.Contains(filtro)
                                     orderby b.NUM_ID ascending
                                     select new ClienteVIEW
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
                                         ESTADO_CLIENTE = f.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _clientes;                     
                }
                else if (tipo == "APELLIDO")
                {
                    string filtro = valor.ToUpper();
                    var _clientes = (from a in con.CLIENTE
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_CLIENTE on a.ESTADO_CLIENTE_ID equals f.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                     where b.APELLIDO.Contains(filtro)
                                     select new ClienteVIEW
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
                                         ESTADO_CLIENTE = f.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _clientes;
                }
                else if (tipo == "RUT")
                {
                    int _valor = 0;
                    int.TryParse(valor, out _valor);
                    var _clientes = (from a in con.CLIENTE
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_CLIENTE on a.ESTADO_CLIENTE_ID equals f.ID
                                     join g in con.SUCURSAL on a.SUCURSAL_ID equals g.ID
                                     where b.NUM_ID == _valor
                                     select new ClienteVIEW
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
                                         ESTADO_CLIENTE = f.NOMBRE,
                                         NOMBRE_SUCURSAL = g.NOMBRE
                                     }).ToList();
                    return _clientes;
                }
                else {
                    return new List<ClienteVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
