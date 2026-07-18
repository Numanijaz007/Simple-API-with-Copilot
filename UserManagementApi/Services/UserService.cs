using UserManagementApi.Models;

namespace UserManagementApi.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public IEnumerable<User> GetAll() => _users;

        public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public User Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public bool Update(int id, User updatedUser)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.Name = updatedUser.Name;
            existing.Email = updatedUser.Email;
            existing.Age = updatedUser.Age;
            return true;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            _users.Remove(existing);
            return true;
        }
    }
}
