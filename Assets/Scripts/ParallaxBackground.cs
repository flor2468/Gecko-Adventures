using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

	[SerializeField] [Range(0, 1)] private float parallaxEffect = 1;
	
	private Vector3 _startPos;
	private float _width;
	private Camera _cam;
	
	private void Start() {
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		
		_cam = Camera.main;
		_width = renderer.bounds.size.x;
		_startPos = transform.position;

		renderer.drawMode = SpriteDrawMode.Tiled;
		renderer.size += new Vector2(2 * _width, 0);
	}

	private void LateUpdate() {
		float camDist = _cam.transform.position.x * (1 - parallaxEffect);
		float dist = _cam.transform.position.x * parallaxEffect;
	
		if (camDist > _startPos.x + 0.5*_width) {
			_startPos.x += _width;
		} else if (camDist < _startPos.x - 0.5*_width) {
			_startPos.x -= _width;
		}
		Vector3 position = transform.position;
		position.x = _startPos.x + dist;
		transform.position = position;
	}
	
	private void CreateCopy(Vector3 offset) {
		GameObject copy = Instantiate(gameObject, transform.position + offset, Quaternion.identity, transform);
		Destroy(copy.GetComponent<ParallaxBackground>());
		
		foreach (Transform child in copy.transform) {
			Destroy(child.gameObject);
		}
	}
}