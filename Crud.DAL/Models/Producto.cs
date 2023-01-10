using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crud.DAL.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public double? Precio { get; set; }

    public DateTime? FechaIngreso { get; set; }
}
