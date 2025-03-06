using FilmProject.Database;
using FilmProject.Models;

namespace FilmProject.Services.Concrete
{
    public class CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            if (categories == null)
            {
                return null;
            }

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return null;
            }

            return category;
        }

        public Category AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void DeleteCategory(Category category)
        {
            var existCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existCategory == null)
            {
                return;
            }

            _context.Categories.Remove(existCategory);
            _context.SaveChanges();
        }
    }
}