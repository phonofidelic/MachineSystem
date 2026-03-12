using System.Net.Http.Headers;

namespace MachineSystem.BlazorHost.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiClientProxyEndpoints(
        this IEndpointRouteBuilder builder)
    {
        var clientFactory = builder.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        var logger = builder.ServiceProvider.GetRequiredService<ILogger<LoggerFactory>>();
        builder.MapMethods("/proxy/{**endpoint}", ["GET", "POST", "PUT", "PATCH", "DELETE"], async (http) =>
        {
            var endpoint = http.GetEndpoint()?.ToString();
            ArgumentException.ThrowIfNullOrWhiteSpace(endpoint);
            logger.LogInformation("Intercepted request to {Endpoint}", endpoint);

            var client = clientFactory.CreateClient("DefaultClient");
            var targetUri = new Uri($"{client.BaseAddress}/{endpoint}");

            var requestMessage = new HttpRequestMessage(new HttpMethod(http.Request.Method), targetUri);

            if (http.Request.ContentLength > 0 || http.Request.Headers.ContainsKey("Transfer-Encoding"))
            {
                requestMessage.Content = new StreamContent(http.Request.Body);

                if (!string.IsNullOrWhiteSpace(http.Request.ContentType))
                {
                    requestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(http.Request.ContentType);
                }
            }

            foreach (var header in http.Request.Headers)
            {
                var addedToRequestHeaders = requestMessage.Headers.TryAddWithoutValidation(
                    header.Key,
                    header.Value.ToArray());
            }

            var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

            http.Response.StatusCode = (int)response.StatusCode;

            foreach (var header in response.Headers)
                http.Response.Headers[header.Key] = header.Value.ToArray();

            foreach (var header in response.Content.Headers)
                http.Response.Headers[header.Key] = header.Value.ToArray();

            http.Response.Headers.Remove("transfer-encoding");

            if (!response.IsSuccessStatusCode)
            {
                // ToDo: Log error
                logger.LogWarning("API request failed. Status {StatusCode}", response.StatusCode);
            }

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            await responseStream.CopyToAsync(http.Response.Body);

            //return new EmptyResult();
        });

        return builder;
    }
}
