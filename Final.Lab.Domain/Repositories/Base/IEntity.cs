namespace Final.Lab.Domain.Repositories.Base;

public interface IEntity
{
    int Id { get; }
    bool IsDeleted { get; set; }
}
