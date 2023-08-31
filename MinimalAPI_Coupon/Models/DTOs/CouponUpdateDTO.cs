namespace MinimalAPI_Coupon.Models.DTOs
{
    public class CouponUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Precent { get; set; }
        public bool IsActive { get; set; }
    }
}
