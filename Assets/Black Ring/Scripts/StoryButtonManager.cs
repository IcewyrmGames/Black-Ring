using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ink.Runtime;

public class StoryButtonManager : MonoBehaviour {
	[SerializeField] IceWyrm.StoryReader reader;
	[SerializeField] List<StoryButton> buttons;

	public void OnStoryUpdated(IceWyrm.StoryView view) {
		//Do nothing if we don't have choices right now.
		if (view.choices.Count == 0) return;

		if (view.choices.Count > buttons.Count) {
			Debug.LogWarning(string.Format("Not enough buttons to display the current number of choices. Need %i, have %i.", view.choices.Count, buttons.Count));
		}

		for (int buttonIndex = 0; buttonIndex < buttons.Count; ++buttonIndex) {
			if (buttonIndex < view.choices.Count) {
				buttons[buttonIndex].gameObject.SetActive(true);
				buttons[buttonIndex].text = view.choices[buttonIndex].text;
				buttons[buttonIndex].choiceIndex = view.choices[buttonIndex].index;
				buttons[buttonIndex].ButtonClicked = OnButtonClicked;

			} else {
				buttons[buttonIndex].gameObject.SetActive(false);
				buttons[buttonIndex].text = "[NO OPTION]";
				buttons[buttonIndex].choiceIndex = 0;
			}
		}
	}

	void OnButtonClicked(StoryButton button) {
		reader.ChooseChoice(button.choiceIndex);
	}
}
