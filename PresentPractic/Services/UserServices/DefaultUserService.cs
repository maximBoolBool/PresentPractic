using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PresentPractic.Models.DbModels;

namespace PresentPractic.Services;

public class DefaultUserService : IUserServices
{
    private  ApplicationContext db;

    public DefaultUserService(ApplicationContext _db)
    {
        db = _db;
    }
    
    public async Task<bool> AddNewUserAsync(string? newUserLogin,string? newUserPassword)
    {
        if (newUserPassword == null || newUserLogin == null)
        {
            return false;
        }

        var userCheck = await db.Users.FirstOrDefaultAsync(user => user.Login.Equals(newUserPassword));

        if (userCheck != null)
            return false;

        await db.Users.AddAsync(new ()
        {
            Login = newUserLogin,
            Password = newUserPassword,
            IsDelete = false,
            userCreatedate = DateTime.UtcNow
        });
        await db.SaveChangesAsync();
        
        return true;
    }

    public async Task<User?> AuthorizeAsync(string? userLogin , string? userPassword)
    {
        if (userLogin == null || userPassword == null)
            return null;

        var response = await db.Users
            .Include(user => user.Presents.Where(user => !user.IsDelete))
            .FirstOrDefaultAsync(user => user.Login.Equals(userLogin) && userPassword.Equals(userPassword) && !user.IsDelete );
        
        return response;
    }
    
    public async Task<bool> ChangeUserPasswordAsync(string? userLogin,string? userOldPassword,string? userNewPassword)
    {
        if (userLogin == null || userOldPassword == null || userNewPassword == null )
        {
            return false;
        }
        
        var count = await db.Database.ExecuteSqlRawAsync("update users set password={0} where(login = {1} and password = {2})",userNewPassword,userLogin,userOldPassword);

        
        return (count == 1) ? true : false;
    }

    public async Task<bool> ChangeUserLoginAsync(string? userOldlogin,string? userNewLogin , string? userPassword )
    {
        if (userOldlogin == null || userNewLogin == null || userPassword == null)
        {
            return false;
        }

        var checkNewLogin = await db.Users.FirstOrDefaultAsync(user => user.Login == userNewLogin);

        if (checkNewLogin != null)
            return false;


        var count = await db.Database.ExecuteSqlRawAsync("update users set login={0} where( login = {1} and password = {2})",userNewLogin,userOldlogin,userPassword);

        return (count == 1) ? true : false;
    }

    public async Task<User?> GetChoosenUserAsync(string? userLogin)
    {
        if (userLogin == null)
            return null;

        var user = await db.Users
            .Include(user => user.Presents.Where(present => !present.IsDelete))
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<string?> GenerateTokenAsync(string? login,string? password )
    {
        var claims = new List<Claim>()
        {
            new ("login",login),
            new ("password",password)
        };

        var jwt = new JwtSecurityToken(
            issuer:AuthOptions.AuthOptions.ISSUER,
            audience:AuthOptions.AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(3)),
            signingCredentials:new SigningCredentials(AuthOptions.AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);

    }
    
    
    
    
}