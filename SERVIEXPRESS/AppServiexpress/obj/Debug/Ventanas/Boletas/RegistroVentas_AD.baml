   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:AppServiexpress.Ventanas.Boletas AppServiexpress.Ventanas.Boletas  5   	   8    2AppServiexpress.Ventanas.Boletas.RegistroVentas_AD   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:AppServiexpress.Ventanas.Boletas     5   	     � Title$  Registro de Ventas��5   	   $	��650��6$   $
��1078��61    � WindowStartupLocation$ CenterScreen=�6>    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6c   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      � ��5
   
   �� 5       �� Color$ 	#FF1834E6��6    �� Opacity$	 0.3Q�60   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgVentas��5      $
��Left=�6%   $	��476��6@   $��
28,111,0,0q�6M   $	��Top=�6a   $
��1015��6y      
IsReadOnly . 6�   �� 5   
   -   5         ��Name$ 	btnBuscar��5      $��Filtrar��6$   $
��Left=�66   $��
336,80,0,0q�6Q   $	��Top=�6e   $	��123��6}   $��26��6�   6�   �� 5   
   -   5        $ btnExcel��5      $��Exportar Excel��6#   $
��Left=�6<   $��
920,80,0,0q�6W   $	��Top=�6k   $	��123��6�   $��26��6�   ���	#FF207244
�������6�   �� 5   
   -   5        $ btnWord��5      $��Exportar Word��6"   $
��Left=�6:   $��
792,80,0,0q�6U   $	��Top=�6i   $	��123��6�   $��26��6�   
����������	#FF2A56966�   �� 5   
   -   5        $ btnTexto��5      $��Exportar Texto��6#   $
��Left=�6<   $��
664,80,0,0q�6W   $	��Top=�6k   $	��123��6�   $��26��6�   6�   �� 5   
   $��Fecha Hasta��6   $
��Left=�6&   $��	28,80,0,0q�6A   $	��Top=�6T   $��98��6l    J� RenderTransformOrigin$ -0.627,-0.269&�6w   �� 5   
   $��Fecha Desde��6   $
��Left=�6&   $��	28,53,0,0q�6A   $	��Top=�6T   $��98��6l   $ -0.627,-0.269&�6w   (  "System.Windows.Controls.DatePicker  5   
   -   5        	  Name$	 dpkFechaDesde��5      $
��Left=�6,   $��
126,54,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�     5   
   -   5        $	 dpkFechaHasta��5      $
��Left=�6,   $��
126,80,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�   �� 5   
   -   5        $ btn_LimpiarFiltro��5      $��Limpiar Filtro��6,   $
��Left=�6E   $��
336,35,0,0q�6`   $	��Top=�6t   $	��123��6�   $��26��6�   6�   �� 5   
   -	   5        $ bntAyuda��5      $��AYUDA��6#   $
��Left=�63   $��	10,10,0,0q�6N   $	��Top=�6a   $��46��6y   $��20��6�   6�   5      5      