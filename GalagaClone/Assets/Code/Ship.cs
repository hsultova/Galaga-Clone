using UnityEngine;

public class Ship : MonoBehaviour
{
	[Header("Ship attributes")]
	public float Speed = 2f;
	public float BoundsOffset = 2f;

	[Header("Camera")]
	public Camera GameCamera;

	[Header("Bullet attributes")]
	public Transform BulletPrefab;
	public float BulletSpawnTime = 0.5f;

	private float _startTime;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Move();
		Fire();
	}

	private void Fire()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(BulletPrefab, transform);
			_startTime = Time.time;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			if (Time.time - _startTime > BulletSpawnTime)
			{
				Instantiate(BulletPrefab, transform);
				_startTime = Time.time;
			}
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			_startTime = 0;
		}
	}

	private void Move()
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
