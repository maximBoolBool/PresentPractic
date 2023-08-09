using System.ComponentModel.DataAnnotations.Schema;

namespace PresentPractic.Models.DbModels;

[Table("presents")]
public class Present
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("present_name")]
    public string PresentName { get; set; }
    
    [Column("present_description")]
    public string PresentDescription { get; set; }
    
    [Column("present_status")]
    public bool Status { get; set; }
    
    [Column("User")]
    [ForeignKey(nameof(Present.User))]
    public Guid UserId { get; set; }
    
    [Column("present_add_date")]
    public DateTime PresentAddDate { get; set; }
    
    public User User { get; set; }
    
}