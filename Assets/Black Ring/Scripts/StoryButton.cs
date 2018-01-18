using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	[SerializeField] TMP_Text textComponent;
	[SerializeField] Image imageComponent;

	[Header( "Normal" )]
	[SerializeField] Color textColor = Color.black;
	[SerializeField] Color backgroundColor = Color.clear;

	[Header( "Hover" )]
	[SerializeField] Color textHoverColor = Color.white;
	[SerializeField] Color backgroundHoverColor = Color.black;

	StoryReading.StoryReader reader;
	int optionIndex;

	public string text {
		get{ return textComponent.text; }
		set{ textComponent.text = value; }
	}

	public void BindOption( StoryReading.StoryReader newReader, int newOptionIndex ) {
		reader = newReader;
		optionIndex = newOptionIndex;
	}

	public void OnEnable() {
        textComponent.color = textColor;
		imageComponent.color = backgroundColor;
	}

	public void OnValidate() {
		if( textComponent ) {
			textComponent.text = gameObject.name;
			textComponent.color = textColor;
		}
		if( imageComponent ) {
			imageComponent.color = backgroundColor;
		}
	}

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
		textComponent.color = textHoverColor;
		imageComponent.color = backgroundHoverColor;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        textComponent.color = textColor;
		imageComponent.color = backgroundColor;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
		reader.ChooseOption( optionIndex );
    }
}
