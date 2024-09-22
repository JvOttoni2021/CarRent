using CarRent.API.Dtos;
using CarRent.Application.Dtos;
using CarRent.Client.Services;
using CarRent.Domain.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;

namespace CarRent.Client.Pages
{
    public partial class Customers
    {
        private List<CustomerDto> CustomerList = new();
        private CustomerDto newCustomer = new CustomerDto(); // Modelo para o novo cliente

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Config { get; set; }
        [Inject] private ITokenService TokenService { get; set; }
        private string errorMessage = "";
        private bool showPostForm = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            HttpClient = new HttpClient(clientHandler);

            var tokenResponse = await TokenService.GetToken("CarRentAPI.read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            // Get na lista de clientes
            var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/customer");

            if (result.IsSuccessStatusCode)
            {
                CustomerList = await result.Content.ReadFromJsonAsync<List<CustomerDto>>();
            }
        }

        private async Task HandleValidPostSubmit()
        {
            var tokenResponse = await TokenService.GetToken("CarRentAPI.write");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await HttpClient.PostAsJsonAsync(Config["apiUrl"] + "/api/customer", newCustomer);

            if (response.IsSuccessStatusCode)
            {

                tokenResponse = await TokenService.GetToken("CarRentAPI.read");
                HttpClient.SetBearerToken(tokenResponse.AccessToken);

                var createdCustomerId = await response.Content.ReadFromJsonAsync<int>();

                var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/customer/" + createdCustomerId);

                if (result.IsSuccessStatusCode)
                {
                    CustomerDto newCustomer = await result.Content.ReadFromJsonAsync<CustomerDto>();
                    CustomerList.Add(newCustomer);
                }
                // Limpa o formulário
                newCustomer = new CustomerDto();
                errorMessage = "";
            }
            else
            {
                // Exibir erro do response
                errorMessage = "";

                var errors = await response.Content.ReadAsStringAsync();

                if (errors != null)
                {
                    errorMessage = errors.Replace("/", "");
                }
            }
        }

        private void ToggleFormPostVisibility()
        {
            showPostForm = !showPostForm;
        }
    }
}
