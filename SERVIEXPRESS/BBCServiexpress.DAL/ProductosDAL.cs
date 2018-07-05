using BBCServiexpress.DAL.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.DAL
{
    public class ProductosDAL
    {
        public List<ProductosVIEW> ListarTodosProductos()
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                                  join b in con.MARCA on a.MARCA_ID equals b.ID
                                  join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                                  join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                                  join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                                  join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                                  join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                                  join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              orderby a.NOMBRE ascending
                              select new ProductosVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      DESCRIPCION = a.DESCRIPCION,
                                      NOMBRE = a.NOMBRE,
                                      FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                      PRECIO_COMPRA = a.PRECIO_COMPRA,
                                      PRECIO_VENTA = a.PRECIO_VENTA,
                                      SKU = a.SKU,
                                      STOCK = a.STOCK,
                                      STOCK_CRITICO = a.STOCK_CRITICO,
                                      CATEGORIA_ID = a.CATEGORIA_ID,
                                      ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                      MARCA_ID = a.MARCA_ID,
                                      CATEGORIA = d.NOMBRE,
                                      ESTADO_PRODUCTO = c.NOMBRE,
                                      MARCA = b.NOMBRE,
                                      SUCURSAL_ID = a.ID_SUCURSAL,
                                      SUCURSAL = f.NOMBRE,
                                      TIPO_PRODUCTO = g.NOMBRE,
                                      TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                      PROVEEDOR = j.DIV_ID,
                                      PROVEEDOR_ID = a.ID_PROVEERDOR
                                  }).ToList();
                return _query;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                              join b in con.MARCA on a.MARCA_ID equals b.ID
                              join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                              join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                              join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                              join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                              join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                              join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              where a.ID_SUCURSAL == idSucursal
                              orderby a.NOMBRE ascending
                              select new ProductosVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  DESCRIPCION = a.DESCRIPCION,
                                  NOMBRE = a.NOMBRE,
                                  FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                  PRECIO_COMPRA = a.PRECIO_COMPRA,
                                  PRECIO_VENTA = a.PRECIO_VENTA,
                                  SKU = a.SKU,
                                  STOCK = a.STOCK,
                                  STOCK_CRITICO = a.STOCK_CRITICO,
                                  CATEGORIA_ID = a.CATEGORIA_ID,
                                  ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                  MARCA_ID = a.MARCA_ID,
                                  CATEGORIA = d.NOMBRE,
                                  ESTADO_PRODUCTO = c.NOMBRE,
                                  MARCA = b.NOMBRE,
                                  SUCURSAL_ID = a.ID_SUCURSAL,
                                  SUCURSAL = f.NOMBRE,
                                  TIPO_PRODUCTO = g.NOMBRE,
                                  TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                  PROVEEDOR = j.DIV_ID,
                                  PROVEEDOR_ID = a.ID_PROVEERDOR
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProductosVIEW CargarProducto(int idProducto)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                              join b in con.MARCA on a.MARCA_ID equals b.ID
                              join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                              join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                              join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                              join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                              join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                              join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              where a.ID == idProducto
                              select new ProductosVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  DESCRIPCION = a.DESCRIPCION,
                                  NOMBRE = a.NOMBRE,
                                  FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                  PRECIO_COMPRA = a.PRECIO_COMPRA,
                                  PRECIO_VENTA = a.PRECIO_VENTA,
                                  SKU = a.SKU,
                                  STOCK = a.STOCK,
                                  STOCK_CRITICO = a.STOCK_CRITICO,
                                  CATEGORIA_ID = a.CATEGORIA_ID,
                                  ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                  MARCA_ID = a.MARCA_ID,
                                  CATEGORIA = d.NOMBRE,
                                  ESTADO_PRODUCTO = c.NOMBRE,
                                  MARCA = b.NOMBRE,
                                  SUCURSAL_ID = a.ID_SUCURSAL,
                                  SUCURSAL = f.NOMBRE,
                                  TIPO_PRODUCTO = g.NOMBRE,
                                  TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                  PROVEEDOR = j.DIV_ID,
                                  PROVEEDOR_ID = a.ID_PROVEERDOR
                              }).FirstOrDefault();
                return _query;
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
                EntitiesServiexpress con = new EntitiesServiexpress();
                if (tipo == "CATEGORIA")
                {
                    string filtro = valor.ToUpper();
                    var _query = (from a in con.PRODUCTO
                                  join b in con.MARCA on a.MARCA_ID equals b.ID
                                  join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                                  join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                                  join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                                  join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                                  join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                                  join j in con.PERSONA on h.PERSONA_ID equals j.ID
                                  where d.NOMBRE.Contains(filtro)
                                  orderby a.NOMBRE ascending
                                  select new ProductosVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      DESCRIPCION = a.DESCRIPCION,
                                      NOMBRE = a.NOMBRE,
                                      FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                      PRECIO_COMPRA = a.PRECIO_COMPRA,
                                      PRECIO_VENTA = a.PRECIO_VENTA,
                                      SKU = a.SKU,
                                      STOCK = a.STOCK,
                                      STOCK_CRITICO = a.STOCK_CRITICO,
                                      CATEGORIA_ID = a.CATEGORIA_ID,
                                      ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                      MARCA_ID = a.MARCA_ID,
                                      CATEGORIA = d.NOMBRE,
                                      ESTADO_PRODUCTO = c.NOMBRE,
                                      MARCA = b.NOMBRE,
                                      SUCURSAL_ID = a.ID_SUCURSAL,
                                      SUCURSAL = f.NOMBRE,
                                      TIPO_PRODUCTO = g.NOMBRE,
                                      TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                      PROVEEDOR = j.DIV_ID,
                                      PROVEEDOR_ID = a.ID_PROVEERDOR
                                  }).ToList();
                    return _query;
                }
                else if (tipo == "MARCA")
                {
                    string filtro = valor.ToUpper();
                    var _query = (from a in con.PRODUCTO
                                  join b in con.MARCA on a.MARCA_ID equals b.ID
                                  join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                                  join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                                  join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                                  join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                                  join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                                  join j in con.PERSONA on h.PERSONA_ID equals j.ID
                                  where b.NOMBRE.Contains(filtro)
                                  orderby a.NOMBRE ascending
                                  select new ProductosVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      DESCRIPCION = a.DESCRIPCION,
                                      NOMBRE = a.NOMBRE,
                                      FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                      PRECIO_COMPRA = a.PRECIO_COMPRA,
                                      PRECIO_VENTA = a.PRECIO_VENTA,
                                      SKU = a.SKU,
                                      STOCK = a.STOCK,
                                      STOCK_CRITICO = a.STOCK_CRITICO,
                                      CATEGORIA_ID = a.CATEGORIA_ID,
                                      ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                      MARCA_ID = a.MARCA_ID,
                                      CATEGORIA = d.NOMBRE,
                                      ESTADO_PRODUCTO = c.NOMBRE,
                                      MARCA = b.NOMBRE,
                                      SUCURSAL_ID = a.ID_SUCURSAL,
                                      SUCURSAL = f.NOMBRE,
                                      TIPO_PRODUCTO = g.NOMBRE,
                                      TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                      PROVEEDOR = j.DIV_ID,
                                      PROVEEDOR_ID = a.ID_PROVEERDOR
                                  }).ToList();
                    return _query;
                }
                else if (tipo == "SKU")
                {
                    string filtro = valor.ToUpper();
                    var _query = (from a in con.PRODUCTO
                                  join b in con.MARCA on a.MARCA_ID equals b.ID
                                  join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                                  join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                                  join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                                  join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                                  join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                                  join j in con.PERSONA on h.PERSONA_ID equals j.ID
                                  where a.SKU.Contains(filtro)
                                  orderby a.NOMBRE ascending
                                  select new ProductosVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      DESCRIPCION = a.DESCRIPCION,
                                      NOMBRE = a.NOMBRE,
                                      FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                      PRECIO_COMPRA = a.PRECIO_COMPRA,
                                      PRECIO_VENTA = a.PRECIO_VENTA,
                                      SKU = a.SKU,
                                      STOCK = a.STOCK,
                                      STOCK_CRITICO = a.STOCK_CRITICO,
                                      CATEGORIA_ID = a.CATEGORIA_ID,
                                      ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                      MARCA_ID = a.MARCA_ID,
                                      CATEGORIA = d.NOMBRE,
                                      ESTADO_PRODUCTO = c.NOMBRE,
                                      MARCA = b.NOMBRE,
                                      SUCURSAL_ID = a.ID_SUCURSAL,
                                      SUCURSAL = f.NOMBRE,
                                      TIPO_PRODUCTO = g.NOMBRE,
                                      TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                      PROVEEDOR = j.DIV_ID,
                                      PROVEEDOR_ID = a.ID_PROVEERDOR
                                  }).ToList();
                    return _query;
                }
                else if (tipo == "NOMBRE")
                {
                    string filtro = valor.ToUpper();
                    var _query = (from a in con.PRODUCTO
                                  join b in con.MARCA on a.MARCA_ID equals b.ID
                                  join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                                  join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                                  join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                                  join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                                  join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                                  join j in con.PERSONA on h.PERSONA_ID equals j.ID
                                  where a.NOMBRE.Contains(filtro)
                                  orderby a.NOMBRE ascending
                                  select new ProductosVIEW
                                  {
                                      ID = a.ID,
                                      FECHA_CREACION = a.FECHA_CREACION,
                                      FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                      DESCRIPCION = a.DESCRIPCION,
                                      NOMBRE = a.NOMBRE,
                                      FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                      PRECIO_COMPRA = a.PRECIO_COMPRA,
                                      PRECIO_VENTA = a.PRECIO_VENTA,
                                      SKU = a.SKU,
                                      STOCK = a.STOCK,
                                      STOCK_CRITICO = a.STOCK_CRITICO,
                                      CATEGORIA_ID = a.CATEGORIA_ID,
                                      ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                      MARCA_ID = a.MARCA_ID,
                                      CATEGORIA = d.NOMBRE,
                                      ESTADO_PRODUCTO = c.NOMBRE,
                                      MARCA = b.NOMBRE,
                                      SUCURSAL_ID = a.ID_SUCURSAL,
                                      SUCURSAL = f.NOMBRE,
                                      TIPO_PRODUCTO = g.NOMBRE,
                                      TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                      PROVEEDOR = j.DIV_ID,
                                      PROVEEDOR_ID = a.ID_PROVEERDOR
                                  }).ToList();
                    return _query;
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

        public List<ProductosVIEW> ListarTodosProductosSucursalProveedor(int idSucursal, int idProveedor)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                              join b in con.MARCA on a.MARCA_ID equals b.ID
                              join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                              join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                              join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                              join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                              join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                              join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              where a.ID_SUCURSAL == idSucursal &&
                              a.ID_PROVEERDOR == idProveedor
                              orderby a.NOMBRE ascending
                              select new ProductosVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  DESCRIPCION = a.DESCRIPCION,
                                  NOMBRE = a.NOMBRE,
                                  FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                  PRECIO_COMPRA = a.PRECIO_COMPRA,
                                  PRECIO_VENTA = a.PRECIO_VENTA,
                                  SKU = a.SKU,
                                  STOCK = a.STOCK,
                                  STOCK_CRITICO = a.STOCK_CRITICO,
                                  CATEGORIA_ID = a.CATEGORIA_ID,
                                  ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                  MARCA_ID = a.MARCA_ID,
                                  CATEGORIA = d.NOMBRE,
                                  ESTADO_PRODUCTO = c.NOMBRE,
                                  MARCA = b.NOMBRE,
                                  SUCURSAL_ID = a.ID_SUCURSAL,
                                  SUCURSAL = f.NOMBRE,
                                  TIPO_PRODUCTO = g.NOMBRE,
                                  TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                  PROVEEDOR = j.DIV_ID,
                                  PROVEEDOR_ID = a.ID_PROVEERDOR
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CrearProducto(PRODUCTO producto)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exProducto = (from a in con.PRODUCTO
                                   where a.SKU == producto.SKU
                                   select a).FirstOrDefault();

                if (_exProducto == null)
                {
                    con.PRODUCTO.Add(producto);
                    con.SaveChanges();
                    return "creado";
                }
                else
                {
                    return "El producto ya existe en los registros";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ActualizarProducto(PRODUCTO producto)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _exProducto = (from a in con.PRODUCTO
                                   where a.ID == producto.ID
                                   select a).FirstOrDefault();

                if (_exProducto != null)
                {
                    _exProducto.FECHA_CREACION = producto.FECHA_CREACION;
                    _exProducto.FECHA_ULTIMO_UPDATE = producto.FECHA_ULTIMO_UPDATE;
                    _exProducto.DESCRIPCION = producto.DESCRIPCION;
                    _exProducto.NOMBRE = producto.NOMBRE;
                    _exProducto.FECHA_VENCIMIENTO = producto.FECHA_VENCIMIENTO;
                    _exProducto.PRECIO_COMPRA = producto.PRECIO_COMPRA;
                    _exProducto.PRECIO_VENTA = producto.PRECIO_VENTA;
                    _exProducto.STOCK = producto.STOCK;
                    _exProducto.STOCK_CRITICO = producto.STOCK_CRITICO;
                    _exProducto.CATEGORIA_ID = producto.CATEGORIA_ID;
                    _exProducto.ESTADO_PRODUCTO_ID = producto.ESTADO_PRODUCTO_ID;
                    _exProducto.MARCA_ID = producto.MARCA_ID;
                    _exProducto.SKU = producto.SKU;
                    _exProducto.ID_PROVEERDOR = producto.ID_PROVEERDOR;
                    _exProducto.ID_SUCURSAL = producto.ID_SUCURSAL;
                    _exProducto.TIPO_PRODUCTO = producto.TIPO_PRODUCTO;
                    con.SaveChanges();
                    return "actualizado";
                }
                else
                {
                    return "El producto no existe en los registros";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductosVIEW> ListarTodosProductosSu_Ca_Ma_Ti(int idSucursal,int idCategoria,int idMarca,int IdTipo)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                              join b in con.MARCA on a.MARCA_ID equals b.ID
                              join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                              join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                              join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                              join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                              join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                              join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              where a.ID_SUCURSAL == idSucursal
                              && a.CATEGORIA_ID == idCategoria
                              && a.MARCA_ID == idMarca
                              && a.TIPO_PRODUCTO == IdTipo
                              orderby a.NOMBRE ascending
                              select new ProductosVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  DESCRIPCION = a.DESCRIPCION,
                                  NOMBRE = a.NOMBRE,
                                  FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                  PRECIO_COMPRA = a.PRECIO_COMPRA,
                                  PRECIO_VENTA = a.PRECIO_VENTA,
                                  SKU = a.SKU,
                                  STOCK = a.STOCK,
                                  STOCK_CRITICO = a.STOCK_CRITICO,
                                  CATEGORIA_ID = a.CATEGORIA_ID,
                                  ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                  MARCA_ID = a.MARCA_ID,
                                  CATEGORIA = d.NOMBRE,
                                  ESTADO_PRODUCTO = c.NOMBRE,
                                  MARCA = b.NOMBRE,
                                  SUCURSAL_ID = a.ID_SUCURSAL,
                                  SUCURSAL = f.NOMBRE,
                                  TIPO_PRODUCTO = g.NOMBRE,
                                  TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                  PROVEEDOR = j.DIV_ID,
                                  PROVEEDOR_ID = a.ID_PROVEERDOR
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProductosVIEW> ListarTodosProductosSu_Ca_Ma(int idSucursal, int idCategoria, int idMarca)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                              join b in con.MARCA on a.MARCA_ID equals b.ID
                              join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                              join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                              join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                              join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                              join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                              join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              where a.ID_SUCURSAL == idSucursal
                              && a.CATEGORIA_ID == idCategoria
                              && a.MARCA_ID == idMarca
                              orderby a.NOMBRE ascending
                              select new ProductosVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  DESCRIPCION = a.DESCRIPCION,
                                  NOMBRE = a.NOMBRE,
                                  FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                  PRECIO_COMPRA = a.PRECIO_COMPRA,
                                  PRECIO_VENTA = a.PRECIO_VENTA,
                                  SKU = a.SKU,
                                  STOCK = a.STOCK,
                                  STOCK_CRITICO = a.STOCK_CRITICO,
                                  CATEGORIA_ID = a.CATEGORIA_ID,
                                  ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                  MARCA_ID = a.MARCA_ID,
                                  CATEGORIA = d.NOMBRE,
                                  ESTADO_PRODUCTO = c.NOMBRE,
                                  MARCA = b.NOMBRE,
                                  SUCURSAL_ID = a.ID_SUCURSAL,
                                  SUCURSAL = f.NOMBRE,
                                  TIPO_PRODUCTO = g.NOMBRE,
                                  TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                  PROVEEDOR = j.DIV_ID,
                                  PROVEEDOR_ID = a.ID_PROVEERDOR
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProductosVIEW> ListarTodosProductosSu_Ca_Ti(int idSucursal, int idCategoria, int IdTipo)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                              join b in con.MARCA on a.MARCA_ID equals b.ID
                              join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                              join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                              join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                              join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                              join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                              join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              where a.ID_SUCURSAL == idSucursal
                              && a.CATEGORIA_ID == idCategoria
                              && a.TIPO_PRODUCTO == IdTipo
                              orderby a.NOMBRE ascending
                              select new ProductosVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  DESCRIPCION = a.DESCRIPCION,
                                  NOMBRE = a.NOMBRE,
                                  FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                  PRECIO_COMPRA = a.PRECIO_COMPRA,
                                  PRECIO_VENTA = a.PRECIO_VENTA,
                                  SKU = a.SKU,
                                  STOCK = a.STOCK,
                                  STOCK_CRITICO = a.STOCK_CRITICO,
                                  CATEGORIA_ID = a.CATEGORIA_ID,
                                  ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                  MARCA_ID = a.MARCA_ID,
                                  CATEGORIA = d.NOMBRE,
                                  ESTADO_PRODUCTO = c.NOMBRE,
                                  MARCA = b.NOMBRE,
                                  SUCURSAL_ID = a.ID_SUCURSAL,
                                  SUCURSAL = f.NOMBRE,
                                  TIPO_PRODUCTO = g.NOMBRE,
                                  TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                  PROVEEDOR = j.DIV_ID,
                                  PROVEEDOR_ID = a.ID_PROVEERDOR
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProductosVIEW> ListarTodosProductosSu_Ma(int idSucursal, int idMarca)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                              join b in con.MARCA on a.MARCA_ID equals b.ID
                              join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                              join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                              join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                              join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                              join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                              join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              where a.ID_SUCURSAL == idSucursal
                              && a.MARCA_ID == idMarca
                              orderby a.NOMBRE ascending
                              select new ProductosVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  DESCRIPCION = a.DESCRIPCION,
                                  NOMBRE = a.NOMBRE,
                                  FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                  PRECIO_COMPRA = a.PRECIO_COMPRA,
                                  PRECIO_VENTA = a.PRECIO_VENTA,
                                  SKU = a.SKU,
                                  STOCK = a.STOCK,
                                  STOCK_CRITICO = a.STOCK_CRITICO,
                                  CATEGORIA_ID = a.CATEGORIA_ID,
                                  ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                  MARCA_ID = a.MARCA_ID,
                                  CATEGORIA = d.NOMBRE,
                                  ESTADO_PRODUCTO = c.NOMBRE,
                                  MARCA = b.NOMBRE,
                                  SUCURSAL_ID = a.ID_SUCURSAL,
                                  SUCURSAL = f.NOMBRE,
                                  TIPO_PRODUCTO = g.NOMBRE,
                                  TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                  PROVEEDOR = j.DIV_ID,
                                  PROVEEDOR_ID = a.ID_PROVEERDOR
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProductosVIEW> ListarTodosProductosSu_Ca(int idSucursal, int idCategoria)
        {
            try
            {
                EntitiesServiexpress con = new EntitiesServiexpress();
                var _query = (from a in con.PRODUCTO
                              join b in con.MARCA on a.MARCA_ID equals b.ID
                              join c in con.ESTADO_PRODUCTO on a.ESTADO_PRODUCTO_ID equals c.ID
                              join d in con.CATEGORIA on a.CATEGORIA_ID equals d.ID
                              join f in con.SUCURSAL on a.ID_SUCURSAL equals f.ID
                              join g in con.TIPO_PRODUCTO on a.TIPO_PRODUCTO equals g.ID
                              join h in con.PROVEEDOR on a.ID_PROVEERDOR equals h.ID
                              join j in con.PERSONA on h.PERSONA_ID equals j.ID
                              where a.ID_SUCURSAL == idSucursal
                              && a.CATEGORIA_ID == idCategoria
                              orderby a.NOMBRE ascending
                              select new ProductosVIEW
                              {
                                  ID = a.ID,
                                  FECHA_CREACION = a.FECHA_CREACION,
                                  FECHA_ULTIMO_UPDATE = a.FECHA_ULTIMO_UPDATE,
                                  DESCRIPCION = a.DESCRIPCION,
                                  NOMBRE = a.NOMBRE,
                                  FECHA_VENCIMIENTO = a.FECHA_VENCIMIENTO,
                                  PRECIO_COMPRA = a.PRECIO_COMPRA,
                                  PRECIO_VENTA = a.PRECIO_VENTA,
                                  SKU = a.SKU,
                                  STOCK = a.STOCK,
                                  STOCK_CRITICO = a.STOCK_CRITICO,
                                  CATEGORIA_ID = a.CATEGORIA_ID,
                                  ESTADO_PRODUCTO_ID = a.ESTADO_PRODUCTO_ID,
                                  MARCA_ID = a.MARCA_ID,
                                  CATEGORIA = d.NOMBRE,
                                  ESTADO_PRODUCTO = c.NOMBRE,
                                  MARCA = b.NOMBRE,
                                  SUCURSAL_ID = a.ID_SUCURSAL,
                                  SUCURSAL = f.NOMBRE,
                                  TIPO_PRODUCTO = g.NOMBRE,
                                  TIPO_PRODUCTO_ID = a.TIPO_PRODUCTO,
                                  PROVEEDOR = j.DIV_ID,
                                  PROVEEDOR_ID = a.ID_PROVEERDOR
                              }).ToList();
                return _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }//
}
