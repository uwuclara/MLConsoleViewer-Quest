using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MLConsoleViewer.QuickMenu
{
	public class ReMenuLabel : UiElement
	{
		private static GameObject _buttonPrefab;
		private static GameObject ButtonPrefab
		{
			get
            {
                if (_buttonPrefab == null)
                {
                    _buttonPrefab = QuickMenuEx.Instance.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_Debug_Row_1/Button_FPS").gameObject;
                }
                return _buttonPrefab;
            }
		}
		public void SetSubtitle(string text)
		{
			TextMeshProUGUI component = base.RectTransform.Find("Text_H4").GetComponent<TextMeshProUGUI>();
			component.richText = true;
			component.text = text;
		}

		public void SetText(string text, int fontsize = 46)
		{
			TextMeshProUGUI componentInChildren = base.RectTransform.Find("Text_H1").GetComponentInChildren<TextMeshProUGUI>();
			componentInChildren.text = text;
			componentInChildren.fontSize = fontsize;
		}
		public ReMenuLabel(Transform transform, string text, string subtitleText, int fontsize = 46)  : base(ButtonPrefab, transform, "Label_" + subtitleText, true)
		{
			Object.DestroyImmediate(base.RectTransform.Find("Text_H1").GetComponent<TextBinding>());
			this.SetText(text, fontsize);
			this.SetSubtitle(subtitleText);
		}
	}
}
