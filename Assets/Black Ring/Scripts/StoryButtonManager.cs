using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ink.Runtime;

public class StoryButtonManager : MonoBehaviour {
	[SerializeField] IceWyrm.StoryReader reader;
	[SerializeField] List<StoryButton> buttons;

	public void Awake() {
		reader.storyChoicesEncountered.AddListener(OnStoryChoicesEncountered);

		for (int buttonIndex = 0; buttonIndex < buttons.Count; ++buttonIndex) {
			buttons[buttonIndex].ButtonClicked = OnButtonClicked;
		}
	}

	public void OnStoryChoicesEncountered(List<Choice> choices) {
		if (choices.Count > buttons.Count) {
			Debug.LogWarning(string.Format("Not enough buttons to display the current number of choices. Need %i, have %i.", choices.Count, buttons.Count));
		}

		for (int buttonIndex = 0; buttonIndex < buttons.Count; ++buttonIndex) {
			if (buttonIndex < choices.Count) {
				buttons[buttonIndex].gameObject.SetActive(true);
				buttons[buttonIndex].text = choices[buttonIndex].text;
				buttons[buttonIndex].choiceIndex = choices[buttonIndex].index;

			} else {
				buttons[buttonIndex].gameObject.SetActive(false);
				buttons[buttonIndex].text = "[NO OPTION]";
				buttons[buttonIndex].choiceIndex = 0;
			}
		}
	}

	public void OnButtonClicked(StoryButton button) {
		reader.ChooseOption(button.choiceIndex);
	}
}
