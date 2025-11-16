using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Immutable result object for login operations.
    /// </summary>
    public sealed class LoginResult
    {
        /// <summary>
        /// Gets a value indicating whether the login was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets the authenticated user if login was successful.
        /// </summary>
        public User? AuthenticatedUser { get; }

        /// <summary>
        /// Gets the error kind if login failed.
        /// </summary>
        public LoginErrorKind ErrorKind { get; }

        /// <summary>
        /// Gets the error message if login failed.
        /// </summary>
        public string? ErrorMessage { get; }

        private LoginResult(bool isSuccess, User? authenticatedUser, LoginErrorKind errorKind, string? errorMessage)
        {
            IsSuccess = isSuccess;
            AuthenticatedUser = authenticatedUser;
            ErrorKind = errorKind;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a successful login result.
        /// </summary>
        /// <param name="user">The authenticated user</param>
        /// <returns>A successful login result</returns>
        public static LoginResult Success(User user)
        {
            return new LoginResult(true, user, LoginErrorKind.None, null);
        }

        /// <summary>
        /// Creates a failed login result.
        /// </summary>
        /// <param name="errorKind">The error kind</param>
        /// <param name="errorMessage">Optional error message</param>
        /// <returns>A failed login result</returns>
        public static LoginResult Failure(LoginErrorKind errorKind, string? errorMessage = null)
        {
            return new LoginResult(false, null, errorKind, errorMessage);
        }
    }

    /// <summary>
    /// Machine-readable error kinds for login failures.
    /// </summary>
    public enum LoginErrorKind
    {
        None,
        InvalidEmail,
        EmailNotRegistered,
        InvalidPassword,
        IncorrectPassword
    }

    /// <summary>
    /// Domain service for user authentication operations.
    /// Contains pure business logic with no console I/O dependencies.
    /// </summary>
    public sealed class LoginSystem
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the LoginSystem class.
        /// </summary>
        /// <param name="userService">The user service for authentication operations</param>
        public LoginSystem(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Authenticates a user with the provided email and password.
        /// </summary>
        /// <param name="email">The user's email address</param>
        /// <param name="password">The user's password</param>
        /// <returns>Login result containing success status and authenticated user or error details</returns>
        public LoginResult AuthenticateUser(string email, string password)
        {
            // Validate email format
            if (!InputValidator.IsValidEmail(email))
            {
                return LoginResult.Failure(LoginErrorKind.InvalidEmail);
            }

            // Check if email is registered
            if (!userService.EmailExists(email))
            {
                return LoginResult.Failure(LoginErrorKind.EmailNotRegistered);
            }

            // Validate password format
            if (!InputValidator.IsValidPassword(password))
            {
                return LoginResult.Failure(LoginErrorKind.InvalidPassword);
            }

            // Get user by email
            User? foundUser = userService.GetUserByEmail(email);
            if (foundUser == null)
            {
                return LoginResult.Failure(LoginErrorKind.EmailNotRegistered);
            }

            // Authenticate password
            bool isValidPassword = userService.Authenticate(foundUser, password);
            if (!isValidPassword)
            {
                return LoginResult.Failure(LoginErrorKind.IncorrectPassword);
            }

            return LoginResult.Success(foundUser);
        }

        /// <summary>
        /// Checks if there are any users registered in the system.
        /// </summary>
        /// <returns>True if there are users, false otherwise</returns>
        public bool HasAnyUsers()
        {
            return userService.HasAnyUsers();
        }

        /// <summary>
        /// Checks if an email is registered in the system.
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>True if registered, false otherwise</returns>
        public bool IsEmailRegistered(string email)
        {
            return userService.EmailExists(email);
        }

        /// <summary>
        /// Gets a user by email address.
        /// </summary>
        /// <param name="email">Email address to look up</param>
        /// <returns>User if found, null otherwise</returns>
        public User? GetUserByEmail(string email)
        {
            return userService.GetUserByEmail(email);
        }

        /// <summary>
        /// Authenticates a user with the provided password.
        /// </summary>
        /// <param name="user">User to authenticate</param>
        /// <param name="password">Password to verify</param>
        /// <returns>True if authentication successful, false otherwise</returns>
        public bool AuthenticateUser(User user, string password)
        {
            return userService.Authenticate(user, password);
        }
    }

    /// <summary>
    /// Handles UI collection of login credentials (email and password).
    /// RESPONSIBILITY: UI-only component for collecting and validating login inputs with retry loops.
    /// Contains NO business logic or authentication - only console I/O.
    /// </summary>
    internal sealed class LoginCredentialsCollector
    {
        private readonly LoginSystem loginSystem;

        public LoginCredentialsCollector(LoginSystem loginSystem)
        {
            this.loginSystem = loginSystem;
        }

        /// <summary>
        /// Collects a valid, registered email from the user with retry loop.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        /// <returns>A valid, registered email address</returns>
        public string CollectValidRegisteredEmail()
        {
            while (true)
            {
                ConsoleUI.DisplayString("Please enter in your email:");
                string email = ConsoleUI.GetString();

                if (!InputValidator.IsValidEmail(email))
                {
                    ConsoleUI.DisplayErrorAgain("Supplied email is invalid");
                    continue;
                }

                if (!loginSystem.IsEmailRegistered(email))
                {
                    ConsoleUI.DisplayError("Email is not registered");
                    continue;
                }

                return email;
            }
        }

        /// <summary>
        /// Collects a valid password from the user with retry loop.
        /// Validates format only - does NOT authenticate.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        /// <returns>A valid password (format-wise)</returns>
        public string CollectValidPassword()
        {
            while (true)
            {
                ConsoleUI.DisplayString("Please enter in your password:");
                string password = ConsoleUI.GetString();

                if (!InputValidator.IsValidPassword(password))
                {
                    ConsoleUI.DisplayErrorAgain("Supplied password is invalid");
                    continue;
                }

                return password;
            }
        }

        /// <summary>
        /// Collects a valid password from the user and authenticates it with retry loop.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        /// <param name="user">The user to authenticate against</param>
        /// <returns>True when authentication successful</returns>
        public bool CollectAndAuthenticatePassword(User user)
        {
            while (true)
            {
                ConsoleUI.DisplayString("Please enter in your password:");
                string password = ConsoleUI.GetString();

                if (!InputValidator.IsValidPassword(password))
                {
                    ConsoleUI.DisplayErrorAgain("Supplied password is invalid");
                    continue;
                }

                bool valid = loginSystem.AuthenticateUser(user, password);
                if (!valid)
                {
                    ConsoleUI.DisplayError("Incorrect Password");
                    continue;
                }

                return true;
            }
        }
    }
}