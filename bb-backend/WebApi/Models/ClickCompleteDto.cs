using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models;

public class ClickCompleteDto
{
    /// ID платежа в системе CLICK
    [BindProperty(Name = "click_trans_id")]
    public long ClickTransId { get; set; }

    /// ID сервиса
    [BindProperty(Name = "service_id")]
    public int ServiceId { get; set; }

    /// Номер платежа в системе CLICK. Отображается в СМС у клиента при оплате.
    [BindProperty(Name = "click_paydoc_id")]
    public long ClickPaydocId { get; set; }

    /// ID заказа(для Интернет магазинов)/лицевого счета/логина в биллинге поставщика
    [BindProperty(Name = "merchant_trans_id")]
    public string MerchantTransId { get; set; }

    /// ID платежа в биллинг системе поставщика для подтверждения, полученный при запросе «Prepare»
    [BindProperty(Name = "merchant_prepare_id")]
    public int MerchantPrepareId { get; set; }

    /// Сумма оплаты (в сумах)
    [BindProperty(Name = "amount")]
    public float Amount { get; set; }

    /// Выполняемое действие. Для Complete — 1
    [BindProperty(Name = "action")]
    public int Action { get; set; }

    /// Код статуса завершения платежа. 0 – успешно. В случае ошибки возвращается код ошибки.
    [BindProperty(Name = "error")]
    public int Error { get; set; }

    /// Описание кода завершения платежа.
    [BindProperty(Name = "error_note")]
    public string ErrorNote { get; set; }

    /// Дата платежа. Формат «YYYY-MM-DD HH:mm:ss»
    [BindProperty(Name = "sign_time")]
    public string SignTime { get; set; }

    /// Проверочная строка, подтверждающая подлинность отправляемого запроса. ХЭШ MD5 из следующих параметров:
    /// md5( click_trans_id + service_id + SECRET_KEY* + merchant_trans_id + merchant_prepare_id + amount + action + sign_time )
    /// SECRET_KEY – уникальная строка, выдаваемая Поставщику при подключении.
    [BindProperty(Name = "sign_string")]
    public string SignString { get; set; }
}