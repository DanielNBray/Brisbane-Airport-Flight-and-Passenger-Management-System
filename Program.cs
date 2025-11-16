namespace DanielBray_AirportA2
{

    /// <summary>
    /// Entry point for the Brisbane Airport management system.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point that initializes the application and starts the main controller.
        /// </summary>
        /// <param name="args">Command line arguments (not used)</param>
        static void Main(string[] args)
        {
            // build dependencies and start application
            InMemoryUserStore userStore = new InMemoryUserStore();
            UserService userService = new UserService(userStore);
            AirportController app = new AirportController(userService);
            app.Run();
        }
    }
}
