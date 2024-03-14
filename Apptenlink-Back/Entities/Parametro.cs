using System;
using System.Collections.Generic;

namespace Apptenlink_Back.Entities
{
    public partial class Parametro
    {
        public int IdParam { get; set; }
        public string Estado { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
