// See https://aka.ms/new-console-template for more information

using System.Text;
using RedisLab;
using StackExchange.Redis;

StringBuilder dataBuffer = new StringBuilder();

#region Create Connection instance
Console.WriteLine("Connecting to redis server...");

RedisModel.Init("127.0.0.1:6379");
var redis = RedisModel.Instance();
var db = redis.ConnectionMultiplexer.GetDatabase();

Console.WriteLine("Connection created.");
#endregion

var result = db.Execute("PING");
Console.WriteLine($"Send command PING, result:{result.ToString()}");

if (db.StringSet("user.name", "bauann"))
{
    Console.WriteLine($"Send command StringSet, key:user.name, result:true");
}
else
{
    Console.WriteLine($"Send command StringSet, result:false");
}

var d = db.StringGet("user.name");
Console.WriteLine($"Send command StringGet, key:user.name, result:{d.ToString()}");
//
db.StringSet("user.name", "fireAndForget", flags: CommandFlags.FireAndForget);

//List data type
db.ListRightPush("list.users", "no.1");
db.ListRightPush("list.users", "no.2");
db.ListRightPush("list.users", "no.3");
Console.WriteLine($"list.users now have {db.ListLength("list.users")} items");
//-1 代表集合的最後一個，-2 是倒數第2個，以此類推
var listResult = db.ListRange("list.users", 0, -1);
foreach (var item in listResult)
{
    dataBuffer.Append($"{item.ToString()}, ");
}

Console.WriteLine($"items in list.user, from 0 to -1:{dataBuffer.ToString()}");

Console.ReadKey();