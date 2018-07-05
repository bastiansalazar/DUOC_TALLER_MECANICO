using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;

namespace BBCServiexpress.NEG
{
    public class UsuarioNEG
    {
        public string ValidarUsuario(string usuario,string clave)
        {            
            if (usuario != "")
            {
                if (clave != "")
                {
                    UsuarioDAL usuarioDAL = new UsuarioDAL();
                    return usuarioDAL.ValidarUsuario(usuario,clave);
                }
                else
                {
                    return "Debe indicar un usuario";
                }
            }
            else
            {
                return "Debe indicar un usuario";
            }
        }

        public List<UsuariosVIEW> ListarTodosUsuarios()
        {
            try
            {
                UsuarioDAL usuarioDAL = new UsuarioDAL();
                return usuarioDAL.ListarTodosUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int ObtenerTipoUsuario(string usu)
        {
            if (usu != "")
            {
                UsuarioDAL usuarioDAL = new UsuarioDAL();
                return usuarioDAL.TipoUsuario(usu);
            }
            else
            {
                return 0;
            }
        }

        public int ObtenerTipoEmpleado(string usuario)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.ObtenerTipoEmpleado(usuario);
        }

        public UsuariosVIEW CargarUsuario(int idUsuario)
        {
            try
            {
                UsuarioDAL usuarioDAL = new UsuarioDAL();
                return usuarioDAL.CargarUsuario(idUsuario);
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
                if (tipo != "" & valor != "")
                {
                    UsuarioDAL usuarioDAL = new UsuarioDAL();
                    return usuarioDAL.FiltrarUsuarios(tipo, valor);
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

        public string CrearUsuario(string nombre, string apellido, string rut, DateTime fecha_nac, string direccion, string email, int comuna, string telefono_fijo, string celular, int tipo_persona, int estado_persona, int tipo_usuario, int estado_usuario, int sucursal,string nombre_usuario,string clave1,string clave2)
        {
            try
            {
                PERSONA persona = new PERSONA();
                USUARIO usuario = new USUARIO();
                UsuarioDAL usuarioDAL = new UsuarioDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    if (apellido != "" & apellido.Trim().Length > 1)
                    {
                        if (rut != "" & rut.Trim().Length > 7)
                        {
                            if (RutValido(rut))
                            {
                                if (direccion != "" & direccion.Trim().Length > 4)
                                {
                                    if (email != "" & email.Trim().Length > 4)
                                    {
                                        if (comuna > -1)
                                        {
                                            if (tipo_persona > -1)
                                            {
                                                if (estado_persona > -1)
                                                {
                                                    if (sucursal > -1)
                                                    {
                                                        if (estado_usuario > -1)
                                                        {
                                                            if (tipo_usuario > -1)
                                                            {
                                                                if (fecha_nac != default(DateTime))
                                                                {
                                                                    if (nombre_usuario != "" & nombre_usuario.Trim().Length > 4)
                                                                    {
                                                                        if (clave1 != "" & clave1.Trim().Length > 6)
                                                                        {
                                                                            if (clave1 == clave2)
                                                                            {
                                                                                persona.NOMBRE = nombre.ToUpper();
                                                                                persona.APELLIDO = apellido.ToUpper();
                                                                                usuario.NOMBRE = nombre_usuario;
                                                                                var arreglo = rut.Split('-');
                                                                                string _rut = arreglo[0];
                                                                                string _dv = arreglo[1];
                                                                                persona.NUM_ID = int.Parse(_rut);
                                                                                persona.DIV_ID = _dv.ToUpper();
                                                                                persona.DIRECCION = direccion.ToUpper();
                                                                                int _teleFijo = 0;
                                                                                int.TryParse(telefono_fijo, out _teleFijo);
                                                                                persona.TELEFONO_FIJO = _teleFijo;
                                                                                int _celular = 0;
                                                                                int.TryParse(celular, out _celular);
                                                                                persona.TELEFONO_CELULAR = _celular;
                                                                                persona.COMUNA_ID = comuna;
                                                                                persona.TIPO_PERSONA_ID = tipo_persona;
                                                                                persona.ESTADO_PERSONA_ID = estado_persona;
                                                                                persona.FECHA_NACIMIENTO = fecha_nac;
                                                                                usuario.SUCURSAL_ID = sucursal;
                                                                                usuario.ESTADO_USUARIO_ID = estado_usuario;
                                                                                usuario.TIPO_USUARIO_ID = tipo_usuario;
                                                                                usuario.FECHA_CREACION = DateTime.Now;
                                                                                usuario.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                                usuario.CONTRACENA = clave1;
                                                                                persona.FECHA_CREACION = DateTime.Now;
                                                                                persona.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                                persona.CORREO = email;
                                                                                return usuarioDAL.CrearUsuario(usuario, persona);
                                                                            }
                                                                            else { return "'Nueva Clave' y 'Repetir Clave' deben coincidir"; }
                                                                        }
                                                                        else { return "La clave debe contener al menos 6 caracteres"; }                                                                        
                                                                    }
                                                                    else { return "El usuario debe contener al menos 4 caracteres"; }
                                                                }
                                                                else { return "Debe indicar fecha de nacimiento del usuario"; }
                                                            }
                                                            else { return "Debe tipo de usuario"; }
                                                        }
                                                        else { return "Debe indicar un estado del empleado"; }
                                                    }
                                                    else { return "Debe indicar una sucursal"; }
                                                }
                                                else { return "Debe indicar un estado de persona"; }
                                            }
                                            else { return "Debe indicar un tipo de persona"; }
                                        }
                                        else { return "Debe indicar una comuna"; }
                                    }
                                    else { return "Debe indicar correo electrónico del cliente"; }
                                }
                                else { return "La dirección debe tener al menos 5 caracteres"; }
                            }
                            else { return "El rut ingresado no es válido. Debe ingresar el rut sin puntos (9999999-9)"; }
                        }
                        else { return "Debe ingresar el rut sin puntos (9999999-9)"; }
                    }
                    else { return "El apellido debe tener al menos 2 caracteres"; }
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarUsuario(string nombre, string apellido, string rut, DateTime fecha_nac, string direccion, string email, int comuna, string telefono_fijo, string celular, int tipo_persona, int estado_persona, int tipo_usuario, int estado_usuario, int sucursal, string nombre_usuario,string clave1, string clave2)
        {
            try
            {
                PERSONA persona = new PERSONA();
                USUARIO usuario = new USUARIO();
                UsuarioDAL usuarioDAL = new UsuarioDAL();

                if (nombre != "" & nombre.Trim().Length > 1)
                {
                    if (apellido != "" & apellido.Trim().Length > 1)
                    {
                        if (rut != "" & rut.Trim().Length > 7)
                        {
                            if (RutValido(rut))
                            {
                                if (direccion != "" & direccion.Trim().Length > 4)
                                {
                                    if (email != "" & email.Trim().Length > 4)
                                    {
                                        if (comuna > -1)
                                        {
                                            if (tipo_persona > -1)
                                            {
                                                if (estado_persona > -1)
                                                {
                                                    if (sucursal > -1)
                                                    {
                                                        if (estado_usuario > -1)
                                                        {
                                                            if (tipo_usuario > -1)
                                                            {
                                                                if (fecha_nac != default(DateTime))
                                                                {                              
                                                                    if (nombre_usuario != "" & nombre_usuario.Trim().Length > 4) {

                                                                        if (clave1.Trim().Length > 1)
                                                                        {
                                                                            if (clave1 != "" & clave1.Trim().Length > 6)
                                                                            {
                                                                                if (clave1 == clave2)
                                                                                {
                                                                                    persona.NOMBRE = nombre.ToUpper();
                                                                                    persona.APELLIDO = apellido.ToUpper();
                                                                                    var arreglo = rut.Split('-');
                                                                                    string _rut = arreglo[0];
                                                                                    string _dv = arreglo[1];
                                                                                    persona.NUM_ID = int.Parse(_rut);
                                                                                    persona.DIV_ID = _dv.ToUpper();
                                                                                    persona.DIRECCION = direccion.ToUpper();
                                                                                    int _teleFijo = 0;
                                                                                    int.TryParse(telefono_fijo, out _teleFijo);
                                                                                    persona.TELEFONO_FIJO = _teleFijo;
                                                                                    int _celular = 0;
                                                                                    int.TryParse(celular, out _celular);
                                                                                    persona.TELEFONO_CELULAR = _celular;
                                                                                    persona.COMUNA_ID = comuna;
                                                                                    persona.TIPO_PERSONA_ID = tipo_persona;
                                                                                    persona.ESTADO_PERSONA_ID = estado_persona;
                                                                                    persona.FECHA_NACIMIENTO = fecha_nac;
                                                                                    usuario.SUCURSAL_ID = sucursal;
                                                                                    usuario.ESTADO_USUARIO_ID = estado_usuario;
                                                                                    usuario.TIPO_USUARIO_ID = tipo_usuario;
                                                                                    usuario.CONTRACENA = clave1;
                                                                                    usuario.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                                    persona.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                                    persona.CORREO = email;
                                                                                    return usuarioDAL.ActualizarUsuario(usuario, persona);
                                                                                }
                                                                                else { return "'Nueva Clave' y 'Repetir Clave' deben coincidir"; }
                                                                            }
                                                                            else { return "La clave debe contener al menos 6 caracteres"; }
                                                                        }
                                                                        else
                                                                        {
                                                                            persona.NOMBRE = nombre.ToUpper();
                                                                            persona.APELLIDO = apellido.ToUpper();
                                                                            var arreglo = rut.Split('-');
                                                                            string _rut = arreglo[0];
                                                                            string _dv = arreglo[1];
                                                                            persona.NUM_ID = int.Parse(_rut);
                                                                            persona.DIV_ID = _dv.ToUpper();
                                                                            persona.DIRECCION = direccion.ToUpper();
                                                                            int _teleFijo = 0;
                                                                            int.TryParse(telefono_fijo, out _teleFijo);
                                                                            persona.TELEFONO_FIJO = _teleFijo;
                                                                            int _celular = 0;
                                                                            int.TryParse(celular, out _celular);
                                                                            persona.TELEFONO_CELULAR = _celular;
                                                                            persona.COMUNA_ID = comuna;
                                                                            persona.TIPO_PERSONA_ID = tipo_persona;
                                                                            persona.ESTADO_PERSONA_ID = estado_persona;
                                                                            persona.FECHA_NACIMIENTO = fecha_nac;
                                                                            usuario.SUCURSAL_ID = sucursal;
                                                                            usuario.ESTADO_USUARIO_ID = estado_usuario;
                                                                            usuario.TIPO_USUARIO_ID = tipo_usuario;
                                                                            usuario.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                            persona.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                            persona.CORREO = email;
                                                                            return usuarioDAL.ActualizarUsuario(usuario, persona);
                                                                        }                                                                        
                                                                    }
                                                                    else { return "Debe indicar fecha de nacimiento del usuario"; }
                                                                }
                                                                else { return "Debe indicar fecha de nacimiento del usuario"; }
                                                            }
                                                            else { return "Debe indicar el tipo de usuario"; }
                                                        }
                                                        else { return "Debe indicar un estado del usuario"; }
                                                    }
                                                    else { return "Debe indicar una sucursal"; }
                                                }
                                                else { return "Debe indicar un estado de persona"; }
                                            }
                                            else { return "Debe indicar un tipo de persona"; }
                                        }
                                        else { return "Debe indicar una comuna"; }
                                    }
                                    else { return "Debe indicar correo electrónico del cliente"; }
                                }
                                else { return "La dirección debe tener al menos 5 caracteres"; }
                            }
                            else { return "El rut ingresado no es válido. Debe ingresar el rut sin puntos (9999999-9)"; }
                        }
                        else { return "Debe ingresar el rut sin puntos (9999999-9)"; }
                    }
                    else { return "El apellido debe tener al menos 2 caracteres"; }
                }
                else { return "El nombre debe tener al menos 2 caracteres"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean RutValido(string _rutIn)
        {
            if (_rutIn.Contains('-'))
            {
                string rutIn = _rutIn.Replace(".", "");
                var arreglo = rutIn.Split('-');
                string RUT = arreglo[0];
                string DV = arreglo[1];

                var rut = RUT;
                var longitud = rut.Length;
                var factor = 2;
                var sumaProducto = 0;
                var con = 0;
                var caracter = 0;
                for (con = longitud - 1; con >= 0; con--)
                {
                    caracter = Int32.Parse(rut.Substring(con, 1));
                    sumaProducto += (factor * caracter);
                    factor++; if (factor > 7) factor = 2;
                }
                var digitoAuxiliar = 11 - (sumaProducto % 11);
                var caracteres = "-123456789K0";
                var digitoCaracter = caracteres.Substring(digitoAuxiliar, 1);
                return DV.ToUpper().Equals(digitoCaracter);
            }
            else
            {
                return false;
            }
        }
        
    }
}
