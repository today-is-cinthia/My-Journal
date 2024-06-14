using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace My_Journal.Models.Ofrenda;

public class Ofrenda
{
    public int IdOfrenda { get; set; }

    public int IdCatOfrenda { get; set; }

    /*[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(strings))]*/
    public double Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public DateTime Fecha { get; set; }

    public int Divisa { get; set; }

    public double TasaCambio { get; set; }

    public int? UsuarioCreacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? UsuarioModifica { get; set; }

    public DateTime? FechaModifica { get; set; }

    public int Estado { get; set; }

    public virtual Usuario? UsuarioCreacionNavigation { get; set; }

}

public class OfrendaViewModel
{
    public Ofrenda Ofrenda { get; set; }

    public Divisa.Divisa Divisa { get; set; }

    public OfrendaCategoria.OfrendasCategoria OfrendaCategoria { get; set; }
}
