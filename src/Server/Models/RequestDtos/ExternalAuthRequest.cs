namespace Server.Models.RequestDtos;

public record ExternalAuthRequest(string Provider, string IdToken);
