using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWeb.Models.Domain
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public int TotalPages { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public int GenreId { get; set; }
        [NotMapped]
        public string ? AuthorName { get; set; }
        [NotMapped]
        public string ? PublisherName { get; set; }
        [NotMapped]
        public string ? GenreName { get; set; }

        [NotMapped]
        public List<SelectListItem> ? AuthorList { get; set; }
        [NotMapped]
        public List<SelectListItem> ? GenreList { get; set; }
        [NotMapped]
        public List<SelectListItem> ? PublisherList { get; set; }
    }
}
