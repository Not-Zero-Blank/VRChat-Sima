using MelonLoader;
using System;
using Sima.Core;
using System.Collections;

internal class Main : MelonLoader.MelonMod
{
    internal const string Version = "1.0.0.0";
    internal const string Name = "SIMA";
    internal const string Author = "Blank";
    public override void OnApplicationStart()
    {
        AppDomain.CurrentDomain.UnhandledException += HandleException;
        Callbacks.StartCallbacks();
        ModuleManager.RegisterModules();
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnApplicationStart();
        Blanks_Patches.PatchAll(delegate (Blanks_Patches patch, Exception e)
        {
            Logs.Error($"Coudnt Patch  [{patch.basis.Name ?? "null"}] PatchingMethod [{patch.PatchingMethod.Name ?? "null"}] Reason: {e}");
        }, delegate (Blanks_Patches patch, Exception e)
        {
            Logs.Text($"Patched [{patch.basis.Name}] with Method {patch.PatchingMethod.Name}");
        });
        MelonCoroutines.Start(AwaitMenu());
    }
    private static IEnumerator AwaitMenu()
    {
        while (UnityEngine.GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)") == null)
        {
            yield return null;
        }
        ButtonManager.MenuLoaded();
    }
        
    public override void OnApplicationLateStart()
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnApplicationLateStart();
    }
    public override void OnApplicationQuit()
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnApplicationQuit();
    }
    public override void OnFixedUpdate()
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnFixedUpdate();
    }
    public override void OnGUI()
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnGUI();
    }
    public override void OnLateUpdate()
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnLateUpdate();
    }
    public override void OnPreferencesLoaded()
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnPreferencesLoaded();
    }
    public override void OnPreferencesLoaded(string filepath)
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnPreferencesLoaded(filepath);
    }
    public override void OnPreferencesSaved()
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnPreferencesSaved();
    }
    public override void OnPreferencesSaved(string filepath)
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnPreferencesLoaded(filepath);
    }
    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnSceneWasInitialized(buildIndex, sceneName);
    }
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnSceneWasLoaded(buildIndex, sceneName);
    }
    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnSceneWasUnloaded(buildIndex, sceneName);
    }
    public override void OnUpdate()
    {
        foreach (SIMA_Module a in ModuleManager.Modules) a.OnUpdate();
    }
    private void HandleException(object sender, UnhandledExceptionEventArgs e)
    {
        Logs.Error(e.ExceptionObject.ToString());
    }
}

