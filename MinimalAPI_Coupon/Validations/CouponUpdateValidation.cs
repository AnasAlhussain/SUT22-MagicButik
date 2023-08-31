using FluentValidation;
using MinimalAPI_Coupon.Models.DTOs;

namespace MinimalAPI_Coupon.Validations
{
    public class CouponUpdateValidation : AbstractValidator<CouponUpdateDTO>
    {
        public CouponUpdateValidation()
        {
            RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Precent).InclusiveBetween(1, 100);
        }
    }
}
