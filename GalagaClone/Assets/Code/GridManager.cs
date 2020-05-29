using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	public float MovingSpeed = 0.5f;
	public float BoundsOffset = 10f;
	public bool IsGridFilled = false;

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

	private void OnFilledCell()
	{
		var filledCells = GridCells.Where(cell => cell.IsFree == false);
		if (filledCells.Count() == GameManager.Instance.SpawnedEnemies)
		{
			IsGridFilled = true;
		}
	}

	public void Move()
	{
		Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
		Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));

		Vector2 newPosition = transform.position;
		if (_isMovingRight)
		{
			newPosition.x = transform.position.x + MovingSpeed * Time.deltaTime;
		}
		else if (_isMovingLeft)
		{
			newPosition.x = transform.position.x - MovingSpeed * Time.deltaTime;
		}

		if (newPosition.x > left.x + BoundsOffset - 7 && newPosition.x < right.x - BoundsOffset - 7)
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
