using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPanelSwitcher : MonoBehaviour {
	public StoryReading.StoryReader reader;
	public GameObject textPanel;
	public GameObject optionsPanel;

	public void Awake() {
		reader.storyTextEncountered.AddListener( (string s)=>{ SwitchToTextPanel(); } );
		reader.storyOptionsEncountered.AddListener( (List<string> o)=>{ SwitchToOptionsPanel(); } );
	}

	void SwitchToTextPanel() {
		textPanel.SetActive( true );
		optionsPanel.SetActive( false );
	}

	void SwitchToOptionsPanel() {
		textPanel.SetActive( false );
		optionsPanel.SetActive( true );
	}
}
