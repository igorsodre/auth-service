namespace Server.Models.Domain;

public record ExternalAuthPayload(string Provider, string IdToken);
