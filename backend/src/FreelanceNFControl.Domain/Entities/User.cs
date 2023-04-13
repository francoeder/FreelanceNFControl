using Microsoft.AspNetCore.Identity;

namespace FreelanceNFControl.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhotoUrl { get; set; }

        public User(string firstName, string lastName, string userName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
        }
    }
}
