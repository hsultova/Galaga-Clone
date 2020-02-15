using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
	public float Speed = 100f;

	public abstract void Move();
	public abstract void OnCollision(Collider2D collision);

	// Update is called once per frame
	void Update()
	{
		Move();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnCollision(collision);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		OnCollision(collision.collider);
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
