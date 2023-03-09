using System.Text.Json.Serialization;

namespace BLL.Models.Click;

public class ClickPrepareBllModel
{
    /// Номер транзакции (итерации) в системе CLICK, т.е. попытки провести платеж.
    public long ClickTransId { get; set; }

    /// ID сервиса
    public int ServiceId { get; set; }

    /// Номер платежа в системе CLICK. Отображается в СМС у клиента при оплате.
    public long ClickPaydocId { get; set; }

    /// ID заказа(для Интернет магазинов)/лицевого счета/логина в биллинге поставщика
    public string MerchantTransId { get; set; }

    /// Сумма оплаты (в сумах)
    public float Amount { get; set; }

    /// Выполняемое действие. Для Prepare — 0
    public int Action { get; set; }

    /// Код статуса завершения платежа. 0 – успешно. В случае ошибки возвращается код ошибки.
    public int Error { get; set; }

    /// Описание кода завершения платежа.
    public string ErrorNote { get; set; }

    /// Дата платежа. Формат «YYYY-MM-DD HH:mm:ss»
    public string SignTime { get; set; }

    /// Проверочная строка, подтверждающая подлинность отправляемого запроса. ХЭШ MD5 из следующих параметров: 
    /// md5( click_trans_id + service_id + SECRET_KEY* + merchant_trans_id + amount + action + sign_time)
    /// SECRET_KEY – уникальная строка, выдаваемая Поставщику при подключении.
    public string SignString { get; set; }
}