using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class NameTag : MonoBehaviour {
	[SerializeField] IceWyrm.StoryReader reader;
	[SerializeField] TMP_Text textComponent;

	public string currentName;

	[SerializeField, HideInInspector] bool isShowingText = false;

	public void Awake() {
		reader.storyTagsEncountered.AddListener(OnTagsEncountered);
		reader.storyTextEncountered.AddListener(OnTextShowing);
		reader.storyChoicesEncountered.AddListener(OnChoicesShowing);
	}

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

	void OnTagsEncountered(List<string> tags) {
		foreach (string tag in tags) {
			if (tag.StartsWith("name:")) {
				currentName = tag.Substring(5);
				RefreshDisplay();
				return;
			}
		}
	}

	void OnTextShowing(string text) {
		isShowingText = true;
		RefreshDisplay();
	}

	void OnChoicesShowing(List<Choice> optionTexts) {
		isShowingText = false;
		RefreshDisplay();
	}
}
