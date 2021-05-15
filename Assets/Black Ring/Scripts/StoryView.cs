using UnityEngine.Events;
using System.Collections.Generic;

namespace IceWyrm {
	//Information about the current position in a story
	public readonly struct StoryView {
		public readonly string prefix;
		public readonly string text;
		public readonly List<string> tags;
		public readonly List<StoryChoice> choices;

		public StoryView(string inPrefix, string inText, List<string> inTags, List<StoryChoice> inChoices) {
			prefix = inPrefix;
			text = inText;
			tags = inTags;
			choices = inChoices;
		}

		public bool ContainsText() { return !string.IsNullOrEmpty(text); }
		public bool ContainsChoices() { return choices != null && choices.Count > 0; }
	}

	[System.Serializable]
	public class StoryViewEvent : UnityEvent<StoryView> {}
}
