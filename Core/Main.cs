using MelonLoader;
using QuickMenuLib;
using System;
using Sima.Core;

internal class Main : MelonLoader.MelonMod
{
    internal const string Version = "1.0.0.0";
    internal const string Name = "Sima";
    internal const string Author = "Blank";
    public override void OnApplicationStart()
    {
        AppDomain.CurrentDomain.UnhandledException += HandleException;
        Console.WriteLine("Hello");
        ModuleManager.RegisterModules();
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnApplicationStart();
    }
    public override void OnApplicationLateStart()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnApplicationLateStart();
    }
    public override void OnApplicationQuit()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnApplicationQuit();
    }
    public override void OnFixedUpdate()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnFixedUpdate();
    }
    public override void OnGUI()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnGUI();
    }
    public override void OnLateUpdate()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnLateUpdate();
    }
    public override void OnPreferencesLoaded()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnPreferencesLoaded();
    }
    public override void OnPreferencesLoaded(string filepath)
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnPreferencesLoaded(filepath);
    }
    public override void OnPreferencesSaved()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnPreferencesSaved();
    }
    public override void OnPreferencesSaved(string filepath)
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnPreferencesLoaded(filepath);
    }
    public override void OnPreSupportModule()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnPreSupportModule();
    }
    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnSceneWasInitialized(buildIndex, sceneName);
    }
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnSceneWasLoaded(buildIndex, sceneName);
    }
    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnSceneWasUnloaded(buildIndex, sceneName);
    }
    public override void OnUpdate()
    {
        foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnUpdate();
    }
    private void HandleException(object sender, UnhandledExceptionEventArgs e)
    {
        Logs.Error(e.ExceptionObject.ToString());
    }
}
internal class Menu : ModMenu
{
    public override string MenuName => Main.Name;
    internal Menu()
    {
        Logo = null; // You can put a sprite here and QuickMenuLib will automatically add it.
    }
    public override void OnQuickMenuInitialized()
    {
        var category = MyModMenu.AddMenuCategory("TestModMenuCat");
        category.AddButton("Test", "This is a test using QuickMenuLib!", () =>
        {
            MelonLogger.Msg("Test Button!");
        });

        MyModMenu.AddSlider("FunnySlider", "Test", (num) => MelonLogger.Msg(num));
    }
    public override void OnWingMenuLeftInitialized()
    {
        MyLeftWingMenu.AddButton("Test", "Test using QuickMenuLib!", () =>
        {
            MelonLogger.Msg("Test Wing Button!");
        });
    }
    public override void OnWingMenuRightInitialized()
    {
        MyRightWingMenu.AddButton("Test", "Test using QuickMenuLib!", () =>
        {
            MelonLogger.Msg("Test Wing Button!");
        });
    }
    public override void OnTargetMenuInitialized()
    {
        MyTargetMenu.AddButton("Test", "Test using QuickMenuLib!", () =>
        {
            MelonLogger.Msg(SelectedUser.prop_String_0);
        });
    }
}

