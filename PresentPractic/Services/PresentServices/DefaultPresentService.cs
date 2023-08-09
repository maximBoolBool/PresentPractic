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
    
    public async Task<Present?> AddNewPresentAsync(string? userID,string? presentName,string? presentDescription)
    {
        if (userID == null || presentName == null || presentDescription == null)
            return null ;

        if (!Guid.TryParse(userID, out Guid ID ))
            return null;
        
        await db.Presents.AddAsync(new ()
        {
            PresentName = presentName,
            PresentDescription = presentDescription,
            UserId = ID,
            PresentAddDate = DateTime.UtcNow
        } );
        
        await db.SaveChangesAsync();
        
        var response = await db.Presents
            .Include(present => present.User )
            .FirstOrDefaultAsync(present => present.PresentName.Equals(presentName) && present.User.Id.Equals(userID));
        
        return response ;
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
    
}