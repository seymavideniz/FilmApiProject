using FilmProject.Models;

namespace FilmProject.Services.Abstract;

public interface ICategoryService
{
    ApiResponse<List<Category>> GetAllCategories();
    ApiResponse<Category> GetCategoryById(int id);
    ApiResponse<Category> AddCategory(Category category);
    ApiResponse<string> DeleteCategory(Category category);
}