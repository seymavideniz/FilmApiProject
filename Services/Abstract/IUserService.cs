using FilmProject.DTO;
using FilmProject.Models;

namespace FilmProject.Services.Abstract;

public interface IUserService
{
    Task<RetApi<string>> SingUpAsync(DtoSignUp dtoSignUp);
    Task<RetApi<string>> SignInAsync(DtoSignIn dtoSignIn);
    Task<RetApi<string>> UpdateUserAsync(Guid userId, DtoUpdateUser dtoUpdateUser);
}