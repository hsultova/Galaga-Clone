using UnityEngine;

public class Ship : MonoBehaviour
{
	[Header("Ship attributes")]
	public float Speed = 2f;
	public float BoundsOffset = 2f;

	[Header("Visual")]
	public GameObject life1;
	public GameObject life2;
	public GameObject life3;

	[Header("Camera")]
	public Camera GameCamera;

	[Header("Bullet attributes")]
	public Transform BulletPrefab;
	public float BulletSpawnTime = 0.5f;

	private float _startTime;

	private int _lives = 3;
	private Vector3 _startPosition;

	public int Lives
	{
		get { return _lives; }
		set
		{
			_lives = value;
			if (_lives == 0)
			{
				GameManager.Instance.LoadGameOverScene();
			}
			else
			{
				Reset();
			}

			if (_lives == 1)
			{
				life1.SetActive(true);
				life2.SetActive(false);
				life3.SetActive(false);
			}
			else if (_lives == 2)
			{
				life1.SetActive(true);
				life2.SetActive(true);
				life3.SetActive(false);
			}
			else if (_lives == 3)
			{
				life1.SetActive(true);
				life2.SetActive(true);
				life3.SetActive(true);
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		_startPosition = transform.position;
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

	public void Reset()
	{
		transform.position = _startPosition;
	}
}
