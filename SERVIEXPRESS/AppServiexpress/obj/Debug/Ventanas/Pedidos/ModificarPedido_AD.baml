   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:AppServiexpress.Ventanas.Pedidos AppServiexpress.Ventanas.Pedidos  5   	   9    3AppServiexpress.Ventanas.Pedidos.ModificarPedido_AD   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:AppServiexpress.Ventanas.Pedidos     5   	     � Title$   Modificar Pedido Productos��5   
   $	��650��6-   $
��1078��6:    � WindowStartupLocation$ CenterScreen=�6G    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6l   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      � ��5
   
   �� 5       �� Color$ 	#FF1834E6��6    �� Opacity$	 0.3Q�60   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgOrdenPedido��5      $
��Left=�6*   $	��476��6E   $��
28,111,0,0q�6R   $	��Top=�6f   $
��1015��6~      
IsReadOnly . 6�   �� 5   
   $��Filtro��6   $
��Left=�6!   $��	28,80,0,0q�6<   $	��Top=�6O   $��98��6g    J� RenderTransformOrigin$ -0.627,-0.269&�6r   �� 5   
   -   5         ��Name$ 	btnBuscar��5      $��Filtrar��6$   $
��Left=�66   $��
336,80,0,0q�6Q   $	��Top=�6e   $	��123��6}   $��26��6�   6�   �� 5   
   -   5        	 ��Name$	 cbxTipoBusqueda��5      $
��Left=�6,   $��
131,53,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�   .@�5      �� $��	PROVEEDOR��6   �� 5      $��SUCURSAL��6   �� 5      $��ESTADO��6   5      �� 5   
   $��Tipo Filtro��6   $
��Left=�6&   $��	28,53,0,0q�6A   $	��Top=�6T   $��98��6l   $ -0.627,-0.269&�6w   �� 5   
   -   5        $ btnExcel��5      $��Exportar Excel��6#   $
��Left=�6<   $��
920,80,0,0q�6W   $	��Top=�6k   $	��123��6�   $��26��6�   ���	#FF207244
�������6�   �� 5   
   -   5        $ btnWord��5      $��Exportar Word��6"   $
��Left=�6:   $��
792,80,0,0q�6U   $	��Top=�6i   $	��123��6�   $��26��6�   
����������	#FF2A56966�   �� 5   
   -   5        $ btnTexto��5      $��Exportar Texto��6#   $
��Left=�6<   $��
664,80,0,0q�6W   $	��Top=�6k   $	��123��6�   $��26��6�   6�   �� 5   
   -   5        $	 cbxTextoBusqueda��5      $
��Left=�6-   $��
131,82,0,0q�6H   $	��Top=�6\   $��26��6t   $	��180��6�   5      �� 5   
   -   5        $ btn_LimpiarFiltro��5      $��Limpiar Filtro��6,   $
��Left=�6E   $��
336,35,0,0q�6`   $	��Top=�6t   $	��123��6�   $��26��6�   6�   �� 5   
   -	   5        $ bntAyuda��5      $��AYUDA��6#   $
��Left=�63   $��	10,10,0,0q�6N   $	��Top=�6a   $��46��6y   $��20��6�   6�   5      5      