﻿using UnityEngine;

class ShipBullet : Bullet
{
	public override void Move()
	{
		transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime);
	}

	public override void OnCollision(Collider2D collision)
	{
		if (collision.gameObject.tag == GalagaHelper.GetTag(Tags.Enemy))
		{
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}