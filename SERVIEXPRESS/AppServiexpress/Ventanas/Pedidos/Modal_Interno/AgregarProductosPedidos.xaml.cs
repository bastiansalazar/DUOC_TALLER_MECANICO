﻿using BBCServiexpress.DAL;
using BBCServiexpress.DAL.Vistas;
using BBCServiexpress.NEG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppServiexpress.Ventanas.Pedidos.Modal_Interno
{
    /// <summary>
    /// Lógica de interacción para AgregarProductosPedidos.xaml
    /// </summary>
    public partial class AgregarProductosPedidos : Window
    {
        private int IdProveedor;
        private int IdSucursal;
        private EmitirPedido_AD _EmitirPedido_AD;

        public AgregarProductosPedidos()
        {
            InitializeComponent();            
        }

        public AgregarProductosPedidos(int idProveedor, int idSucursal, EmitirPedido_AD emitirPedido_AD)
        {
            InitializeComponent();
            IdProveedor = idProveedor;
            IdSucursal = idSucursal;
            _EmitirPedido_AD = emitirPedido_AD;
            CargarCombos();
        }
        private void CargarCombos()
        {
            ProveedoresNEG proveedoresNEG = new ProveedoresNEG();
            CategoriaNEG categoriaNEG = new CategoriaNEG();
            MarcaNEG marcaNEG = new MarcaNEG();
            ProductosNEG productosNEG = new ProductosNEG();
            cbxTipoProducto.IsEnabled = false;

            try
            {
                txtNombreProveedor.Text = proveedoresNEG.CargarProveedor(IdProveedor).NOMBRE_EMPRESA;
             
                List<CATEGORIA> listaCategoria = categoriaNEG.ListarCategorias();
                if (listaCategoria.Count > 0)
                {
                    cbxCategoria.ItemsSource = listaCategoria;
                    cbxCategoria.DisplayMemberPath = "NOMBRE";
                    cbxCategoria.SelectedValuePath = "ID";
                }

                List<MARCA> listaMarca = marcaNEG.ListarMarcas();
                if (listaMarca.Count > 0)
                {
                    cbxMarca.ItemsSource = listaMarca;
                    cbxMarca.DisplayMemberPath = "NOMBRE";
                    cbxMarca.SelectedValuePath = "ID";
                }

                List<ProductosVIEW> listaProductos = productosNEG.ListarTodosProductosSucursalProveedor(IdSucursal, IdProveedor);
                if (listaProductos.Count > 0)
                {
                    cbxProducto.ItemsSource = listaProductos;
                    cbxProducto.DisplayMemberPath = "NOMBRE";
                    cbxProducto.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }
        }

        private void cbxCategoria_Seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (cbxCategoria.SelectedValue != null)
            {
                try
                {
                    int idCategoria = int.Parse(cbxCategoria.SelectedValue.ToString());
                    TipoProductoNEG tipoProductoNEG = new TipoProductoNEG();
                    List<TIPO_PRODUCTO> lista = tipoProductoNEG.ListarTProductosCategoria(idCategoria);
                    if (lista.Count > 0)
                    {
                        cbxTipoProducto.ItemsSource = lista;
                        cbxTipoProducto.DisplayMemberPath = "NOMBRE";
                        cbxTipoProducto.SelectedValuePath = "ID";
                        cbxTipoProducto.IsEnabled = true;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }

            }

        }

        private void btnFiltrarProductos_Click(object sender, RoutedEventArgs e)
        {
            int categoria = 0;
            int marca = 0;
            int tipoProducto = 0;

            if (cbxCategoria.SelectedValue != null)
                categoria = int.Parse(cbxCategoria.SelectedValue.ToString());

            if (cbxMarca.SelectedValue != null)
                marca = int.Parse(cbxMarca.SelectedValue.ToString());

            if (cbxTipoProducto.SelectedValue != null)
                tipoProducto = int.Parse(cbxTipoProducto.SelectedValue.ToString());

            try
            {
                if (categoria > 0 || marca > 0 || tipoProducto > 0 )
                {
                    ProductosNEG productosNEG = new ProductosNEG();
                    List<ProductosVIEW> listaProductos = productosNEG.FiltrarProductosSu_Ca_Ma_Ti(IdSucursal,categoria,marca,tipoProducto);
                    if (listaProductos.Count > 0)
                    {
                        cbxProducto.ItemsSource = listaProductos;
                        cbxProducto.DisplayMemberPath = "NOMBRE";
                        cbxProducto.SelectedValuePath = "ID";
                    }
                    else
                    {
                        List<ProductosVIEW> listaProductos2 = productosNEG.ListarTodosProductosSucursal(IdSucursal);
                        if (listaProductos2.Count > 0)
                        {
                            cbxProducto.ItemsSource = listaProductos2;
                            cbxProducto.DisplayMemberPath = "NOMBRE";
                            cbxProducto.SelectedValuePath = "ID";
                        }
                        MessageBox.Show("No existen productos para la sucursal con los filtros indicados");
                    }
                }
                else
                {
                    MessageBox.Show("Debe indicar al menos un filtro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
            }          
        }

        private void btnAgregarProd_Click(object sender, RoutedEventArgs e)
        {

            if (cbxProducto.SelectedValue != null)
            {
                if (txtCantidad.Text.Trim().Length > 0)
                {
                    int cantidad = 0;
                    int.TryParse(txtCantidad.Text, out cantidad);
                    if (cantidad > 0)
                    {
                        int _idProd = int.Parse(cbxProducto.SelectedValue.ToString());
                        decimal _preTot = int.Parse(txtValorTotal.Text.ToString());
                        ProductosNEG productosNEG = new ProductosNEG();
                        var datos = productosNEG.CargarProducto(_idProd);
                        _EmitirPedido_AD.AgregarItemTablaProductos(_idProd,datos.NOMBRE,cantidad, Convert.ToDecimal(datos.PRECIO_COMPRA),_preTot);
                        Limpiar();
                    }
                    else
                    {
                        txtCantidad.Text = "";
                        MessageBox.Show("Debe indicar una cantidad");
                    }
                }
                else
                {
                    MessageBox.Show("Debe indicar una cantidad");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto");
            }            
        }

        private void cbxProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxProducto.SelectedValue != null)
            {
                try
                {
                    int idProducto = int.Parse(cbxProducto.SelectedValue.ToString());
                    ProductosNEG productosNEG = new ProductosNEG();
                    Int32 valor = Convert.ToInt32(productosNEG.CargarProducto(idProducto).PRECIO_COMPRA);
                    txtValorUni.Text = valor.ToString();
                    if(txtCantidad.Text.Trim().Length > 0)
                    {
                        int cantidad = 0;
                        int.TryParse(txtCantidad.Text,out cantidad);
                        if (cantidad > 0)
                        {
                            int total = 0;
                            total = valor * cantidad;
                            txtValorTotal.Text = total.ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.TargetSite + "\n" + ex.Message.ToString());
                }

            }
        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtValorUni.Text.Trim().Length > 0)
            {
                int cantidad = 0;
                int valor = int.Parse(txtValorUni.Text);
                int.TryParse(txtCantidad.Text, out cantidad);
                if (cantidad > 0)
                {
                    int total = 0;
                    total = valor * cantidad;
                    txtValorTotal.Text = total.ToString();
                }
                else
                {
                    txtCantidad.Text = "";
                }
            }
        }

        private void Limpiar()
        {
            txtCantidad.Text = "";
            txtValorUni.Text = "";
            txtValorTotal.Text = "";
            cbxProducto.SelectedIndex = -1;

        }

    }
}