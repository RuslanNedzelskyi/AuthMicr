namespace AuthMicr.Helpers
{
    public static class Constants
    {
        /// <summary>
        /// The base URI for the Datasync service.
        /// </summary>
        public static string ServiceUri = "https://demo-datasync-quickstart.azurewebsites.net";

        /// <summary>
        /// The application (client) ID for the native app within Azure Active Directory
        /// </summary>
        public static string ApplicationId = "1b10f552-685d-49b0-8009-1f7c236b74bf";

        /// <summary>
        /// The list of scopes to request
        /// </summary>
        public static string[] Scopes = new[]
        {
          "api://1b10f552-685d-49b0-8009-1f7c236b74bf/access_as_user"
      };
    }
}
