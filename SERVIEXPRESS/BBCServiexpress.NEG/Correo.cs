using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class Correo
    {
        public MailMessage CorreoConfirmacionEnvioPedido(string nombredoc, string emailSucursal, string nombreEmpleado,string emailProveedor,string nombreProveedor)
        {
            MailMessage mensaje = new MailMessage();
            mensaje.Subject = "Orden de Pedido - SERVIEXPRESS";
            mensaje.To.Add(new MailAddress(emailProveedor));
            mensaje.CC.Add(new MailAddress(emailSucursal));
            mensaje.From = new MailAddress("sistemaserviexpress@serviexpress.cl", "Serviexpress");
            mensaje.Attachments.Add(new Attachment(nombredoc));
            mensaje.Body = "Hola " + nombreProveedor + "\nEn este correo se encuentra adjunta una Orden de Pedido," +
                " solicitada a través del Sistema de Serviexpress. " +
                "\nPara su correcta visualización, debe tener instalado Adobe Reader. " +
                "\n\nAtentamente le saluda" +
                "\nSistema de Serviexpress" +
                "\n\n****No conteste a este correo****";

            return mensaje;

        }//fin CorreoConfirmacionSolicitudHora

        public MailMessage CorreoDevolucionPedido(string respuesta, string emailSuc, string emailPro, string nombreProveedor, string folio,string motivo)
        {
            MailMessage mensaje = new MailMessage();
            mensaje.Subject = "Devolución de Pedido - SERVIEXPRESS";
            mensaje.To.Add(new MailAddress(emailPro));
            mensaje.CC.Add(new MailAddress(emailSuc));
            mensaje.From = new MailAddress("sistemaserviexpress@serviexpress.cl", "Serviexpress");
            mensaje.Attachments.Add(new Attachment(respuesta));
            mensaje.Body = "Hola " + nombreProveedor + "\nLe comunicamos que hemos rechazado el pedido con Orden de Pedido N°:"+folio+"," +
                " solicitado a través del Sistema de Serviexpress. El motivo de la devolución es el siguiente:" +
                "\n" +motivo+
                "\nLe hemos adjuntado un documento con mas detalles al respecto" +
                "\nPara su correcta visualización, debe tener instalado Adobe Reader. " +
                "\n\nAtentamente le saluda" +
                "\nSistema de Serviexpress" +
                "\n\n****No conteste a este correo****";

            return mensaje;
        }

        public MailMessage CorreoFinalizacionRequerimiento(string nombreRutaDoc, string correoCliente, string nombreCliente, string folio)
        {
            MailMessage mensaje = new MailMessage();
            mensaje.Subject = "Finalización de Trabajo - SERVIEXPRESS";
            mensaje.To.Add(new MailAddress(correoCliente));
            mensaje.From = new MailAddress("sistemaserviexpress@serviexpress.cl", "Serviexpress");
            mensaje.Attachments.Add(new Attachment(nombreRutaDoc));
            mensaje.Body = "Hola " + nombreCliente + "\nLe comunicamos que hemos finalizado su Orden de Trabajo N°:" + folio + "." +
                "\nLe hemos adjuntado un documento con mas detalles al respecto" +
                "\nPara su correcta visualización, debe tener instalado Adobe Reader. " +
                "\n\nAtentamente le saluda" +
                "\nSistema de Serviexpress" +
                "\n\n****No conteste a este correo****";

            return mensaje;
        }

        public MailMessage CorreoEnvioFactura(string rutaDcoc, string correoCliente, string nombreCliente, string folio)
        {
            MailMessage mensaje = new MailMessage();
            mensaje.Subject = "Comprobante de Pago - SERVIEXPRESS";
            mensaje.To.Add(new MailAddress(correoCliente));
            mensaje.From = new MailAddress("sistemaserviexpress@serviexpress.cl", "Serviexpress");
            mensaje.Attachments.Add(new Attachment(rutaDcoc));
            mensaje.Body = "Hola " + nombreCliente + 
                "\nLe hemos adjuntado una copia de la factura N°"+folio +
                "\nPara su correcta visualización, debe tener instalado Adobe Reader. " +
                "\n\nAtentamente le saluda" +
                "\nSistema de Serviexpress" +
                "\n\n****No conteste a este correo****";

            return mensaje;
        }
    }
}
