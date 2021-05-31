using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryInteractable : Interactable {
	//The stitch in the story we will jumpt to when interacting, in the form <KnotName> or <KnotName>.<StitchName>
	[SerializeField] string targetStitch;
	//Ignore this interactable if the target stitch has already been played before
	[SerializeField] bool ignoreIfAlreadyPlayed;

	public override bool CanInteract() {
		return IceWyrm.StoryReader.instance && (!ignoreIfAlreadyPlayed || !IceWyrm.StoryReader.instance.HasPlayedStitch(targetStitch));
	}

	public override void Interact() {
		IceWyrm.StoryReader.instance.JumpToStitch(targetStitch);
	}
}
