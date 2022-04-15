
using System;
using UnityEngine;
using XButtonAPI;

namespace ProjectXoX.Utility.ButtonAPI
{
	public interface IButtonPage
	{
        QMSingleButton AddSingle(string text, string tooltip, Action onClick, Sprite sprite = null);
		QMToggleButton AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false);
		QMToggleButton AddToggle(string text, string tooltip, Action<bool> configValue);
		QMNestedButton AddMenuPage(string text, string tooltip = "", Sprite sprite = null);
		QMCategoryPage AddCategoryPage(string text, string tooltip = "", Sprite sprite = null);
		QMNestedButton GetMenuPage(string name);
		QMCategoryPage GetCategoryPage(string name);
	}
}
