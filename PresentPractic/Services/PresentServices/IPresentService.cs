using PresentPractic.Models.DbModels;

namespace PresentPractic.Services;

public interface IPresentService
{
    public Task<Present?> AddNewPresentAsync(string? userID ,string? presentName,string? presentDescription);

    public Task<bool> ChangePresentStatusAsync(string? PresentId);
}