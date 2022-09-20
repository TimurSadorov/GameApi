using Common.Specification;
using TestApp.Entities;

namespace TestApp.Specifications;

public class GenreByNames: Specification<Genre>
{
    public GenreByNames(IEnumerable<string> names)
    {
        Conditional = genre => names
            .Select(name => name.ToLower())
            .Contains(genre.Name.ToLower());
    }
}