using System;
using System.Collections.Generic;
using My_Journal.Models.Ofrenda;
using My_Journal.Models.OfrendaCategoria;

namespace My_Journal;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public bool Estado { get; set; }

    public int? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModifica { get; set; }

    public DateTime? FechaModifica { get; set; }

    public virtual ICollection<DiezmoDetalle> DiezmoDetalles { get; set; } = new List<DiezmoDetalle>();

    public virtual ICollection<Diezmo> Diezmos { get; set; } = new List<Diezmo>();

    public virtual ICollection<EgresosVario> EgresosVarios { get; set; } = new List<EgresosVario>();

    public virtual ICollection<EgresosVariosDetalle> EgresosVariosDetalles { get; set; } = new List<EgresosVariosDetalle>();

    public virtual ICollection<IngresosVario> IngresosVarios { get; set; } = new List<IngresosVario>();

    public virtual ICollection<IngresosVariosDetalle> IngresosVariosDetalles { get; set; } = new List<IngresosVariosDetalle>();

    public virtual ICollection<Miembro> Miembros { get; set; } = new List<Miembro>();

    public virtual ICollection<Ofrenda> Ofrenda { get; set; } = new List<Ofrenda>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<PagosCategoria> PagosCategoria { get; set; } = new List<PagosCategoria>();

    public virtual ICollection<PagosDetalle> PagosDetalles { get; set; } = new List<PagosDetalle>();

    public virtual ICollection<OfrendasCategoria> OfrendasCategorias { get; set; } = new List<OfrendasCategoria>();
}
