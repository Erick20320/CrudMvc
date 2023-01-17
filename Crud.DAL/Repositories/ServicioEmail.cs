using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Crud.DAL.Repositories.Contracts;
using Crud.DAL.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace Crud.DAL.Repositories
{
    public class ServicioEmail : IServicioEmail
    {
        private const string templatePath = @"Views/Autenticacion//{0}.html";
        private readonly SMTPConfig _smtpConfig;

        public async Task SendEmailForEmailConfirmation(UsuarioEmailOpciones usuarioEmailOpciones)
        {
            usuarioEmailOpciones.Subject = UpdatePlaceHolders("Hola {{UserName}}, Confirme su identificación de correo electrónico.", usuarioEmailOpciones.PlaceHolders);

            usuarioEmailOpciones.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"), usuarioEmailOpciones.PlaceHolders);

            await SendEmail(usuarioEmailOpciones);
        }

        public async Task SendEmailForForgotPassword(UsuarioEmailOpciones usuarioEmailOpciones)
        {
            usuarioEmailOpciones.Subject = UpdatePlaceHolders("Hola {{UserName}}, Restablecer su contraseña.", usuarioEmailOpciones.PlaceHolders);

            usuarioEmailOpciones.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), usuarioEmailOpciones.PlaceHolders);

            await SendEmail(usuarioEmailOpciones);
        }

        public ServicioEmail(IOptions<SMTPConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        private async Task SendEmail(UsuarioEmailOpciones usuarioEmailOpciones)
        {
            MailMessage mail = new MailMessage
            {
                Subject = usuarioEmailOpciones.Subject,
                Body = usuarioEmailOpciones.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in usuarioEmailOpciones.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }
    }
}
