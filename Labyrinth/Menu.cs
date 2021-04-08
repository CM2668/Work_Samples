using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public GameObject controller;

	[Header("Pause Menu")]
	public GameObject pauseMenu;
	public Button resume, options, quit;

	[Header("Options")]
	public GameObject optionMenu;
	public GameObject doApply;
	public Slider volumeI;
	public Button back, apply, yes, no;


	[Header("Settings")]
	float volume;

	void Start()
	{
		controller = GameObject.Find("Controller");
		controller.GetComponent<Controller>().startTime = Time.timeSinceLevelLoad;

		resume.onClick.AddListener(Resume);
		options.onClick.AddListener(Options);
		quit.onClick.AddListener(Quit);
		back.onClick.AddListener(Back);
		apply.onClick.AddListener(Apply);
		yes.onClick.AddListener(Yes);
		no.onClick.AddListener(No);

		volumeI.onValueChanged.AddListener(delegate { volume = volumeI.value; });

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		pauseMenu.SetActive(false);
		optionMenu.SetActive(false);
		controller.GetComponent<Controller>().paused = false;
		controller.GetComponent<Controller>().SetBaseSettings();
		controller.GetComponent<Controller>().ApplySettings();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			pauseMenu.SetActive(true);
			controller.GetComponent<Controller>().paused = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	void Resume()
	{
		pauseMenu.SetActive(false);
		optionMenu.SetActive(false);
		controller.GetComponent<Controller>().paused = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	void Options() { pauseMenu.SetActive(false); optionMenu.SetActive(true); }
	void Quit()
	{
		controller.GetComponent<Controller>().paused = false;
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
	void Back()
	{
		if (controller.GetComponent<Controller>().CheckSettingsApplied(volume))
		{
			optionMenu.SetActive(false);
			pauseMenu.SetActive(true);
		}
		else
			doApply.SetActive(true);
	}
	void Apply() { controller.GetComponent<Controller>().ApplySettings(volume); }
	void Yes() { Apply(); Back(); }
	void No()
	{
		Controller con = controller.GetComponent<Controller>();
		volume = con.volume;
		volumeI.value = volume;
		doApply.SetActive(false);
		Back();
	}
}
