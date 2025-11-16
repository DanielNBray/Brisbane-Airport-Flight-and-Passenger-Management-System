using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Handles the password change flow for authenticated users.
    /// RESPONSIBILITY: Coordinating password change UI and domain operations.
    /// </summary>
    internal sealed class PasswordChangeHandler
    {
        private readonly IUserService userService;

        public PasswordChangeHandler(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Handles the complete password change flow for the logged in user.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        /// <param name="currentUser">The user whose password is to be changed.</param>
        public void HandleChangePassword(User currentUser)
        {
            while (true)
            {
                string current = AirportMenu.PromptCurrentPassword();
                if (!currentUser.VerifyPassword(current))
                {
                    ConsoleUI.DisplayErrorAgain("Entered password does not match existing password");
                    continue;
                }
                break;
            }

            string newPassword = AirportMenu.PromptNewPassword();
            userService.ChangePassword(currentUser, newPassword);
        }
    }
}

