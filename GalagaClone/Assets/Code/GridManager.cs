using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

[Flags]
public enum State
{
	None = 0,
	Move = 1,
	Expand = 2
}


/// <summary>
/// Logical representation of the grid. Manages all the grid behaviour like a grid movement left and right.
/// </summary>
public class GridManager : MonoBehaviour
{
	[Header("Adjust grid moving speed when moving left and right")]
	public float MovingSpeed = 0.5f;
	[Header("Adjust grid expanding speed when moving up and down, increasing and decreasing the distance between objects")]
	public float ExpandingSpeed = 0.3f;
	public float BoundsOffset = 10f;

	public List<GridCell> GridCells = new List<GridCell>();
	public List<GridCell> OrderedGridCells = new List<GridCell>();

	public State State = State.Move;

	private bool _isMovingLeft = true;
	private bool _isMovingRight = false;
	private Vector3 _leftBounding;
	private Vector3 _rightBounding;

	// Start is called before the first frame update
	void Start()
	{
		_leftBounding = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
		_rightBounding = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));

		foreach (Transform cell in transform)
		{
			var gridCell = cell.GetComponent<GridCell>();
			GridCells.Add(gridCell);
			gridCell.FillCell += OnFilledCell;
		}

		OrderedGridCells = GridCells.OrderByDescending(cell => cell.FillIndex).ToList();
		startTime = Time.time;
		randomTime =  UnityEngine.Random.Range(1000, 2000); 
	}

	int i = 0;
	private float startTime;
	int randomTime;
	private void Update()
	{
		//if (Time.time - startTime > 10)
		//{
		//	State = State.Expand;
		//	i++;

		//	if (i > randomTime)
		//	{
		//		startTime = Time.time;
		//		randomTime = UnityEngine.Random.Range(1000, 2000);
		//		i = 0;
		//	}
		//}
		//else
		//{
		//	State = State.Move;
		//}

		//if (State == State.Move)
		//{
		//	Move();
		//}
		//else if (State == State.Expand)
		//{
		//	Expand();
		//}
		Move();

		//Expand();
	}

	private void OnDestroy()
	{
		foreach (var gridCell in GridCells)
		{
			gridCell.FillCell -= OnFilledCell;
		}
	}

	private void OnFilledCell()
	{

	}

	public void Move()
	{
		Vector2 newPosition = transform.position;
		if (_isMovingRight)
		{
			newPosition.x = transform.position.x + MovingSpeed * Time.deltaTime;
		}
		else if (_isMovingLeft)
		{
			newPosition.x = transform.position.x - MovingSpeed * Time.deltaTime;
		}

		var offset = 7;
		if (newPosition.x > _leftBounding.x + BoundsOffset - offset && newPosition.x < _rightBounding.x - BoundsOffset - offset)
		{
			transform.position = newPosition;
		}
		else
		{
			_isMovingLeft = !_isMovingLeft;
			_isMovingRight = !_isMovingRight;
		}
	}

	public void Expand()
	{
		float i = 1;
		foreach (var cell in GridCells)
		{
			Vector2 newPosition = cell.transform.position;
			if (_isMovingRight)
			{
				newPosition.x = cell.transform.position.x + ExpandingSpeed * i * Time.deltaTime;
				newPosition.y = cell.transform.position.y - ExpandingSpeed * 1.5f * Time.deltaTime;
			}
			else if (_isMovingLeft)
			{
				newPosition.x = cell.transform.position.x - ExpandingSpeed * i * Time.deltaTime;
				newPosition.y = cell.transform.position.y + ExpandingSpeed * 1.5f * Time.deltaTime;
			}

			i += 0.1f;

			float offset = 5;
			if (newPosition.x > _leftBounding.x + offset && newPosition.x < _rightBounding.x - offset)
			{
				cell.transform.position = newPosition;
			}
			else
			{
				_isMovingLeft = !_isMovingLeft;
				_isMovingRight = !_isMovingRight;
			}
		}
	}
}
