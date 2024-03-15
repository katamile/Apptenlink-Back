using Apptelink_Back.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Apptelink_Back.Entities
{
    public partial class DetalleFactura
    {
        [Required(ErrorMessage = "El ID del ítem es obligatorio.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El ID del ítem debe ser un número entero mayor a cero.")]
        public int IdItem { get; set; }

        [Required(ErrorMessage = "El ID de la factura es obligatorio.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El ID de la factura debe ser un número entero mayor a cero.")]
        public int IdFactura { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El ID del producto debe ser un número entero mayor a cero.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe ser un número decimal.")]
        public decimal Precio { get; set; }

        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "La cantidad del producto debe ser un número entero mayor a cero.")]
        public int Cantidad { get; set; }

        public decimal? Subtotal { get; set; }

        [Required(ErrorMessage = "El estado no puede ser nulo.")]
        [RegularExpression("^[AI]$", ErrorMessage = $"El campo estado debe ser {Globales.ESTADO_ACTIVO} o {Globales.ESTADO_INACTIVO}.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "La fecha de estado no puede ser nula.")]
        public DateTime? FechaCreacion { get; set; }

        [JsonIgnore]
        public virtual Factura IdFacturaNavigation { get; set; } = null!;

        [JsonIgnore]
        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
