using FilmProject.DTO;
using FilmProject.Models;
using Microsoft.AspNetCore.Mvc;
using FilmProject.Services;
using FilmProject.Services.Abstract;
using FilmProject.Services.Concrete;

namespace FilmProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var result = _categoryService.GetAllCategories();

        if (!string.IsNullOrEmpty(result.Error))
        {
            if (result.Error == "CategoryNotFound")
                return NotFound(result);

            return BadRequest(result);
        }

        return Ok(result);
    }


    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        var result = _categoryService.GetCategoryById(id);

        if (!string.IsNullOrEmpty(result.Error))
        {
            if (result.Error == "CategoryNotFound")
                return NotFound(result);

            return BadRequest(result);
        }

        return Ok(result);
    }


    [HttpPost]
    public ActionResult AddCategory(DtoAddCategory categoryDto)
    {
        var category = new Category
        {
            CategoryName = categoryDto.Name
        };

        var result = _categoryService.AddCategory(category);

        if (result.Error != null)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(int id)
    {
        var categoryResult = _categoryService.GetCategoryById(id);
        if (categoryResult?.Data == null)
            return NotFound(categoryResult);

        var deleteResult = _categoryService.DeleteCategory(categoryResult.Data);
        if (!string.IsNullOrEmpty(deleteResult.Error))
            return BadRequest(deleteResult);

        return Ok(deleteResult);
    }
}