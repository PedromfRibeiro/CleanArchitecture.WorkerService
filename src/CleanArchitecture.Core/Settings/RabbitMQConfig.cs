using System.Xml;
using CleanArchitecture.Core.Extensions;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace CleanArchitecture.Core.Settings;

public class RabbitMQConfig(IConfiguration _configuration)
{
    public string hostName = _configuration.GetExpectedValue<string>("Configs:RabbitMQ:HostName");

    public string password = _configuration.GetExpectedValue<string>("Configs:RabbitMQ:Password");

    public int port = _configuration.GetExpectedValue<int>("Configs:RabbitMQ:Port");

    public string userName = _configuration.GetExpectedValue<string>("Configs:RabbitMQ:UserName");

    private IConnection? _connection;

    public async Task<IConnection> GetConnection()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
                Port = port
            };

            _connection = await factory.CreateConnectionAsync();
        }

        return _connection;
    }
}
