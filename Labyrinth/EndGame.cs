using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
	public Button goAgain, toMain, quit, addScore;
	public InputField playerName;

	public string[] saves, current;

	void Awake()
	{
		goAgain.onClick.AddListener(GoAgain);
		toMain.onClick.AddListener(ToMain);
		quit.onClick.AddListener(Quit);
		addScore.onClick.AddListener(AddScore);

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		ReadLeaderboard();
	}

	void GoAgain()
	{
		Controller con = GameObject.Find("Controller").GetComponent<Controller>();
		if (con.isBaby)
			SceneManager.LoadScene(3);
		if (con.isNormal || con.isHard)
			SceneManager.LoadScene(1);

	}
	void ToMain()
	{
		Controller con = GameObject.Find("Controller").GetComponent<Controller>();
		con.leaderboardNames.Clear();
		con.leaderboardTimes.Clear();
		SceneManager.LoadScene(0);
	}
	void Quit() { Application.Quit(); }
	void AddScore()
	{
		Controller con = GameObject.Find("Controller").GetComponent<Controller>();
		con.AddToLeaderboard(playerName.text);
		con.SaveScore(playerName.text, con.endTime);
		playerName.gameObject.SetActive(false);
		addScore.gameObject.SetActive(false);
		UpdateLeaderboard();
	}
	void ReadLeaderboard()
	{
		Controller con = GameObject.Find("Controller").GetComponent<Controller>();
		StreamReader reader = new StreamReader(con.savePath);
		string saveFile = reader.ReadToEnd(); reader.Close();
		char[] delim1 = new char[] { '\n' }; char[] delim2 = new char[] { '\t' };

		saves = saveFile.Split(delim1);
		for(int i = 0; i < saves.Length; i++)
		{
			current = saves[i].Split(delim2);
			con.AddToLeaderboard(current[0], int.Parse(current[1]));
		}
		UpdateLeaderboard();
	}
	void UpdateLeaderboard()
	{
		Controller con = GameObject.Find("Controller").GetComponent<Controller>();
		con.SortLeaderboard();
		for(int i = 0; i < con.leaderboardNames.Count && i < 6; i++)
		{
			GameObject.Find("Name " + i).GetComponent<Text>().text = con.leaderboardNames[i];
			GameObject.Find("Score " + i).GetComponent<Text>().text = con.leaderboardTimes[i].ToString();
		}
	}
}
