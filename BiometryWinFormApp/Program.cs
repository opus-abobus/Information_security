namespace BiometryWinFormApp {
    public static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            ApplicationConfiguration.Initialize();

            var startForm = new StartForm();
            _ = new FormManager(startForm);

            Application.Run(startForm);
        }
    }
}