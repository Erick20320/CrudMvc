using Crud.BLL.Service.Contracts;
using Crud.DAL.Models;
using Crud.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.BLL.Service
{
    public class EmailService : IEmailService
    {
        private readonly IServicioEmail _emailRepo;

        public EmailService(IServicioEmail emailRepo)
        {
            _emailRepo = emailRepo;
        }
        public async Task SendEmailForEmailConfirmation(UsuarioEmailOpciones usuarioEmailOpciones)
        {
            try
            {
                await _emailRepo.SendEmailForEmailConfirmation(usuarioEmailOpciones);
            }
            catch
            {
                throw;
            }
        }

        public async Task SendEmailForForgotPassword(UsuarioEmailOpciones usuarioEmailOpciones)
        {
            try
            {
                await _emailRepo.SendEmailForForgotPassword(usuarioEmailOpciones);
            }
            catch
            {
                throw;
            }
        }
    }
}
