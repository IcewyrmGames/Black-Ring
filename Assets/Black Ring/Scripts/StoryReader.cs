using UnityEngine;
using UnityEngine.Events;
using Ink.Runtime;
using System.Collections.Generic;

namespace IceWyrm {
	[System.Serializable]
	public class ReaderEvent : UnityEvent<StoryReader> {}
	[System.Serializable]
	public class StringEvent : UnityEvent<string> {}
	[System.Serializable]
	public class StringListEvent : UnityEvent<List<string>> {}
	[System.Serializable]
	public class ChoiceListEvent : UnityEvent<List<Choice>> {}
	[System.Serializable]
	public class IntEvent : UnityEvent<int> {}

	public class StoryReader : MonoBehaviour, ISerializationCallbackReceiver {
		//The compiled JSON text file that describes the story
		[SerializeField] TextAsset compiledStory;

		//Should the story play automatically when starting?
		[SerializeField] bool playOnStart = true;
		//Should we use a maximal continue, which will add up all the text until the next option when we do a single step?
		[SerializeField] bool useMaximalContinue = false;

		//The story object that is executing the Ink story
		Story story;

		//Fired when a story begins playing
		public ReaderEvent storyStarted;
		//Fired when a story has finished playing
		public ReaderEvent storyEnded;

		//Fired when the next step in a story is some text to display
		public StringEvent storyTextEncountered;
		//Fired when some tags are encountered in the story
		public StringListEvent storyTagsEncountered;

		//Fired when the next step in a story is to select a choice
		public ChoiceListEvent storyChoicesEncountered;
		//Fired when an option is chosen in a story
		public IntEvent storyOptionChosen;

		[SerializeField, HideInInspector] string serializedData;

		public void Start() {
			if (compiledStory) {
				story = new Story(compiledStory.text);
				story.onError += OnInkError;

				if (playOnStart) {
					NextStep();
				}
			}
		}

		public void NextStep() {
			if (story.canContinue) {
				if (useMaximalContinue) story.ContinueMaximally();
				else story.Continue();

				if (story.currentTags.Count > 0)
					storyTagsEncountered.Invoke(story.currentTags);
				storyTextEncountered.Invoke(story.currentText);

			} else if (story.currentChoices.Count > 0) {
				storyChoicesEncountered.Invoke(story.currentChoices);

			} else {
				storyEnded.Invoke(this);
			}
		}

		public void ChooseOption(int index) {
			if (story.currentChoices.Count > 0) {
				story.ChooseChoiceIndex(index);
				NextStep();
			} else {
				Debug.LogWarning("Cannot choose option, there are no current options");
			}
		}

		void OnInkError(string message, Ink.ErrorType type) {
			if (type == Ink.ErrorType.Warning) Debug.LogWarning(message);
			else Debug.LogError(message);
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize() {
			if (story != null) serializedData = story.state.ToJson();
			else serializedData = null;
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize() {
			if (compiledStory && !string.IsNullOrEmpty(serializedData)) {
				story = new Story(compiledStory.text);
				story.onError += OnInkError;
				story.state.LoadJson(serializedData);
			}
		}
	}
}
