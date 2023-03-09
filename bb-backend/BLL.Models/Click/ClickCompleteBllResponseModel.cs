namespace BLL.Models.Click;

public class ClickCompleteBllResponseModel
{
    /// ID платежа в системе CLICK
    public long ClickTransId { get; set; }

    /// ID заказа(для Интернет магазинов)/лицевого счета/логина в биллинге поставщика
    public string MerchantTransId { get; set; }

    /// ID транзакции завершения платежа в биллинг системе (может быть NULL)
    public int MerchantConfirmId { get; set; }

    /// Код статуса запроса. 0 – успешно. В случае ошибки возвращается код ошибки.
    public int Error { get; set; }

    /// Описание кода завершения платежа.
    public string ErrorNote { get; set; }

    public static ClickCompleteBllResponseModel FromError(int error)
    {
        return new ClickCompleteBllResponseModel()
        {
            Error = error
        };
    }
}