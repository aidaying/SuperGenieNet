using Genie.Core.Entities.Identity;

namespace Genie.Core.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
