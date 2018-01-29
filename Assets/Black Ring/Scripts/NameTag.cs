using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameTag : MonoBehaviour {
	[SerializeField] StoryReading.StoryReader reader;
	[SerializeField] TMP_Text textComponent;

	public string currentName;

	[SerializeField, HideInInspector] bool IsShowingText = false;

	public void Awake() {
		reader.storyTagsEncountered.AddListener( OnTagsEncountered );
		reader.storyTextEncountered.AddListener( OnTextShowing );
		reader.storyOptionsEncountered.AddListener( OnOptionsShowing );
	}

	public void OnValidate() {
		if( textComponent ) {
			textComponent.SetText( currentName );
		}
	}

	void RefreshDisplay() {
		if( IsShowingText && !string.IsNullOrEmpty( currentName ) ) {
			textComponent.SetText( currentName );
			gameObject.SetActive( true );
		} else {
			gameObject.SetActive( false );
		}
	}

	void OnTagsEncountered( List<string> tags ) {
		foreach( string tag in tags ) {
			if( tag.StartsWith( "name:" ) ) {
				currentName = tag.Substring( 5 );
				RefreshDisplay();
				return;
			}
		}
	}

	void OnTextShowing( string text ) {
		IsShowingText = true;
		RefreshDisplay();
	}

	void OnOptionsShowing( List<string> optionTexts ) {
		IsShowingText = false;
		RefreshDisplay();
	}
}
