using Cooklee.Data.Entities.Order;
using Cooklee.Data.Repository.Contract;
using Cooklee.Data.Service.Contract;
using Cooklee.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Service.Services
{
    public class PaymentService : IPaymentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepositery;
        private readonly IConfiguration _configuration;
        private static readonly string apiUrl = "https://accept.paymobsolutions.com/api";

        public PaymentService(IUnitOfWork unitOfWork, ICartRepository icartRepositery, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _cartRepositery = icartRepositery;
            _configuration = configuration;
        }


        public async Task<decimal> GetAmountAsync(string cartid)
        {

            var cart = await _cartRepositery.GetCartAsync(cartid);
            if (cart is null) return 0;
            var shippingPrice = 0m;
            if (cart.Items.Count > 0)
            {
                foreach (var item in cart.Items)
                {
                    var meal = await _unitOfWork.MealRepository.GetAsync(item.Id);
                    if (item.Price != meal.Price)
                    {
                        item.Price = meal.Price;
                    }


                }
            }

            var Amount = (long)cart.Items.Sum(item => item.Price * item.Quantity * 100);
            var Currency = "EGP";


            return Amount;

        }

        public async Task<string> GetAuthTokenAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new
                {
                    api_key = _configuration["Payment:api_key"]
                };


               // HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7003/api/students", stdData);
                HttpResponseMessage response = await client.PostAsJsonAsync( apiUrl+"/auth/tokens", requestBody);
                response.EnsureSuccessStatusCode(); // This line throws the exception

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenObject = JObject.Parse(responseContent);

                return tokenObject["token"].ToString();
            }
        }

        public async Task<string> GeteOrderIdAsync(string AuthToken, decimal amount)
        {
            using (HttpClient client = new HttpClient())
            {
               // client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var orderRequestBody = new
                {
                    auth_token = AuthToken,
                    delivery_needed = false,
                    amount_cents = (amount * 100).ToString(), // Convert to cents
                    currency = "EGP",
                    items = new object[] { }
                };

                 var response = await client.PostAsJsonAsync(apiUrl + "/ecommerce/orders", orderRequestBody);
                  response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var orderObject = JObject.Parse(responseContent);

                return orderObject["id"].ToString();
            }
        }


        public async Task<string> GetRequestPaymentKeyAsync(string AuthToken, string PayOrderId, decimal amount , string orderEmail )
        {
            var order = await _unitOfWork.OrderRepository.GetOrderByEmail(orderEmail);
            using (HttpClient client = new HttpClient())
            {
               // client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var requestBody = new
                {
                    auth_token = AuthToken,
                    amount_cents = (int)(amount * 100), // Convert amount to cents
                    order_id = PayOrderId,
                    billing_data = new
                    {
                        apartment = order.ShipmentDetails.Apartment,
                        email = order.ShipmentDetails.Email, //
                        floor = order.ShipmentDetails.Floor,
                        first_name = order.ShipmentDetails.FirstName, //
                        street = order.ShipmentDetails.Street,
                        building = order.ShipmentDetails.Building,
                        phone_number = order.ShipmentDetails.PhoneNumber, //
                        shipping_method = order.ShipmentDetails.ShippingMethod,
                        postal_code = order.ShipmentDetails.PostalCode, //
                        city = order.ShipmentDetails.City,
                        country = order.ShipmentDetails.Country,
                        last_name = order.ShipmentDetails.LastName,
                        state = order.ShipmentDetails.State
                    },
                    currency = "EGP",
                    integration_id = _configuration["Payment:integration_id"]
                };

                var response = await client.PostAsJsonAsync(apiUrl + "/acceptance/payment_keys", requestBody);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var paymentKeyObject = JObject.Parse(responseContent);

                return paymentKeyObject["token"].ToString();
            }
        }



    }
}
