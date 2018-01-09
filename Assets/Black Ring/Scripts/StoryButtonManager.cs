using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryButtonManager : MonoBehaviour {
	public StoryReading.StoryReader reader;
	public StoryButton buttonPrefab;
	public RectTransform buttonContainer;

	[SerializeField] List<StoryButton> createdButtons;

	public void Awake() {
		reader.storyOptionsEncountered.AddListener( OnStoryOptionsEncountered );
	}

	public void OnStoryOptionsEncountered(List<string> optionTexts) {
		for (int buttonIndex = createdButtons.Count; buttonIndex < optionTexts.Count; ++buttonIndex) {
			StoryButton newButton = Instantiate<StoryButton>( buttonPrefab, buttonContainer );
			createdButtons.Add( newButton );
			int capturedButtonIndex = buttonIndex;
			newButton.onClick.AddListener( ()=>{ reader.ChooseOption( capturedButtonIndex ); } );
		}

		for (int buttonIndex = 0; buttonIndex < createdButtons.Count; ++buttonIndex) {
			if (buttonIndex < optionTexts.Count) {
				createdButtons[buttonIndex].gameObject.SetActive( true );
				createdButtons[buttonIndex].text = optionTexts[buttonIndex];

			} else {
				createdButtons[buttonIndex].gameObject.SetActive( false );
			}
		}
	}
}
