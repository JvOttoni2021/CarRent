using CarRent.Application.Dtos;
using CarRent.Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CarRent.Client.Pages
{
    public partial class PaymentReceipts
    {
        private List<PaymentReceiptDto> PaymentReceiptList = new();

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Config { get; set; }
        [Inject] private ITokenService TokenService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPayments();
        }

        private async Task LoadPayments()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            HttpClient = new HttpClient(clientHandler);

            var tokenResponse = await TokenService.GetToken("CarRentAPI.read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            // Get na lista de pagamentos
            var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/paymentReceipt");

            if (result.IsSuccessStatusCode)
            {
                PaymentReceiptList = await result.Content.ReadFromJsonAsync<List<PaymentReceiptDto>>();
            }
        }
    }
}
