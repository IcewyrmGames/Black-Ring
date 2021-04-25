using UnityEngine;
using TMPro;

public class StoryWindow : MonoBehaviour {
	[SerializeField] IceWyrm.StoryReader reader;
	[SerializeField] TMP_Text textComponent;

	public void OnStoryUpdated(IceWyrm.StoryView view) {
		if (view.ContainsText()) {
			textComponent.text = view.text;
		}
	}
}
