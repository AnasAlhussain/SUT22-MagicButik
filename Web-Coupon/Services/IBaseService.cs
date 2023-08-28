using Web_Coupon.Models;

namespace Web_Coupon.Services
{
    public interface IBaseService:IDisposable
    {
        ResponsDto resopnsModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
