namespace HseBank.Repositories;

public class DataRepository
{
    // Эмуляция базы данных с использованием словаря
    public Dictionary<string, Dictionary<Guid, object>> Data { get; } =
        new()
        {
            { "accounts", new Dictionary<Guid, object>() },
            { "categories", new Dictionary<Guid, object>() },
            { "operations", new Dictionary<Guid, object>() }
        };

    public Dictionary<string, Dictionary<Guid, object>> LoadAll()
    {
        return Data;
    }

    public void Save(string key, Guid id, object obj)
    {
        if (Data.ContainsKey(key)) Data[key][id] = obj;
    }
}