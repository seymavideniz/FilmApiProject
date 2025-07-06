using FilmProject.DTO;

namespace FilmProject.Services.Abstract;

public interface IUserService
{
    Task<string> SingUpAsync(DtoSignUp dtoSignUp);
    Task<string> SignInAsync(DtoSignIn dtoSignIn);
    Task<string> UpdateUserAsync(Guid userId, DtoUpdateUser dtoUpdateUser);
}