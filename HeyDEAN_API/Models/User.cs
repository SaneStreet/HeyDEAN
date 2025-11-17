public class User
{
    public Guid Id {get; set;}
    public string? Username {get; set;}
    public string? PasswordHashed {get; set;}
    public DateTime? CreatedAt {get; set;} = DateTime.UtcNow;

}