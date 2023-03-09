namespace BLL.Models.Click;

public class ClickCompleteBllResponseModel
{
    /// ID платежа в системе CLICK
    public long ClickTransId;

    /// ID заказа(для Интернет магазинов)/лицевого счета/логина в биллинге поставщика
    public string MerchantTransId;

    /// ID транзакции завершения платежа в биллинг системе (может быть NULL)
    public int MerchantConfirmId;

    /// Код статуса запроса. 0 – успешно. В случае ошибки возвращается код ошибки.
    public int Error;

    /// Описание кода завершения платежа.
    public string ErrorNote;
}