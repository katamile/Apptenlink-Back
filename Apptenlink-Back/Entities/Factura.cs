using Apptelink_Back.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apptelink_Back.Entities
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        [Required(ErrorMessage = "El ID de la factura es obligatorio.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El ID de la factura debe ser un número entero mayor a cero.")]
        public int IdFactura { get; set; }

        [Required(ErrorMessage = "El número de factura es obligatorio.")]
        public string NumeroFactura { get; set; } = null!;

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El ID del cliente debe ser un número entero mayor a cero.")]
        public int IdCliente { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? PorcentajeIgv { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }

        [Required(ErrorMessage = "El estado no puede ser nulo.")]
        [RegularExpression("^[AI]$", ErrorMessage = $"El campo estado debe ser {Globales.ESTADO_ACTIVO} o {Globales.ESTADO_INACTIVO}.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "La fecha de estado no puede ser nula.")]
        public DateTime FechaCreacion { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
