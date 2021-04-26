using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[HotReloadInvokable]
public class StoryWindow : MonoBehaviour {
	[SerializeField] IceWyrm.StoryReader reader;

	[SerializeField] GameObject contentTextPanel;
	[SerializeField] GameObject namePanel;
	[SerializeField] GameObject choicesPanel;

	[SerializeField] TMP_Text contentTextComponent;
	[SerializeField] TMP_Text nameTextComponent;

	[SerializeField] List<StoryButton> buttons;
	[SerializeField] Button continueButton;

	public void Start() {
		foreach (StoryButton button in buttons) {
			button.ButtonClicked = OnButtonClicked;
		}
		continueButton.onClick.AddListener(OnContinueClicked);

		reader.storyUpdated.AddListener(OnStoryUpdated);
		OnStoryUpdated(reader.GetCurrentView());
	}

	public void OnHotReload() {
		Start();
	}

	public void OnStoryUpdated(IceWyrm.StoryView view) {
		if (view.ContainsChoices()) {
			contentTextPanel.SetActive(false);
			namePanel.SetActive(false);
			choicesPanel.SetActive(true);

			//Rebuild the buttons for each choice
			if (view.choices.Count > buttons.Count) {
				Debug.LogWarning(string.Format("Not enough buttons to display the current number of choices. Need %i, have %i.", view.choices.Count, buttons.Count));
			}

			for (int buttonIndex = 0; buttonIndex < buttons.Count; ++buttonIndex) {
				if (buttonIndex < view.choices.Count) {
					buttons[buttonIndex].gameObject.SetActive(true);
					buttons[buttonIndex].text = view.choices[buttonIndex].text;
					buttons[buttonIndex].choiceIndex = view.choices[buttonIndex].index;

				} else {
					buttons[buttonIndex].gameObject.SetActive(false);
					buttons[buttonIndex].text = "[NO OPTION]";
					buttons[buttonIndex].choiceIndex = 0;
				}
			}

		} else {
			contentTextPanel.SetActive(true);
			choicesPanel.SetActive(false);

			//Show the current text
			contentTextComponent.text = view.text;

			//Set the proper state for the name panel and text
			string name = null;
			foreach (string tag in view.tags) {
				if (tag.StartsWith("name:")) {
					name = tag.Substring(5);
					break;
				}
			}

			if (!string.IsNullOrEmpty(name)) {
				nameTextComponent.SetText(name);
				namePanel.SetActive(true);
			} else {
				namePanel.SetActive(false);
			}
		}
	}

	void OnButtonClicked(StoryButton button) {
		reader.ChooseChoice(button.choiceIndex);
	}

	void OnContinueClicked() {
		reader.Continue();
	}
}
