using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Models.Domain
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PublisherName { get; set; }
    }
}
