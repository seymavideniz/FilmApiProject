using FilmProject.Models;

namespace FilmProject.Services.Abstract;

public interface ICategoryService
{
    RetApi<List<Category>> GetAllCategories();
    RetApi<Category> GetCategoryById(int id);
    RetApi<Category> AddCategory(Category category);
    RetApi<string> DeleteCategory(Category category);
}