using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

public class App : IExternalApplication
{
    public Result OnStartup(UIControlledApplication application)
    {
        // Create a custom ribbon tab
        string tabName = "Parameters";
        application.CreateRibbonTab(tabName);

        // Add a new ribbon panel
        RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Parameter Tools");

        // Create a push button for the parameter scanner
        PushButtonData buttonData = new PushButtonData(
            "ParameterScanner",
            "Parameter Scanner",
            Assembly.GetExecutingAssembly().Location,
            "ParameterScanner.ParameterScannerCommand");

        PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;
        pushButton.LargeImage = new BitmapImage(new Uri("C:/Users/Bruno/source/repos/Revit Add-in Project/ClassLibrary1/Autodesk-Revit-icon.png"));

        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }
}
