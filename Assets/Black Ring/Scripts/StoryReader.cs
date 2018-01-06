using UnityEngine;
using UnityEngine.Events;
using Ink.Runtime;
using System.Collections.Generic;

namespace StoryReading {
	[System.Serializable]
	public class ReaderEvent : UnityEvent<StoryReader> {}
	[System.Serializable]
	public class StringEvent : UnityEvent<string> {}
	[System.Serializable]
	public class StringListEvent : UnityEvent<List<string>> {}
	[System.Serializable]
	public class IntEvent : UnityEvent<int> {}

	public class StoryReader : MonoBehaviour, ISerializationCallbackReceiver {
		//The JSON text file that describes the story
		[SerializeField] TextAsset _storyJSON;
		public TextAsset jsonText {get {return _storyJSON;}}

		//Should the story play automatically when starting?
		[SerializeField] bool _playOnStart = true;
		//Should we require a step between showing some text, then showing a list of the options? Or should we always immediately go to the options when we have them.
		[SerializeField] bool _stepBetweenTextAndOptions = true;
		//Should we use a maximal continue, which will add up all the text until the next option when we do a single step?
		[SerializeField] bool _useMaximalContinue = false;

		[SerializeField, HideInInspector] bool _isPlaying;
		public bool isPlaying {get {return _isPlaying;}}

		Story _inkStory;
		public Story currentStory {get {return _inkStory;}}

		//Fired when a new story opject is loaded
		public ReaderEvent newStoryLoaded;

		//Fired when a story begins playing
		public ReaderEvent storyStarted;
		//Fired when a story has finished playing
		public ReaderEvent storyEnded;

		//Fired when the next step in a story is some text to display
		public StringEvent storyTextEncountered;
		//Fired when some tags are encountered in the story
		public StringListEvent storyTagsEncountered;

		//Fired when the next step in a story is to select an option
		public StringListEvent storyOptionsEncountered;
		//Fired when an option is chosen in a story
		public IntEvent storyOptionChosen;

		[SerializeField, HideInInspector] string _serializedData;

		List<string> _currentChoicesText = new List<string>( 10 );

		void Start() {
			if (_storyJSON) LoadStory(_storyJSON, _playOnStart);
		}

		public void LoadStory(TextAsset storyJSON, bool playImmediately) {
			_storyJSON = storyJSON;
			_isPlaying = false;

			_inkStory = new Story(_storyJSON.text);
			newStoryLoaded.Invoke( this );

			if (playImmediately) {
				PlayStory();
			}
		}

		public void PlayStory() {
			if (_isPlaying) return;
			_isPlaying = true;
			storyStarted.Invoke( this );
		}

		public void NextStep() {
			if (_inkStory.canContinue) {
				if( _useMaximalContinue ) _inkStory.ContinueMaximally();
				else _inkStory.Continue();

				if( _inkStory.currentTags.Count > 0 )
					storyTagsEncountered.Invoke( _inkStory.currentTags );
				storyTextEncountered.Invoke( _inkStory.currentText );

				//Recurse to the next step if the next step won't result in more text
				if (!_inkStory.canContinue && !_stepBetweenTextAndOptions) {
					NextStep();
				}

			} else if (_inkStory.currentChoices.Count > 0) {
				_currentChoicesText.Clear();
				for (int i = 0; i < _inkStory.currentChoices.Count; i++) {
					_currentChoicesText.Add( _inkStory.currentChoices[i].text );
				}
				storyOptionsEncountered.Invoke( _currentChoicesText );

			} else {
				storyEnded.Invoke( this );
			}
		}

		public void ChooseOption(int index) {
			if (_inkStory.currentChoices.Count > 0) {
				_inkStory.ChooseChoiceIndex(index);
				NextStep();
			} else {
				Debug.LogWarning("Cannot choose option, there are no current options");
			}
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize() {
			if (_inkStory != null) _serializedData = _inkStory.state.ToJson();
			else _serializedData = null;
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize() {
			if (_storyJSON && !string.IsNullOrEmpty(_serializedData)) {
				_inkStory = new Story(_storyJSON.text);
				_inkStory.state.LoadJson(_serializedData);
			}
		}
    }
}
