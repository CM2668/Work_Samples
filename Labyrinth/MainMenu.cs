using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public GameObject controller, mainCamera, guide, player;
	public Button any;
	public Light guideLight;

	[Header("Main Menu")]
	public GameObject main;
	public Button start, options, quit;

	[Header("Difficulty")]
	public GameObject difficulty;
	public Button baby, normal, hard, diffBack;

	[Header("Options")]
	public GameObject option;
	public GameObject doApply;
	public Slider volumeI;
	public Button opBack, apply, yes, no;

	[Header("Settings")]
	public float volume;

	[Header("Animation Settings")]
	public float frameRate;
	public float frameRateMod;
	public float optionsX, optionsZ;
	public float diffX, diffZ;

	void Awake()
	{
		any.onClick.AddListener(Any);
		start.onClick.AddListener(StartGame);
		options.onClick.AddListener(Options);
		quit.onClick.AddListener(Quit);
		baby.onClick.AddListener(Baby);
		normal.onClick.AddListener(Normal);
		hard.onClick.AddListener(Hard);
		diffBack.onClick.AddListener(DiffBack);
		opBack.onClick.AddListener(OpBack);
		apply.onClick.AddListener(Apply);
		yes.onClick.AddListener(Yes);
		no.onClick.AddListener(No);

		volumeI.onValueChanged.AddListener(delegate { volume = volumeI.value; });
	}

	void Start()
	{
		controller = GameObject.Find("Controller");
		//guide.GetComponent<ParticleSystem>().Stop();

		main.SetActive(true);
		difficulty.SetActive(false);
		option.SetActive(false);
	}

	void Update()
	{

		if (guide.transform.position.y < -2.5f || guide.transform.position.y > -2f)
			guide.transform.position = new Vector3(guide.transform.position.x, -2f, guide.transform.position.z);

		mainCamera.transform.position = new Vector3(guide.transform.position.x + 3.25f, mainCamera.transform.position.y, guide.transform.position.z + 1f);
	}

	void Any() { StartCoroutine(AnyButton()); }

#region Main Menu Controls
	void StartGame() { StartCoroutine(ToDifficultyScene()); }
	void Options() { StartCoroutine(ToOptionsScene()); }
	void Quit() { Application.Quit(); }
#endregion
#region Difficulty Controls
	void Baby() { controller.GetComponent<Controller>().isBaby = true; UnityEngine.SceneManagement.SceneManager.LoadScene(3); }
	void Normal() { controller.GetComponent<Controller>().isNormal = true; UnityEngine.SceneManagement.SceneManager.LoadScene(1); }
	void Hard() { UnityEngine.SceneManagement.SceneManager.LoadScene(1); }
	void DiffBack() { StartCoroutine(FromDifficulty()); }
#endregion
#region Option Menu Controlls
	void OpBack()
	{
		if (controller.GetComponent<Controller>().CheckSettingsApplied(volume))
			StartCoroutine(FromOptions());
		else
			doApply.SetActive(true);
	}
	void Apply()
	{
		controller.GetComponent<Controller>().ApplySettings(volume);
	}
	void Yes() { Apply(); OpBack(); }
	void No()
	{
		Controller con = controller.GetComponent<Controller>();
		volume = con.volume;
		volumeI.value = volume;
		doApply.SetActive(false);
		OpBack();
	}
#endregion

	IEnumerator ToOptionsScene()
	{
		main.SetActive(false);

		for (int i = 0; i < 2.5f * 30; i++)
		{
			guide.transform.position = new Vector3(guide.transform.position.x - (optionsX * frameRate), guide.transform.position.y, guide.transform.position.z - (optionsZ * frameRate));
			yield return new WaitForSecondsRealtime(frameRate * frameRateMod);
		}

		//guide.GetComponent<Animator>().SetBool("toOptions", true);
		//yield return new WaitForSecondsRealtime(2.5f);
		//guide.GetComponent<Animator>().SetBool("toOptions", false);
		option.SetActive(true);
	}

	IEnumerator FromOptions()
	{
		option.SetActive(false);

		for (int i = 0; i < 2.5f * 30; i++)
		{
			guide.transform.position = new Vector3(guide.transform.position.x + (optionsX * frameRate), guide.transform.position.y, guide.transform.position.z + (optionsZ * frameRate));
			yield return new WaitForSecondsRealtime(frameRate * frameRateMod);
		}

		//guide.GetComponent<Animator>().SetBool("toMainFromOptions", true);
		//yield return new WaitForSecondsRealtime(2.5f);
		//guide.GetComponent<Animator>().SetBool("toMainFromOptions", false);
		main.SetActive(true);
	}

	IEnumerator ToDifficultyScene()
	{
		main.SetActive(false);

		for (int i = 0; i < 2.5f * 30; i++)
		{
			guide.transform.position = new Vector3(guide.transform.position.x + (diffX * frameRate), guide.transform.position.y, guide.transform.position.z + (diffZ * frameRate));
			yield return new WaitForSecondsRealtime(frameRate * frameRateMod);
		}

		//guide.GetComponent<Animator>().SetBool("toDifficulty", true);
		//yield return new WaitForSecondsRealtime(2.5f);
		//guide.GetComponent<Animator>().SetBool("toDifficulty", false);
		difficulty.SetActive(true);
	}

	IEnumerator FromDifficulty()
	{
		difficulty.SetActive(false);

		for (int i = 0; i < 2.5f * 30; i++)
		{
			guide.transform.position = new Vector3(guide.transform.position.x - (diffX * frameRate), guide.transform.position.y, guide.transform.position.z - (diffZ * frameRate));
			yield return new WaitForSecondsRealtime(frameRate * frameRateMod);
		}

		//guide.GetComponent<Animator>().SetBool("toMainFromDifficulty", true);
		//yield return new WaitForSecondsRealtime(2.5f);
		//guide.GetComponent<Animator>().SetBool("toMainFromDifficulty", false);
		main.SetActive(true);
	}

	IEnumerator AnyButton()
	{
		any.gameObject.SetActive(false);

		for(int i = 0; i < 30; i++)
		{
			Color temp = any.gameObject.GetComponent<Image>().color;
			temp.a = temp.a - 1 * frameRate;
			any.gameObject.GetComponent<Image>().color = temp;
			guideLight.intensity = guideLight.intensity + (1.57f * frameRate);
			yield return new WaitForSecondsRealtime(frameRate);
		}

		guide.GetComponent<ParticleSystem>().Play();
		main.SetActive(true);
		player.SetActive(true);
	}
}