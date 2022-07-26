using UltraPlay.Application.Interfaces;

namespace UltraPlay.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}