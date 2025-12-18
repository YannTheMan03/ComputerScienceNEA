namespace iteration1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            using (var usernameForm = new LeaderboardForm())
            {
                Application.Run(new menuForm());
                // Wait for user input (ShowDialog pauses execution)
                if (usernameForm.ShowDialog() == DialogResult.OK)
                {
                    // Get the entered username
                    string username = usernameForm.Username;

                    // Now launch the main game form and pass the username
                    Application.Run(new Form1(username));
                }
                else
                {
                    // If user closes the username form or cancels, just exit
                    return;
                }
            }
        }
    }  
}