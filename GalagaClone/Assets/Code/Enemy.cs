using UnityEngine;

//public enum State
//{
//	Stay,
//	Move
//}

public class Enemy : MonoBehaviour
{
	public float Speed = 50f;
	public float ShootPeriod = 5f;
	public float MovePeriod = 2f;
	public Transform BulletPrefab;

	public int Score = 50;

	//private State _state;
	private float _shootTime = 0.0f;
	private float _moveTime = 0.0f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (IsReadytoMove())
		{
			Move();
		}

		if (IsReadytoShoot())
		{
			Fire();
		}
	}

	private bool IsReadytoShoot()
	{
		if (Time.time > _shootTime)
		{
			_shootTime += ShootPeriod;
			return true;
		}

		return false;
	}

	private bool IsReadytoMove()
	{
		if (Time.time > _moveTime)
		{
			_moveTime += MovePeriod;
			return true;
		}

		return false;
	}

	private void Fire()
	{
		Instantiate(BulletPrefab, transform);
	}

	private void Move()
	{
		transform.position = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
		Vector3 down = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
		Vector3 up = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
		if (transform.position.y > up.y || transform.position.y < down.y)
		{
			transform.position = new Vector2(transform.position.x, -transform.position.y);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == GalagaHelper.GetTag(Tags.Player))
		{
			GameManager.Instance.Ship.Lives--;
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}
