using Apptenlink_Back.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apptenlink_Back.Entities
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        [Required(ErrorMessage = "El código de Producto no puede ser nulo.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El código de Producto debe ser un número entero mayor a cero.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El código de Producto no puede ser nulo.")]
        [StringLength(15, ErrorMessage = "El código de Producto debe contener máximo 15 caracteres.")]
        public string Codigo { get; set; } = null!;

        [Required(ErrorMessage = "El nombre de Producto no puede ser nulo.")]
        [StringLength(100, ErrorMessage = "El nombre de Producto debe contener máximo 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe ser válido.")]
        public decimal Precio { get; set; }

        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El código de Producto debe ser un número entero mayor a cero.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El estado no puede ser nulo.")]
        [RegularExpression("^[AI]$", ErrorMessage = $"El campo estado debe ser {Globales.ESTADO_ACTIVO} o {Globales.ESTADO_INACTIVO}.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "La fecha de estado no puede ser nula.")]
        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
