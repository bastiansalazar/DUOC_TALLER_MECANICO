#pragma checksum "..\..\..\..\Ventanas\Inicio\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DC82ED867AA1DC4C7A33437839B2DC8DDFFE7000"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using AppServiexpress;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AppServiexpress
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 13 "..\..\..\..\Ventanas\Inicio\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl1;

#line default
#line hidden


#line 14 "..\..\..\..\Ventanas\Inicio\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl2;

#line default
#line hidden


#line 15 "..\..\..\..\Ventanas\Inicio\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUsuario;

#line default
#line hidden


#line 16 "..\..\..\..\Ventanas\Inicio\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtClave;

#line default
#line hidden


#line 17 "..\..\..\..\Ventanas\Inicio\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEntrar;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AppServiexpress;component/ventanas/inicio/mainwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\Ventanas\Inicio\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.InicioSesion = ((AppServiexpress.MainWindow)(target));
                    return;
                case 2:
                    this.lbl1 = ((System.Windows.Controls.Label)(target));
                    return;
                case 3:
                    this.lbl2 = ((System.Windows.Controls.Label)(target));
                    return;
                case 4:
                    this.txtUsuario = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 5:
                    this.txtClave = ((System.Windows.Controls.PasswordBox)(target));
                    return;
                case 6:
                    this.btnEntrar = ((System.Windows.Controls.Button)(target));

#line 17 "..\..\..\..\Ventanas\Inicio\MainWindow.xaml"
                    this.btnEntrar.Click += new System.Windows.RoutedEventHandler(this.btnEntrar_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window InicioSesion;
        internal System.Windows.Controls.Image img1;
    }
}
