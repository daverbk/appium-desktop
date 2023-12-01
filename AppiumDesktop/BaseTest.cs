namespace AppiumDesktop;

public class BaseTest
{
    [OneTimeSetUp]
    public void StartWindowsDriver() => WinAppDriverProvider.StartWinAppDriver();

    [OneTimeTearDown]
    public void StopCurrentDriver() => WinAppDriverProvider.StopWinAppDriver();
}
