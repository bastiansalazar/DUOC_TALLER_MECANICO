   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:AppServiexpress.Ventanas.Pedidos AppServiexpress.Ventanas.Pedidos  5   	   :    4AppServiexpress.Ventanas.Pedidos.EmitirDevolucion_AD   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:AppServiexpress.Ventanas.Pedidos     5   	     � Title$!  Emitir Devolucion de Pedido��5   	   $	��650��6-   $
��1078��6:    � WindowStartupLocation$ CenterScreen=�6G    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6l   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      �  J� RenderTransformOrigin$ 0.5,0.5&�6   $��0,0,2,0q�6+   ��5
   
   �� 5       �� Color$ 	#FF1834E6��6    �� Opacity$	 0.3Q�60   .+�5   
   �� ���	#FFF4F4F5$
��Left=�6%   $	��589��6@   $��	90,10,0,0q�6M   
���   �$	��Top=�6o   $	��802��6�   �� 5   
   -   5         ��Name$ 
btnLimpiar��5      $��Limpiar��6%   $
��Left=�67   $��
917,35,0,0q�6R   $	��Top=�6f   $	��123��6~   $��26��6�   6�   �� 5   
   $�� ��6   $
��Left=�6   $��558,394,0,0q�66   $	��Top=�6K   � 5   
   $��113,35,176,20q�6   .+�5      �� $
��Left=�6   $��62��6/   $	��Top=�6;   $	��147��6S   $J��D/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_logo.png��6_   �� 5      $��FORMULARIO DE DEVOLUCIÓN��6   $
��Left=�67   $��
261,37,0,0q�6R   $	��Top=�6f   $��14(�6~   $
��Bold"�6�   $	��219��6�   &   System.Windows.Controls.DataGrid  5      -   5          Name$ dgProductos��5      $
��Left=�6,   $	��138��6G   $��
10,341,0,0q�6T   $	��Top=�6h   $	��748��6�   $	��767��6�   �� 5      $��Email Proveedor��6   $
��Left=�6.   $��
10,484,0,0q�6I   $	��Top=�6]   $��97��6u   $ 0.509,0.577&�6�   �� 5      $��Productos Solicitados��6   $
��Left=�64   $��
10,315,0,0q�6O   $	��Top=�6c   $��12(�6{   $
��Bold"�6�   $	��219��6�   �� 5      $
��Left=�6   $��
10,151,0,0q�63   $
��Wrap=�6G   $����Estimado Proveedor, hemos generado un formulario de devolución de productos, ya que el pedido no fue recepcionado por los motivos que se detallan a continuación:��6[   $	��Top=�6  $	��757��6  $��36��6(  �� 5      -   5         ��Name$ cbxFolio��5      $
��Left=�6)   $��
88,120,0,0q�6D   $	��Top=�6X   $��26��6p   $	��120��6}   6�   �� 5      $��Folio OP��6   $
��Left=�6'   $��
10,120,0,0q�6B   $	��Top=�6V   $��56��6n   $ 0.509,0.577&�6y   �� 5      -   5        	 ��Name$	 	txtMotivo��5      
 �� 	MaxLength$

 2500��6)   $
��Left=�6:   $	��118��6U   $	��748��6c   $��
10,192,0,0q�6o    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $��Center=�6�   �� 5      -   5        $	 txtEmailProveedor��5      $
��Left=�61   $��26��6L   $	��646��6Y   $��112,484,0,0q�6e   $
 Wrap=�6z   $	��Top=�6�   $��Center=�6�   $
 48��6�   �� 5      $��Email Sucursal��6   $
��Left=�6-   $��
10,515,0,0q�6H   $	��Top=�6\   $��97��6t   $ 0.509,0.577&�6   �� 5      -   5        $	 txtEmailSucursal��5      $
��Left=�60   $��26��6K   $	��646��6X   $��112,515,0,0q�6d   $
 Wrap=�6y   $	��Top=�6�   $��Center=�6�   $
 48��6�   �� 5      $��Sucursal��6   $
��Left=�6'   $��	10,89,0,0q�6B   $	��Top=�6U   $��73��6m   $ 0.509,0.577&�6x   �� 5      -   5        $ cbxSucursal��5      $
��Left=�6,   $��	88,89,0,0q�6G   $	��Top=�6Z   $��26��6r   $	��419��6   6�   5       �� 5!   
   -   5        $ 	btnEnviar��5!      $��Enviar Devolución��6$   $
��Left=�6@   $��917,136,0,0q�6[   $	��Top=�6p   $	��123��6�   $��26��6�   6�   �� 5"   
   -	   5        $ btnExportar��5"      $��Exportar PDF��6&   $
��Left=�6=   $��
917,84,0,0q�6X   $	��Top=�6l   $	��123��6�   $��26��6�   
����������	#FFCD38386�   �� 5#   
   -
   5        $ bntAyuda��5#      $��AYUDA��6#   $
��Left=�63   $��10,10,0,589q�6N   $��46��6c   6n   5(      5)      