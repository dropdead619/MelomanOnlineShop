using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;

namespace Services
{
    public class QiwiPaymentService
    {
        public bool Purchase(decimal price)
        {
            try
            {
                var client = BillPaymentsClientFactory.Create(
secretKey: "eyJ2ZXJzaW9uIjoiUDJQIiwiZGF0YSI6eyJwYXlpbl9tZXJjaGFudF9zaXRlX3VpZCI6InJpMmQ3ai0wMCIsInVzZXJfaWQiOiI3NzcxODM2NTkxOCIsInNlY3JldCI6IjQ2MzhkNjg4ODEzZTY1MjkwNWQ4YjMxNzI1N2I5ODUyNWI0NjE2ODAxMTE3NzYwMzQ1YmI1MWE3M2U2OWFmOWUifX0="
);
                var invoice = client.CreateBill(
        info: new CreateBillInfo
        {
            BillId = RandomCodeGenerationService.Generate(10),
            Amount = new MoneyAmount
            {
                //Это тестовое значение суммы, для тестового режима
                ValueDecimal = 1m,
                CurrencyEnum = CurrencyEnum.Rub
            },
            Comment = $"Вам выставлен счет на сумму {price} рублей. С уважением, Онлайн-Магазин Meloman",
            ExpirationDateTime = DateTime.Now.AddHours(3)
        }
    );

                //Открываю форму оплаты в браузере по умолчанию (Я чет не могу затестить, т.к. на свой кошелек невозможно переводить. 
                //Там было написано, что установите значение в 1 рубль для тестов. Можем вместе протестить это позже
                var invoiceURL = invoice.PayUrl.AbsoluteUri;
                invoiceURL = invoiceURL.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {invoiceURL}") { CreateNoWindow = true });

                while (invoice.Status.ValueString == "WAITING" || invoice.Status.ValueString == "PAID")
                {
                    if (invoice.Status.ValueString == "PAID")
                    {
                        return true;
                    }
                    else if (invoice.Status.ValueString == "WAITING")
                    {
                        Console.Clear();
                        Console.WriteLine("Ваш заказ обрабатывается, пожалуйста подождите...");
                        System.Threading.Thread.Sleep(10000);
                        continue;
                    }
                    else
                    {
                        client.CancelBill(invoice.BillId);
                        return false;
                    };
                }
            }
            catch (AggregateException aggregateException)
            {
                var exception = aggregateException.GetBaseException();
                Console.WriteLine(exception.Message);
            }
            return false;
        }
    }
}