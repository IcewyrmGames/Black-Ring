using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StoryScrollTextBox : MonoBehaviour {
	[SerializeField] TMP_Text textComponent;
	[SerializeField] Image newTextBoxImage;
	[SerializeField] LayoutElement newTextBoxLayoutElement;
	[SerializeField] HorizontalLayoutGroup layoutGroup;

	public string text {
		get { return textComponent.text; }
		set { textComponent.text = value; }
	}

	public void Settle(float duration) {
		if (duration > 0.0f) {
			newTextBoxImage.DOFade(0.0f, duration).onComplete += ()=> newTextBoxImage.gameObject.SetActive(false);
			newTextBoxLayoutElement.DOPreferredSize(new Vector2(0.0f, 0.0f), duration);
			DOTween.To(()=>layoutGroup.spacing, (x)=>layoutGroup.spacing = x, 0.0f, duration);

		} else {
			newTextBoxImage.gameObject.SetActive(false);
			newTextBoxLayoutElement.preferredWidth = 0.0f;
			layoutGroup.spacing = 0.0f;
		}
	}
}
