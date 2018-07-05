using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class ProveedoresDAL
    {
        public List<ProveedoresVIEW> ListarTodosProveedores()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _proveedores = (from a in con.PROVEEDOR
                                  join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                  join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                  join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                  join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                  join f in con.ESTADO_PROVEEDOR on a.ESTADO_PROVEEDOR_ID equals f.ID
                                  join h in con.ESTADO_PROVEEDOR on a.TIPO_PROVEEDOR_ID equals h.ID
                                    orderby a.NOMBRE_EMPRESA ascending
                                    select new ProveedoresVIEW
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
                                      ESTADO_PROVEEDOR = f.NOMBRE,
                                      TIPO_PROVEEDOR = h.NOMBRE,
                                      NOMBRE_EMPRESA = a.NOMBRE_EMPRESA
                                  }).ToList();
                return _proveedores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProveedoresVIEW CargarProveedor(int idProveedor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _proveedor = (from a in con.PROVEEDOR
                                 join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                 join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                 join d in con.PROVINCIA on c.PROVINCIA_ID equals d.ID
                                 join e in con.REGION on d.REGION_ID equals e.ID
                                 where a.ID == idProveedor
                                 select new ProveedoresVIEW
                                 {
                                     ID = a.ID,
                                     NOMBRE = b.NOMBRE,
                                     NOMBRE_EMPRESA = a.NOMBRE_EMPRESA,
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
                                     IdEstadoProveedor = a.ESTADO_PROVEEDOR_ID,
                                     IdTipoProveedor = a.TIPO_PROVEEDOR_ID,
                                     IdPais = e.PAIS_ID,
                                     IdRegion = d.REGION_ID,
                                     IdProvincia = c.PROVINCIA_ID
                                 }).FirstOrDefault();
                return _proveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ProveedoresVIEW> FiltrarProveedores(string tipo, string valor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "NOMBRE")
                {
                    string filtro = valor.ToUpper();
                    var _proveedor = (from a in con.PROVEEDOR
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_PROVEEDOR on a.ESTADO_PROVEEDOR_ID equals f.ID
                                     join h in con.TIPO_PROVEEDOR on a.TIPO_PROVEEDOR_ID equals h.ID
                                     where b.NOMBRE.Contains(filtro)
                                      orderby a.NOMBRE_EMPRESA ascending
                                      select new ProveedoresVIEW
                                     {
                                         ID = a.ID,
                                         NOMBRE = b.NOMBRE,
                                         NOMBRE_EMPRESA = a.NOMBRE_EMPRESA,
                                         APELLIDO = b.APELLIDO,
                                         NUM_ID = b.NUM_ID,
                                         DIV_ID = b.DIV_ID,
                                         DIRECCION = b.DIRECCION,
                                         COMUNA = c.NOMBRE,
                                         TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                         TELEFONO_FIJO = b.TELEFONO_FIJO,
                                         ESTADO_PERSONA = d.NOMBRE,
                                         TIPO_PERSONA = e.NOMBRE,
                                         ESTADO_PROVEEDOR = f.NOMBRE,
                                         TIPO_PROVEEDOR = h.NOMBRE
                                     }).ToList();
                    return _proveedor;
                }
                else if (tipo == "APELLIDO")
                {
                    string filtro = valor.ToUpper();
                    var _proveedor = (from a in con.PROVEEDOR
                                     join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_PROVEEDOR on a.ESTADO_PROVEEDOR_ID equals f.ID
                                     join h in con.TIPO_PROVEEDOR on a.TIPO_PROVEEDOR_ID equals h.ID
                                     where b.APELLIDO.Contains(filtro)
                                      orderby a.NOMBRE_EMPRESA ascending
                                      select new ProveedoresVIEW
                                     {
                                         ID = a.ID,
                                         NOMBRE = b.NOMBRE,
                                         NOMBRE_EMPRESA = a.NOMBRE_EMPRESA,
                                         APELLIDO = b.APELLIDO,
                                         NUM_ID = b.NUM_ID,
                                         DIV_ID = b.DIV_ID,
                                         DIRECCION = b.DIRECCION,
                                         COMUNA = c.NOMBRE,
                                         TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                         TELEFONO_FIJO = b.TELEFONO_FIJO,
                                         ESTADO_PERSONA = d.NOMBRE,
                                         TIPO_PERSONA = e.NOMBRE,
                                         ESTADO_PROVEEDOR = f.NOMBRE,
                                         TIPO_PROVEEDOR = h.NOMBRE
                                     }).ToList();
                    return _proveedor;
                }
                else if (tipo == "RUT")
                {
                    int _valor = 0;
                    int.TryParse(valor, out _valor);
                    var _proveedor = (from a in con.PROVEEDOR
                                      join b in con.PERSONA on a.PERSONA_ID equals b.ID
                                     join c in con.COMUNA on b.COMUNA_ID equals c.ID
                                     join d in con.ESTADO_PERSONA on b.ESTADO_PERSONA_ID equals d.ID
                                     join e in con.TIPO_PERSONA on b.TIPO_PERSONA_ID equals e.ID
                                     join f in con.ESTADO_PROVEEDOR on a.ESTADO_PROVEEDOR_ID equals f.ID
                                     join h in con.TIPO_PROVEEDOR on a.ESTADO_PROVEEDOR_ID equals h.ID
                                     where b.NUM_ID == _valor
                                      orderby a.NOMBRE_EMPRESA ascending
                                      select new ProveedoresVIEW
                                     {
                                         ID = a.ID,
                                         NOMBRE = b.NOMBRE,
                                         NOMBRE_EMPRESA = a.NOMBRE_EMPRESA,
                                         APELLIDO = b.APELLIDO,
                                         NUM_ID = b.NUM_ID,
                                         DIV_ID = b.DIV_ID,
                                         DIRECCION = b.DIRECCION,
                                         COMUNA = c.NOMBRE,
                                         TELEFONO_CELULAR = b.TELEFONO_CELULAR,
                                         TELEFONO_FIJO = b.TELEFONO_FIJO,
                                         ESTADO_PERSONA = d.NOMBRE,
                                         TIPO_PERSONA = e.NOMBRE,
                                         ESTADO_PROVEEDOR = f.NOMBRE,
                                         TIPO_PROVEEDOR = h.NOMBRE
                                     }).ToList();
                    return _proveedor;
                }
                else
                {
                    return new List<ProveedoresVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string CrearProveedor(PROVEEDOR proveedor, PERSONA persona)
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
                    var _ultimo = (from a in con.PROVEEDOR
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    proveedor.ID = _ultimo + 1;
                    proveedor.PERSONA_ID = _id.ID;
                    con.PROVEEDOR.Add(proveedor);
                    con.SaveChanges();

                    return "creado";
                }
                else
                {
                    proveedor.PERSONA_ID = _exPersona.ID;
                    var _exProveedor = (from a in con.PROVEEDOR
                                       where a.PERSONA_ID == proveedor.PERSONA_ID
                                       select a).FirstOrDefault();
                    var _ultimo = (from a in con.PROVEEDOR
                                   orderby a.ID descending
                                   select a.ID).FirstOrDefault();
                    proveedor.ID = _ultimo + 1;
                    if (_exProveedor ==null)
                    {
                        con.PROVEEDOR.Add(proveedor);
                        con.SaveChanges();
                        return "creado";
                    }
                    else
                    {
                        return "El proveedor ya existe en los registros";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarProveedor(PROVEEDOR proveedor, PERSONA persona)
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

                    return "El proveedor no existe en los registros";
                }
                else
                {
                    proveedor.PERSONA_ID = _exPersona.ID;
                    var _exProveedor = (from a in con.PROVEEDOR
                                       where a.PERSONA_ID == proveedor.PERSONA_ID
                                       select a).FirstOrDefault();
                    if (_exProveedor == null)
                    {
                        return "El proveedor no existe en los registros";

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

                        //actualizar proveedor
                        _exProveedor.ESTADO_PROVEEDOR_ID = proveedor.ESTADO_PROVEEDOR_ID;
                        _exProveedor.TIPO_PROVEEDOR_ID = proveedor.TIPO_PROVEEDOR_ID;
                        _exProveedor.FECHA_ULTIMO_UPDATE = proveedor.FECHA_ULTIMO_UPDATE;
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
