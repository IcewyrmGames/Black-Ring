using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StoryUI : MonoBehaviour {
	[System.Serializable]
	public struct TextBoxPrefixPair {
		public string prefix;
		public StoryScrollTextBox textBox;
	}

	[SerializeField] IceWyrm.StoryReader reader;

	[Header("Elements")]
	[SerializeField] GameObject textBoxPanel;
	[SerializeField] GameObject choicesPanel;
	[SerializeField] Button continueButton;

	[Header("Prefab Pieces")]
	//The default text box used when a more specific one cannot be found
	[SerializeField] StoryScrollTextBox defaultTextBoxPrefab;
	//The text boxes to use for each prefix in the story text
	[SerializeField] List<TextBoxPrefixPair> textBoxPrefabs;
	//The prefab to create for each choice button
	[SerializeField] StoryButton buttonPrefab;

	[Header("Behavior")]
	[SerializeField] uint precreatedButtonCount = 3;
	[SerializeField] float textSettleDuration = 0.2f;

	[SerializeField, HideInInspector] List<StoryScrollTextBox> textBoxes;
	[SerializeField, HideInInspector] List<StoryButton> buttons;
	[SerializeField, HideInInspector] StoryScrollTextBox latestTextBox;

	public void Start() {
		reader.storyUpdated.AddListener(OnStoryUpdated);
		continueButton.onClick.AddListener(OnContinueButtonClicked);

		//Hide any existing text boxes, which may be present for testing UI layouts in the editor.
		foreach (Transform child in textBoxPanel.transform) {
			StoryScrollTextBox textBox = child.gameObject.GetComponent<StoryScrollTextBox>();
			if (textBox) {
				textBox.gameObject.SetActive(false);
			}
		}

		//Find existing buttons, and bind them.
		foreach (Transform child in choicesPanel.transform) {
			StoryButton button = child.gameObject.GetComponent<StoryButton>();
			if (button) {
				buttons.Add(button);
				button.buttonClicked = OnChoiceButtonClicked;
			}
		}

		//Create enough buttons to reach our precreated count
		for (int index = buttons.Count; index < precreatedButtonCount; ++index) {
			CreateChoiceButton();
		}

		OnStoryUpdated(reader.GetCurrentView());
	}

	void OnHotReload() {
		reader.storyUpdated.AddListener(OnStoryUpdated);
		continueButton.onClick.AddListener(OnContinueButtonClicked);
		foreach (StoryButton button in buttons) {
			button.buttonClicked = OnChoiceButtonClicked;
		}
	}

	public void OnStoryUpdated(IceWyrm.StoryView view) {
		if (view.ContainsChoices()) {
			choicesPanel.SetActive(true);
			continueButton.gameObject.SetActive(false);
			SetChoiceButtons(view.choices);
		} else {
			choicesPanel.SetActive(false);
			continueButton.gameObject.SetActive(true);
			AddTextBox(view.prefix, view.text);
		}
	}

	void AddTextBox(string prefix, string text) {
		StoryScrollTextBox prefab = FindTextBoxPrefab(prefix);

		StoryScrollTextBox block = Instantiate(prefab, Vector3.zero, Quaternion.identity);
		block.transform.SetParent(textBoxPanel.transform, false);
		block.text = text;
		textBoxes.Add(block);

		if (latestTextBox) latestTextBox.Settle(textSettleDuration);
		latestTextBox = block;
	}

	void SetChoiceButtons(List<IceWyrm.StoryChoice> choices) {
		if (buttons.Count < choices.Count) {
			//Create more choice buttons to match the number of choices
			int additional = choices.Count - buttons.Count;
			for (int index = 0; index < additional; ++index) {
				CreateChoiceButton();
			}
		}

		//Update the buttons
		for (int index = 0; index < choices.Count; ++index) {
			buttons[index].gameObject.SetActive(true);
			buttons[index].text = choices[index].text;
			buttons[index].choiceIndex = choices[index].index;
		}
		//Disable extra buttons
		for (int index = choices.Count; index < buttons.Count; ++index) {
			buttons[index].gameObject.SetActive(false);
		}
	}

	void CreateChoiceButton() {
		StoryButton newButton = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
		newButton.transform.SetParent(choicesPanel.transform, false);
		newButton.buttonClicked = OnChoiceButtonClicked;
		buttons.Add(newButton);
	}

	void OnContinueButtonClicked() {
		reader.Continue();
	}

	void OnChoiceButtonClicked(StoryButton button) {
		reader.ChooseChoice(button.choiceIndex);
	}

	StoryScrollTextBox FindTextBoxPrefab(string prefix) {
		int index = textBoxPrefabs.FindIndex(0, (TextBoxPrefixPair pair) => { return pair.prefix == prefix; });
		if (index >= 0) {
			return textBoxPrefabs[index].textBox;
		} else {
			return defaultTextBoxPrefab;
		}
	}
}
