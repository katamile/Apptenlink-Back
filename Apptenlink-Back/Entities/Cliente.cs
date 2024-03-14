using Apptelink_Back.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apptelink_Back.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        [Required(ErrorMessage = "El código de Cliente no puede ser nulo.")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "El código de Cliente debe ser un número entero mayor a cero.")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El número identificación no puede ser nulo.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "El número identificación debe contener 10 dígitos numéricos.")]
        public string Identificacion { get; set; } = null!;

        [Required(ErrorMessage = "El nombre no puede ser nulo.")]
        [StringLength(100, ErrorMessage = "El nombre debe contener máximo 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido no puede ser nulo.")]
        [StringLength(100, ErrorMessage = "El apellido debe contener máximo 100 caracteres.")]
        public string Apellido { get; set; } = null!;

        [StringLength(100, ErrorMessage = "La dirección debe contener máximo 100 caracteres.")]
        public string? Direccion { get; set; }

        [RegularExpression("^[a-zA-Z0-9_!#$%&’*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El estado no puede ser nulo.")]
        [RegularExpression("^[AI]$", ErrorMessage = $"El campo estado debe ser {Globales.ESTADO_ACTIVO} o {Globales.ESTADO_INACTIVO}.")]
        public string Estado { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de creación no puede ser nula.")]
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
