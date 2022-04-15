using ProjectXoX.Utility;
using ProjectXoX.Utility.ButtonAPI;
using ProjectXoX.Utility.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;
using Object = UnityEngine.Object;

namespace XButtonAPI
{
    public static class Utils
    {
        public static Sprite SetSprite(this string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            else
            {
                byte[] array = new WebClient().DownloadData(url);
                bool flag2 = array == null || array.Length == 0;
                if (flag2)
                {
                    return null;
                }
                else
                {
                    Texture2D texture2D = new Texture2D(512, 512);
                    bool flag3 = !Il2CppImageConversionManager.LoadImage(texture2D, array);
                    if (flag3)
                    {
                        return null;
                    }
                    else
                    {
                        Sprite sprite = Sprite.CreateSprite(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect, default(Vector4), false);
                        sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                        return sprite;
                    }
                }
            }
        }
        public static Sprite GetSprite(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            else
            {
                byte[] array = new WebClient().DownloadData(url);
                bool flag2 = array == null || array.Length == 0;
                if (flag2)
                {
                    return null;
                }
                else
                {
                    Texture2D texture2D = new Texture2D(512, 512);
                    bool flag3 = !Il2CppImageConversionManager.LoadImage(texture2D, array);
                    if (flag3)
                    {
                        return null;
                    }
                    else
                    {
                        Sprite sprite = Sprite.CreateSprite(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect, default(Vector4), false);
                        sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                        return sprite;
                    }
                }
            }
        }
    }
    public static class XButtonAPI
    {
        public static List<QMSingleButton> allSingleButtons = new List<QMSingleButton>();
        public static List<QMEmptyButton> allEmpty = new List<QMEmptyButton>();
        public static List<QMSingleToggleButton> allSingleToggleButtons = new List<QMSingleToggleButton>();
        public static List<QMToggleButton> allToggleButtons = new List<QMToggleButton>();
        public static List<QMNestedButton> allNestedButtons = new List<QMNestedButton>();
        public static List<QMLabel> allLabelsButton = new List<QMLabel>();
        public static List<QMSlider> allSliders = new List<QMSlider>(); //Broken

        public static List<QMEmptyWing> allEmptyWings = new List<QMEmptyWing>();
        public static List<QMSingleWing> allSingleWings = new List<QMSingleWing>();
        public static List<QMToggleWing> allToggleWings = new List<QMToggleWing>();
        public static List<QMNestedWing> allnestedWings = new List<QMNestedWing>();

        public static List<QMCategoryPage> allcategorypages = new List<QMCategoryPage>();

        public static List<QMTabMenu> allTabsButton = new List<QMTabMenu>(); //Broken
    }
    public class QMWingBase
    {
        public string Name { get; }
        public GameObject GameObject { get; }
        public RectTransform RectTransform { get; }
        public QMWingBase(GameObject original, Transform parent, Vector3 pos, string name, bool defaultState = true) : this(original, parent, name, defaultState)
        {
            GameObject.transform.localPosition = pos;
        }

        public QMWingBase(GameObject original, Transform parent, string name, bool defaultState = true)
        {
            GameObject = Object.Instantiate(original, parent);
            GameObject.name = GetCleanName(name);
            Name = GameObject.name;

            GameObject.SetActive(defaultState);
            RectTransform = GameObject.GetComponent<RectTransform>();
        }
        public static string GetCleanName(string name)
        {
            return Regex.Replace(Regex.Replace(name, "<.*?>", string.Empty), @"[^0-9a-zA-Z_]+", string.Empty);
        }
        public void ClearScroll(ScrollRect scrollRect)
        {
            foreach (var obj in scrollRect.content)
            {
                if (obj.Cast<Transform>() == null) continue;
                Object.Destroy(obj.Cast<Transform>().gameObject);
            }
        }
        public void SetUIPage(UIPage uiPage, string _menuName, MenuStateController menuStateController)
        {
            uiPage.field_Public_String_0 = _menuName;
            uiPage.field_Private_Boolean_1 = true;
            uiPage.field_Protected_MenuStateController_0 = menuStateController;
            uiPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            uiPage.field_Private_List_1_UIPage_0.Add(uiPage);

            menuStateController.field_Private_Dictionary_2_String_UIPage_0.Add(uiPage.field_Public_String_0, uiPage);
        }

        public void SetActive(bool isActive)
        {
            GameObject.SetActive(isActive);
        }
        public void SetIntractable(bool isIntractable)
        {
            GameObject.GetComponent<Button>().interactable = isIntractable;
        }
        public void SetAction(System.Action buttonAction)
        {
            GameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            GameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.GetComponent<Button>().onClick.AddListener(UnhollowerRuntimeLib.DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
        }
        public void SetTooltip(string tooltip)
        {
            GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = tooltip;
            GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_1 = tooltip;
        }
        public void SetText(string buttonText)
        {
            GameObject.transform.Find("Container/Text_QM_H3").GetComponent<TextMeshProUGUI>().text = buttonText;
            GameObject.transform.Find("Container/Text_QM_H3").GetComponent<TextMeshProUGUI>().richText = true;
        }
        public void SetPageText(string PageText)
        {
            RectTransform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = PageText;
            RectTransform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().richText = true;
        }
        public void SetTextPosition(Vector2 pos)
        {
            GameObject.transform.Find("Container/Text_QM_H3").GetComponent<RectTransform>().anchoredPosition = pos;
        }
        public void SetBackround(string sprite)
        {
            GameObject.transform.Find("Background").gameObject.GetComponent<Image>().sprite = sprite.SetSprite();
        }
        public void SetIcon(string sprite)
        {
            GameObject.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite.SetSprite();
        }
        public void SeticonArrow(string sprite)
        {
            GameObject.transform.Find("Icon_Arrow").gameObject.GetComponent<Image>().sprite = sprite.SetSprite();
        }
        public void SetBackround(bool active)
        {
            GameObject.transform.Find("Background").gameObject.SetActive(active);
        }
        public void SetIcon(bool active)
        {
            if (active)
                SetTextPosition(new Vector2(50f, 0f));

            GameObject.transform.Find("Icon").gameObject.SetActive(active);
        }
        public void SeticonArrow(bool active)
        {
            GameObject.transform.Find("Icon_Arrow").gameObject.SetActive(active);
        }
    }
    public class QMButtonBase
    {
        public string Name { get; }
        public GameObject GameObject { get; }
        public RectTransform RectTransform { get; }
        public QMButtonBase(GameObject original, Transform parent, Vector3 pos, string name, bool defaultState = true) : this(original, parent, name, defaultState)
        {
            GameObject.transform.localPosition = pos;
        }

        public QMButtonBase(GameObject original, Transform parent, string name, bool defaultState = true)
        {
            GameObject = Object.Instantiate(original, parent);
            GameObject.name = GetCleanName(name);
            Name = GameObject.name;

            GameObject.SetActive(defaultState);
            RectTransform = GameObject.GetComponent<RectTransform>();
        }
        public UIPage SetUIPage(string _menuName)
        {
            UIPage UiPage = GameObject.AddComponent<UIPage>();
            UiPage.field_Public_String_0 = $"QuickMenu{_menuName}";
            UiPage.field_Private_Boolean_1 = true;
            UiPage.field_Protected_MenuStateController_0 = QMStuff.MenuStateCtrl;
            UiPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            UiPage.field_Private_List_1_UIPage_0.Add(UiPage);

            QMStuff.MenuStateCtrl.field_Private_Dictionary_2_String_UIPage_0.Add(UiPage.field_Public_String_0, UiPage);

            return UiPage;
        }
        public void ClearScroll(Transform transform)
        {
            foreach (var obj in transform)
            {
                if (obj.Cast<Transform>() == null) continue;
                Object.Destroy(obj.Cast<Transform>().gameObject);
            }
        }
        public void SetScrollRect()
        {
            RectTransform.GetComponentInChildren<ScrollRect>().gameObject.SetActive(true);
            RectTransform.GetComponentInChildren<ScrollRect>().enabled = true;
            RectTransform.GetComponentInChildren<ScrollRect>().verticalScrollbar = RectTransform.GetComponentInChildren<ScrollRect>().transform.Find("Scrollbar").GetComponent<Scrollbar>();
            RectTransform.GetComponentInChildren<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
            RectTransform.GetComponentInChildren<ScrollRect>().viewport.GetComponent<RectMask2D>().enabled = true;

        }
        public void SetGridLayout(Transform transform)
        {
            GridLayoutGroup gridLayoutGroup = transform.Find("Buttons").GetComponent<GridLayoutGroup>();
            GridLayoutGroup gridLayout = transform.gameObject.AddComponent<GridLayoutGroup>();
            gridLayout.spacing = gridLayoutGroup.spacing;
            gridLayout.cellSize = gridLayoutGroup.cellSize;
            gridLayout.constraint = gridLayoutGroup.constraint;
            gridLayout.constraintCount = gridLayoutGroup.constraintCount;
            gridLayout.startAxis = gridLayoutGroup.startAxis;
            gridLayout.startCorner = gridLayoutGroup.startCorner;
            gridLayout.childAlignment = TextAnchor.UpperLeft;
            gridLayout.padding = gridLayoutGroup.padding;
            gridLayout.padding.top = 8;
            gridLayout.padding.left = 64;
        }
        public void SetPageText(string PageText)
        {
            RectTransform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = PageText;
            RectTransform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().richText = true;
        }
        public void SetText(string text)
        {
            RectTransform.GetComponentInChildren<TextMeshProUGUI>().text = text;
            RectTransform.GetComponentInChildren<TextMeshProUGUI>().richText = true;
        }
        public void SetIfSprite(Sprite sprite)
        {
            var Text = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            if (sprite == null)
            {
                Text.fontSize = 30;
                Text.transform.localPosition = new Vector3(Text.transform.localPosition.x, -25f);

                Object.DestroyImmediate(RectTransform.Find("Icon").gameObject);
            }
            else
            {
                var iconImage = RectTransform.Find("Icon").GetComponent<Image>();
                iconImage.sprite = sprite;
                iconImage.overrideSprite = sprite;
            }
        }
        public void SetAction(Action onClick)
        {
            GameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            GameObject.GetComponent<Button>().onClick.AddListener(new Action(onClick));
        }
        public void SetTooltip(string tooltip, string Alternativ)
        {
            RectTransform.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = tooltip;
            RectTransform.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_1 = Alternativ;
        }


        public static string GetCleanName(string name)
        {
            return Regex.Replace(Regex.Replace(name, "<.*?>", string.Empty), @"[^0-9a-zA-Z_]+", string.Empty);
        }

        public void DestroyMe()
        {
            try
            {
                Object.Destroy(GameObject);
            }
            catch { }
        }

        public void SetIcon(string url)
        {
            RectTransform.Find("Icon").GetComponent<Image>().sprite = url.SetSprite();
        }
        public void SetIcon(bool Activate)
        {
            RectTransform.Find("Icon").gameObject.SetActive(Activate);
        }
    }
    public class QMSlider : QMButtonBase
    {
        public TextMeshProUGUI TextComponent;
        public TextMeshProUGUI Percent;

        private readonly Slider Slider;
        public QMSlider(string Title, Action<float> onValueChanged, Transform transform, bool ShowPercent,string Tooltip, string percent = "", float maxValue = 100f, float MinValue = 0f) : base (QMStuff.SliderTemplate(), transform, $"ValueSlider_{Title}")
        {
            this.TextComponent = base.RectTransform.Find("Text_QM_H4").GetComponent<TextMeshProUGUI>();
            if (ShowPercent)
            {
                this.Percent = base.RectTransform.Find("Text_QM_H4 (1)").GetComponent<TextMeshProUGUI>();
                this.Percent.text = percent;
            }
            else
            {
                GameObject.DestroyImmediate(base.RectTransform.Find("Text_QM_H4 (1)").gameObject);
            }
            this.TextComponent.richText = true;
            this.TextComponent.text = Title;

            base.SetTooltip(Tooltip, Tooltip);

            this.Slider = GameObject.GetComponentInChildren<Slider>();
            this.Slider.minValue = MinValue;
            this.Slider.maxValue = maxValue;
            this.Slider.onValueChanged = new Slider.SliderEvent();
            this.Slider.onValueChanged.AddListener(new Action<float>(onValueChanged));

            XButtonAPI.allSliders.Add(this);

        }
    }
    public class QMLabel : QMButtonBase
    {
        public void SetSubtitle(string text)
        {
            var SubtitleTextComponent = base.RectTransform.Find("Text_H4").GetComponent<TextMeshProUGUI>();
            SubtitleTextComponent.richText = true;
            SubtitleTextComponent.text = text;
        }
        public void SetText(string text, int fontsize = 56)
        {
            var textMesh = base.RectTransform.Find("Text_H1").GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = text;
            textMesh.fontSize = fontsize;
        }
        public QMLabel(Transform transform, string text, string subtitleText, int fontsize = 56) : base(QMStuff.LabelTemplate(), transform, $"Label_{subtitleText}")
        {
            GameObject.DestroyImmediate(base.RectTransform.Find("Text_H1").GetComponent<TextBinding>());
            this.SetText(text, fontsize);
            this.SetSubtitle(subtitleText);

            XButtonAPI.allLabelsButton.Add(this);
        }
    }
    public class QMNestedWing : QMWingBase
    {
        private readonly Wing _wing;
        private readonly string _menuName;
        private readonly Transform _container;
        public QMSingleWing SingleWing;
        public QMNestedWing(string ButtonText,string PageText, bool left = false, string tooltip = "", Sprite sprite = null) : base(QMStuff.WingMenuTemplate(), (left ? QMStuff.LeftWing : QMStuff.RightWing).field_Public_RectTransform_0, $"{(left ? "Left" : "Right")}Wing{GetCleanName(PageText)}", false)
        {
            _menuName = GetCleanName(PageText);
            _wing = left ? QMStuff.LeftWing : QMStuff.RightWing;
            _container = RectTransform.GetComponentInChildren<ScrollRect>().content;
            RectTransform.GetComponentInChildren<VerticalLayoutGroup>().spacing = -20;

            base.SetPageText(PageText);
            base.ClearScroll(RectTransform.GetComponentInChildren<ScrollRect>());
            base.SetUIPage(GameObject.GetComponent<UIPage>(), _menuName, _wing.field_Private_MenuStateController_0);
            RectTransform.GetChild(0).GetComponentInChildren<Button>(true).gameObject.SetActive(true);
            RectTransform.GetChild(0).GetComponentInChildren<Button>(true).transform.Find("Icon").gameObject.SetActive(true);

            var components = new Il2CppSystem.Collections.Generic.List<Behaviour>();
            RectTransform.GetChild(0).GetComponentInChildren<Button>(true).GetComponents(components);

            foreach (var comp in components) comp.enabled = true;

            SingleWing = new QMSingleWing(ButtonText, tooltip, () =>
            {
                _wing.field_Private_MenuStateController_0.Method_Public_Void_String_UIContext_Boolean_0(_menuName);
            }, sprite, left);

            XButtonAPI.allnestedWings.Add(this);
        }
        public QMNestedWing(Transform transform, string PageText, bool left = false, string tooltip = "", Sprite sprite = null) : base(QMStuff.WingMenuTemplate(), (left ? QMStuff.LeftWing : QMStuff.RightWing).field_Public_RectTransform_0, $"{(left ? "Left" : "Right")}Wing{GetCleanName(PageText)}", false)
        {
            _menuName = GetCleanName(PageText);
            _wing = left ? QMStuff.LeftWing : QMStuff.RightWing;
            _container = RectTransform.GetComponentInChildren<ScrollRect>().content;
            RectTransform.GetComponentInChildren<VerticalLayoutGroup>().spacing = -20;

            base.SetPageText(PageText);
            base.ClearScroll(RectTransform.GetComponentInChildren<ScrollRect>());
            base.SetUIPage(GameObject.GetComponent<UIPage>(), _menuName, _wing.field_Private_MenuStateController_0);
            RectTransform.GetChild(0).GetComponentInChildren<Button>(true).gameObject.SetActive(true);
            RectTransform.GetChild(0).GetComponentInChildren<Button>(true).transform.Find("Icon").gameObject.SetActive(true);

            var components = new Il2CppSystem.Collections.Generic.List<Behaviour>();
            RectTransform.GetChild(0).GetComponentInChildren<Button>(true).GetComponents(components);

            foreach (var comp in components) comp.enabled = true;

            SingleWing = new QMSingleWing(PageText, tooltip, () =>
            {
                _wing.field_Private_MenuStateController_0.Method_Public_Void_String_UIContext_Boolean_0(_menuName);
            }, transform, false, true, false, sprite);

            XButtonAPI.allnestedWings.Add(this);
        }
        public QMEmptyWing AddEmpty()
        {
            return new QMEmptyWing(_container);
        }
        public QMSingleWing AddButton(string text, string tooltip, Action onClick, bool arrow = true, bool background = true, bool separator = false, Sprite sprite = null)
        {
            return new QMSingleWing(text, tooltip, onClick, _container, arrow, background, separator, sprite);
        }
        public QMToggleWing AddToggle(string TextON, Action ON, string TextOFF, Action OFF, bool DefaultPosition, bool arrow = true, bool background = true, bool Icon = true, bool Separator = false)
        {
            return new QMToggleWing(_container, TextON, ON, TextOFF, OFF, DefaultPosition, background, arrow, Icon, Separator);
        }

        public QMNestedWing AddSubMenu(string text, string tooltip, bool left = true, Sprite sprite = null)
        {
            return new QMNestedWing(_container, text, left, tooltip, sprite);
        }
        public QMNestedButton AddMenuPage(bool root, string ButtonText, string PageText, string tooltip, Sprite sprite = null)
        {
            return new QMNestedButton(_container, ButtonText, PageText, root, tooltip, sprite);
        }
        public QMCategoryPage AddCatergory(bool root, string text, string tooltip,Sprite sprite = null)
        {
            return new QMCategoryPage(_container, text, root, true, tooltip, sprite);
        }
    }
    public class QMNestedButton : QMButtonBase, IButtonPage
    {
        public Transform _container;
        public Transform menuContents;
        public UIPage UiPage { get; }
        public QMSingleButton MenuButton;
        public QMNestedButton(Transform transform, string ButtonText,string PageText, bool isRoot = false, string tooltip = "", Sprite sprite = null) : base(QMStuff.NestedMenuTemplate(), QMStuff.NestedMenuTemplate().transform.parent, $"Menu_{PageText}", false)
        {
            var Name = GetCleanName(PageText);
            RectTransform.GetChild(0).name = $"Header_{Name}";
            Object.DestroyImmediate(GameObject.GetComponent<DevMenu>());
            if (!isRoot) RectTransform.GetChild(0).GetComponentInChildren<Button>(true).gameObject.SetActive(true);

            base.SetPageText(PageText);
            base.ClearScroll(RectTransform.Find("Scrollrect/Viewport/VerticalLayoutGroup/Buttons"));
            this.UiPage = base.SetUIPage(Name);
            this._container = RectTransform.GetComponentInChildren<ScrollRect>().content;
            this.menuContents = GameObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");

            Object.DestroyImmediate(_container.GetComponent<VerticalLayoutGroup>());
            base.SetGridLayout(_container);
            base.SetScrollRect();

            Object.DestroyImmediate(_container.Find("Buttons").gameObject);
            Object.DestroyImmediate(_container.Find("Spacer_8pt").gameObject);

            if (isRoot)
            {
                QMStuff.MenuStateCtrl.field_Public_ArrayOf_UIPage_0.ToList().Add(UiPage);
                QMStuff.MenuStateCtrl.field_Public_ArrayOf_UIPage_0 = QMStuff.MenuStateCtrl.field_Public_ArrayOf_UIPage_0.ToList().ToArray();
            }


            MenuButton = new QMSingleButton(ButtonText, tooltip, () =>
            {
                if (isRoot) QMStuff.MenuStateCtrl.SwitchToRootPage($"QuickMenu{Name}");
                else QMStuff.MenuStateCtrl.PushPage($"QuickMenu{Name}");
            }, transform, sprite);

            XButtonAPI.allNestedButtons.Add(this);
        }
        public QMSlider AddSlider(string Title, Action<float> Value, bool ShowPercent, string Percent = "",string ToolTip = null, float maxValue = 100f, float MinValue = 0f)
        {
            return new QMSlider(Title, Value, menuContents, ShowPercent, Percent, ToolTip, MinValue, maxValue);
        }
        public QMSingleToggleButton AddButtonToggle(String TextON, Action ActionON, String TextOFF, Action ActionOFF, bool postion = false, string TooltipON = null, string ToolTipOFF = null)
        {
            return new QMSingleToggleButton(_container, TextON, ActionON, TextOFF, ActionOFF, postion, TooltipON, ToolTipOFF);
        }
        public QMEmptyButton AddPlaceHolder()
        {
            return new QMEmptyButton(_container);
        }
        public QMLabel AddLabel(string text, string Subtitle, int FontSize = 56)
        {
            return new QMLabel(_container, text, Subtitle, FontSize);
        }
        public QMSingleButton AddButton(string text, string tooltip, Action onClick, Sprite sprite = null)
        {
            return new QMSingleButton(text, tooltip, onClick, _container, sprite);
        }

        public QMToggleButton AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false)
        {
            return new QMToggleButton(text, tooltip, onToggle, _container, defaultValue);
        }

        public QMNestedButton AddMenuPage(string ButtonText, string PageText, string tooltip = "", bool IsRoot = false, Sprite sprite = null)
        {
            return new QMNestedButton(_container, ButtonText, PageText, IsRoot, tooltip, sprite);
        }

        public QMCategoryPage AddCategoryPage(string text, string tooltip = "",Sprite sprite = null)
        {
            if (GetCategoryPage(text) != null) return GetCategoryPage(text);
            return new QMCategoryPage(_container, text, false, false, tooltip, sprite);
        }

        public QMNestedButton GetMenuPage(string name)
        {
            return XButtonAPI.allNestedButtons.FirstOrDefault(m => m.Name == GetCleanName($"Menu_{name}"));
        }

        public QMCategoryPage GetCategoryPage(string name)
        {
            return XButtonAPI.allcategorypages.FirstOrDefault(m => m.Name == GetCleanName($"Menu_{name}"));
        }

        public static QMNestedButton Create(Transform transform, string ButtonText, string PageText, bool isRoot, string tooltip = "", Sprite sprite = null)
        {
            return new QMNestedButton(transform, ButtonText, PageText, isRoot, tooltip, sprite);
        }

        public QMToggleButton AddToggle(string text, string tooltip, Action<bool> configValue)
        {
            return new QMToggleButton(text, tooltip, configValue, _container);
        }

        public QMNestedButton AddMenuPage(string ButtonText, string PageText, string tooltip = "", Sprite sprite = null)
        {
            return new QMNestedButton(_container, ButtonText, PageText, false, tooltip, sprite);
        }

        public QMSingleButton AddSingle(string text, string tooltip, Action onClick, Sprite sprite = null)
        {
            throw new NotImplementedException();
        }

        public QMNestedButton AddMenuPage(string text, string tooltip = "", Sprite sprite = null)
        {
            throw new NotImplementedException();
        }

    }
    public class QMToggleWing : QMWingBase
    {
        public Transform container;
        public GameObject backround;
        public GameObject icon;
        public GameObject icon_arrow;
        private bool state { get; set; }
        private string OnText { get; set; }
        private string OffText { get; set; }
        private Action OffAction { get; set; }
        private Action OnAction { get; set; }

        public QMToggleWing(Transform transform, String btnTextOn, System.Action btnActionOn, String btnTextOff, System.Action btnActionOff, bool position = false, bool backroundshow = true, bool arrow = true, bool iconshow = true, bool Separator = false) : base(QMStuff.SingleWingTemplate(), transform, $"Button_{btnTextOn}")
        {
            container = RectTransform.Find("Container").transform;
            container.Find("Background").gameObject.SetActive(backroundshow);
            container.Find("Icon_Arrow").gameObject.SetActive(arrow);
            RectTransform.Find("Separator").gameObject.SetActive(Separator);
            var iconImage = container.Find("Icon").GetComponent<Image>();
            iconImage.gameObject.SetActive(false);

            if (!iconshow)
                SetTextPosition(new Vector2(50f, 0f));

            SetText(position ? btnTextOn : btnTextOff);
            OnText = btnTextOn;
            OffText = btnTextOff;

            setAction();
            OnAction = btnActionOn;
            OffAction = btnActionOff;

            state = position;

            SetActive(true);


            XButtonAPI.allToggleWings.Add(this);
        }
        public QMToggleWing(String btnTextOn, System.Action btnActionOn, String btnTextOff, System.Action btnActionOff, bool left = true, bool position = false, bool backroundshow = true, bool arrow = true, bool iconshow = true) : base(QMStuff.SingleWingTemplate(), (left ? QMStuff.LeftWing : QMStuff.RightWing).field_Public_RectTransform_0.Find("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup"), $"Button_{btnTextOn}")
        {
            container = RectTransform.Find("Container").transform;
            container.Find("Background").gameObject.SetActive(backroundshow);
            container.Find("Icon_Arrow").gameObject.SetActive(arrow);
            var iconImage = container.Find("Icon").GetComponent<Image>();
            iconImage.gameObject.SetActive(false);

            if (!iconImage)
                SetTextPosition(new Vector2(50f, 0f));

            SetText(position ? btnTextOn : btnTextOff);
            OnText = btnTextOn;
            OffText = btnTextOff;

            setAction();
            OnAction = btnActionOn;
            OffAction = btnActionOff;

            state = position;

            SetActive(true);


            XButtonAPI.allToggleWings.Add(this);
        }
        public void setToggleState(bool toggleOn, bool shouldInvoke = false)
        {
            state = toggleOn;
            SetText(toggleOn ? OnText : OffText);
            try
            {
                if (toggleOn && shouldInvoke)
                {
                    OnAction.Invoke();
                }
                if (!toggleOn && shouldInvoke)
                {
                    OffAction.Invoke();
                }
            }
            catch { }
        }
        public void setAction()
        {
            GameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            GameObject.GetComponent<Button>().onClick.AddListener(new System.Action(() =>
            {
                state = !state;
                if (state)
                {
                    setToggleState(true, true);
                }
                else
                {
                    setToggleState(false, true);
                }
            }));
        }
    }
    public class QMEmptyWing : QMWingBase
    {
        public QMEmptyWing(Transform parent) : base(QMStuff.SingleWingTemplate(), parent, $"Button_Empty")
        {
            var container = RectTransform.Find("Container").transform;
            container.Find("Background").gameObject.SetActive(false);
            container.Find("Icon_Arrow").gameObject.SetActive(false);
            RectTransform.sizeDelta = new Vector2(420f, 123f);
            RectTransform.Find("Separator").gameObject.SetActive(false);
            Object.DestroyImmediate(container.Find("Icon").gameObject);
            Object.DestroyImmediate(container.Find("Text_QM_H3").gameObject);

            XButtonAPI.allEmptyWings.Add(this);
        }
    }
    public class QMSingleWing : QMWingBase
    {
        public QMSingleWing(string text, string tooltip, Action onClick, Sprite sprite = null, bool left = true, bool arrow = true, bool background = true, bool separator = false) : base(QMStuff.SingleWingTemplate(), (left ? QMStuff.LeftWing : QMStuff.RightWing).field_Public_RectTransform_0.Find("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup"), $"Button_{text}")
        {
            var container = RectTransform.Find("Container").transform;
            container.Find("Background").gameObject.SetActive(background);
            container.Find("Icon_Arrow").gameObject.SetActive(arrow);
            RectTransform.Find("Separator").gameObject.SetActive(separator);
            var iconImage = container.Find("Icon").GetComponent<Image>();
            if (sprite != null)
            {
                iconImage.sprite = sprite;
                iconImage.overrideSprite = sprite;
                SetTextPosition(new Vector2(115f, 0f));
            }
            else
            {
                SetTextPosition(new Vector2(50f, 0f));
                iconImage.gameObject.SetActive(false);
            }


            SetText(text);
            SetAction(onClick);
            SetTooltip(tooltip);

            XButtonAPI.allSingleWings.Add(this);
        }

        public QMSingleWing(string text, string tooltip, Action onClick, Transform parent, bool arrow = false, bool background = true, bool separator = false, Sprite sprite = null) : base(QMStuff.SingleWingTemplate(), parent, $"Button_{text}")
        {
            var container = RectTransform.Find("Container").transform;
            container.Find("Background").gameObject.SetActive(background);
            container.Find("Icon_Arrow").gameObject.SetActive(arrow);
            RectTransform.Find("Separator").gameObject.SetActive(separator);
            var iconImage = container.Find("Icon").GetComponent<Image>();
            if (sprite != null)
            {
                iconImage.sprite = sprite;
                iconImage.overrideSprite = sprite;
                SetTextPosition(new Vector2(115f, 0f));
            }
            else
            {
                SetTextPosition(new Vector2(50f, 0f));
                iconImage.gameObject.SetActive(false);
            }

            SetText(text);
            SetAction(onClick);
            SetTooltip(tooltip);

            XButtonAPI.allSingleWings.Add(this);
        }
    }
    public class QMSingleToggleButton : QMButtonBase
    {
        private bool state { get; set; }
        private string OnText { get; set; }
        private string OffText { get; set; }
        private Action OffAction { get; set; }
        private Action OnAction { get; set; }

        public QMSingleToggleButton(Transform transform, String btnTextOn, System.Action btnActionOn, String btnTextOff, System.Action btnActionOff, bool position = false, string TooltipON = null,string ToolTipOFF = null) : base(QMStuff.SingleButtonTemplate(), transform, $"ToggleButton_{btnTextOn}")
        {
            Object.DestroyImmediate(RectTransform.Find("Icon").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Icon_Secondary").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Badge_Close").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Badge_MMJump").gameObject);


            TextMeshProUGUI Text = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            Text.fontSize = 30;
            Text.transform.localPosition = new Vector3(Text.transform.localPosition.x, -25f);

            base.SetTooltip(TooltipON, ToolTipOFF);

            SetText(position ? btnTextOn : btnTextOff);
            OnText = btnTextOn;
            OffText = btnTextOff;

            setAction();
            OnAction = btnActionOn;
            OffAction = btnActionOff;

            state = position;
        }
        public void SetActive(bool isActive)
        {
            GameObject.SetActive(isActive);
        }
        public void setToggleState(bool toggleOn, bool shouldInvoke = false)
        {
            state = toggleOn;
            SetText(toggleOn ? OnText : OffText);
            try
            {
                if (toggleOn && shouldInvoke)
                {
                    OnAction.Invoke();
                }
                if (!toggleOn && shouldInvoke)
                {
                    OffAction.Invoke();
                }
            }
            catch { }
        }
        public void setAction()
        {
            GameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            GameObject.GetComponent<Button>().onClick.AddListener(new System.Action(() =>
            {
                state = !state;
                if (state)
                {
                    setToggleState(true, true);
                }
                else
                {
                    setToggleState(false, true);
                }
            }));
        }
        public static QMSingleToggleButton Create(Transform transform, String btnTextOn, System.Action btnActionOn, String btnTextOff, System.Action btnActionOff, bool position = false)
        {
            return new QMSingleToggleButton(transform, btnTextOn, btnActionOn, btnTextOff, btnActionOff, position);
        }
    }

    public class QMToggleButton : QMButtonBase
    {
        private static Sprite OnIconSprite => QMStuff.Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<Image>().sprite;
        private readonly Toggle toggle;
        private readonly ToggleIcon toggleIcon;


        private bool _valueHolder;
        public QMToggleButton(string text, string tooltip, Action<bool> onToggle, Transform parent, bool defaultValue = false) : base(QMStuff.ToggleButtonTemplate(), parent, $"Button_Toggle{text}")
        {
            base.SetText(text);
            base.SetTooltip(tooltip, tooltip);
            RectTransform.Find("Icon_On").GetComponent<Image>().sprite = OnIconSprite;

            toggleIcon = GameObject.GetComponent<ToggleIcon>();

            toggle = GameObject.GetComponent<Toggle>();
            toggle.onValueChanged = new Toggle.ToggleEvent();
            toggle.onValueChanged.AddListener(new Action<bool>(toggleIcon.OnValueChanged));
            toggle.onValueChanged.AddListener(new Action<bool>(onToggle));

            Toggle(defaultValue, false);
            GameObject.AddComponent<ActionListener>().OnEnabled += UpdateToggleIfNeeded;

            XButtonAPI.allToggleButtons.Add(this);
        }

        public void Toggle(bool value, bool callback = true, bool updateVisually = false)
        {
            _valueHolder = value;
            toggle.Set(value, callback);
            if (updateVisually) UpdateToggleIfNeeded();
        }

        private void UpdateToggleIfNeeded()
        {
            toggleIcon.OnValueChanged(_valueHolder);
        }
    }
    public class QMSingleButton : QMButtonBase
    {
        public QMSingleButton(string text, string tooltip, Action onClick, Transform parent, Sprite sprite = null) : base(QMStuff.SingleButtonTemplate(), parent, $"Button_{text}")
        {
            base.SetText(text);
            base.SetTooltip(tooltip, tooltip);
            base.SetAction(onClick);
            base.SetIfSprite(sprite);

            Object.DestroyImmediate(RectTransform.Find("Icon_Secondary").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Badge_Close").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Badge_MMJump").gameObject);

            XButtonAPI.allSingleButtons.Add(this);
        }
    }
    public class QMCategoryPage : QMButtonBase
    {
        private readonly Transform _container;

        private readonly List<QMCategory> _categories = new List<QMCategory>();

        public QMSingleButton Button;
        public QMSingleWing wing;

        private readonly string Names;
        private readonly bool Root;
        public QMCategoryPage(string text, bool isRoot = false) : base(QMStuff.CatergoyPageTemplate(), QMStuff.CatergoyPageTemplate().transform.parent, $"Menu_{text}", false)
        {
            Names = GetCleanName(text);
            Root = isRoot;

            var scrollRect = RectTransform.GetComponentInChildren<ScrollRect>();
            scrollRect.content.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
            scrollRect.enabled = true;
            scrollRect.verticalScrollbar = scrollRect.transform.Find("Scrollbar").GetComponent<Scrollbar>();
            scrollRect.viewport.GetComponent<RectMask2D>().enabled = true;

            Object.DestroyImmediate(GameObject.GetComponent<LaunchPadQMMenu>());

            var child = RectTransform.GetChild(0);
            child.GetComponentInChildren<TextMeshProUGUI>().text = text;
            child.GetComponentInChildren<TextMeshProUGUI>().richText = true;

            RectTransform.SetSiblingIndex(QMStuff.Instance.field_Public_Transform_0.Find("Window/QMParent/Modal_AddMessage").GetSiblingIndex());

            if (!isRoot) child.GetComponentInChildren<Button>(true).gameObject.SetActive(true);

            _container = RectTransform.GetComponentInChildren<ScrollRect>().content;
            foreach (var obj in _container)
            {
                var control = obj.Cast<Transform>();
                if (control != null) Object.Destroy(control.gameObject);
            }

            var page = SetUIPage(Names);
            if (isRoot)
            {
                var rootPages = QMStuff.MenuStateCtrl.field_Public_ArrayOf_UIPage_0.ToList();
                rootPages.Add(page);
                QMStuff.MenuStateCtrl.field_Public_ArrayOf_UIPage_0 = rootPages.ToArray();
            }

            XButtonAPI.allcategorypages.Add(this);
        }
        public QMCategoryPage(Transform transform, string text, bool isRoot = false, bool IsWing = false, string tooltip = "", Sprite sprite = null) : base(QMStuff.CatergoyPageTemplate(), QMStuff.CatergoyPageTemplate().transform.parent, $"Menu_{text}", false)
        {
            Names = GetCleanName(text);
            Root = isRoot;
            var scrollRect = RectTransform.GetComponentInChildren<ScrollRect>();
            scrollRect.content.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
            scrollRect.enabled = true;
            scrollRect.verticalScrollbar = scrollRect.transform.Find("Scrollbar").GetComponent<Scrollbar>();
            scrollRect.viewport.GetComponent<RectMask2D>().enabled = true;

            Object.DestroyImmediate(GameObject.GetComponent<LaunchPadQMMenu>());

            var child = RectTransform.GetChild(0);
            child.GetComponentInChildren<TextMeshProUGUI>().text = text;
            child.GetComponentInChildren<TextMeshProUGUI>().richText = true;
            UnityEngine.Object.DestroyImmediate(child.Find("RightItemContainer/Button_QM_Expand").gameObject);

            RectTransform.SetSiblingIndex(QMStuff.Instance.field_Public_Transform_0.Find("Window/QMParent/Modal_AddMessage").GetSiblingIndex());

            if (!isRoot) child.GetComponentInChildren<Button>(true).gameObject.SetActive(true);

            _container = RectTransform.GetComponentInChildren<ScrollRect>().content;
            foreach (var obj in _container)
            {
                var control = obj.Cast<Transform>();
                if (control != null) Object.Destroy(control.gameObject);
            }

            var page = SetUIPage(Names);
            if (isRoot)
            {
                var rootPages = QMStuff.MenuStateCtrl.field_Public_ArrayOf_UIPage_0.ToList();
                rootPages.Add(page);
                QMStuff.MenuStateCtrl.field_Public_ArrayOf_UIPage_0 = rootPages.ToArray();
            }

            if (IsWing)
            {
                wing = new QMSingleWing(text, tooltip, () => { Open(); }, transform, false, true, false, sprite);
            }
            else
            {
                Button = new QMSingleButton(text, tooltip, () => { Open(); }, transform, sprite);
            }

            XButtonAPI.allcategorypages.Add(this);
        }
        public void Open()
        {
            if (Root) QMStuff.MenuStateCtrl.SwitchToRootPage($"QuickMenu{Names}");
            else QMStuff.MenuStateCtrl.PushPage($"QuickMenu{Names}");
        }
        public QMCategory AddCategory(string title)
        {
            var existingCategory = GetCategory(title);
            if (existingCategory != null) return existingCategory;


            var catergory = new QMCategory(title, _container);
            _categories.Add(catergory);
            return catergory; 
        }

        public QMCategory GetCategory(string name)
        {
            return _categories.FirstOrDefault(c => c.Name == GetCleanName(name));
        }
    }
    public class QMCategory
    {
        public QMHeader Header;
        public QMContainer _buttonContainer;
        public string Name { get; }
        public QMCategory(string title, Transform parent = null)
        {
            Name = QMButtonBase.GetCleanName(title);
            Header = new QMHeader(title, parent);
            _buttonContainer = new QMContainer(Name, parent);
        }
        public QMSlider AddSlider(string Title, Action<float> Value, bool ShowPercent, string Percent = "", string ToolTip = null, float maxValue = 100f, float MinValue = 0f)
        {
            return new QMSlider(Title, Value, _buttonContainer.RectTransform, ShowPercent, Percent, ToolTip, maxValue, MinValue);
        }
        public QMSingleToggleButton AddButtonToggle(String TextON, Action ActionON, String TextOFF, Action ActionOFF, bool postion = false, string TooltipON = null, string ToolTipOFF = null)
        {
            return new QMSingleToggleButton(_buttonContainer.RectTransform, TextON, ActionON, TextOFF, ActionOFF, postion , TooltipON, ToolTipOFF);
        }
        public QMLabel AddLabel(string text, string subtitle, int fontsize = 56)
        {
            return new QMLabel(_buttonContainer.RectTransform, text, subtitle, fontsize);
        }
        public QMEmptyButton AddPlaceHolder()
        {
            return new QMEmptyButton(_buttonContainer.RectTransform);
        }
        public QMSingleButton AddButton(string text, string tooltip, Action onClick, Sprite sprite = null)
        {
            return new QMSingleButton(text, tooltip, onClick, _buttonContainer.RectTransform, sprite);
        }

        public QMToggleButton AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false)
        {
            return new QMToggleButton(text, tooltip, onToggle, _buttonContainer.RectTransform, defaultValue);
        }

        public QMNestedButton AddMenuPage(string ButtonText, string PageText, string tooltip = "", bool IsRoot = false, Sprite sprite = null)
        {
            var existingPage = GetMenuPage(PageText);
            if (existingPage != null) return existingPage;

            return new QMNestedButton(_buttonContainer.RectTransform, ButtonText, PageText, IsRoot, tooltip, sprite);
        }

        public QMCategoryPage AddCategoryPage(string text,string tooltip = "", Sprite sprite = null)
        {
            var existingPage = GetCategoryPage(text);
            if (existingPage != null) return existingPage;

            return new QMCategoryPage(_buttonContainer.RectTransform, text, false, false, tooltip, sprite);
        }

        public RectTransform RectTransform => _buttonContainer.RectTransform;

        public QMNestedButton GetMenuPage(string name)
        {
            return XButtonAPI.allNestedButtons.FirstOrDefault(m => m.Name == QMButtonBase.GetCleanName($"Menu_{name}"));
        }

        public QMCategoryPage GetCategoryPage(string name)
        {
            return XButtonAPI.allcategorypages.FirstOrDefault(m => m.Name == QMButtonBase.GetCleanName($"Menu_{name}"));
        }

        public QMToggleButton AddToggle(string text, string tooltip, Action<bool> configValue)
        {
            return new QMToggleButton(text, tooltip, configValue, _buttonContainer.RectTransform);
        }

        public QMNestedButton AddMenuPage(string text, string tooltip = "", Sprite sprite = null)
        {
            throw new NotImplementedException();
        }
    }
    public class QMEmptyButton : QMButtonBase
    {
        public QMEmptyButton(Transform parent) : base (QMStuff.ContainerTemplate(), parent, "Button_Empty")
        {
            for (int i = GameObject.transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.DestroyImmediate(GameObject.transform.GetChild(i).gameObject);
            }

            XButtonAPI.allEmpty.Add(this);
        }
    }
    public class QMHeader : QMButtonBase
    {
        public QMHeader(string title, Transform parent) : base(QMStuff.HeaderTemplate(), (parent == null ? QMStuff.HeaderTemplate().transform.parent : parent), $"Header_{title}")
        {
            GameObject.GetComponentInChildren<TextMeshProUGUI>().text = title;
            GameObject.GetComponentInChildren<TextMeshProUGUI>().richText = true;
            GameObject.GetComponentInChildren<TextMeshProUGUI>().transform.parent.GetComponent<HorizontalLayoutGroup>().childControlWidth = true;
        }
    }
    public class QMContainer : QMButtonBase
    {
        public GridLayoutGroup gridLayout;
        public QMContainer(string name, Transform parent = null) : base(QMStuff.ContainerTemplate(), (parent == null ? QMStuff.ContainerTemplate().transform.parent : parent), $"Buttons_{name}")
        {
            foreach (var obj in RectTransform)
            {
                var control = obj.Cast<Transform>();
                if (control == null)
                {
                    continue;
                }
                Object.Destroy(control.gameObject);
            }

            SetButtonAnchor(TextAnchor.UpperCenter);
        }
        public void SetButtonAnchor(TextAnchor textAnchor)
        {
            gridLayout = GameObject.GetComponent<GridLayoutGroup>();

            gridLayout.childAlignment = textAnchor;
            gridLayout.padding.top = 8;
            gridLayout.padding.left = 64;
        }
    }

    public class QMTabMenu : QMButtonBase
    {
        public QMTabMenu(string name, string tooltip, string pageName, Sprite sprite) : base(QMStuff.TabButtonTemplate(), QMStuff.TabButtonTemplate().transform.parent, $"Page_{name}")
        {
            MenuTab menuTab = RectTransform.GetComponent<MenuTab>();
            menuTab.field_Public_String_0 = GetCleanName($"QuickMenu{pageName}");
            menuTab.field_Private_MenuStateController_0 = QMStuff.MenuStateCtrl;

            base.SetAction(menuTab.ShowTabContent);
            base.SetTooltip(tooltip, tooltip);

            Image iconImage = RectTransform.Find("Icon").GetComponent<Image>();
            iconImage.sprite = sprite;
            iconImage.overrideSprite = sprite;

            XButtonAPI.allTabsButton.Add(this);
        }
    }
    public static class QMStuff
    {
        public static MenuStateController MenuStateCtrl => Instance.prop_MenuStateController_0; 
        public static VRC.UI.Elements.QuickMenu Instance
        {
            get
            {
                return Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>();
            }
        }

        public static Wing[] Wings
        {
            get
            {
                return Object.FindObjectsOfType<Wing>();
            }
        }
        public static Wing LeftWing
        {
            get
            {
                return Wings.FirstOrDefault(w => w.field_Public_WingPanel_0 == Wing.WingPanel.Left);
            }
        }

        public static Wing RightWing
        {
            get
            {
                return Wings.FirstOrDefault(w => w.field_Public_WingPanel_0 == Wing.WingPanel.Right);
            }
        }
        public static GameObject WingMenuTemplate()
        {
            return LeftWing.field_Public_RectTransform_0.Find("WingMenu").gameObject;
        }
        public static GameObject SingleWingTemplate()
        {
            return WingMenuTemplate().transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Button_Profile").gameObject;
        }
        public static GameObject SingleButtonTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().content.Find("Buttons_QuickActions/Button_Respawn").gameObject;
        }
        public static Transform MainMenu()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().content.Find("Buttons_QuickActions");
        }
        public static GameObject ToggleButtonTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Settings/Panel_QM_ScrollRect").GetComponent<ScrollRect>().content.Find("Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject;
        }
        public static GameObject ContainerTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().content.Find("Buttons_QuickActions").gameObject;
        }
        public static GameObject CatergoyPageTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Dashboard").gameObject;
        }
        public static GameObject NestedMenuTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_DevTools").gameObject;
        }
        public static GameObject HeaderTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().content.Find("Header_QuickActions").gameObject;
        }
        public static GameObject LabelTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_Debug/Button_FPS").gameObject;
        }
        public static GameObject SliderTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_AudioSettings/Content/Audio/VolumeSlider_Master").gameObject;
        }
        public static GameObject PanelTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_AudioSettings/Content/Audio").gameObject;
        }

        public static VRCUiManager GetVRCUiMInstance()
        {
            return VRCUiManager.prop_VRCUiManager_0;
        }
        public static GameObject TabButtonTemplate()
        {
            return Instance.field_Public_Transform_0.Find("Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings").gameObject;
        }
        public static bool CheckMethod(MethodInfo method, string match)
        {
            try
            {
                foreach (XrefInstance xrefInstance in XrefScanner.XrefScan(method))
                {
                    if (xrefInstance.Type == XrefType.Global && xrefInstance.ReadAsObject().ToString().Contains(match))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
            }
            return false;
        }
    }
}