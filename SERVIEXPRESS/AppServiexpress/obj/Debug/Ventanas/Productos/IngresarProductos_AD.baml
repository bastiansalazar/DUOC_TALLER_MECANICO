   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:AppServiexpress.Ventanas.Productos"AppServiexpress.Ventanas.Productos  5   	   =    7AppServiexpress.Ventanas.Productos.IngresarProductos_AD   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:AppServiexpress.Ventanas.Productos     5   	     � Title$  Ingresar Productos��5   	   $	��650��6$   $
��1078��61    � WindowStartupLocation$ CenterScreen=�6>    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6c   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      �  J� RenderTransformOrigin$ 0.5,0.5&�6   $��0,0,2,0q�6+   ��5
   
   �� 5       �� Color$ 	#FF1834E6��6    �� Opacity$	 0.3Q�60   .+�5   
   �� $
��Left=�6   $	��679��62   $��
484,10,0,0q�6?   $	��Top=�6S   $��2��6k   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgProductos��5      $
��Left=�6(   $	��329��6C   $��
28,111,0,0q�6P   $	��Top=�6d   $	��431��6|      
IsReadOnly . 6�   �� 5   
   $��Texto Busqueda��6   $
��Left=�6)   $��	28,80,0,0q�6D   $	��Top=�6W   $��98��6o   $ -0.627,-0.269&�6z   �� 5   
   -   5         ��Name$ txtBusqueda��5      $��26��6'   $	��180��64   $��
131,80,0,0q�6@   	 �� TextWrapping$
	 Wrap=�6T   $	��Top=�6h   $
��Left=�6�   $��Center=�6�   
 �� CharacterCasing$
 Upper=�6�   �� 5   
   -   5         ��Name$ 	btnBuscar��5      $��Buscar��6$   $
��Left=�65   $��
336,80,0,0q�6P   $	��Top=�6d   $	��123��6|   $��26��6�   6�   �� 5   
   $��Nombre��6   $
��Left=�6!   $��508,166,0,0q�6<   $	��Top=�6Q   �� 5   
   -   5        $ 	txtNombre��5      $
��Left=�6%   $��26��6@   $	��439��6M   $��601,166,0,0q�6Y   $
	 Wrap=�6n   $	��Top=�6�   $��Center=�6�   $
 Upper=�6�    �� 	MaxLength$ 48��6�   �� 5   
   $��Descripción��6   $
��Left=�6&   $��508,198,0,0q�6A   $	��Top=�6V   �� 5   
   -   5        $ txtDescripcion��5      $
��Left=�6*   $��26��6E   $	��439��6R   $��601,196,0,0q�6^   $
	 Wrap=�6s   $	��Top=�6�   $��Center=�6�   $
 Upper=�6�   $ 98��6�   �� 5   
   $��	Categoria��6   $
��Left=�6$   $��508,384,0,0q�6?   $	��Top=�6T   �� 5   
   -   5        $ btnCrear��5      $��Ingresar Nuevo Producto��6#   $
��Left=�6E   $��878,496,0,0q�6`   $	��Top=�6u   $	��161��6�   $��26��6�   6�   �� 5   
   -   5         ��Name$ cbxTipoBusqueda��5      $
��Left=�6,   $��
131,53,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�   .@�5      �� $��NOMBRE��6    �� 
IsSelected . 6+   �� 5      $��	CATEGORIA��6   �� 5      $	��SKU��6   �� 5      $��MARCA��6   5      �� 5   
   $��Tipo Busqueda��6   $
��Left=�6(   $��	28,53,0,0q�6C   $	��Top=�6V   $��98��6n   $ -0.627,-0.269&�6y   �� 5    
   -   5        $ cbxCategoria��5       $
��Left=�6)   $��601,384,0,0q�6D   $	��Top=�6Y   $��26��6q   $	��439��6~   5!      �� 5"   
   -	   5        $ 
btnLimpiar��5"      $��Limpiar��6%   $
��Left=�67   $��
917,35,0,0q�6R   $	��Top=�6f   $	��123��6~   $��26��6�   6�   �� 5#   
   $��Fecha Venc.��6   $
��Left=�6&   $��508,228,0,0q�6A   $	��Top=�6V   $ 0.731,-0.231&�6n   (  "System.Windows.Controls.DatePicker  5$   
   -
   5          Name$ dpkFechaVenc��5$      $
��Left=�6+   $��600,228,0,0q�6F   $	��Top=�6[   $��26��6s   $	��180��6�   �� 5%   
   $��Precio Compra��6   $
��Left=�6(   $��508,260,0,0q�6C   $	��Top=�6X   �� 5&   
   -   5        $ txtPrecioCompra��5&      $
��Left=�6+   $��26��6F   $	��161��6S   $��601,258,0,0q�6_   $
	 Wrap=�6t   $	��Top=�6�   $��Center=�6�   $ 9��6�   �� 5'   
   $��Precio Venta��6   $
��Left=�6'   $��508,290,0,0q�6B   $	��Top=�6W   �� 5(   
   -   5        $ txtPrecioVenta��5(      $
��Left=�6*   $��26��6E   $	��161��6R   $��601,290,0,0q�6^   $
	 Wrap=�6s   $	��Top=�6�   $��Center=�6�   $ 9��6�   �� 5)   
   $��Stock��6   $
��Left=�6    $��508,322,0,0q�6;   $	��Top=�6P   �� 5*   
   -   5        $ txtStock��5*      $
��Left=�6$   $��26��6?   $	��161��6L   $��601,322,0,0q�6X   $
	 Wrap=�6m   $	��Top=�6�   $��Center=�6�   $ 9��6�   �� 5+   
   $��Stock Critico��6   $
��Left=�6(   $��508,354,0,0q�6C   $	��Top=�6X   �� 5,   
   -   5        $ txtStockCritico��5,      $
��Left=�6+   $��26��6F   $	��161��6S   $��601,352,0,0q�6_   $
	 Wrap=�6t   $	��Top=�6�   $��Center=�6�   $ 9��6�   �� 5-   
   $��Estado Prod.��6   $
��Left=�6'   $��507,416,0,0q�6B   $	��Top=�6W   �� 5.   
   -   5        $ 	cbxEstado��5.      $
��Left=�6&   $��600,414,0,0q�6A   $	��Top=�6V   $��26��6n   $	��162��6{   �� 5/   
   $��Marca Prod.��6   $
��Left=�6&   $��785,416,0,0q�6A   $	��Top=�6V   �� 50   
   -   5        $ cbxMarca��50      $
��Left=�6%   $��878,414,0,0q�6@   $	��Top=�6U   $��26��6m   $	��162��6z   �� 51   
   $��	Proveedor��6   $
��Left=�6$   $��507,135,0,0q�6?   $	��Top=�6T   �� 52   
   -   5        $ cbxProveedor��52      $
��Left=�6)   $��600,135,0,0q�6D   $	��Top=�6Y   $��26��6q   $	��439��6~   �� 53   
   $��Sucural��6   $
��Left=�6"   $��508,104,0,0q�6=   $	��Top=�6R   �� 54   
   -   5        $ cbxSucursal��54      $
��Left=�6(   $��601,104,0,0q�6C   $	��Top=�6X   $��26��6p   $	��439��6}   �� 55   
   $��
Tipo Prod.��6   $
��Left=�6%   $��508,445,0,0q�6@   $	��Top=�6U   �� 56   
   -   5        $ cbxTipoProducto��56      $
��Left=�6,   $��601,445,0,0q�6G   $	��Top=�6\   $��26��6t   $	��439��6�   �� 57   
   -   5        $ btn_LimpiarFiltro��57      $��Limpiar Filtro��6,   $
��Left=�6E   $��
336,35,0,0q�6`   $	��Top=�6t   $	��123��6�   $��26��6�   6�   �� 58   
   -   5        $ bntAyuda��58      $��AYUDA��6#   $
��Left=�63   $��	10,10,0,0q�6N   $	��Top=�6a   $��46��6y   $��20��6�   6�   5:      5;      