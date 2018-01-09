using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryButton : MonoBehaviour {
	[SerializeField] Button buttonComponent;
	[SerializeField] TMP_Text textComponent;

	public string text {
		get{ return textComponent.text; }
		set{ textComponent.text = value; }
	}

	public Button.ButtonClickedEvent onClick {
		get{ return buttonComponent.onClick; }
	}
}
