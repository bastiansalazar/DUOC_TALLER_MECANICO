   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:AppServiexpress.Ventanas.Usuarios.Modal_Interno/AppServiexpress.Ventanas.Usuarios.Modal_Interno  5   	   C    =AppServiexpress.Ventanas.Usuarios.Modal_Interno.CargarPersona   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Olocal=clr-namespace:AppServiexpress.Ventanas.Usuarios.Modal_Interno     5   	     � Title$  Cargar Persona��5   	   $��363.5��6    $��589.59��6/    � WindowStartupLocation$ CenterScreen=�6>    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6c   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      � ���	#FFFB9F9F.+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgPersonas��5
      $
��Left=�6'   $	��192��6B   $��	10,86,0,0q�6O   $	��Top=�6b   $	��562��6z      
IsReadOnly . 6�   �� 5   
   $��Texto Busqueda��6   $
��Left=�6)   $��	18,55,0,0q�6D   $	��Top=�6W   $��98��6o    J� RenderTransformOrigin$ -0.627,-0.269&�6z   �� 5   
   -   5         ��Name$ txtBusqueda��5      $��26��6'   $	��180��64   $��
121,55,0,0q�6@    �� TextWrapping$
 Wrap=�6T   $	��Top=�6h   $
��Left=�6�   $��Center=�6�    �� CharacterCasing$ Upper=�6�   �� 5   
   -   5        	 ��Name$	 	btnBuscar��5      $��Buscar��6$   $
��Left=�65   $��
326,55,0,0q�6P   $	��Top=�6d   $	��123��6|   $��26��6�   6�   �� 5   
   -   5        
 ��Name$
 cbxTipoBusqueda��5      $
��Left=�6,   $��
121,28,0,0q�6G   $	��Top=�6[   $��26��6s   $	��180��6�   .@�5      �� $��NOMBRE��6    �� 
IsSelected . 6+   �� 5      $��APELLIDO��6   �� 5      $	��RUT��6   5      �� 5   
   $��Tipo Busqueda��6   $
��Left=�6(   $��	18,28,0,0q�6C   $	��Top=�6V   $��98��6n   $ -0.627,-0.269&�6y   �� 5   
   -   5        $	 btn_LimpiarFiltro��5      $��Limpiar Filtro��6,   $
��Left=�6E   $��
326,10,0,0q�6`   $	��Top=�6t   $	��123��6�   $��26��6�   6�   �� 5   
   -   5        $	 
btn_Cargar��5      $��Cargar Datos��6%   $
��Left=�6<   $��249,283,0,0q�6W   $	��Top=�6l   $	��123��6�   $��26��6�   6�   5      5      