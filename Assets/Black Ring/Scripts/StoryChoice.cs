namespace IceWyrm {
	public readonly struct StoryChoice {
		public readonly string prefix;
		public readonly string text;
		public readonly int index;

		public StoryChoice(string inPrefix, string inText, int inIndex) {
			prefix = inPrefix;
			text = inText;
			index = inIndex;
		}
	}
}