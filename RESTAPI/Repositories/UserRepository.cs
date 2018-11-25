using System;
using System.Linq;
using System.Security.Cryptography;
using RESTAPI.Data;
using RESTAPI.Exceptions;
using RESTAPI.Models;

namespace RESTAPI.Repositories
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        User GetById(int id);
        User Create(User user, string password);
        void Update(User userParam, string password = null);
        void Delete(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly RESTAPIContext _context;

        public UserRepository(RESTAPIContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;

            var user = _context.User.SingleOrDefault(u => u.Username == username);

            // user does not exist
            if (user == null) return null;

            // password is incorrect
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;

            return user;
        }

        public User GetById(int id)
        {
            return _context.User.Find(id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new AuthenticationException("Password is required");

            if (_context.User.Any(u => u.Username == user.Username))
                throw new AuthenticationException($"Username {user.Username} is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.User.Find(userParam.UserId);

            // user does not exist
            if (user == null) throw new AuthenticationException("User does not exist");

            // username has changed so check if the new username is available
            if (userParam.Username != user.Username)
            {
                if (_context.User.Any(u => u.Username == userParam.Username))
                    throw new AuthenticationException($"Username {userParam.Username} is already taken");
            }

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            // a new password has been entered, alter it
            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.User.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
        }

        #region Helpers

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace string", nameof(password));

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace string", nameof(password));
            if (storedHash.Length != 64)
                throw new ArgumentException($"Expecting password hash of 64 bytes, got {storedHash.Length} bytes",
                    nameof(storedHash));
            if (storedSalt.Length != 128)
                throw new ArgumentException($"Expecting password salt of 128 bytes, got {storedSalt.Length} bytes",
                    nameof(storedSalt));

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != storedHash[i]).Any())
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}