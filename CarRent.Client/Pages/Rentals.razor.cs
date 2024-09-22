using CarRent.Application.Dtos;
using CarRent.Client.Services;
using CarRent.Domain.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;

namespace CarRent.Client.Pages
{
    public partial class Rentals
    {
        private List<RentalDto> RentalList = new();
        private RentalDto newRental = new RentalDto(); // Modelo para o novo carro
        private RentalDto updatedRental = new RentalDto(); // Modelo para o novo carro

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Config { get; set; }
        [Inject] private ITokenService TokenService { get; set; }
        private string errorMessage = "";
        private string errorMessagePut = "";
        private bool showPostForm = false;
        private bool showPutForm = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadRentals();
        }

        private async Task LoadRentals()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            HttpClient = new HttpClient(clientHandler);

            var tokenResponse = await TokenService.GetToken("CarRentAPI.read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            // Get na lista de locações
            var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/rental");

            if (result.IsSuccessStatusCode)
            {
                RentalList = await result.Content.ReadFromJsonAsync<List<RentalDto>>();
            }
        }

        private async Task HandleValidPostSubmit()
        {
            var tokenResponse = await TokenService.GetToken("CarRentAPI.write");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await HttpClient.PostAsJsonAsync(Config["apiUrl"] + "/api/rental", newRental);

            if (response.IsSuccessStatusCode)
            {

                tokenResponse = await TokenService.GetToken("CarRentAPI.read");
                HttpClient.SetBearerToken(tokenResponse.AccessToken);

                var createdRentalId = await response.Content.ReadFromJsonAsync<int>();

                var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/rental/" + createdRentalId);

                if (result.IsSuccessStatusCode)
                {
                    RentalDto newRental = await result.Content.ReadFromJsonAsync<RentalDto>();
                    RentalList.Add(newRental);
                }
                // Limpa o formulário
                newRental = new RentalDto();
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

            var response = await HttpClient.PutAsJsonAsync(Config["apiUrl"] + "/api/rental", updatedRental);

            if (response.IsSuccessStatusCode)
            {

                tokenResponse = await TokenService.GetToken("CarRentAPI.read");
                HttpClient.SetBearerToken(tokenResponse.AccessToken);

                var updatedRentalId = await response.Content.ReadFromJsonAsync<int>();

                var result = await HttpClient.GetAsync(Config["apiUrl"] + "/api/rental/" + updatedRentalId);

                var newUpdatedRental = await result.Content.ReadFromJsonAsync<RentalDto>();

                var rentalIndex = RentalList.FindIndex(rental => rental.Id == newUpdatedRental.Id);

                if (rentalIndex != -1)
                {
                    // Substituir a locação existente na lista pela atualizada
                    RentalList[rentalIndex] = newUpdatedRental;
                }

                // Limpa o formulário
                updatedRental = new RentalDto();
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

        //private void LoadCarForEdit(CarDto car)
        //{
        //    // Carregar os dados do carro selecionado para edição
        //    updatedCar = car;
        //    showPutForm = true;
        //}
    }
}
