﻿using System;
using System.Collections.Generic;

namespace My_Journal.Models.OfrendaCategoria;

public class OfrendasCategoria
{
    public int IdCatOfrenda { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Estado { get; set; }

    public int? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModifica { get; set; }

    public DateTime? FechaModifica { get; set; }

    public virtual Usuario? UsuarioCreacionNavigation { get; set; }
}
