using Microsoft.EntityFrameworkCore;
using PresentPractic.Models.DbModels;

namespace PresentPractic.Services;

public class DefaultPresentService : IPresentService
{

    private ApplicationContext db;

    public DefaultPresentService(ApplicationContext _db)
    {
        db = _db;
    }
    
    public async Task<Present?> AddNewPresentAsync(string? userLogin,string? presentName,string? presentDescription)
    {
        if (userLogin == null || presentName == null || presentDescription == null)
            return null ;

        var newPresent = new Present()
        {
            Id = Guid.NewGuid(),
            PresentName = presentName,
            PresentDescription = presentDescription,
            UserId = await db.Users.Where(user => user.Login.Equals(userLogin)).Select(user => user.Id)
                .FirstOrDefaultAsync(),
            PresentAddDate = DateTime.UtcNow
        };
        
        await db.Presents.AddAsync(newPresent);
        await db.SaveChangesAsync();
        return newPresent ;
    }
    
    public async Task<bool> ChangePresentStatusAsync(string? PresentId)
    {

        if (PresentId == null)
            return false;

        if (!Guid.TryParse(PresentId, out Guid ID ))
            return false;
        
        
        var choosenPresent = await db.Presents.FirstOrDefaultAsync(present => present.Id.Equals(ID));


        if (choosenPresent == null)
            return false;
        
        choosenPresent.Status = true;
        db.Presents.Update(choosenPresent);
        await db.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeletePresentAsync(string? presentId)
    {
        if (!Guid.TryParse(presentId, out Guid presentGuid))
            return false;
        
        var present = await db.Presents.FirstOrDefaultAsync(present => present.Id.Equals(presentGuid));

        if (present is null)
            return false;

        present.IsDelete = true;

        db.Presents.Update(present);
        await db.SaveChangesAsync();
        return true;
    }
}