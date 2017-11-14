namespace SuperHeroCatalogue.Application.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int IdRole { get; set; }
    }
}