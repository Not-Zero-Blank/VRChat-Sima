using System;
using System.Collections;
using System.Linq;
using MelonLoader;
using XButtonAPI;
using UnityEngine;
using VRC.Core;
using VRC.UI.Core;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace ProjectXoX.Utility.Extension
{
    public static class ButtonExtension
    {
        private delegate void OnValueChangedDelegate(object toggleIcon, bool arg0);
        private static OnValueChangedDelegate _onValueChanged;

        public static void OnValueChanged(this object toggleIcon, bool arg0)
        {
            if (_onValueChanged == null) _onValueChanged = (OnValueChangedDelegate)Delegate.CreateDelegate(typeof(OnValueChangedDelegate), toggleIcon.GetType().GetMethods().FirstOrDefault(m => m.Name.StartsWith("Method_Private_Void_Boolean_PDM_") && QMStuff.CheckMethod(m, "Toggled")));
            _onValueChanged(toggleIcon, arg0);
        }

        private delegate void PushPageDelegate(MenuStateController menuStateCtrl, string pageName, UIContext uiContext, bool clearPageStack);
        private static PushPageDelegate _pushPage;

        public static void PushPage(this MenuStateController menuStateCtrl, string pageName, UIContext uiContext = null, bool clearPageStack = false)
        {
            if (_pushPage == null) _pushPage = (PushPageDelegate)Delegate.CreateDelegate(typeof(PushPageDelegate), typeof(MenuStateController).GetMethods().FirstOrDefault(m => m.Name.StartsWith("Method_Public_Void_String_UIContext_Boolean_") && QMStuff.CheckMethod(m, "No page named")));
            _pushPage(menuStateCtrl, pageName, uiContext, clearPageStack);
        }
        
        private delegate void SwitchToRootPageDelegate(MenuStateController menuStateCtrl, string pageName, UIContext uiContext, bool clearPageStack, bool inPlace);
        private static SwitchToRootPageDelegate _switchToRootPage;

        public static void SwitchToRootPage(this MenuStateController menuStateCtrl, string pageName, UIContext uiContext = null, bool clearPageStack = false, bool inPlace = false)
        {
            if (_switchToRootPage == null) _switchToRootPage = (SwitchToRootPageDelegate)Delegate.CreateDelegate(typeof(SwitchToRootPageDelegate), typeof(MenuStateController).GetMethods().FirstOrDefault(m => m.Name.StartsWith("Method_Public_Void_String_UIContext_Boolean_") && QMStuff.CheckMethod(m, "UIPage not in root page list:")));
            _switchToRootPage(menuStateCtrl, pageName, uiContext, clearPageStack, inPlace);
        }
    }
}
