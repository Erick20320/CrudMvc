using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.DAL.Models;
[Keyless]
public partial class Usuario : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
