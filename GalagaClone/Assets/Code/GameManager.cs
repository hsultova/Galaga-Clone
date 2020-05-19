using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Ship Ship;
	public Enemy Enemy;
	public Text ScoreText;

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
		SpawnEnemy();
	}

	private void SpawnEnemy()
	{
		Vector3 cameraPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
		Instantiate(Enemy, cameraPosition, Quaternion.identity);
	}

	public void LoadGameOverScene()
	{
		SceneManager.LoadScene(GalagaHelper.GetScene(Scene.GameOver));
	}
}

