using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryPanelSwitcher : MonoBehaviour {
	public GameObject textPanel;
	public GameObject choicesPanel;

	public void OnStoryUpdated(IceWyrm.StoryView view) {
		if (!view.ContainsChoices()) {
			textPanel.SetActive(true);
			choicesPanel.SetActive(false);
		} else {
			textPanel.SetActive(false);
			choicesPanel.SetActive(true);
		}
	}
}
