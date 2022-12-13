namespace ItemService.EventProcessor;

public interface IProcessaEvento
{
    void Processar(string message);
}