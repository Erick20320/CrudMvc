using Crud.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.BLL.Service.Contracts
{
    public interface IEmailService
    {
        Task SendEmailForEmailConfirmation(UsuarioEmailOpciones usuarioEmailOpciones);
        Task SendEmailForForgotPassword(UsuarioEmailOpciones usuarioEmailOpciones);
    }
}
