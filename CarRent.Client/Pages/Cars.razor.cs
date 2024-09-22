using CarRent.API.Dtos;
using CarRent.Client.Services;
using CarRent.Domain.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;

namespace CarRent.Client.Pages
{
    public partial class Cars
    {
        private List<CarDto> CarList = new();
        private CarDto newCar = new CarDto(); // Modelo para o novo carro
        private CarDto updatedCar = new CarDto(); // Modelo para o novo carro

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Config { get; set; }
        [Inject] private ITokenService TokenService { get; set; }
        private string errorMessage = "";
        private string errorMessagePut = "";
        private bool showPostForm = false;
        private bool showPutForm = false;

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

            // Get na lista de carros
            var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/car");

            if (result.IsSuccessStatusCode)
            {
                CarList = await result.Content.ReadFromJsonAsync<List<CarDto>>();
            }
        }

        private async Task HandleValidPostSubmit()
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
                // Exibir erro do response
                errorMessage = "";

                var errors = await response.Content.ReadAsStringAsync();

                if (errors != null)
                {
                    errorMessage = errors.Replace("/", "");
                }
            }
        }

        private async Task HandleValidPutSubmit()
        {
            var tokenResponse = await TokenService.GetToken("CarRentAPI.write");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await HttpClient.PutAsJsonAsync(Config["apiUrl"] + "/api/car", updatedCar);

            if (response.IsSuccessStatusCode)
            {

                tokenResponse = await TokenService.GetToken("CarRentAPI.read");
                HttpClient.SetBearerToken(tokenResponse.AccessToken);

                var createdCarId = await response.Content.ReadFromJsonAsync<int>();

                var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/car/" + createdCarId);

                var updatedCar = await result.Content.ReadFromJsonAsync<CarDto>();

                var carIndex = CarList.FindIndex(car => car.Id == updatedCar.Id);

                if (carIndex != -1)
                {
                    // Substituir o carro existente na lista pelo atualizado
                    CarList[carIndex] = updatedCar;
                }

                // Limpa o formulário
                updatedCar = new CarDto();
                errorMessagePut = "";
            }
            else
            {
                // Exibir erro do response
                errorMessagePut = "";

                var errors = await response.Content.ReadAsStringAsync();

                if (errors != null)
                {
                    errorMessagePut = errors.Replace("/", "");
                }
            }
        }

        private void ToggleFormPostVisibility()
        {
            showPostForm = !showPostForm;
        }

        private void ToggleFormPutVisibility()
        {
            showPutForm = !showPutForm;
        }

        private void LoadCarForEdit(CarDto car)
        {
            // Carregar os dados do carro selecionado para edição
            updatedCar = car;
            showPutForm = true;
        }
    }
}
