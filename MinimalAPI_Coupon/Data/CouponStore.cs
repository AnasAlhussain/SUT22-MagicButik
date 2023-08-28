using MinimalAPI_Coupon.Models;

namespace MinimalAPI_Coupon.Data
{
    public static class CouponStore
    {
        public static List<Coupon> couponlist = new List<Coupon>
        {
            new Coupon{Id = 1, Name = "10OOF", Precent =10,IsActive = true },
            new Coupon{Id = 2, Name = "20OOF", Precent =20,IsActive = false },
            new Coupon{Id = 3, Name = "30OOF", Precent =30,IsActive = false },
        };
    }
}
