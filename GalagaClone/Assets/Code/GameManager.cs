using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Ship Ship;
	public Text ScoreText;

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

	public void LoadGameOverScene()
	{
		SceneManager.LoadScene(GalagaHelper.GetScene(Scenes.GameOver));
	}
}

