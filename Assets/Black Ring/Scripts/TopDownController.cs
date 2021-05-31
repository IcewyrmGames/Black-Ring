using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), HotReloadInvokable]
public class TopDownController : MonoBehaviour {
	[SerializeField] IceWyrm.StoryReader reader;

	//The max speed at which the character can move in any direction
	[SerializeField] float maxMoveSpeed = 5.0f;

	Rigidbody2D rb;
	Vector2 inputDirection = Vector2.zero;

	Interactable hoveredInteractable;
	List<Collider2D> scratchColliders = new List<Collider2D>();

	void Start() {
		rb = GetComponent<Rigidbody2D>();

		if (reader) {
			reader.storyUpdated.AddListener(OnStoryUpdated);
			reader.storyEnded.AddListener(OnStoryEnded);

			enabled = !reader.isInProgress;
		}
	}

	// Update is called once per frame
	void Update() {
		inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

		UpdateHoveredInteractable();

		if (Input.GetButtonDown("Jump") && hoveredInteractable) {
			hoveredInteractable.Interact();
		}
	}

	void OnDisable() {
		inputDirection = Vector2.zero;
	}

	void FixedUpdate() {
		rb.velocity = inputDirection * maxMoveSpeed;
		inputDirection = Vector2.zero;
	}

	void OnHotReload() {
		reader.storyUpdated.AddListener(OnStoryUpdated);
		reader.storyEnded.AddListener(OnStoryEnded);
	}

	void OnStoryUpdated(IceWyrm.StoryView view) {
		//When the story is updated, including when jumping to a new stitch, disable any player control of the character
		enabled = false;
	}

	void OnStoryEnded() {
		//Once the story has ended, we can re-enable control of the character
		enabled = true;
	}

	void UpdateHoveredInteractable() {
		Vector2 position = transform.position;

		//Do an overlap to find everything that could have an interactable we can interact with
		ContactFilter2D filter = default(ContactFilter2D).NoFilter();
		scratchColliders.Clear();
		Physics2D.OverlapCircle(position, 1.5f, filter, scratchColliders);

		//Find the closest interactable to the player in 2D space
		float closestSquaredDistance = float.MaxValue;
		Interactable closestInteractable = null;
		foreach (Collider2D collider in scratchColliders) {
			//Check the distance first because it's faster than getting and testing the interactable component.
			//If this object is not closest than our closest interactable then it's already irrelevant whether it has an interactable component.
			Vector2 interactablePosition = collider.transform.position;
			float squaredDistance = (interactablePosition - position).sqrMagnitude;

			if (squaredDistance < closestSquaredDistance) {
				Interactable interactable = collider.gameObject.GetComponent<Interactable>();
				if (interactable && interactable.CanInteract()) {
					closestSquaredDistance = squaredDistance;
					closestInteractable = interactable;
				}
			}
		}

		//If the hovered interactable is no longer the closest, switch to hovering it instead of the old one.
		if (hoveredInteractable != closestInteractable) {
			if (hoveredInteractable) {
				hoveredInteractable.OnUnhover();
			}
			hoveredInteractable = closestInteractable;
			if (hoveredInteractable) {
				hoveredInteractable.OnHover();
			}
		}
	}
}
