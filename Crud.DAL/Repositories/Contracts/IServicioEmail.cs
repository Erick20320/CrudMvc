using Crud.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Repositories.Contracts
{
    public interface IServicioEmail
    {
        Task SendEmailForEmailConfirmation(UsuarioEmailOpciones usuarioEmailOpciones);
        Task SendEmailForForgotPassword(UsuarioEmailOpciones usuarioEmailOpciones);
    }
}
