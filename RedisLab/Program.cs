// See https://aka.ms/new-console-template for more information

using RedisLab;
using StackExchange.Redis;

Console.WriteLine("Hello, World!");

RedisModel.Init("127.0.0.1:6379");
var redis = RedisModel.Instance();
var db = redis.ConnectionMultiplexer.GetDatabase();
db.StringSet("user.name", "victor.k");
var d = db.StringGet("user.name");
Console.WriteLine($"Data From Redis:{d}");
Console.ReadKey();
Console.WriteLine($"Today is :{DateTime.Now.ToString("yyyy-MM-dd")}");
//redis.Close();
