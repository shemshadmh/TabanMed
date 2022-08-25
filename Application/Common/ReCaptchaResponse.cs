using System.Text.Json.Serialization;

namespace Application.Common;

public class ReCaptchaResponse
{
    [JsonPropertyName("success")] public bool Success { get; set; }

    [JsonPropertyName("challenge_ts")] public DateTime Challenge_ts { get; set; }

    [JsonPropertyName("hostname")] public string Hostname { get; set; }

    [JsonPropertyName("error-codes")] public List<string> ErrorCodes { get; set; }
}