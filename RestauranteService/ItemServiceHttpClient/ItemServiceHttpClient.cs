using System.Text;
using System.Text.Json;
using RestauranteService.Dtos;

namespace RestauranteService.ItemServiceHttpClient
{
    public class ItemServiceHttpClient : IItemServiceHttpClient
    {
        private readonly HttpClient _httpClient;

        public ItemServiceHttpClient(HttpClient httpClient) => _httpClient = httpClient;
            
        public void EnviarRestauranteParaItemService(RestauranteReadDto restauranteReadDto)
        {
            var content = new StringContent
            (
                JsonSerializer.Serialize(restauranteReadDto),
                Encoding.UTF8,
                "application/json"
            );
        }
    }
}