using BookStoreWeb.Models.Domain;
using BookStoreWeb.Repositories.Abstract;

namespace BookStoreWeb.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext context;

        public BookService(DatabaseContext context)
        {
            this.context = context;
        }
        
        public bool Add(Book model)
        {
            try
            {
                context.Books.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                {
                    return false;
                }
                else
                {
                    context.Remove(data);
                    context.SaveChanges();
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Book FindById(int id)
        {
            return context.Books.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            var data = (from book in context.Books
                        join Author in context.Authors on book.AuthorId equals Author.Id
                        join Publisher in context.Publishers on book.PublisherId equals Publisher.Id
                        join Genre in context.Genres on book.GenreId equals Genre.Id
                        select new Book
                        {
                            Id = book.Id,
                            AuthorId = book.AuthorId,
                            GenreId = book.GenreId,
                            Isbn = book.Isbn,
                            PublisherId = book.PublisherId,
                            Title = book.Title,
                            TotalPages = book.TotalPages,
                            GenreName = Genre.Name,
                            AuthorName = Author.AuthorName,
                            PublisherName = Publisher.PublisherName
                        }).ToList();
            return data;
        }

        public bool Update(Book model)
        {
            try
            {
                context.Books.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
