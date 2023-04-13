namespace FreelanceNFControl.Core.Responses.Login
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoUrl { get; set; }
    }
}
