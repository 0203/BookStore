using BookStoreWeb.Models.Domain;
using BookStoreWeb.Repositories.Abstract;

namespace BookStoreWeb.Repositories.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly DatabaseContext context;

        public AuthorService(DatabaseContext context)
        {
            this.context = context;
        }
        
        public bool Add(Author model)
        {
            try
            {
                context.Authors.Add(model);
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

        public Author FindById(int id)
        {
            return context.Authors.Find(id);
        }

        public IEnumerable<Author> GetAll()
        {
            return context.Authors.ToList();
        }

        public bool Update(Author model)
        {
            try
            {
                context.Authors.Update(model);
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
