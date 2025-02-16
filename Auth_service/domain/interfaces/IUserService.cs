using Auth_service.domain.entity;

namespace Auth_service.domain.interfaces
{
    public interface IUserService<T>
    {
        public T getById(int id,string password);
        public bool DeleteById(int id, string password);
        public bool Add(T obj);
        public bool Update(T obj, string password);
        public int getId(string password, string email);
    }
}
