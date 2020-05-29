using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Ship Ship;
	public Text ScoreText;

	public GameObject Grid;
	public SpawnPoint[] SpawnPoints;

	public List<GridCell> GridCells = new List<GridCell>();

	public int SpawnedEnemies = 0;
	public GameObject EnemiesParentGameObject;
	public GameObject DynamicGameObject;

	private bool _isGridFilled = false;

	private int _score = 0;
	public int Score
	{
		get { return _score; }
		set
		{
			_score = value;
			ScoreText.text = _score.ToString();
		}
	}

	public static GameManager Instance = null;

	void Awake()
	{
		if (!Instance)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		StartCoroutine(SpawAtPointsInSequance(5));

		foreach (Transform cell in Instance.Grid.transform)
		{
			var gridCell = cell.GetComponent<GridCell>();
			GridCells.Add(gridCell);
			gridCell.FillCell += OnFilledCell;
		}

		GridCells = GridCells.OrderByDescending(cell => cell.FillIndex).ToList();

		foreach (var point in SpawnPoints)
		{
			foreach (var pair in point.EnemyGroups)
				SpawnedEnemies += pair.EnemyGroup.Enemies;
		}
	}

	private void OnFilledCell()
	{
		var filledCells = GridCells.Where(cell => cell.IsFree == false);
		if (filledCells.Count() == SpawnedEnemies)
		{
			_isGridFilled = true;
		}
	}

	private void Update()
	{
		if (_isGridFilled)
		{
			MoveGrid();
		}
	}

	IEnumerator SpawAtPointsInSequance(int seconds)
	{
		foreach (var point in SpawnPoints)
		{
			var offset = 0;
			foreach (var pair in point.EnemyGroups)
			{
				SpawnEnemies(pair.EnemyGroup.Enemies,
				pair.EnemyGroup.Offset,
				pair.EnemyGroup.Enemy,
				new Vector2(point.transform.position.x + offset, point.transform.position.y),
				pair.Pattern);
				offset += 5;
			}
			yield return new WaitForSeconds(seconds);
		}
	}

	private void SpawnEnemies(int enemiesToSpawn, float offset, GameObject prefab, Vector3 position, GameObject pattern)
	{
		for (int i = 0; i < enemiesToSpawn; i++)
		{
			var instantiatedEnemy = Instantiate(prefab, new Vector2(position.x + i * offset, position.y), Quaternion.identity);
			instantiatedEnemy.GetComponent<MoveByPattern>().Pattern = pattern;

			if (DynamicGameObject != null && EnemiesParentGameObject != null)
				EnemiesParentGameObject.transform.SetParent(DynamicGameObject.transform);
			instantiatedEnemy.transform.SetParent(EnemiesParentGameObject.transform);
		}
	}

	private void MoveGrid()
	{
		EnemiesParentGameObject.transform.position = Vector3.MoveTowards(EnemiesParentGameObject.transform.position, new Vector2(EnemiesParentGameObject.transform.position.x + 1, EnemiesParentGameObject.transform.position.y), 1f * Time.deltaTime);
	}

	public void LoadGameOverScene()
	{
		SceneManager.LoadScene(GalagaHelper.GetScene(Scene.GameOver));
	}
}

