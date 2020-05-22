﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum State
{
	Stay,
	Move
}

public class Enemy : MonoBehaviour
{
	public float Speed = 50f;
	public float ShootPeriod = 5f;
	public float MovePeriod = 2f;
	public Transform BulletPrefab;

	public EnemyType Type;

	public int Score = 50;

	//private State _state;
	private float _shootTime = 0.0f;
	private float _moveTime = 0.0f;

	private MoveByPattern _moveByPatternComponent;
	private bool _canMoveByGrid = false;
	private bool _isMovingByGrid = false;

	private Vector3 _snapToPosition;

	private List<GridCell> _orderedGrid = new List<GridCell>();


	// Start is called before the first frame update
	void Start()
	{
		_moveByPatternComponent = GetComponent<MoveByPattern>();

		if (_moveByPatternComponent == null)
			Debug.Log("Move By Pattern Component is missing");
		_moveByPatternComponent.PatternFinished += OnPatternFinished;

		_moveByPatternComponent.StartPattern();

		foreach (Transform cell in GameManager.Instance.Grid.transform)
		{
			var gridCell = cell.GetComponent<GridCell>();
			_orderedGrid.Add(gridCell);
		}

		_orderedGrid = _orderedGrid.OrderByDescending(cell => cell.FillIndex).ToList();
	}

	private void OnDestroy()
	{
		_moveByPatternComponent.PatternFinished -= OnPatternFinished;
	}

	// Update is called once per frame
	void Update()
	{
		//if (IsReadytoMove())
		//{
		//	Move();
		//}
		MoveByGrid();

		if (IsReadytoShoot())
		{
			Fire();
		}
	}

	private void MoveByGrid()
	{
		if (_isMovingByGrid && transform.position == _snapToPosition)
		{
			_isMovingByGrid = false;
			_canMoveByGrid = false;
			_snapToPosition = Vector3.zero;
		}
		else if (_snapToPosition != Vector3.zero)
		{
			transform.position = Vector3.MoveTowards(transform.position, _snapToPosition, _moveByPatternComponent.MoveSpeed * Time.deltaTime);
		}

		if (_canMoveByGrid && !_isMovingByGrid)
		{
			foreach (var gridCell in _orderedGrid)
			{
				if (gridCell.IsFree && gridCell.Type == Type)
				{
					gridCell.IsFree = false;
					_isMovingByGrid = true;
					transform.position = Vector3.MoveTowards(transform.position, gridCell.transform.position, _moveByPatternComponent.MoveSpeed * Time.deltaTime);
					_snapToPosition = gridCell.transform.position;
					break;
				}
			}
		}
	}

	private void OnPatternFinished()
	{
		_canMoveByGrid = true;
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

	//private void Move()
	//{
	//	transform.position = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
	//	Vector3 down = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
	//	Vector3 up = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
	//	if (transform.position.y > up.y || transform.position.y < down.y)
	//	{
	//		transform.position = new Vector2(transform.position.x, -transform.position.y);
	//	}
	//}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == GalagaHelper.GetTag(Tag.Player))
		{
			GameManager.Instance.Ship.Lives--;
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}
