using System.IdentityModel.Tokens.Jwt;
using PresentPractic.Models.DbModels;

namespace PresentPractic.Services;

public interface IUserServices
{
    public Task<bool> AddNewUserAsync(string? newUserLogin,string? newUserPassword);
    public Task<User?> AuthorizeAsync(string? userLogin,string? userPassword);
    
    public Task<bool> ChangeUserPasswordAsync(string? userLogin , string? userOldPassword , string? userNewLogin );
    public Task<bool> ChangeUserLoginAsync(string? userOldlogin,string? userNewLogin , string? userPassword );

    public Task<User?> GetChoosenUserAsync(string? userLogin);


    public Task<string?> GenerateTokenAsync(string? login,string? password);


}