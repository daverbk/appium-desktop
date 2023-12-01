using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System.Diagnostics;

namespace AppiumDesktop;

public class WinAppDriverProvider
{
    private const string SpotifyFolderPath = "C:\\Users\\David.Rabko\\AppData\\Roaming\\Spotify\\";
    private static Process? _winAppDriverProcess;
    private static Process? _winAppDriverProcessSpotify;

    public static WindowsDriver<AppiumWebElement>? WinAppDriver;
    public static int AppProcessId { get; private set; }

    public static void StartWinAppDriver()
    {
        _winAppDriverProcess =
            Process.Start(
                $@"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)}\Windows Application Driver\WinAppDriver.exe");

        _winAppDriverProcessSpotify =
            Process.Start(new ProcessStartInfo($"{SpotifyFolderPath}Spotify.exe", @"/e /n")
            {
                CreateNoWindow = false,
                WindowStyle = ProcessWindowStyle.Normal
            });

        AppProcessId = _winAppDriverProcessSpotify!.Id;

        AppiumOptions options = new AppiumOptions();
        options.AddAdditionalCapability("deviceName", "WindowsPC");

        WinAppDriver = new WindowsDriver<AppiumWebElement>(new Uri("http://localhost:4723/"), options);
    }

    public static void StopWinAppDriver()
    {
        WinAppDriver?.Quit();

        if (!_winAppDriverProcess!.HasExited)
        {
            _winAppDriverProcess.Kill();
        }

        var spotifyProcess = Process.GetProcessesByName("Spotify").First();
        spotifyProcess.Kill();
        spotifyProcess.WaitForExit();
    }
}
