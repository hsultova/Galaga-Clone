using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float Speed = 50f;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
		if (collision.gameObject.tag == "Player")
		{
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}
