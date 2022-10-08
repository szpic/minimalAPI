using minimalAPI.Webservices.Interfaces;
using System.Xml.Serialization;

namespace minimalAPI.Webservices.Extensions;

public static class WebserviceExtension
{
    public static async Task<TResult>GetAsAsync<TResult>(this HttpClient? client, string endpoint)
    {

        var response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return DeserializeXml<TResult>(content);
    }

    static TResult DeserializeXml<TResult>(string sourceXML)
    {
        var serializer = new XmlSerializer(typeof(TResult));
        TResult result = default(TResult);

        using (TextReader reader = new StringReader(sourceXML))
        {
            result = (TResult)serializer.Deserialize(reader);
        }

        return result;
    }
}

