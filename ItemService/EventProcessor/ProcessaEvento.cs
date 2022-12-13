using System.Text.Json;
using AutoMapper;
using ItemService.Data;
using ItemService.Dtos;
using ItemService.Models;

namespace ItemService.EventProcessor;

public class ProcessaEvento : IProcessaEvento
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public ProcessaEvento(IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _mapper = mapper;
        _scopeFactory = scopeFactory;
    }

    public void Processar(string message)
    {
        using var scope = _scopeFactory.CreateScope();

        var itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();

        var restaurante = _mapper.Map<Restaurante>(JsonSerializer.Deserialize<RestauranteReadDto>(message));

        if (!itemRepository.ExisteRestauranteExterno(restaurante.Id))
        {
            itemRepository.CreateRestaurante(restaurante);
            itemRepository.SaveChanges();
        }
    }
}