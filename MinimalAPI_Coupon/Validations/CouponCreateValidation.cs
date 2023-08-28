using FluentValidation;
using MinimalAPI_Coupon.Models.DTOs;

namespace MinimalAPI_Coupon.Validations
{
    public class CouponCreateValidation:AbstractValidator<CouponCreateDTO>
    {
        public CouponCreateValidation() 
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Precent).InclusiveBetween(1, 100);
        
        
        }
    }
}
