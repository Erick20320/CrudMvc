using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Models
{
    public class ConfirmarEmail
    {
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public bool EmailSent { get; set; }
        public bool EmailVerified { get; set; }
    }
}

