using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.Entidades
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}
