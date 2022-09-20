using Common.Models;
using Common.Specification;
using TestApp.Entities;

namespace TestApp.Specifications;

public class EntityByIdSpec<TEntity>: Specification<TEntity> 
    where TEntity: BaseEntity
{
    public EntityByIdSpec(int id)
    {
        Conditional = entity => entity.Id == id;
    }
}