namespace BLL.Models.CreatePayment;

public class CreatePaymentResponse
{
    public int MerchantId { get; set; }
    public long MerchantUserId { get; set; }
    public int ServiceId { get; set; }
    public string TransId { get; set; }
    public float TransAmount { get; set; }
    public string ReturnUrl { get; set; }
}