using System.ComponentModel.DataAnnotations;

namespace ClaremontBarAPI.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Product name is required", AllowEmptyStrings = false)]
        public string ProductName { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }

        public string ProductCategory { get; set; }

        public string Spirit { get; set; }

        [Required(ErrorMessage = "Price is required", AllowEmptyStrings = false)]
        public decimal Price { get; set; }
    }
}