using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace UserRegistrationDemo
{
    
    // Represents a registered user.
    public class User
    {
        
        // The username for the user.
        public string Username { get; set; }
        
        // The user's password.
        public string Password { get; set; }
        
        // The user's email address.
        public string Email { get; set; }
    }

    
    // Represents the confirmation message returned upon successful registration.
    public class RegistrationConfirmation
    {
        
        // The registered username.
        public string Username { get; set; }
        
        // A success message.
       public string Message { get; set; }
    }

    
    // Service responsible for registering users with input validation and unique username enforcement.
    public class UserRegistrationService
    {
        // In-memory store for registered users.
        private readonly List<User> _users = new List<User>();

        
        // Registers a new user after validating input data and ensuring username uniqueness.
        // Returns a confirmation object upon success.
        
        // <param name="username">The username for the new user.</param>
        // <param name="password">The password for the new user.</param>
        // <param name="email">The email address for the new user.</param>
        // <returns>A RegistrationConfirmation object containing the username and a success message.</returns>
        // <exception cref="ArgumentException">Thrown when input data is invalid.</exception>
        // <exception cref="InvalidOperationException">Thrown when the username already exists.</exception>
        public RegistrationConfirmation RegisterUser(string username, string password, string email)
        {
            // Validate Username: must be between 5 and 20 characters and only alphanumeric.
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");
            if (username.Length < 5 || username.Length > 20)
                throw new ArgumentException("Username must be between 5 and 20 characters long.");
            if (!Regex.IsMatch(username, "^[a-zA-Z0-9]+$"))
                throw new ArgumentException("Username can only contain alphanumeric characters.");

            // Validate Password: must be at least 8 characters and include at least one special character.
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.");
            if (password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");
            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                throw new ArgumentException("Password must include at least one special character.");

            // Validate Email: must follow a valid format.
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");
            try
            {
                var addr = new MailAddress(email);
                if (addr.Address != email)
                    throw new ArgumentException("Invalid email format.");
            }
            catch
            {
                throw new ArgumentException("Invalid email format.");
            }

            // Ensure username uniqueness.
            if (_users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Username already exists.");

            // Create and store the new user.
            var user = new User { Username = username, Password = password, Email = email };
            _users.Add(user);

            // Return a confirmation object.
            return new RegistrationConfirmation
            {
                Username = username,
                Message = "Registration successful"
            };
        }

        
        // Helper method to check if a user with the given username is already registered.
        
        // <param name="username">The username to check.</param>
        // <returns>True if the user is registered; otherwise, false.</returns>
        public bool IsUserRegistered(string username)
        {
            return _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
