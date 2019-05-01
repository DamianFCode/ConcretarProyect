using System;
using System.Net.Mail;
using RestSharp;
using RestSharp.Authenticators;

namespace Concretar.Helper
{
    public static class EmailHelper
    {
        
        /* ENVIO DE EMAILS */
        public static void SendMail(MailMessage mail)
        {
            string metodoEnvio = Parametro.GetValue("MetodoEnvio");

            switch (metodoEnvio)
            {
                case "SMTP":
                    SendMailSMTP(mail);
                    break;
                case "Mailgun":
                    SendMailMailgun(mail);
                    break;
            }
        }

        private static void SendMailSMTP(MailMessage mail)
        {
            try
            {
                //Obtengo valores de tabla parámetro:

                string servidor = Parametro.GetValue("Servidor");
                bool enableSsl = Parametro.GetValue("EnableSsl") == "1" ? true : false;
                int port = Convert.ToInt32(Parametro.GetValue("Puerto"));
                string usr = Parametro.GetValue("Usuario");
                string psw = Parametro.GetValue("Contraseña");

                SmtpClient cliente = new SmtpClient(servidor)
                {
                    EnableSsl = enableSsl,
                    Port = port,
                    Credentials = new System.Net.NetworkCredential(usr, psw)
                };
                cliente.Send(mail);
            }
            catch
            {
                throw;
            }
        }
        public static IRestResponse SendMailMailgun(MailMessage mail)
        {
            string apiKey = Parametro.GetValue("MailGunApiKey");
            string mailGunDominio = Parametro.GetValue("MailGunDominio");
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri(Parametro.GetValue("UrlApiMailgun"));
                client.Authenticator = new HttpBasicAuthenticator("api", apiKey);
                RestRequest request = new RestRequest();
                request.AddParameter("domain", mailGunDominio, ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Qualia <"+ mail.From.Address+">");
                request.AddParameter("to", mail.To.ToString());
                request.AddParameter("subject", mail.Subject);
                request.AddParameter("html", mail.Body);
                request.Method = Method.POST;
                var send = client.Execute(request);
                return send;
            }
            catch
            {
                throw;
            }
        }
    }
}
