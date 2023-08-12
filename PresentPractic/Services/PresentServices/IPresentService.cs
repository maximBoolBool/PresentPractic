using PresentPractic.Models.DbModels;

namespace PresentPractic.Services;

public interface IPresentService
{
    public Task<Present?> AddNewPresentAsync(string? userLogin ,string? presentName,string? presentDescription);

    public Task<bool> ChangePresentStatusAsync(string? PresentId);

    public Task<bool> DeletePresentAsync(string? presentId);

}