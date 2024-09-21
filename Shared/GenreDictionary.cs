using GameStore.Models;

namespace GameStore.Shared;

public class GenreDictionary
{
    public static Dictionary<Genre, string> GenreDescription = new()
    {
        { Genre.Fighting, "Fighting" },
        { Genre.Racing, "Racing" },
        { Genre.RolePlaying, "Role Playing" },
        { Genre.KidsAndFamily, "Kids and Family" },
        { Genre.Sports, "Sports" },
    };

    public static string GenreDescriptionString(string genre)
    {
        Enum.TryParse(genre, out Genre gameGenre);
        GenreDescription.TryGetValue(gameGenre, out string description);
        return description ?? string.Empty;
    }
}