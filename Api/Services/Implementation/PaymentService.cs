using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Showdown_hub.Api.Services.Interface;
using Showdown_hub.Models;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Api.Services.Implementation
{
    public class PaymentService : IPaymentSrvice

    {
         private readonly HttpClient _httpClient;

        private readonly string _payStackSecretKey;

        public PaymentService(IConfiguration _configuration ,HttpClient httpClient )
        {
           _payStackSecretKey= _configuration["PayStack:SecretKey"];
            _httpClient = httpClient;
            
        }
     public async Task<PayStackReponse<PayStackTransactionResponse>> InitialPayment(PaystackRequest request)
{
    try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _payStackSecretKey);

                var response = await _httpClient.PostAsJsonAsync("https://api.paystack.co/transaction/initialize", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<PayStackReponse<PayStackTransactionResponse>>();
                    return result;
                }
                else
                {
                    // Handle non-success status codes
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Paystack API returned an error: {response.StatusCode}. Response: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the HttpRequestException
                throw new Exception("Error initializing payment: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Log or handle any other exceptions
                throw new Exception("An unexpected error occurred: " + ex.Message, ex);
            }
        }

    }
}