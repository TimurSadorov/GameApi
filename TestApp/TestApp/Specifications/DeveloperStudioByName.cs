using Common.Specification;
using TestApp.Entities;

namespace TestApp.Specifications;

public class DeveloperStudioByName : Specification<DeveloperStudio>
{
    public DeveloperStudioByName(string? name)
    {
        Conditional = studio => !string.IsNullOrEmpty(name) && studio.Name.ToLower() == name.ToLower();
    }
}