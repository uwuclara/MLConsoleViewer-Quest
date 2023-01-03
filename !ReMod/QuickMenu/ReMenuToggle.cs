using System;
using System.Collections.Generic;
using System.Linq;
using Il2CppSystem.Reflection;
using MelonLoader;
using TMPro;
using UnhollowerBaseLib.Attributes;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using Object = UnityEngine.Object;

namespace MLConsoleViewer.QuickMenu
{
    public class ReMenuToggle : UiElement
    {
        private readonly Toggle _toggleComponent;

        public bool Interactable
        {
            get => _toggleComponent.interactable;
            set
            {
                _toggleComponent.interactable = value;
                            
                if(_toggleStyleElement != null)
                    _toggleStyleElement.OnEnable();
            }
        }

        private bool _valueHolder;
        public bool Value
        {
            get => _valueHolder;
            set => Toggle(value);
        }
        
        private StyleElement _toggleStyleElement;

        private TextMeshProUGUI _textComponent;
        public string Text
        {
            get => _textComponent.text;
            set => _textComponent.text = value;
        }
        
        private VRC.UI.Elements.Tooltips.UiToggleTooltip _tooltip;
        
        public string Tooltip {
            get => _tooltip != null ? _tooltip.field_Public_String_0 : "";
            set
            {
                if (_tooltip == null) return;
                _tooltip.field_Public_String_0 = value;
                _tooltip.field_Public_String_1 = value;
            }
        }
        public ReMenuToggle(string text, string tooltip, Action<bool> onToggle, Transform parent, bool defaultValue = false) : this(text, tooltip, onToggle, parent, defaultValue, null, null) { }
        public ReMenuToggle(string text, string tooltip, Action<bool> onToggle, Transform parent, bool defaultValue = false, Sprite iconOn = null, Sprite iconOff = null) : base(QuickMenuEx.TogglePrefab, parent, $"Button_Toggle{text}")
        {
            var icon = RectTransform.Find("Icon_On").GetComponent<Image>();
            icon.sprite = iconOn ?? QuickMenuEx.OnIconSprite;
            icon = RectTransform.Find("Icon_Off").GetComponent<Image>();
            icon.sprite = iconOff ?? QuickMenuEx.OffIconSprite;

            Object.DestroyImmediate(GameObject.GetComponent<UIInvisibleGraphic>()); // Fix for having clickable area overlap main quickmenu ui

            _toggleComponent = GameObject.GetComponent<Toggle>();
            _toggleComponent.onValueChanged = new Toggle.ToggleEvent();
            _toggleComponent.onValueChanged.AddListener(new Action<bool>(onToggle));
            
            _toggleStyleElement = GameObject.GetComponent<StyleElement>();

            _textComponent = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            _textComponent.text = text;
            _textComponent.richText = true;
            _textComponent.color = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
            _textComponent.m_fontColor = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
            _textComponent.m_htmlColor = new Color(0.4157f, 0.8902f, 0.9765f, 1f);

            _tooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiToggleTooltip>();
            _tooltip.field_Public_String_0 = tooltip;
            _tooltip.field_Public_String_1 = tooltip;
            
            Toggle(defaultValue,false);
        }

        public ReMenuToggle(Transform transform) : base(transform)
        {
            _toggleComponent = GameObject.GetComponent<Toggle>();
        }

        public void Toggle(bool value, bool callback = true)
        {
            _valueHolder = value;
            _toggleComponent.Set(value, callback);
        }
    }
}
