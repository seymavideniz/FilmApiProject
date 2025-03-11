using FilmProject.Models;
namespace FilmProject.Services.Abstract;

public interface ICategoryService
{
    List<Category> GetAllCategories();
    Category GetCategoryById(int id);
    Category AddCategory(Category category);
    void DeleteCategory(Category category);
}