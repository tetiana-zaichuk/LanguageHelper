namespace LanguageHelper.DataAccessLayer.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
