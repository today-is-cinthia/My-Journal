using System;
using System.Collections.Generic;

namespace My_Journal.Models.Divisa;

public partial class Divisa
{
    public int IdDivisa { get; set; }

    public string CodDivisa { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Simbolo { get; set; } = null!;

    public virtual ICollection<DiezmoDetalle> DiezmoDetalles { get; set; } = new List<DiezmoDetalle>();

    public virtual ICollection<EgresosVariosDetalle> EgresosVariosDetalles { get; set; } = new List<EgresosVariosDetalle>();

    public virtual ICollection<IngresosVariosDetalle> IngresosVariosDetalles { get; set; } = new List<IngresosVariosDetalle>();

    public virtual ICollection<PagosDetalle.PagosDetalle> PagosDetalles { get; set; } = new List<PagosDetalle.PagosDetalle>();
}
