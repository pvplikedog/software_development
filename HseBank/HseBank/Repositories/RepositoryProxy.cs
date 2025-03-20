namespace HseBank.Repositories;

public class RepositoryProxy
{
    private readonly Dictionary<string, Dictionary<Guid, object>> _cache;
    private readonly DataRepository _repository;

    public RepositoryProxy(DataRepository repository)
    {
        _repository = repository;
        _cache = _repository.LoadAll();
    }

    public object Get(string key, Guid id)
    {
        if (_cache.ContainsKey(key) && _cache[key].ContainsKey(id))
            return _cache[key][id];
        return null;
    }

    public IEnumerable<object> GetAll(string key)
    {
        if (_cache.ContainsKey(key))
            return _cache[key].Values;
        return Enumerable.Empty<object>();
    }

    public void Save(string key, Guid id, object obj)
    {
        if (!_cache.ContainsKey(key))
            _cache[key] = new Dictionary<Guid, object>();

        _cache[key][id] = obj;
        _repository.Save(key, id, obj);
    }

    public void Delete(string key, Guid id)
    {
        if (_cache.ContainsKey(key))
            _cache[key].Remove(id);

        if (_repository.Data.ContainsKey(key))
            _repository.Data[key].Remove(id);
    }

    public void Clear(string key)
    {
        if (_cache.ContainsKey(key))
            _cache[key].Clear();

        if (_repository.Data.ContainsKey(key))
            _repository.Data[key].Clear();
    }
}