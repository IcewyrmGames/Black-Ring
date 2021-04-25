namespace IceWyrm {
	public readonly struct StoryChoice {
		public readonly string text;
		public readonly int index;

		public StoryChoice(string inText, int inIndex) {
			text = inText;
			index = inIndex;
		}
	}
}