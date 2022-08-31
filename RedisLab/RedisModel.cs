using System.ComponentModel;
using StackExchange.Redis;

namespace RedisLab;

public sealed class RedisModel
{
    private static string _connectionString = null;

    public readonly ConnectionMultiplexer ConnectionMultiplexer;
    
    private static readonly Lazy<RedisModel> _redisModel = new Lazy<RedisModel>(() =>
    {
        if (string.IsNullOrEmpty(_connectionString))
            throw new InvalidOperationException("connection string null.");
        return new RedisModel(_connectionString);
    });

    //singleton
    private RedisModel(string connectionString)
    {
        ConnectionMultiplexer = ConnectionMultiplexer.Connect(_connectionString);
        //_db = _connectionMultiplexer.GetDatabase();
    }

    public static void Init(string connectionString)
    {
        if (_redisModel.IsValueCreated) throw new InvalidOperationException("Instance has been set already.");

        _connectionString = connectionString;
    }

    public static RedisModel Instance()
    {
        return _redisModel.Value;
    }
}