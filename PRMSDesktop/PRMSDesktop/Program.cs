using PRMSDesktop.Constants;
using PRMSDesktop.Forms.Auth;
using PRMSDesktop.Forms.Properties;
using PRMSDesktop.Helpers;
using PRMSDesktop.Services;

namespace PRMSDesktop
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

            ApiClient.Configure(ApiRoutes.ServerIP);


            ApplicationConfiguration.Initialize();

            if (SessionManager.RestoreSession())
            {
                ApiClient.ApplyAuthorizationHeader();
                Application.Run(new FrmPropertiesList());
            }
            else
            {
                Application.Run(new FrmLogin());
            }
        }
    }
}