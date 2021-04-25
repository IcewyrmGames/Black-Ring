using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryPanelSwitcher : MonoBehaviour {
	public IceWyrm.StoryReader reader;
	public GameObject textPanel;
	public GameObject choicesPanel;

	public void Awake() {
		reader.storyTextEncountered.AddListener((string s)=>{ SwitchToTextPanel(); });
		reader.storyChoicesEncountered.AddListener((List<Choice> c)=>{ SwitchToChoicesPanel(); });
	}

	void SwitchToTextPanel() {
		textPanel.SetActive(true);
		choicesPanel.SetActive(false);
	}

	void SwitchToChoicesPanel() {
		textPanel.SetActive(false);
		choicesPanel.SetActive(true);
	}
}
