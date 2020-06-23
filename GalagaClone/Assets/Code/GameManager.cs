using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Ship Ship;
	public Text ScoreText;

	public GameObject Grid;
	public GridManager GridManager;
	public SpawnPoint[] SpawnPoints;
	public float SecondsBetweenSpawningGroups = 5f;
	public float OffsetBetweenGoups = 5f;
	public int SpawnedEnemies = 0;
	public GameObject EnemiesParentGameObject;
	public GameObject DynamicGameObject;

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

		StartCoroutine(SpawAtPointsInSequance(SecondsBetweenSpawningGroups, OffsetBetweenGoups));

		foreach (var point in SpawnPoints)
		{
			foreach (var pair in point.EnemyGroups)
				SpawnedEnemies += pair.EnemyGroup.Enemies;
		}
	}	

	private void Update()
	{
	}

	IEnumerator SpawAtPointsInSequance(float seconds, float offsetBetweenGroups)
	{
		int i = 0;
		foreach (var point in SpawnPoints)
		{
			i++;
			float offset = 0;
			foreach (var pair in point.EnemyGroups)
			{
				SpawnEnemies(pair.EnemyGroup.Enemies,
				pair.EnemyGroup.Offset,
				pair.EnemyGroup.Enemy,
				new Vector2(point.transform.position.x + offset, point.transform.position.y),
				pair.Pattern);
				offset += offsetBetweenGroups;
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

