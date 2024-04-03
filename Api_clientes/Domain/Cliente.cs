using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Api_clientes.Domain
{
    public class Cliente
    {
        [Key]
        public int id_cli { get; set; }
        public string? nom_cli { get; set; }
        public string? ruc_cli { get; set; }
        public string? gia { get; set; }
        public string? correo { get; set; }
        public bool estado_cli { get; set; }
    }
}
