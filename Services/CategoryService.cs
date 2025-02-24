using FilmProject.Models;
using FilmProject.Database;
using Microsoft.Extensions.Logging.Abstractions;

namespace FilmProject.Services
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

        public bool DeleteCategory(Category category)
        {
            var categoryCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}