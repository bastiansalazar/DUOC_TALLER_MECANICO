﻿using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class EmpleadosNEG
    {
        public List<EmpleadosVIEW> ListarTodosEmpleados()
        {
            try
            {
                EmpleadosDAL empleadosDAL = new EmpleadosDAL();
                return empleadosDAL.ListarTodosEmpleados();
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
                EmpleadosDAL empleadosDAL = new EmpleadosDAL();
                return empleadosDAL.ListarTodosEmpleadosSucursal(sucursal);
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
                EmpleadosDAL empleadosDAL = new EmpleadosDAL();
                return empleadosDAL.CargarEmpleado(idEmpleado);
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
                if (tipo != "" & valor != "")
                {
                    EmpleadosDAL empleadosDAL = new EmpleadosDAL();
                    return empleadosDAL.FiltrarEmpleados(tipo, valor);
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

        public string CrearEmpleado(string nombre,string apellido,string rut,DateTime fecha_nac,string direccion,string email,int comuna,string telefono_fijo,string celular,int tipo_persona,int estado_persona,int tipo_empleado,int estado_empleado,int sucursal)
        {
            try
            {
                PERSONA persona = new PERSONA();
                EMPLEADO empleado = new EMPLEADO();
                EmpleadosDAL empleadosDAL = new EmpleadosDAL();

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
                                                        if (estado_empleado > -1)
                                                        {
                                                            if (tipo_empleado > -1)
                                                            {
                                                                if (fecha_nac != default(DateTime))
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
                                                                    empleado.SUCURSAL_ID = sucursal;
                                                                    empleado.ESTADO_EMPLEADO_ID = estado_empleado;
                                                                    empleado.TIPO_EMPLEADO_ID = tipo_empleado;
                                                                    empleado.FECHA_CREACION = DateTime.Now;
                                                                    empleado.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                    persona.FECHA_CREACION = DateTime.Now;
                                                                    persona.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                    persona.CORREO = email;
                                                                    return empleadosDAL.CrearEmpleado(empleado, persona);
                                                                }
                                                                else { return "Debe indicar fecha de nacimiento del empleado"; }
                                                            }
                                                            else { return "Debe tipo de empleado"; }
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

        public string ActualizarEmpleado(string nombre, string apellido, string rut, DateTime fecha_nac, string direccion, string email, int comuna, string telefono_fijo, string celular, int tipo_persona, int estado_persona, int tipo_empleado, int estado_empleado, int sucursal)
        {
            try { 
            PERSONA persona = new PERSONA();
            EMPLEADO empleado = new EMPLEADO();
            EmpleadosDAL empleadosDAL = new EmpleadosDAL();

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
                                                    if (estado_empleado > -1)
                                                    {
                                                        if (tipo_empleado > -1)
                                                        {
                                                            if (fecha_nac != default(DateTime))
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
                                                                empleado.SUCURSAL_ID = sucursal;
                                                                empleado.ESTADO_EMPLEADO_ID = estado_empleado;
                                                                empleado.TIPO_EMPLEADO_ID = tipo_empleado;
                                                                empleado.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                persona.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                                persona.CORREO = email;
                                                                return empleadosDAL.ActualizarEmpleado(empleado, persona);
                                                            }
                                                            else { return "Debe indicar fecha de nacimiento del empleado"; }
                                                        }
                                                        else { return "Debe tipo de empleado"; }
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

        public Boolean RutValido(string _rutIn)
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

    }
}
