using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BBCServiexpress.NEG
{
    public class ServerCorreo
    {
        public SmtpClient InstanciaServer()
        {
            try
            {
                string ser = "smtp.gmail.com";
                int port = 587;
                string mai ="sistema.serviexpress@gmail.com";
                string pas = "portafolio2018";
                //parametriza el servidos STMP para enviar el correo
                SmtpClient server = new SmtpClient(ser, port);
                server.Credentials = new System.Net.NetworkCredential(mai, pas);
                //server.UseDefaultCredentials = false;
                server.EnableSsl = true;
                return server;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
