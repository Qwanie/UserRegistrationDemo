using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistrationDemo;

namespace UserRegistrationDemoTests
{
    
    // Contains unit tests for the UserRegistrationService.
    
    [TestClass]
    public class UserRegistrationServiceTests
    {
        private UserRegistrationService? _service;

        
        // Initializes a new instance of the service before each test.
        [TestInitialize]
        public void Setup()
        {
            _service = new UserRegistrationService();
        }

        
        // Tests that registering a user with valid data returns a confirmation
        // and the user is stored in the system.
        [TestMethod]
        public void RegisterUser_WithValidData_ShouldReturnConfirmationAndStoreUser()
        {
            // Arrange
            var username = "validUser1";
            var password = "Passw0rd!";
            var email = "user@example.com";

            // Act
            var result = _service?.RegisterUser(username, password, email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(username, result.Username);
            Assert.IsTrue(result.Message.Contains("successful"));
            Assert.IsTrue(_service?.IsUserRegistered(username));
        }

        
        // Tests that a username shorter than 5 characters throws an ArgumentException.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterUser_WithShortUsername_ShouldThrowArgumentException()
        {
            // Arrange: username less than 5 characters.
            var username = "usr";
            var password = "Passw0rd!";
            var email = "user@example.com";

            // Act
            _ = _service?.RegisterUser(username, password, email);
        }

        
        // Tests that a username longer than 20 characters throws an ArgumentException.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterUser_WithLongUsername_ShouldThrowArgumentException()
        {
            // Arrange: username more than 20 characters.
            var username = new string('a', 21); // 21 characters.
            var password = "Passw0rd!";
            var email = "user@example.com";

            // Act
            _ = _service?.RegisterUser(username, password, email);
        }

        
        // Tests that a username containing non-alphanumeric characters throws an ArgumentException.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterUser_WithNonAlphanumericUsername_ShouldThrowArgumentException()
        {
            // Arrange: username with non-alphanumeric characters.
            var username = "user@name";
            var password = "Passw0rd!";
            var email = "user@example.com";

            // Act
            _ = _service?.RegisterUser(username, password, email);
        }

        
        // Tests that a password shorter than 8 characters throws an ArgumentException.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterUser_WithShortPassword_ShouldThrowArgumentException()
        {
            // Arrange: password less than 8 characters.
            var username = "validUser";
            var password = "Pass!"; // Too short.
            var email = "user@example.com";

            // Act
            _ = _service?.RegisterUser(username, password, email);
        }

        
        // Tests that a password without any special character throws an ArgumentException.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterUser_WithPasswordWithoutSpecialCharacter_ShouldThrowArgumentException()
        {
            // Arrange: password without any special character.
            var username = "validUser";
            var password = "Password1"; // No special character.
            var email = "user@example.com";

            // Act
            _ = _service?.RegisterUser(username, password, email);
        }

        
        // Tests that an invalid email format throws an ArgumentException.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterUser_WithInvalidEmail_ShouldThrowArgumentException()
        {
            // Arrange: invalid email format.
            var username = "validUser";
            var password = "Passw0rd!";
            var email = "useratexample.com"; // Missing '@'.

            // Act
            _ = _service?.RegisterUser(username, password, email);
        }

        
        // Tests that attempting to register a duplicate username throws an InvalidOperationException.
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterUser_WithDuplicateUsername_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var username = "validUser";
            var password = "Passw0rd!";
            var email = "user@example.com";

            // Act: Register the user for the first time.
            _ = _service?.RegisterUser(username, password, email);
            // Act: Attempt to register the same username again.
            _ = _service?.RegisterUser(username, "AnotherP@ss1", "another@example.com");
        }
    }

}
