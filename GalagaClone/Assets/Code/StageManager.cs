using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
	public GameObject PlayerText;
	public GameObject StageText;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(ShowStageText());
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	IEnumerator ShowStageText()
	{
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(2);

		PlayerText.SetActive(false);
		StageText.SetActive(true);
		yield return new WaitForSeconds(2);

		LoadStage();
	}


	private static void LoadStage()
	{
		SceneManager.LoadScene(GalagaHelper.GetScene(Scenes.Stage1));
	}

}
