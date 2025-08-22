namespace NorthwindRestApi.Models
{
    public class LoggedUser
    {
        public string Username { get; set; } = null!;
        public int AccessLevelId { get; set; }
        public string? Token { get; set; }
    }
}
