using StockAppSQ20.Dtos.Email;

namespace StockAppSQ20.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto request);
    }
}
