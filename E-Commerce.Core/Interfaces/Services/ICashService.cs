namespace E_Commerce.Core.Interfaces.Services
{
    public interface ICashService
    {
        Task SetCashResponseAsync(string key, object response, TimeSpan time);
        Task<string?> GetCashResponseAsync(string key);

    }
}
