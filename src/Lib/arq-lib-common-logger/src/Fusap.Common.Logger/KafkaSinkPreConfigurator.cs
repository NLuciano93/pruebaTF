using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Fusap.Common.Logger
{
    internal static class KafkaSinkPreConfigurator
    {

        private const string KafkaSinkName = "Kafka";

        internal static void PreConfigure(IConfiguration configuration)
        {
            var kafkaSinkConfig = SinkConfigurationReader.GetSinkConfigurationSectionOrDefault(configuration, KafkaSinkName);
            if (kafkaSinkConfig == default(ConfigurationSection))
            {
                return;
            }

            var bootstrapServers = kafkaSinkConfig.GetValue<string>("Args:bootstrapServers");
            if (string.IsNullOrEmpty(bootstrapServers))
            {
                return;
            }

            if (!bootstrapServers.StartsWith("SSL"))
            {
                return;
            }

            CreateSslCaCertificate(configuration);
        }

        private static void CreateSslCaCertificate(IConfiguration configuration)
        {
            var caCertificateContentKey = "Kafka:SslCaContent";
            var sslCaCertificateContent = configuration[caCertificateContentKey] ?? throw new InvalidOperationException(
                $"Missing CA certificate configuration for Serilog Kafka Sink: '{caCertificateContentKey}'");
            var folderPath = configuration.GetValue("Kafka:SslCaTargetFolder", Path.GetTempPath());
            var fileName = configuration.GetValue("Kafka:SslCaFileName", "ca-cert");
            var filePath = Path.Combine(folderPath, fileName);
            File.WriteAllText(filePath, sslCaCertificateContent);
        }
    }
}
