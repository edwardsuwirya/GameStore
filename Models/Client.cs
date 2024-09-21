using System.ComponentModel.DataAnnotations;
using GameStore.Shared.Validations;

namespace GameStore.Models;

public class Client
{
    public int Id { get; set; }
    [Required] [StringLength(50)] public string FirstName { get; set; } = string.Empty;
    [Required] [StringLength(50)] public string LastName { get; set; } = string.Empty;
    [Required] [StringLength(50)] public string Email { get; set; } = string.Empty;
    [Required] [StringLength(20)] public string Phone { get; set; } = string.Empty;
    [Required] [StringLength(75)] public string Address { get; set; } = string.Empty;
    [Required] [StringLength(10)] public string Status { get; set; } = string.Empty;

    [MinAge(AllowedMinAge = 21)] public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
}