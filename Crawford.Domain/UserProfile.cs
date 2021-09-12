namespace Crawford.Domain
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool? Active { get; set; }
    }
}
