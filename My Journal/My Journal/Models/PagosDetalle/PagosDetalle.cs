using System;
using System.Collections.Generic;
using My_Journal.Models.Divisa;
using My_Journal.Models.Pagos;

namespace My_Journal.Models.PagosDetalle;

public partial class PagosDetalle
{
    public int IdDetalle { get; set; }

    public int IdPago { get; set; }

    public int IdCategoria { get; set; }

    public double Cantidad { get; set; }

    public int? Divisa { get; set; }

    public double? TasaCambio { get; set; }

    public int? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModifica { get; set; }

    public DateTime? FechaModifica { get; set; }

    public virtual Divisa.Divisa? DivisaNavigation { get; set; }

    public virtual PagosCategoria.PagosCategoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Pago IdPagoNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioCreacionNavigation { get; set; }
}
