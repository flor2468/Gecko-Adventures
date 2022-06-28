using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCloseup : MonoBehaviour {

	[SerializeField] private PickupHandler pickupHandler;
	[SerializeField] private Animator transition;

	private PlayerControls _controls;
	private bool _isVisible;
	
	private void OnEnable() {
		_controls = new PlayerControls();
		_controls.Enable();

		pickupHandler.maskCollectEvent.AddListener(OnMaskCollect);
	}

	private void OnDisable() {
		_controls.Disable();
	}

	public void OnMaskCollect() {
		transition.SetTrigger("FadeIn");
		_isVisible = true;
	}

	private void Update() {
		if (_isVisible && _controls.Player.Interact.WasPerformedThisFrame()) {
			_isVisible = false;
			transition.SetTrigger("FadeOut");
		}
	}
}