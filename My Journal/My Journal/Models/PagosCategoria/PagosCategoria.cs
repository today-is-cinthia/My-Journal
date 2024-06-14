using System;
using System.Collections.Generic;
using My_Journal.Models.PagosDetalle;

namespace My_Journal.Models.PagosCategoria;

public partial class PagosCategoria
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Estado { get; set; }

    public int? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModifica { get; set; }

    public DateTime? FechaModifica { get; set; }

    public virtual ICollection<PagosDetalle.PagosDetalle> PagosDetalles { get; set; } = new List<PagosDetalle.PagosDetalle>();

    public virtual Usuario? UsuarioCreacionNavigation { get; set; }
}
