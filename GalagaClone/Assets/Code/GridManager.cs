using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Logical representation of the grid. Manages all the grid behaviour like a grid movement left and right.
/// </summary>
public class GridManager : MonoBehaviour
{
	public float MovingSpeed = 0.5f;
	public float BoundsOffset = 10f;

	public List<GridCell> GridCells = new List<GridCell>();

	private bool _isMovingLeft = true;
	private bool _isMovingRight = false;

	// Start is called before the first frame update
	void Start()
	{
		foreach (Transform cell in transform)
		{
			var gridCell = cell.GetComponent<GridCell>();
			GridCells.Add(gridCell);
			gridCell.FillCell += OnFilledCell;
		}

		GridCells = GridCells.OrderByDescending(cell => cell.FillIndex).ToList();
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
		Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
		Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));

		float offset = 7;
		Vector2 newPosition = transform.position;
		if (_isMovingRight)
		{
			newPosition.x = transform.position.x + MovingSpeed * Time.deltaTime;
		}
		else if (_isMovingLeft)
		{
			newPosition.x = transform.position.x - MovingSpeed * Time.deltaTime;
		}

		if (newPosition.x > left.x + BoundsOffset - offset && newPosition.x < right.x - BoundsOffset - offset)
		{
			transform.position = newPosition;
		}
		else
		{
			_isMovingLeft = !_isMovingLeft;
			_isMovingRight = !_isMovingRight;
		}
	}
}
