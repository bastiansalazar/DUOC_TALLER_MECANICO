   M S B A M L   `   `   `  ����J  FAppServiexpress, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullo<clr-namespace:AppServiexpress.Ventanas.Pedidos.Modal_Interno.AppServiexpress.Ventanas.Pedidos.Modal_Interno  5   	   M    GAppServiexpress.Ventanas.Pedidos.Modal_Interno.AgregarProductosPedidos3   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Nlocal<clr-namespace:AppServiexpress.Ventanas.Pedidos.Modal_Interno     5   	     � Title$  Agregar Productos��5   
   $��363.5��6$   $��589.59��63    � WindowStartupLocation$ CenterScreen=�6B    � Icon$L F/AppServiexpress;component/Contenidos/Imagenes/Serviexpress_icono2.png��6g   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6  .��5	      � ���	#FFFB9F9F.+�5
   
   �� $��	Proveedor��6   $
��Left=�6$   $��	35,26,0,0q�6?   $	��Top=�6R   $��72��6j    J� RenderTransformOrigin$ 0.509,0.577&�6u   �� 5   
   -   5         ��Name$ txtNombreProveedor��5      $
��Left=�6.   $��26��6I   $	��436��6V   $��
107,26,0,0q�6b    �� TextWrapping$
 Wrap=�6v   $	��Top=�6�   $��Center=�6�    �� CharacterCasing$ Upper=�6�    �� 	MaxLength$ 48��6�    �� 
IsReadOnly . 6�   �� 5   
   -   5        	 ��Name$	 cbxCategoria��5      $
��Left=�6)   $��
107,62,0,0q�6D   $	��Top=�6X   $��26��6p   $	��436��6}   6�   �� 5   
   $��	Categoria��6   $
��Left=�6$   $��	35,62,0,0q�6?   $	��Top=�6R   $��72��6j   $ 0.509,0.577&�6u   �� 5   
   -   5        $	 cbxMarca��5      $
��Left=�6%   $��
107,93,0,0q�6@   $	��Top=�6T   $��26��6l   $	��436��6y   �� 5   
   $��Marca��6   $
��Left=�6    $��	35,93,0,0q�6;   $	��Top=�6N   $��43��6f   $ 0.509,0.577&�6q   �� 5   
   -   5        $	 cbxTipoProducto��5      $
��Left=�6,   $��107,124,0,0q�6G   $	��Top=�6\   $��26��6t   $	��436��6�   �� 5   
   $��
Tipo Prod.��6   $
��Left=�6%   $��
35,124,0,0q�6@   $	��Top=�6T   $��67��6l   $ 0.509,0.577&�6w   �� 5   
   -   5        $	 cbxProducto��5      $
��Left=�6(   $��107,198,0,0q�6C   $	��Top=�6X   $��26��6p   $	��436��6}   6�   �� 5   
   $��Producto��6   $
��Left=�6#   $��
35,198,0,0q�6>   $	��Top=�6R   $��67��6j   $ 0.509,0.577&�6u   �� 5   
   $��Cantidad��6   $
��Left=�6#   $��
35,229,0,0q�6>   $	��Top=�6R   $��67��6j   $ 0.509,0.577&�6u   �� 5   
   -   5        $ txtCantidad��5      $
��Left=�6'   $��26��6B   $��56��6O   $��107,229,0,0q�6Z   $
 Wrap=�6o   $	��Top=�6�   $��Center=�6�   $ Upper=�6�   $ 48��6�   6�   �� 5   
   $��	Valor Uni��6   $
��Left=�6$   $��168,229,0,0q�6?   $	��Top=�6T   $��67��6l   $ 0.509,0.577&�6w   �� 5   
   -   5        $ txtValorUni��5      $
��Left=�6'   $��26��6B   $	��122��6O   $��235,229,0,0q�6[   $
 Wrap=�6p   $	��Top=�6�   $��Center=�6�   $ Upper=�6�   $ 48��6�    . 6�   �� 5   
   $��Valor Total��6   $
��Left=�6&   $��357,229,0,0q�6A   $	��Top=�6V   $��67��6n   $ 0.806,0.462&�6y   �� 5   
   -   5        $ txtValorTotal��5      $
��Left=�6)   $��26��6D   $	��119��6Q   $��424,229,0,0q�6]   $
 Wrap=�6r   $	��Top=�6�   $��Center=�6�   $ Upper=�6�   $ 48��6�    . 6�   �� 5   
   -	   5        
 ��Name$
 btnAgregarProd��5      $��Agregar Productos��6)   $
��Left=�6E   $��234,272,0,0q�6`   $	��Top=�6u   $	��123��6�   $��26��6�   6�   �� 5   
   -
   5        $
 btnFiltrarProductos��5      $��Filtrar Productos��6.   $
��Left=�6J   $��
35,160,0,0q�6e   $	��Top=�6y   $	��123��6�   $��26��6�   6�   �� 5   
   -   5        $
 btnAgregarProd_Copy��5      $��Modificar Detalle��6.   $
��Left=�6J   $��234,272,0,0q�6e   $	��Top=�6z   $	��123��6�   $��26��6�   $y�Hidden=�6�   {�.  6�   5      5      