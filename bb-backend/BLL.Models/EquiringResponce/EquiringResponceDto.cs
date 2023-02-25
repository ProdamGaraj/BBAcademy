namespace BLL.Models.EquiringResponce;

public class EquiringResponceDto
{
    public long ClickTransId { get; set; }
    public string MerchantTransId {get;set;}		
    public int MerchantConfirmId {get;set;}	
    public int Error {get;set;}	
    public string ErrorNote {get;set;}	
}