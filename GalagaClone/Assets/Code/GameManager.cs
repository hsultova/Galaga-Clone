using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Ship Ship;
	public Text ScoreText;

	public GameObject Grid;
	public EnemyGroup[] EnemyGroups;

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
		foreach (var group in EnemyGroups)
		{
			Vector3 cameraPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
			SpawnEnemies(group.Enemies, group.Offset, group.Prefab, cameraPosition);
		}
	}

	private void SpawnEnemies(int enemiesToSpawn, float offset, GameObject prefab, Vector3 position)
	{
		for (int i = 0; i < enemiesToSpawn; i++)
		{
			var instantiatedEnemy = Instantiate(prefab, new Vector2(position.x + i * offset, position.y + 5), Quaternion.identity);			
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

