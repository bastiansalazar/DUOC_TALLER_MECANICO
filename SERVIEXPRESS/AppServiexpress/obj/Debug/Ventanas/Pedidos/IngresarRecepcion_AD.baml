   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:AppServiexpress.Ventanas.Pedidos AppServiexpress.Ventanas.Pedidos  5   	   ;    5AppServiexpress.Ventanas.Pedidos.IngresarRecepcion_AD   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:AppServiexpress.Ventanas.Pedidos     5   	     � Title$#  Ingresar Recepción de Pedido��5   	   $	��650��6.   $
��1078��6;    � WindowStartupLocation$ CenterScreen=�6H    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6m   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      �  J� RenderTransformOrigin$ 0.5,0.5&�6   $��0,0,2,0q�6+   ��5
   
   �� 5       �� Color$ 	#FF1834E6��6    �� Opacity$	 0.3Q�60   .+�5   
   �� $
��Left=�6   $	��679��62   $��
484,10,0,0q�6?   $	��Top=�6S   $��2��6k   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgControlRecepcion��5      $
��Left=�6/   $	��329��6J   $��
28,111,0,0q�6W   $	��Top=�6k   $	��431��6�      
IsReadOnly . 6�   �� 5   
   $��Fecha Hasta��6   $
��Left=�6&   $��	28,80,0,0q�6A   $	��Top=�6T   $��98��6l   $ -0.627,-0.269&�6w   �� 5   
   -   5         ��Name$ 	btnBuscar��5      $��Buscar��6$   $
��Left=�65   $��
336,80,0,0q�6P   $	��Top=�6d   $	��123��6|   $��26��6�   6�   �� 5   
   $��
Comentario��6   $
��Left=�6%   $��508,535,0,0q�6@   $	��Top=�6U   �� 5   
   -   5        	 ��Name$	 txtComentario��5      $
��Left=�6)   $��26��6D   $	��455��6Q   $��585,535,0,0q�6]   
 �� TextWrapping$

 Wrap=�6r   $	��Top=�6�   $��Center=�6�    �� 	MaxLength$ 98��6�   �� 5   
   -   5        $ bntIngresar��5      $��Ingresar Recepción��6&   $
��Left=�6C   $��878,566,0,0q�6^   $	��Top=�6s   $	��161��6�   $��26��6�   6�   �� 5   
   $��Fecha Desde��6   $
��Left=�6&   $��	28,53,0,0q�6A   $	��Top=�6T   $��98��6l   $ -0.627,-0.269&�6w   �� 5   
   -   5        $ 
btnLimpiar��5      $��Limpiar��6%   $
��Left=�67   $��
917,35,0,0q�6R   $	��Top=�6f   $	��123��6~   $��26��6�   6�   �� 5   
   $��Empleado Responsable��6   $
��Left=�6/   $��507,201,0,0q�6J   $	��Top=�6_   �� 5   
   -   5         ��Name$ cbxEmpleado��5      $
��Left=�6(   $��645,201,0,0q�6C   $	��Top=�6X   $��26��6p   $	��394��6}   �� 5   
   $��Sucural��6   $
��Left=�6"   $��508,104,0,0q�6=   $	��Top=�6R   �� 5   
   -   5        $ cbxSucursal��5      $
��Left=�6(   $��645,104,0,0q�6C   $	��Top=�6X   $��26��6p   $	��395��6}   6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dpkFechaDesde��5      $
��Left=�6,   $��
126,54,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�     5   
   -	   5        $ dpkFechaHasta��5      $
��Left=�6,   $��
126,80,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�   �� 5   
   -
   5        $ btnExcel��5      $��Exportar Excel��6#   $
��Left=�6<   $��335,445,0,0q�6W   $	��Top=�6l   $	��123��6�   $��26��6�   ���	#FF207244
�������6�   �� 5   
   -   5        $ btnWord��5      $��Exportar Word��6"   $
��Left=�6:   $��207,445,0,0q�6U   $	��Top=�6j   $	��123��6�   $��26��6�   
����������	#FF2A56966�   �� 5   
   -   5        $ btnTexto��5      $��Exportar Texto��6#   $
��Left=�6<   $��
79,445,0,0q�6W   $	��Top=�6k   $	��123��6�   $��26��6�   6�   �� 5    
   $��Folio Orden Pedido��6   $
��Left=�6-   $��508,135,0,0q�6H   $	��Top=�6]   �� 5!   
   -   5        $ 
cbxFolioOP��5!      $
��Left=�6'   $��645,135,0,0q�6B   $	��Top=�6W   $��26��6o   $	��161��6|   6�   �� 5"   
   $��Fecha Recepción��6   $
��Left=�6*   $��509,166,0,0q�6E   $	��Top=�6Z   $ 0.731,-0.231&�6r     5#   
   -   5        $ dkpFechaRecepcion��5#      $
��Left=�60   $��645,166,0,0q�6K   $	��Top=�6`   $��26��6x   $	��180��6�   �� 5$   
   $��Estado Control��6   $
��Left=�6)   $��508,232,0,0q�6D   $	��Top=�6Y   �� 5%   
   -   5        $ cbxEstadoControl��5%      $
��Left=�6-   $��645,232,0,0q�6H   $	��Top=�6]   $��26��6u   $	��161��6�     5&   
   -   5        $ dgDetalleControlRecepcion��5&      $
��Left=�66   $	��233��6Q   $��509,297,0,0q�6^   $	��Top=�6s   $	��530��6�    . 6�   �� 5'   
   -   5        $ btnModificarDetalle��5'      $��Modificar Producto��6.   $
��Left=�6K   $��830,266,0,0q�6f   $	��Top=�6{   $	��111��6�   $��26��6�   6�   �� 5(   
   -   5        $ btnQuitarDetalle��5(      $��Quitar Producto��6+   $
��Left=�6E   $��946,266,0,0q�6`   $	��Top=�6u   $��93��6�   $��26��6�   6�   �� 5)   
   -   5        $ btnPDF��5)      $��Exportar PDF��6!   $
��Left=�68   $��
336,49,0,0q�6S   $	��Top=�6g   $	��123��6   $��26��6�   
����������	#FFCD38386�   �� 5*   
   -   5        $ btnAgregarDetalle��5*      $��Agregar Producto��6,   $
��Left=�6G   $��714,266,0,0q�6b   $	��Top=�6w   $	��111��6�   $��26��6�   6�   �� 5+   
   -   5        $ btn_LimpiarFiltro��5+      $��Limpiar Filtro��6,   $
��Left=�6E   $��
335,10,0,0q�6`   $	��Top=�6t   $	��123��6�   $��26��6�   6�   �� 5,   
   -   5        $ bntAyuda��5,      $��AYUDA��6#   $
��Left=�63   $��
10,0,0,589q�6N   $��Bottom=�6b   $��46��6}   $��20��6�   6�   5.      5/      