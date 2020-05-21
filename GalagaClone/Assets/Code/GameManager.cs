using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Ship Ship;
	public Text ScoreText;

	public GameObject Grid;

	[Header("Enemies")]
	public Enemy Enemy;
	[SerializeField]
	private int enemiesToSpawn;
	[SerializeField]
	private float enemyOffset;

	public List<EnenmySpritePair> EnemySprites;

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
		SpawnEnemies(enemiesToSpawn, enemyOffset);
	}

	private void SpawnEnemies(int max, float offset)
	{
		for (int i = 0; i < max; i++)
		{
			var random = Random.Range(0, 3);
			Enemy.GetComponent<SpriteRenderer>().sprite = EnemySprites.ToArray()[random].Image;

			Vector3 cameraPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
			var instantiatedEnemy = Instantiate(Enemy, new Vector2(cameraPosition.x + i*offset, cameraPosition.y + 5), Quaternion.identity);
			var dynamic = GameObject.FindGameObjectWithTag("Dynamic");
			if(dynamic != null)
				instantiatedEnemy.transform.SetParent(dynamic.transform);
			
		}

	}

	public void LoadGameOverScene()
	{
		SceneManager.LoadScene(GalagaHelper.GetScene(Scene.GameOver));
	}
}

