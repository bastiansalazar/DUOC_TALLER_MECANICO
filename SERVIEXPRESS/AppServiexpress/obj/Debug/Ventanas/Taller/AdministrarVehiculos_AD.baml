   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:AppServiexpress.Ventanas.TallerAppServiexpress.Ventanas.Taller  5   	   =    7AppServiexpress.Ventanas.Taller.AdministrarVehiculos_AD   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:AppServiexpress.Ventanas.Taller     5   	     � Title$  Administrar Vehículos��5   	    � WindowStartupLocation$ CenterScreen=�6'   $	��650��6L   $
��1078��6Y    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6f   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      � ��5
   
   �� 5       �� Color$ 	#FF1834E6��6    �� Opacity$	 0.3Q�60   .+�5   
   �� $
��Left=�6   $	��679��62   $��
484,10,0,0q�6?   $	��Top=�6S   $��2��6k   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgVehiculos��5      $
��Left=�6(   $	��329��6C   $��
28,111,0,0q�6P   $	��Top=�6d   $	��431��6|      
IsReadOnly . 6�   �� 5   
   $��Texto Busqueda��6   $
��Left=�6)   $��	28,80,0,0q�6D   $	��Top=�6W   $��98��6o    J� RenderTransformOrigin$ -0.627,-0.269&�6z   �� 5   
   -   5         ��Name$ txtBusqueda��5      $��26��6'   $	��180��64   $��
131,80,0,0q�6@   	 �� TextWrapping$
	 Wrap=�6T   $	��Top=�6h   $
��Left=�6�   $��Center=�6�   
 �� CharacterCasing$
 Upper=�6�   �� 5   
   -   5         ��Name$ 	btnBuscar��5      $��Buscar��6$   $
��Left=�65   $��
336,80,0,0q�6P   $	��Top=�6d   $	��123��6|   $��26��6�   6�   �� 5   
   $��Tipo de Vehiculo��6   $
��Left=�6+   $��508,121,0,0q�6F   $	��Top=�6[   �� 5   
   $��Marca de Vehiculo��6   $
��Left=�6,   $��508,152,0,0q�6G   $	��Top=�6\   �� 5   
   $��Cliente Responsable��6   $
��Left=�6.   $��508,184,0,0q�6I   $	��Top=�6^   �� 5   
   -   5        $ btnModificar��5      $��	Modificar��6'   $
��Left=�6;   $��911,370,0,0q�6V   $	��Top=�6k   $	��123��6�   $��26��6�   6�   �� 5   
   -   5        $ btnCrear��5      $��Crear��6#   $
��Left=�63   $��765,370,0,0q�6N   $	��Top=�6c   $	��123��6{   $��26��6�   6�   �� 5   
   -   5         ��Name$ cbxTipoBusqueda��5      $
��Left=�6,   $��
131,53,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�   .@�5      �� $
��TIPO��6    �� 
IsSelected . 6)   �� 5      $��MARCA��6   �� 5      $��PATENTE��6   5      �� 5   
   $��Tipo Busqueda��6   $
��Left=�6(   $��	28,53,0,0q�6C   $	��Top=�6V   $��98��6n   $ -0.627,-0.269&�6y   �� 5   
   -   5        $ 
btnLimpiar��5      $��Limpiar��6%   $
��Left=�67   $��
917,75,0,0q�6R   $	��Top=�6f   $	��123��6~   $��26��6�   6�   �� 5   
   -   5        $ cbxTipoVehiculo��5      $
��Left=�6,   $��655,120,0,0q�6G   $	��Top=�6\   $��26��6t   $	��385��6�   �� 5   
   -	   5        $ 
cbxCliente��5      $
��Left=�6'   $��655,183,0,0q�6B   $	��Top=�6W   $��26��6o   $	��385��6|   �� 5    
   $!��Identificador de Vehículo:��6   $
��Left=�65   $��
508,90,0,0q�6P   $	��Top=�6d   �� 5!   
   -
   5         ��Name$ lbl_IdVehiculo��5!      $�� ��6(   $
��Left=�63   $��
655,90,0,0q�6N   $	��Top=�6b   �� 5"   
   -   5        $ cbxMarcaVehiculo��5"      $
��Left=�6-   $��655,152,0,0q�6H   $	��Top=�6]   $��26��6u   $	��385��6�   �� 5#   
   $��Patente de Vehiculo��6   $
��Left=�6.   $��508,215,0,0q�6I   $	��Top=�6^   �� 5$   
   -   5        $ 
txtPatente��5$      $��26��6&   $	��180��63   $��655,215,0,0q�6?   $
	 Wrap=�6T   $	��Top=�6h   $
��Left=�6�   $��Center=�6�   $
 Upper=�6�    �� 	MaxLength$ 7��6�   �� 5%   
   -   5        $ btn_LimpiarFiltro��5%      $��Limpiar Filtro��6,   $
��Left=�6E   $��
336,35,0,0q�6`   $	��Top=�6t   $	��123��6�   $��26��6�   6�   �� 5&   
   -   5        $ bntAyuda��5&      $��AYUDA��6#   $
��Left=�63   $��	10,10,0,0q�6N   $	��Top=�6a   $��46��6y   $��20��6�   6�   5(      5)      