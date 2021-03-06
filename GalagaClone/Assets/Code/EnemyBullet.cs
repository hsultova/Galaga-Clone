﻿using UnityEngine;

class EnemyBullet : Bullet
{
	public override void Move()
	{
		transform.position = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
	}

	public override void OnCollision(Collider2D collision)
	{
		if (collision.gameObject.tag == GalagaHelper.GetTag(Tag.Player))
		{
			GameManager.Instance.Ship.Lives--;
			Destroy(gameObject);
		}
	}
}

