using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class ArticuloCreateUpdateDTO
    {

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "La descripción no puede tener más de 200 caracteres.")]
        public string Descripcion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock actual no puede ser negativo.")]
        public int StockActual { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo.")]
        public int StockMinimo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock máximo no puede ser negativo.")]
        public int StockMaximo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Precio { get; set; }

        [MaxLength(100, ErrorMessage = "La marca no puede tener más de 100 caracteres.")]
        public string Marca { get; set; }

        [MaxLength(100, ErrorMessage = "El modelo no puede tener más de 100 caracteres.")]
        public string Modelo { get; set; }

        [MaxLength(4, ErrorMessage = "El año debe tener 4 dígitos.")]
        [RegularExpression(@"\d{4}", ErrorMessage = "El año debe ser un número de 4 dígitos.")]
        public string Anno { get; set; }
    }
}
