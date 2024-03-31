using StockAppSQ20.Model;

namespace StockAppSQ20.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}