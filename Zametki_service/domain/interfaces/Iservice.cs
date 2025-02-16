namespace Zametki_service.domain.interfaces
{
    public interface Iservice<T>
    {
        public T getById(int id,int UserId);
        public List<T> GetAll(int relatedId);
        public bool DeleteById(int id, int UserId);
        public bool Add(T obj, int UserId);
        public bool Update(T obj, int UserId);
    }
}
