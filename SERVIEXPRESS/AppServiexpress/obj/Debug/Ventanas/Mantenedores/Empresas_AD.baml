   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:AppServiexpress.Ventanas.Mantenedores%AppServiexpress.Ventanas.Mantenedores  5   	   7    1AppServiexpress.Ventanas.Mantenedores.Empresas_AD   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Elocal3clr-namespace:AppServiexpress.Ventanas.Mantenedores     5   	     � Title$  Empresas��5      $	��650��6   $
��1078��6&    � WindowStartupLocation$ CenterScreen=�63    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6X   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      � ��5
   
   �� 5       �� Color$ 	#FF1834E6��6    �� Opacity$	 0.3Q�60   .+�5   
   �� $
��Left=�6   $	��679��62   $��
484,10,0,0q�6?   $	��Top=�6S   $��2��6k   �� 5   
   $��Tipo Busqueda��6   $
��Left=�6(   $��	28,53,0,0q�6C   $	��Top=�6V   $��98��6n    J� RenderTransformOrigin$ -0.627,-0.269&�6y   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgDatos��5      $
��Left=�6$   $	��329��6?   $��
28,111,0,0q�6L   $	��Top=�6`   $	��431��6x      
IsReadOnly . 6�   �� 5   
   $��Texto Busqueda��6   $
��Left=�6)   $��	28,80,0,0q�6D   $	��Top=�6W   $��98��6o   $ -0.627,-0.269&�6z   �� 5   
   -   5         ��Name$ txtBusqueda��5      $��26��6'   $	��180��64   $��
131,80,0,0q�6@   	 �� TextWrapping$
	 Wrap=�6T   $	��Top=�6h   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5        
 ��Name$
 	btnBuscar��5      $��Buscar��6$   $
��Left=�65   $��
336,80,0,0q�6P   $	��Top=�6d   $	��123��6|   $��26��6�   6�   �� 5   
   $��Razon Social��6   $
��Left=�6'   $��600,142,0,0q�6B   $	��Top=�6W   �� 5   
   -   5        $ txtRazonSocial��5      $
��Left=�6*   $��26��6E   $	��324��6R   $��682,142,0,0q�6^   $
	 Wrap=�6s   $	��Top=�6�   $��Center=�6�    �� 	MaxLength$ 48��6�    �� CharacterCasing$ Upper=�6�   �� 5   
   -   5         ��Name$ cbxTipoBusqueda��5      $
��Left=�6,   $��
131,53,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�   .@�5      �� $��NOMBRE��6    �� 
IsSelected . 6+   5      �� 5   
   $	��Id:��6   $
��Left=�6   $��602,111,0,0q�69   $	��Top=�6N   �� 5   
   -   5         ��Name$ lblId��5      $�� ��6   $
��Left=�6*   $��682,111,0,0q�6E   $	��Top=�6Z   $	��192��6r   �� 5   
   -   5        $
 btnModificar��5      $��	Modificar��6'   $
��Left=�6;   $��917,354,0,0q�6V   $	��Top=�6k   $	��123��6�   $��26��6�   6�   �� 5   
   -   5        $
 btnCrear��5      $��Crear��6#   $
��Left=�63   $��771,354,0,0q�6N   $	��Top=�6c   $	��123��6{   $��26��6�   6�   �� 5   
   -	   5        $ txtDireccion��5      $
��Left=�6(   $��26��6C   $	��324��6P   $��682,173,0,0q�6\   $
	 Wrap=�6q   $	��Top=�6�   $��Center=�6�   $ 48��6�   $ Upper=�6�   �� 5   
   $��
Dirección��6   $
��Left=�6$   $��602,173,0,0q�6?   $	��Top=�6T   $ 0.578,0.436&�6l   �� 5   
   -
   5        $ txtRut��5      $
��Left=�6"   $��26��6=   $	��324��6J   $��682,204,0,0q�6V   $
	 Wrap=�6k   $	��Top=�6   $��Center=�6�   $ 10��6�   $ Upper=�6�   �� 5   
   $	��Rut��6   $
��Left=�6   $��602,204,0,0q�69   $	��Top=�6N   �� 5    
   -   5        $ txtTelefono��5       $
��Left=�6'   $��26��6B   $	��324��6O   $��682,235,0,0q�6[   $
	 Wrap=�6p   $	��Top=�6�   $��Center=�6�   $ 9��6�   $ Upper=�6�   �� 5!   
   $��	Teléfono��6   $
��Left=�6#   $��602,235,0,0q�6>   $	��Top=�6S   �� 5"   
   -   5        $ cbxEstadoEmpresa��5"      $
��Left=�6-   $��682,266,0,0q�6H   $	��Top=�6]   $��26��6u   $	��180��6�   5#      �� 5$   
   $��Estado��6   $
��Left=�6!   $��602,266,0,0q�6<   $	��Top=�6Q   �� 5%   
   -   5        $
 btn_LimpiarFiltro��5%      $��Limpiar Filtro��6,   $
��Left=�6E   $��
336,35,0,0q�6`   $	��Top=�6t   $	��123��6�   $��26��6�   6�   �� 5&   
   -   5        $
 bntAyuda��5&      $��AYUDA��6#   $
��Left=�63   $��
10,0,0,589q�6N   $��Bottom=�6b   $��46��6}   $��20��6�   6�   5'      5(      