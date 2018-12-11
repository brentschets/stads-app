namespace RESTAPI.Utils
{
    public class RegisterStoreDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}