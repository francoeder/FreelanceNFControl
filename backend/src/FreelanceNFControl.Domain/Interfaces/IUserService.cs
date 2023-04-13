using FreelanceNFControl.Core.Responses.Login;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Infra.Core.Requests.User;

namespace FreelanceNFControl.Domain.Interfaces
{
    public interface IUserService
    {
        Task Add(User entity, string password);
        Task<LoginResponse> AuthenticateUser(LoginRequest request);
        Task<LoginResponse> AuthenticateUser(User user, string password);
    }
}
