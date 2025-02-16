namespace Zametki_service.domain
{
    public interface Iservice<T>
    {
        public T getById(int id);
        public List<T> GetAll(int relatedId);
        public bool DeleteById(int id);
        public bool Add(T obj);
        public bool Update(T obj);
    }
}
