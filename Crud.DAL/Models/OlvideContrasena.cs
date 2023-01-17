using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Models
{
    public class OlvideContrasena
    {
        [Required, EmailAddress, Display(Name = "Dirección de correo electrónico registrado")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
