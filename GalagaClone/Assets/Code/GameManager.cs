using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Ship Ship;
	public Text ScoreText;

	public GameObject Grid;
	public SpawnPoint[] SpawnPoints;

	public int SpawnedEnemies = 0;
	public GameObject EnemiesParentGameObject;
	public GameObject DynamicGameObject;

	public GridManager GridManager;

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
		GridManager = Grid.GetComponent<GridManager>();

		StartCoroutine(SpawAtPointsInSequance(5));

		foreach (var point in SpawnPoints)
		{
			foreach (var pair in point.EnemyGroups)
				SpawnedEnemies += pair.EnemyGroup.Enemies;
		}
	}	

	private void Update()
	{
		GridManager.Move();
	}

	IEnumerator SpawAtPointsInSequance(int seconds)
	{
		int i = 0;
		foreach (var point in SpawnPoints)
		{
			i++;
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

			if(i < SpawnPoints.Length)
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

	

	public void LoadGameOverScene()
	{
		SceneManager.LoadScene(GalagaHelper.GetScene(Scene.GameOver));
	}
}

