using FilmProject.Database;
using FilmProject.Models;
using FilmProject.Services.Abstract;

namespace FilmProject.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public ApiResponse<List<Category>> GetAllCategories()
        {
            try
            {
                var categories = _context.Categories.ToList();

                if (categories == null || !categories.Any())
                {
                    return new ApiResponse<List<Category>>
                    {
                        Error = "CategoryNotFound",
                        Message = "No categories were found.",
                        Data = null
                    };
                }

                return new ApiResponse<List<Category>>
                {
                    Error = null,
                    Message = "Categories retrieved successfully.",
                    Data = categories
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Category>>
                {
                    Error = "ServerError",
                    Message = ex.Message,
                    Data = null

                };
            }
        }


        public ApiResponse<Category> GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return new ApiResponse<Category>
                {
                    Error = "Error",
                    Message = "Category not found.",
                    Data = null
                };
            }

            return new ApiResponse<Category>
            {
                Error = null,
                Message = "Category retrieved successfully.",
                Data = category
            };
        }

        public ApiResponse<Category> AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();

                return new ApiResponse<Category>
                {
                    Error = null,
                    Message = "Category added successfully.",
                    Data = category
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Category>
                {
                    Error = "ErrorAddingCategory",
                    Message = ex.Message,
                    Data = null
                };
            }
        }


        public ApiResponse<string> DeleteCategory(Category category)
        {
            var existCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existCategory == null)
            {
                return new ApiResponse<string>
                {
                    Error = "CategoryNotFound",
                    Message = "Category not found.",
                    Data = null
                };
            }

            _context.Categories.Remove(existCategory);
            _context.SaveChanges();

            return new ApiResponse<string>
            {
                Error = null,
                Message = "Category deleted successfully.",
                Data = "Success"
            };
        }
    }
}