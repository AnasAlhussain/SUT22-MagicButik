using Newtonsoft.Json;
using System.Text;
using Web_Coupon.Models;

namespace Web_Coupon.Services
{
    public class BaseService : IBaseService
    {
        public ResponsDto resopnsModel { get ; set ; }
        public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
                this._httpClient = httpClient;
                this.resopnsModel = new ResponsDto();
        }



        

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {

                var client = _httpClient.CreateClient("SUT22CouponAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8,"application/json");
                }

                HttpResponseMessage apiResp = null;
                switch (apiRequest.ApiType)
                {
                    case StaticDetails.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                
                }
                apiResp = await client.SendAsync(message);

                var apiContent = await apiResp.Content.ReadAsStringAsync();
                var apiResonsDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResonsDto;
            }

            catch (Exception e)
            {

                var dto = new ResponsDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                    
                };

                var res = JsonConvert.SerializeObject(dto);
                var apiResponsDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponsDto;
            }


        }



        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
