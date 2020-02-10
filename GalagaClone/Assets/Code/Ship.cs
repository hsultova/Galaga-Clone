using UnityEngine;

public class Ship : MonoBehaviour
{
	public float Speed = 2f;
	public float BoundsOffset = 2f;
	public Camera GameCamera;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 left = GameCamera.ViewportToWorldPoint(new Vector3(0, 0, GameCamera.nearClipPlane));
		Vector3 right = GameCamera.ViewportToWorldPoint(new Vector3(1, 0, GameCamera.nearClipPlane));

		Vector2 newPosition = transform.position;
		if (Input.GetKey(KeyCode.RightArrow))
		{
			newPosition.x = transform.position.x + Speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			newPosition.x = transform.position.x - Speed * Time.deltaTime;
		}

		if (newPosition.x > left.x + BoundsOffset && newPosition.x < right.x - BoundsOffset)
		{
			transform.position = newPosition;
		}
	}
}
