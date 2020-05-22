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
			var dynamic = GameObject.FindGameObjectWithTag("Dynamic");
			if (dynamic != null)
				instantiatedEnemy.transform.SetParent(dynamic.transform);
		}
	}

	public void LoadGameOverScene()
	{
		SceneManager.LoadScene(GalagaHelper.GetScene(Scene.GameOver));
	}
}

