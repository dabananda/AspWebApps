using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspWebApps.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(50, ErrorMessage = "Category name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100.")]
        public int DisplayOrder { get; set; }
    }
}
