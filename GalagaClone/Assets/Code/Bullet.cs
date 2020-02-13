using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float Speed = 100f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime);
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}
