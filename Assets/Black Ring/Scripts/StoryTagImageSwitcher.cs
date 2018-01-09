using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTagImageSwitcher : MonoBehaviour {
	[System.Serializable]
	public struct SpriteNamePair {
		public string tagSuffix;
		public Sprite sprite;
	}

	public StoryReading.StoryReader reader;
	public Image imageComponent;
	public string tagPrefix;
	public SpriteNamePair[] sprites;

	static char[] tagSplitSeparator = new char[]{ '_' };

	public void Awake() {
		reader.storyTagsEncountered.AddListener( OnTagsEncountered );
	}

	public void OnTagsEncountered(List<string> tags) {
		foreach( string tag in tags) {
			string[] splitTag = tag.Split( tagSplitSeparator );

			if (splitTag.GetLength(0) != 2) return;

			if (splitTag[0] == tagPrefix) {
				foreach (SpriteNamePair pair in sprites) {
					if (splitTag[1] == pair.tagSuffix) {
						imageComponent.sprite = pair.sprite;
						return;
					}
				}
			}
		}
	}
}
