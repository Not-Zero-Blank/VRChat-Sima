using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VRC;

namespace Sima.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class SingleButton : Attribute
    {
        string Parent = "qm";
        public SingleButton()
        {

        }
        public SingleButton(string ParentName)
        {
            Parent = ParentName;
        }
    }
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ToggleButton : Attribute
    {
        string Parent = "qm";
        public ToggleButton()
        {

        }
        public ToggleButton(string ParentName)
        {
            Parent = ParentName;
        }
    }
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class Toggle : Attribute
    {
        public Toggle()
        {

        }
    }
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class WingButton : Attribute
    {
        string Parent = "qm";
        public WingButton()
        {

        }
        public WingButton(string ParentName)
        {
            Parent = ParentName;
        }
    }
    public abstract class SIMA_Module
    {
        /// <summary>
        /// Runs when the Module gets Enabled
        /// </summary>
        //public abstract void OnEnable();
        /// <summary>
        /// Runs when the Module gets Disabled
        /// </summary>
        //public abstract void OnDisable();
        /// <summary>
        /// Runs when a Scene has Loaded and is passed the Scene's Build Index and Name.
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <param name="sceneName"></param>
        public virtual void OnSceneWasLoaded(int buildIndex, string sceneName) { }
        /// <summary>
        /// Runs when a Scene has Initialized and is passed the Scene's Build Index and Name.
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <param name="sceneName"></param>
        public virtual void OnSceneWasInitialized(int buildIndex, string sceneName) { }
        /// <summary>
        /// Runs when a Scene has Unloaded and is passed the Scene's Build Index and Name.
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <param name="sceneName"></param>
        public virtual void OnSceneWasUnloaded(int buildIndex, string sceneName) { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnGUI() { }
        /// <summary>
        /// Can run multiple times per frame. Mostly used for Physics.
        /// </summary>
        public virtual void OnFixedUpdate() { }
        public virtual void OnPreferencesSaved() { }
        public virtual void OnPreferencesSaved(string filepath) { }
        public virtual void OnPreferencesLoaded() { }
        public virtual void OnPreferencesLoaded(string filepath) { }
        /// <summary>
        /// Get Called if the avatar changes
        /// </summary>
        /// <param name="player"></param>
        /// The Player
        /// <param name="oldavatar"></param>
        /// The Avatar the user used before is null if its the first avatar
        /// <param name="newavatar"></param>
        /// The Avatar he Player is now using
        public virtual void OnAvatarReady(VRC.Player player, VRC.Core.ApiAvatar oldavatar, VRC.Core.ApiAvatar newavatar) { }
        public virtual void OnApplicationStart() { }
        public virtual void UIManagerIntialized() { }
        public virtual void OnApplicationQuit() { }
        public virtual void OnWorldJoined(VRC.Core.ApiWorld joinedworld, VRC.Core.ApiWorldInstance joinedinstance) { }
        public virtual void OnWorldLeft() { }
        public virtual void WorldIntialized() { }
        public virtual void PlayerJoined(Player player) { }
        public virtual void PlayerLeft(Player player) { }
        public virtual void OnUIManagerIntialized() { }
    }
    internal class ButtonManager : SIMA_Module
    {
        static Dictionary<string, XButtonAPI.QMNestedButton> Menu = new Dictionary<string, XButtonAPI.QMNestedButton>();
        static XButtonAPI.QMNestedButton MainMenu;
        static List<string> MenusToCreate = new List<string>();
        static List<ButtonInformations> ToggleButtonsToCreate = new List<ButtonInformations>();
        static List<ButtonInformations> TogglePropertysToCreate = new List<ButtonInformations>();
        static List<ButtonInformations> SingleButtonsToCreate = new List<ButtonInformations>();
        static List<ButtonInformations> WingsToCreate = new List<ButtonInformations>();
        public override void OnUIManagerIntialized()
        {
            var category = new XButtonAPI.QMCategory("SIMA MODULES");
            UnityEngine.GameObject.Destroy(UnityEngine.GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners"));
            MainMenu = category.AddMenuPage("SIMA", "SIMA MENU", "A");
            foreach (string menu in MenusToCreate)
            {
                Menu.Add(menu, MainMenu.AddMenuPage(menu, $"{menu}", $"Opening {menu}", false));
            }
            foreach (ButtonInformations Toggle in ToggleButtonsToCreate)
            {
                Logs.Text($"Creating Button for {Toggle.Text} ");
                try
                {
                    string categorytext = Toggle.Category;
                    string menutext = Toggle.Text;
                    XButtonAPI.QMNestedButton value;
                    Menu.TryGetValue(categorytext, out value);
                    value.AddButtonToggle($"{menutext} ON", delegate ()
                    {
                        Toggle.method.Invoke(Toggle.instance, new object[] { true });
                    }, $"{menutext} OFF", delegate ()
                    {
                        Toggle.method.Invoke(Toggle.instance, new object[] { false });
                    });
                }
                catch
                {
                    Logs.Error($"Failed!");
                }
            }
            foreach (ButtonInformations Toggle in TogglePropertysToCreate)
            {
                Logs.Text($"Creating Button for {Toggle.Text} ");
                try
                {
                    string categorytext = Toggle.Category;
                    string menutext = Toggle.Text;
                    XButtonAPI.QMNestedButton value;
                    Menu.TryGetValue(categorytext, out value);
                    bool state = (bool)Toggle.property.GetValue(Toggle.instance);
                    value.AddButtonToggle($"{menutext} ON", delegate ()
                    {
                        bool on = (bool)Toggle.property.GetValue(Toggle.instance);
                        Toggle.property.SetValue(Toggle.instance, !on);
                    }, $"{menutext} OFF", delegate ()
                    {
                        bool off = (bool)Toggle.property.GetValue(Toggle.instance);
                        Toggle.property.SetValue(Toggle.instance, !off);
                    }, state);
                }
                catch
                {
                    Logs.Error($"Failed!");
                }
            }
            foreach (ButtonInformations Single in SingleButtonsToCreate)
            {
                Logs.Text($"Creating Button for {Single.Text} ");
                try
                {
                    string categorytext = Single.Category;
                    string menutext = Single.Text;
                    XButtonAPI.QMNestedButton value;
                    Menu.TryGetValue(categorytext, out value);
                    value.AddButton($"{menutext}", $"", delegate ()
                    {
                        Single.method.Invoke(Single.instance, null);
                    });
                }
                catch
                {
                    Logs.Error($"Failed!");
                }
            }
        }
        public static void ToggleButton(Type type, MethodBase button)
        {
            var info = new ButtonInformations(type, button);
            if (!MenusToCreate.Contains(info.Category))
            {
                MenusToCreate.Add(info.Category);
            }
            ToggleButtonsToCreate.Add(info);
        }
        public static void ToggleButtonProperty(Type type, FieldInfo button)
        {
            var info = new ButtonInformations(type, button);
            if (!MenusToCreate.Contains(info.Category))
            {
                MenusToCreate.Add(info.Category);
            }
            TogglePropertysToCreate.Add(info);
        }
        public static void SingleButton(Type type, MethodBase button)
        {
            var info = new ButtonInformations(type, button);
            if (!MenusToCreate.Contains(info.Category))
            {
                MenusToCreate.Add(info.Category);
            }
            SingleButtonsToCreate.Add(info);
        }
    }
    class ButtonInformations
    {
        public ButtonInformations(Type type, MethodBase button)
        {
            Category = type.Namespace.Split('.').Last();
            Text = button.Name;
            method = button;
            instance = Activator.CreateInstance(type);
        }
        public ButtonInformations(Type type, FieldInfo button)
        {
            Category = type.Namespace.Split('.').Last();
            Text = button.Name;
            property = button;
            instance = Activator.CreateInstance(type);
        }
        public string Category;
        public string Text;
        public MethodBase method;
        public FieldInfo property;
        public Object instance;
    }
    internal static class ModuleManager
    {
        static Type toggle = typeof(ToggleButton);
        static Type proptoggle = typeof(Toggle);
        static Type single = typeof(SingleButton);
        static Type wing = typeof(WingButton);
        public class ModuleV2
        {
            public ModuleV2(Type module)
            {
                try
                {
                    type = module;
                    if (AssemblyName != "SIMA")
                    {
                        Logs.Warning("Initalizing " + type.Name + " from " + AssemblyName);
                    }
                    else
                    {
                        Logs.Text($"[{Category}] Loading " + type.Name + " module");
                    }
                    var x = (SIMA_Module)Activator.CreateInstance(module);
                    Modules.Add(x);
                    foreach (MethodBase Button in module.GetMethods())
                    {

                        foreach (var Attribute in Button.CustomAttributes)
                        {
                            if (Attribute.AttributeType == toggle)
                            {
                                ButtonManager.ToggleButton(module, Button);
                            }
                            else if (Attribute.AttributeType == single)
                            {
                                ButtonManager.SingleButton(module, Button);
                            }
                            else if (Attribute.AttributeType == wing)
                            {
                                Logs.Error($"Wingbutton in {module} is not Implemented!");
                            }
                        }
                    }
                    foreach (FieldInfo Button in module.GetFields())
                    {
                        foreach (var Attribute in Button.CustomAttributes)
                        {
                            if (Attribute.AttributeType == proptoggle)
                            {
                                ButtonManager.ToggleButtonProperty(module, Button);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logs.Error($"Coudnt load {AssemblyName} [{Category}] Reason: {e.ToString()}");
                }
            }
            public string AssemblyName => type.Name;
            public string Category => type.Namespace.Split('.').Last();
            public Type type;
        }
        public static List<SIMA_Module> Modules = new List<SIMA_Module>();
        public static void RegisterModules()
        {
            var interfacetype = typeof(SIMA_Module);
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            List<Type> Modules = new List<Type>();
            foreach (Assembly a in assemblys)
            {
                try
                {
                    types.AddRange(a.GetTypes());
                }
                catch (System.Reflection.ReflectionTypeLoadException reflect)
                {
                    types.AddRange(reflect.Types);
                }
            }
            foreach (Type a in types)
            {
                if (interfacetype.IsAssignableFrom(a) && !a.IsAbstract)
                {
                    Modules.Add(a);
                }
            }
            Logs.Warning($"Found [{Modules.Count()-1}] Modules");
            foreach (Type a in Modules) new ModuleV2(a);
            Modules.Clear();
            types.Clear();
        }
    }
}
