using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Data.Reposiotry.Interface
{
    public interface IPaymentRepo
    {
         Task<string> InitializeTransaction(PaymentRequest paymentRequest, string email);

    }
}