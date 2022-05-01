using UnityEngine;

public class TongueMovement : MonoBehaviour {
	
	[SerializeField] private TongueControl tongue;
	[SerializeField] private LayerMask attachLayerMask;

	private PlayerControls _controls;
	
	private void Awake() {
		_controls = new PlayerControls();
	}
	
	private void OnEnable() {
		_controls.Enable();
	}

	private void OnDisable() {
		_controls.Disable();
	}

	private void Update() {
		//makes tongue tip touch attach point while being attached
		if (tongue.IsAttached()) {
			Vector2 attachPoint = tongue.GetAttachPoint();
			transform.right = GetAimDir(attachPoint);
			float distance = (attachPoint - (Vector2) transform.position).magnitude;
			tongue.SetExtendDistance(distance);
			
		//animates tongue extend on left click
		} else if (!tongue.IsExtending() && _controls.Player.TongueShoot.WasPerformedThisFrame()) {
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(_controls.Player.MousePos.ReadValue<Vector2>());
			transform.right = GetAimDir(mousePos);
			tongue.PlayExtend();
		}
	}

	private Vector2 GetAimDir(Vector2 aim) {
		return new Vector2(
			aim.x - transform.position.x,
			aim.y - transform.position.y);
	}

	public void OnTongueTrigger(Collider2D other) {
		if (!tongue.IsExtending()) {
			return;
		}
		if (IsAttachable(other.gameObject)) {
			tongue.Attach(other);
		}
	}

	private bool IsAttachable(GameObject other) {
		return (attachLayerMask & 1 << other.layer) != 0;
	}
}
