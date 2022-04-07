using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sima.Core
{
    public interface IModule
    {
        /// <summary>
        /// Runs when the Module gets Enabled
        /// </summary>
        void OnEnable();
        /// <summary>
        /// Runs when the Module gets Disabled
        /// </summary>
        void OnDisable();
        /// <summary>
        /// Runs when a Scene has Loaded and is passed the Scene's Build Index and Name.
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <param name="sceneName"></param>
        void OnSceneWasLoaded(int buildIndex, string sceneName);
        /// <summary>
        /// Runs when a Scene has Initialized and is passed the Scene's Build Index and Name.
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <param name="sceneName"></param>
        void OnSceneWasInitialized(int buildIndex, string sceneName);
        /// <summary>
        /// Runs when a Scene has Unloaded and is passed the Scene's Build Index and Name.
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <param name="sceneName"></param>
        void OnSceneWasUnloaded(int buildIndex, string sceneName);
        /// <summary>
        /// Can run multiple times per frame. Mostly used for Physics.
        /// </summary>
        void OnFixedUpdate();
        void OnPreSupportModule();
        void OnApplicationStart();
        void OnApplicationLateStart();
        void OnUpdate();
        void OnLateUpdate();
        void OnGUI();
        void OnApplicationQuit();
        void OnPreferencesSaved();
        void OnPreferencesSaved(string filepath);
        void OnPreferencesLoaded();
        void OnPreferencesLoaded(string filepath);
    }
    internal static class ModuleManager
    {
        public class Module
        {
            public Module(Type module)
            {
                try
                {
                    type = module;
                    Logs.Text("Initalizing " + type.Name + "from " + AssemblyName);
                    instance = Activator.CreateInstance(module);
                }
                catch (Exception e)
                {
                    Logs.Error($"Coudnt Load {module.Name} Reason: {e}");
                    return;
                }
                OnEnableMethod = module.GetMethod(nameof(OnEnable));
                OnDisableMethod = module.GetMethod(nameof(OnDisable));
                OnFixedUpdateMethod = module.GetMethod(nameof(OnFixedUpdate));
                OnSceneWasInitializedMethod = module.GetMethod(nameof(OnSceneWasInitialized));
                OnSceneWasLoadedMethod = module.GetMethod(nameof(OnSceneWasLoaded));
                OnSceneWasUnloadedMethod = module.GetMethod(nameof(OnSceneWasUnloaded));
                OnPreSupportModuleMethod = module.GetMethod(nameof(OnPreSupportModule));
                OnApplicationStartMethod = module.GetMethod(nameof(OnApplicationStart));
                OnApplicationLateStartMethod = module.GetMethod(nameof(OnApplicationLateStart));
                OnUpdateMethod = module.GetMethod(nameof(OnUpdate));
                OnLateUpdateMethod = module.GetMethod(nameof(OnLateUpdate));
                OnGUIMethod = module.GetMethod(nameof(OnGUI));
                OnApplicationQuitMethod = module.GetMethod(nameof(OnApplicationQuit));
                OnPreferencesSavedMethod = module.GetMethod(nameof(OnPreferencesSaved), new Type[0]);
                OnPreferencesSaved2Method = module.GetMethod(nameof(OnPreferencesSaved), new Type[] { typeof(System.String) });
                OnPreferencesLoadedMethod = module.GetMethod(nameof(OnPreferencesLoaded), new Type[0]);
                OnPreferencesLoaded2Method = module.GetMethod(nameof(OnPreferencesLoaded), new Type[] { typeof(System.String) });
                Modules.Add(this);
            }
            public Type type;
            public string ModuleName => type.Name;
            public string AssemblyName => type.Assembly.GetName().Name;
            object instance;
            bool enabled = true;
            #region MethodInfos
            MethodInfo OnEnableMethod;
            MethodInfo OnDisableMethod;
            MethodInfo OnFixedUpdateMethod;
            MethodInfo OnSceneWasInitializedMethod;
            MethodInfo OnSceneWasLoadedMethod;
            MethodInfo OnSceneWasUnloadedMethod;
            MethodInfo OnPreSupportModuleMethod;
            MethodInfo OnApplicationStartMethod;
            MethodInfo OnApplicationLateStartMethod;
            MethodInfo OnUpdateMethod;
            MethodInfo OnLateUpdateMethod;
            MethodInfo OnGUIMethod;
            MethodInfo OnApplicationQuitMethod;
            MethodInfo OnPreferencesSavedMethod;
            MethodInfo OnPreferencesSaved2Method;
            MethodInfo OnPreferencesLoadedMethod;
            MethodInfo OnPreferencesLoaded2Method;
            #endregion
            #region Methods
            public void OnEnable()
            {
                try
                {
                    OnEnableMethod.Invoke(instance, null);
                    enabled = true;
                }
                catch (NotImplementedException a) 
                {
                    Logs.Error($"Cant Enable {ModuleName} Its not implemented!");
                    enabled = false;
                }
                catch (Exception e)
                {
                    Logs.Error($"Failed Enabling {ModuleName} Reason: " + e);
                    enabled = false;
                }
            }
            public void OnDisable()
            {
                try
                {
                    OnDisableMethod.Invoke(instance, null);
                    enabled = false;
                }
                catch (NotImplementedException a)
                {
                    Logs.Error($"Cant Enable {ModuleName} Its not implemented!");
                    enabled = false;
                }
                catch (Exception e)
                {
                    Logs.Error($"Failed Disabling {ModuleName} Reason: " + e);
                    enabled = false;
                }
            }
            public void OnSceneWasLoaded(int buildIndex, string sceneName)
            {
                if (!enabled) return;
                if (OnSceneWasLoadedMethod == null) return;
                try
                {
                    OnSceneWasLoadedMethod.Invoke(instance, new object[] { buildIndex, sceneName });
                }
                catch (NotImplementedException a) { OnSceneWasLoadedMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnSceneWasLoaded {ModuleName} Reason: {e}"); }
            }
            public void OnSceneWasInitialized(int buildIndex, string sceneName)
            {
                if (!enabled) return;
                if (OnSceneWasInitializedMethod == null) return;
                try
                {
                    OnSceneWasInitializedMethod.Invoke(instance, new object[] { buildIndex, sceneName });
                }
                catch (NotImplementedException a) { OnSceneWasInitializedMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnSceneWasInitialized {ModuleName} Reason: {e}"); }
            }
            public void OnSceneWasUnloaded(int buildIndex, string sceneName)
            {
                if (!enabled) return;
                if (OnSceneWasUnloadedMethod == null) return;
                try
                {
                    OnSceneWasUnloadedMethod.Invoke(instance, new object[] { buildIndex, sceneName });
                }
                catch (NotImplementedException a) { OnSceneWasUnloadedMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnSceneWasUnloaded {ModuleName} Reason: {e}"); }
            }
            public void OnFixedUpdate()
            {
                if (!enabled) return;
                if (OnFixedUpdateMethod == null) return;
                try
                {
                    OnFixedUpdateMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnFixedUpdateMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnFixedUpdate {ModuleName} Reason: {e}"); }
            }
            public void OnPreSupportModule()
            {
                if (!enabled) return;
                if (OnPreSupportModuleMethod == null) return;
                try
                {
                    OnPreSupportModuleMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnPreSupportModuleMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnPreSupportModule {ModuleName} Reason: {e}"); }
            }
            public void OnApplicationStart()
            {
                if (!enabled) return;
                if (OnApplicationStartMethod == null) return;
                try
                {
                    OnApplicationStartMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnApplicationStartMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnApplicationStart {ModuleName} Reason: {e}"); }
            }
            public void OnApplicationLateStart()
            {
                if (!enabled) return;
                if (OnApplicationLateStartMethod == null) return;
                try
                {
                    OnApplicationLateStartMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnApplicationLateStartMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnApplicationLateStart {ModuleName} Reason: {e}"); }
            }
            public void OnUpdate()
            {
                if (!enabled) return;
                if (OnUpdateMethod == null) return;
                try
                {
                    OnUpdateMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnUpdateMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnUpdate {ModuleName} Reason: {e}"); }
            }
            public void OnLateUpdate()
            {
                if (!enabled) return;
                if (OnLateUpdateMethod == null) return;
                try
                {
                    OnLateUpdateMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnLateUpdateMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnLateUpdate {ModuleName} Reason: {e}"); }
            }
            public void OnGUI()
            {
                if (!enabled) return;
                if (OnGUIMethod == null) return;
                try
                {
                    OnGUIMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnGUIMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnGUI {ModuleName} Reason: {e}"); }
            }
            public void OnApplicationQuit()
            {
                if (!enabled) return;
                if  (OnApplicationQuitMethod == null) return;
                try
                {
                    OnApplicationQuitMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnApplicationQuitMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnApplicationQuit {ModuleName} Reason: {e}"); }
            }
            public void OnPreferencesSaved()
            {
                if (!enabled) return;
                if (OnPreferencesSavedMethod == null) return;
                try
                {
                    OnPreferencesSavedMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnPreferencesSavedMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnPreferencesSaved {ModuleName} Reason: {e}"); }
            }
            public void OnPreferencesSaved(string filepath)
            {
                if (!enabled) return;
                if (OnPreferencesSaved2Method == null) return;
                try
                {
                    OnPreferencesSaved2Method.Invoke(instance, new object[] { filepath });
                }
                catch (NotImplementedException a) { OnPreferencesSaved2Method = null; }
                catch (Exception e) { Logs.Error($"Failed OnPreferencesSaved {ModuleName} Reason: {e}"); }
            }
            public void OnPreferencesLoaded()
            {
                if (!enabled) return;
                if (OnPreferencesLoadedMethod == null) return;
                try
                {
                    OnPreferencesLoadedMethod.Invoke(instance, null);
                }
                catch (NotImplementedException a) { OnPreferencesLoadedMethod = null; }
                catch (Exception e) { Logs.Error($"Failed OnPreferencesLoaded {ModuleName} Reason: {e}"); }
            }
            public void OnPreferencesLoaded(string filepath)
            {
                if (!enabled) return;
                if (OnPreferencesLoaded2Method == null) return;
                try
                {
                    OnPreferencesLoaded2Method.Invoke(instance, new object[] { filepath });
                }
                catch (NotImplementedException a) { OnPreferencesLoaded2Method = null; }
                catch (Exception e) { Logs.Error($"Failed OnPreferencesLoaded {ModuleName} Reason: {e}"); }
            }
            #endregion
        }
        public static List<Module> Modules = new List<Module>();
        public static void RegisterModules()
        {
    var interfacetype = typeof(IModule);
    var types = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => interfacetype.IsAssignableFrom(p) && !p.IsInterface);
    Logs.Warning($"Found [{types.Count()}] Modules");
    foreach (Type a in types) new Module(a);
}
    }
}
