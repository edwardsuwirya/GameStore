using System.ComponentModel.DataAnnotations;

namespace GameStore.Models;

public class UserAccess
{
    [Required] [StringLength(20)] public string UserName { get; set; }
    [Required] [StringLength(10)] public string Password { get; set; }
}