using CarRent.API.Dtos;
using CarRent.Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;

namespace CarRent.Client.Pages
{
    public partial class Cars
    {
        private List<CarDto> CarList = new();
        private CarDto newCar = new CarDto(); // Modelo para o novo carro

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Config { get; set; }
        [Inject] private ITokenService TokenService { get; set; }
        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            await LoadCars();
        }

        private async Task LoadCars()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            HttpClient = new HttpClient(clientHandler);

            var tokenResponse = await TokenService.GetToken("CarRentAPI.read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/car");

            if (result.IsSuccessStatusCode)
            {
                CarList = await result.Content.ReadFromJsonAsync<List<CarDto>>();
            }
        }

        private async Task HandleValidSubmit()
        {
            var tokenResponse = await TokenService.GetToken("CarRentAPI.write");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await HttpClient.PostAsJsonAsync(Config["apiUrl"] + "/api/car", newCar);

            if (response.IsSuccessStatusCode)
            {

                tokenResponse = await TokenService.GetToken("CarRentAPI.read");
                HttpClient.SetBearerToken(tokenResponse.AccessToken);

                var createdCarId = await response.Content.ReadFromJsonAsync<int>();

                var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/car/" + createdCarId);

                if (result.IsSuccessStatusCode)
                {
                    CarDto newCar = await result.Content.ReadFromJsonAsync<CarDto>();
                    CarList.Add(newCar);
                }
                // Limpa o formulário
                newCar = new CarDto();
                errorMessage = "";
            }
            else
            {
                errorMessage = "";

                var errors = await response.Content.ReadAsStringAsync();

                if (errors != null)
                {
                    errorMessage = errors.Replace("/", "");
                }
            }
        }
    }
}
