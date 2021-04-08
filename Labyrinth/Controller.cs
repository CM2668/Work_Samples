using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
	public static Controller instance { get; private set; } 
	public bool isBaby, isNormal, isHard, paused;
	public Vector3 babyStart, normalStart;

	[Header("Settings")]
	public List<float> baseVolumes;
	public float volume;

	public AudioSource[] sounds;

	[Header("Leaderboard")]
	public string savePath;
	public float startTime, endTime;
	public List<float> leaderboardTimes;
	public List<string> leaderboardNames;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void PlayerDeath(GameObject player)
	{
		if (isBaby)
		{
			player.transform.position = babyStart;
			player.transform.Rotate(Vector3.down);
		}
		else if (isNormal)
		{
			player.transform.position = normalStart;
			player.transform.Rotate(Vector3.up);
		}
		else if (isHard)
		{
			SceneManager.LoadScene(1);
		}
	}

	#region Settings
	public void ApplySettings(float vol) { volume = vol; ApplySettings(); }
	public void ApplySettings()
	{
		sounds = FindObjectsOfType<AudioSource>();
		if(sounds.Length > 0)
		{
			for (int i = 0; i < sounds.Length; i++)
			{
				sounds[i].volume = (volume / 100) * baseVolumes[i];
			}
		}
	}
	public bool CheckSettingsApplied(float vol)
	{
		if (vol == volume)
			return true;
		else
			return false;
	}
	public void SetBaseSettings()
	{
		AudioSource[] sounds = FindObjectsOfType<AudioSource>();
		for(int i = 0; i < sounds.Length; i++)
		{
			baseVolumes.Add(sounds[i].volume);
		}
	}
	#endregion

	#region Leaderboard
	public void AddToLeaderboard(string name) { leaderboardTimes.Add(endTime); leaderboardNames.Add(name); SortLeaderboard(); }
	public void AddToLeaderboard(string name, float time) { leaderboardTimes.Add(time); leaderboardNames.Add(name); SortLeaderboard(); }
	public void SaveScore(string name, float time)
	{
		StreamWriter writer = new StreamWriter(savePath, true);
		writer.Write("\n" + name + "\t" + time);
		writer.Close();
	}
	public void SortLeaderboard()
	{
		for(int i = 0; i < leaderboardTimes.Count; i++)
		{
			for(int j = i + 1; j < leaderboardTimes.Count; j++)
			{
				if(leaderboardTimes[i] > leaderboardTimes[j])
				{
					float time = leaderboardTimes[i]; string name = leaderboardNames[i];
					leaderboardTimes[i] = leaderboardTimes[j]; leaderboardNames[i] = leaderboardNames[j];
					leaderboardTimes[j] = time; leaderboardNames[j] = name;
				}
			}
		}
	}
	#endregion
}
