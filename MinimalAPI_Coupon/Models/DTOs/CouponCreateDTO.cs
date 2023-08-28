namespace MinimalAPI_Coupon.Models.DTOs
{
    public class CouponCreateDTO
    {
        public string Name { get; set; }
        public int Precent { get; set; }

        public bool IsActive { get; set; }
    }
}
