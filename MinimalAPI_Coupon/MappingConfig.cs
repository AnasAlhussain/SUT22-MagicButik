using AutoMapper;
using MinimalAPI_Coupon.Models;
using MinimalAPI_Coupon.Models.DTOs;

namespace MinimalAPI_Coupon
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        
        {

            CreateMap<Coupon, CouponCreateDTO>().ReverseMap();
            CreateMap<Coupon, CouponDTO>().ReverseMap();
            CreateMap<Coupon, CouponUpdateDTO>().ReverseMap();
        }
    }
}
