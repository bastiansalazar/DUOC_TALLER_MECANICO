using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class ProductosNEG
    {
        public List<ProductosVIEW> ListarTodosProductos()
        {
            try
            {
                ProductosDAL productosDAL = new ProductosDAL();
                return productosDAL.ListarTodosProductos();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ProductosVIEW> ListarTodosProductosSucursal(int idSucursal)
        {
            try
            {
                ProductosDAL productosDAL = new ProductosDAL();
                return productosDAL.ListarTodosProductosSucursal(idSucursal);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ProductosVIEW CargarProducto(int idproducto)
        {
            try
            {
                ProductosDAL productosDAL = new ProductosDAL();
                return productosDAL.CargarProducto(idproducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductosVIEW> FiltrarProductos(string tipo, string valor)
        {
            try
            {
                if (tipo != "" & valor != "")
                {
                    ProductosDAL productosDAL = new ProductosDAL();
                    return productosDAL.FiltrarProductos(tipo, valor);
                }
                else
                {
                    return new List<ProductosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string CrearProducto(int tipoProducto,string descripcion,string nombre, DateTime fechaVencimiento,decimal precioCompra,decimal precioVenta,int sucursal,int proveedor,int stock, int stockCritico,int categoria,int estado, int marca)
        {
            try
            {
                PRODUCTO producto = new PRODUCTO();
                ProductosDAL productosDAL = new ProductosDAL();

                if (tipoProducto >-1)
                {
                    if (sucursal > -1)
                    {
                        if (proveedor > -1)
                        {
                            if (nombre.Trim().Length >= 5)
                            {
                                if (categoria > -1)
                                {
                                    if (estado > -1)
                                    {
                                        if (marca > -1)
                                        {
                                            producto.DESCRIPCION = descripcion;
                                            producto.NOMBRE = nombre;
                                            producto.FECHA_CREACION = DateTime.Now;
                                            producto.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                            producto.PRECIO_COMPRA = precioCompra;
                                            producto.PRECIO_VENTA = precioVenta;
                                            producto.STOCK = stock;
                                            producto.STOCK_CRITICO = stockCritico;
                                            producto.CATEGORIA_ID = categoria;
                                            producto.ESTADO_PRODUCTO_ID = estado;
                                            producto.MARCA_ID = marca;
                                            producto.ID_PROVEERDOR = proveedor;
                                            producto.ID_SUCURSAL = sucursal;
                                            producto.TIPO_PRODUCTO = tipoProducto;
                                            string proveedorSKU = proveedor.ToString(); 
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (proveedorSKU.Length < 3)
                                                    proveedorSKU = "0" + proveedorSKU;
                                            }
                                            string categoriaSKU = categoria.ToString(); 
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (categoriaSKU.Length < 3)
                                                    categoriaSKU = "0" + categoriaSKU;
                                            }
                                            string tipoProductoSKU = tipoProducto.ToString(); 
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (tipoProductoSKU.Length < 3)
                                                    tipoProductoSKU = "0" + tipoProductoSKU;
                                            }
                                            string _diaSKU = "00";
                                            string _mesSKU = "00";
                                            string _anioSKU = "0000";
                                            if (fechaVencimiento != default(DateTime))
                                            {
                                                producto.FECHA_VENCIMIENTO = fechaVencimiento;
                                                _diaSKU = fechaVencimiento.Day.ToString();
                                                for (int i = 0; i < 2; i++)
                                                {
                                                    if (_diaSKU.Length < 2)
                                                        _diaSKU = "0" + _diaSKU;
                                                }
                                                _mesSKU = fechaVencimiento.Month.ToString();
                                                for (int i = 0; i < 2; i++)
                                                {
                                                    if (_mesSKU.Length < 2)
                                                        _mesSKU = "0" + _mesSKU;
                                                }
                                                _anioSKU = fechaVencimiento.Year.ToString();
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    if (_anioSKU.Length < 4)
                                                        _anioSKU = "0" + _anioSKU;
                                                }
                                            }                                                                                            
                                            string fechaSKU = fechaVencimiento.ToString();
                                            producto.SKU = proveedorSKU + categoriaSKU + _diaSKU + _mesSKU + _anioSKU + tipoProductoSKU;
                                            return productosDAL.CrearProducto(producto);
                                        }
                                        else { return "Debe indicar una marca"; }
                                    }
                                    else { return "Debe indicar un estado"; }
                                }
                                else { return "Debe indicar una categoria"; }
                            }
                            else { return "El nombre debe tener al menos 5 caracteres"; }
                        }
                        else { return "Debe indicar un proveedor"; }
                    }
                    else { return "Debe indicar una sucursal"; }
                }
                else { return "Debe indicar una tipo de producto"; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductosVIEW> ListarTodosProductosSucursalProveedor(int idSucursal, int idProveedor)
        {
            try
            {
                ProductosDAL productosDAL = new ProductosDAL();
                return productosDAL.ListarTodosProductosSucursalProveedor(idSucursal, idProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarProducto(int idProducto,int tipoProducto, string descripcion, string nombre, DateTime fechaVencimiento, decimal precioCompra, decimal precioVenta, int sucursal, int proveedor, int stock, int stockCritico, int categoria, int estado, int marca)
        {
            try
            {
                PRODUCTO producto = new PRODUCTO();
                ProductosDAL productosDAL = new ProductosDAL();

                if(idProducto > 0)
                {
                    if (tipoProducto > -1)
                    {
                        if (sucursal > -1)
                        {
                            if (proveedor > -1)
                            {
                                if (nombre.Trim().Length >= 5)
                                {
                                    if (categoria > -1)
                                    {
                                        if (estado > -1)
                                        {
                                            if (marca > -1)
                                            {
                                                producto.ID = idProducto;
                                                producto.DESCRIPCION = descripcion;
                                                producto.NOMBRE = nombre;
                                                producto.FECHA_ULTIMO_UPDATE = DateTime.Now;
                                                producto.PRECIO_COMPRA = precioCompra;
                                                producto.PRECIO_VENTA = precioVenta;                                                
                                                producto.STOCK = stock;
                                                producto.STOCK_CRITICO = stockCritico;
                                                producto.CATEGORIA_ID = categoria;
                                                producto.ESTADO_PRODUCTO_ID = estado;
                                                producto.MARCA_ID = marca;
                                                producto.ID_PROVEERDOR = proveedor;
                                                producto.ID_SUCURSAL = sucursal;
                                                producto.TIPO_PRODUCTO = tipoProducto;
                                                string proveedorSKU = proveedor.ToString();
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    if (proveedorSKU.Length < 3)
                                                        proveedorSKU = "0" + proveedorSKU;
                                                }
                                                string categoriaSKU = categoria.ToString();
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    if (categoriaSKU.Length < 3)
                                                        categoriaSKU = "0" + categoriaSKU;
                                                }
                                                string tipoProductoSKU = tipoProducto.ToString();
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    if (tipoProductoSKU.Length < 3)
                                                        tipoProductoSKU = "0" + tipoProductoSKU;
                                                }
                                                string _diaSKU = "00";
                                                string _mesSKU = "00";
                                                string _anioSKU = "0000";
                                                if (fechaVencimiento != default(DateTime))
                                                {
                                                    producto.FECHA_VENCIMIENTO = fechaVencimiento;
                                                    _diaSKU = fechaVencimiento.Day.ToString();
                                                    for (int i = 0; i < 2; i++)
                                                    {
                                                        if (_diaSKU.Length < 2)
                                                            _diaSKU = "0" + _diaSKU;
                                                    }
                                                    _mesSKU = fechaVencimiento.Month.ToString();
                                                    for (int i = 0; i < 2; i++)
                                                    {
                                                        if (_mesSKU.Length < 2)
                                                            _mesSKU = "0" + _mesSKU;
                                                    }
                                                    _anioSKU = fechaVencimiento.Year.ToString();
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        if (_anioSKU.Length < 4)
                                                            _anioSKU = "0" + _anioSKU;
                                                    }
                                                }
                                                string fechaSKU = fechaVencimiento.ToString();
                                                producto.SKU = proveedorSKU + categoriaSKU + _diaSKU + _mesSKU + _anioSKU + tipoProductoSKU;
                                                return productosDAL.ActualizarProducto(producto);
                                            }
                                            else { return "Debe indicar una marca"; }
                                        }
                                        else { return "Debe indicar un estado"; }
                                    }
                                    else { return "Debe indicar una categoria"; }
                                }
                                else { return "El nombre debe tener al menos 5 caracteres"; }
                            }
                            else { return "Debe indicar un proveedor"; }
                        }
                        else { return "Debe indicar una sucursal"; }
                    }
                    else { return "Debe indicar una tipo de producto"; }
                }
                else { return "Debe seleccinoar un producto"; }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductosVIEW> FiltrarProductosSu_Ca_Ma_Ti(int idSucursal,int idCategoria,int idMarca,int idTipo)
        {
            try
            {
                ProductosDAL productosDAL = new ProductosDAL();                

                if (idCategoria > 0 && idMarca > 0 && idTipo > 0)
                {
                    return productosDAL.ListarTodosProductosSu_Ca_Ma_Ti(idSucursal, idCategoria, idMarca, idTipo);
                }
                else if (idCategoria > 0 && idMarca > 0)
                {
                    return productosDAL.ListarTodosProductosSu_Ca_Ma(idSucursal, idCategoria, idMarca);
                }
                else if (idCategoria > 0 && idTipo > 0)
                {
                    return productosDAL.ListarTodosProductosSu_Ca_Ti(idSucursal, idCategoria, idTipo);
                }
                else if (idCategoria > 0)
                {
                    return productosDAL.ListarTodosProductosSu_Ca(idSucursal, idCategoria);
                }
                else if (idMarca > 0)
                {
                    return productosDAL.ListarTodosProductosSu_Ma(idSucursal, idMarca);
                }
                else
                {
                    return new List<ProductosVIEW>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        

    }
}
