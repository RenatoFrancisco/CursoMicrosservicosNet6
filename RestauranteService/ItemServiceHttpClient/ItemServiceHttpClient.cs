using System.Text;
using System.Text.Json;
using RestauranteService.Dtos;

namespace RestauranteService.ItemServiceHttpClient
{
    public class ItemServiceHttpClient : IItemServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ItemServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async void EnviarRestauranteParaItemService(RestauranteReadDto restauranteReadDto)
        {
            var content = new StringContent
            (
                JsonSerializer.Serialize(restauranteReadDto),
                Encoding.UTF8,
                "application/json"
            );

            await _httpClient.PostAsync(_configuration["ItemService"], content);
        }
    }
}