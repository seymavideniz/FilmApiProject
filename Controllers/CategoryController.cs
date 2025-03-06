using FilmProject.DTO;
using FilmProject.Models;
using Microsoft.AspNetCore.Mvc;
using FilmProject.Services;
using FilmProject.Services.Concrete;

namespace FilmProject.Controllers;

[ApiController]
[Route("api/[controller]")]


public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = _categoryService.GetAllCategories();
        var categoryDtos = categories.Select(c => new DtoCategory
        {
            Id = c.CategoryId,
            Name = c.CategoryName,
        }).ToList();

        return Ok(categoryDtos);
    }

    [HttpGet("{id}")]
    public ActionResult GetCategory(int id)
    {
        var category = _categoryService.GetCategoryById(id);

        var categoryDto = new DtoCategory
        {
            Id = category.CategoryId,
            Name = category.CategoryName
        };

        return Ok(categoryDto);
    }

    [HttpPost]
    public ActionResult AddCategory(DtoCategory categoryDto)
    { 
        var category = new Category
        {
            CategoryName = categoryDto.Name
        };

        _categoryService.AddCategory(category);
        return Ok("Category added successfully");
    }

    [HttpDelete]
    public ActionResult DeleteCategory(int id)
    {
        var category = _categoryService.GetCategoryById(id);
        _categoryService.DeleteCategory(category);
        {
            return Ok("Category deleted successfully");
        } 
    }
}