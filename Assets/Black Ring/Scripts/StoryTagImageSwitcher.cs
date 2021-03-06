﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HotReloadInvokable]
public class StoryTagImageSwitcher : MonoBehaviour {
	[System.Serializable]
	public struct SpriteNamePair {
		public string tagSuffix;
		public Sprite sprite;
	}

	[SerializeField] IceWyrm.StoryReader reader;

	public Image imageComponent;
	public string tagPrefix;
	public SpriteNamePair[] sprites;

	static char[] tagSplitSeparator = new char[]{ '_' };

	public void Start() {
		reader.storyUpdated.AddListener(OnStoryUpdated);
	}

	void OnHotReload() {
		Start();
	}

	void OnStoryUpdated(IceWyrm.StoryView view) {
		foreach (string tag in view.tags) {
			string[] splitTag = tag.Split(tagSplitSeparator);

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
