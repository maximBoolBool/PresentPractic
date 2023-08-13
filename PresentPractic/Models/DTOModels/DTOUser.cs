namespace PresentPractic.Models.DTOModels;

public class DTOUser
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public List<DTOPresent> Presents { get; set; }
}