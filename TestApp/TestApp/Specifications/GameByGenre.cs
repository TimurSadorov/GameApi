using Common.Specification;
using TestApp.Entities;

namespace TestApp.Specifications;

public class GameByGenre : Specification<Game>
{
    public GameByGenre(string genreName)
    {
        Conditional = game => game.Genres
            .Select(genre => genre.Name.ToLower())
            .Contains(genreName.ToLower());
    }
}