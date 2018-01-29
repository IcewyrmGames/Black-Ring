using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryButtonManager : MonoBehaviour {
	[SerializeField] StoryReading.StoryReader reader;
	[SerializeField] List<StoryButton> buttons;

	public void Awake() {
		reader.storyOptionsEncountered.AddListener( OnStoryOptionsEncountered );

		for( int buttonIndex = 0; buttonIndex < buttons.Count; ++buttonIndex ) {
			buttons[buttonIndex].BindOption( reader, buttonIndex );
		}
	}

	public void OnStoryOptionsEncountered(List<string> optionTexts) {
		if( optionTexts.Count > buttons.Count ) {
			Debug.LogWarning( string.Format( "Not enough buttons to display the current number of options. Need %i, have %i.", optionTexts.Count, buttons.Count ) );
		}

		for (int buttonIndex = 0; buttonIndex < buttons.Count; ++buttonIndex) {
			if (buttonIndex < optionTexts.Count) {
				buttons[buttonIndex].gameObject.SetActive( true );
				buttons[buttonIndex].text = optionTexts[buttonIndex];

			} else {
				buttons[buttonIndex].gameObject.SetActive( false );
				buttons[buttonIndex].text = "[NO OPTION]";
			}
		}
	}
}
