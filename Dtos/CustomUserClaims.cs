namespace GameStore.Dtos;

public record CustomUserClaims(int Id, string Name = null!, string Email = null!, string Role = null!);