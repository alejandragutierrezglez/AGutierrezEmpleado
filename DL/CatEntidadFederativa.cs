using System;
using System.Collections.Generic;

namespace DL;

public partial class CatEntidadFederativa
{
    public int IdEstado { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
