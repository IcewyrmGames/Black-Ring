using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class NameTag : MonoBehaviour {
	[SerializeField] TMP_Text textComponent;

	public string currentName;

	[SerializeField, HideInInspector] bool isShowingText = false;

	public void OnValidate() {
		if (textComponent) {
			textComponent.SetText(currentName);
		}
	}

	void RefreshDisplay() {
		if (isShowingText && !string.IsNullOrEmpty(currentName)) {
			textComponent.SetText(currentName);
			gameObject.SetActive(true);
		} else {
			gameObject.SetActive(false);
		}
	}

	public void OnStoryUpdated(IceWyrm.StoryView view) {
		isShowingText = !view.ContainsChoices();

		foreach (string tag in view.tags) {
			if (tag.StartsWith("name:")) {
				currentName = tag.Substring(5);
				break;
			}
		}

		RefreshDisplay();
	}
}
