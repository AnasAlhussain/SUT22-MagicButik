using MinimalAPI_Coupon.Models.DTOs;

namespace Web_Coupon.Services
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;
        public CouponService(IHttpClientFactory clientFactory) :base(clientFactory) 
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateCouponAsync<T>(CouponDTO couponDTO)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = couponDTO,
                Url = StaticDetails.CouponApiBase + "/api/coupon",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteCouponAsync<T>(int id)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.CouponApiBase + "/api/coupon/" + id,
                AccessToken = ""
            });
        }

        public Task<T> GetAllCoupon<T>()
        {
            return this.SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponApiBase + "/api/coupons",
                AccessToken = ""
            });
        }

        public async Task<T> GetCouponById<T>(int id)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponApiBase + "/api/coupon/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateCouponAsync<T>(CouponDTO couponDTO)
        {
            return await this.SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = couponDTO,
                Url = StaticDetails.CouponApiBase + "/api/coupon",
                AccessToken = ""
            });
        }
    }
}
