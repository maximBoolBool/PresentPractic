using System.ComponentModel.DataAnnotations.Schema;

namespace PresentPractic.Models.DbModels;

[Table("users")]
public class User
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("login")]
    public string Login { get; set; }
    
    [Column("password")]
    public string Password { get; set; }     
    
    [Column("is_delete")]
    public bool IsDelete { get; set; }
    
    
    [Column("create_date_time")]
    public DateTime userCreatedate { get; set; }
    
    public List<Present> Presents { get; set; }

}