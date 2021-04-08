using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.CompareTag("Player"))
		{
			Controller con = GameObject.Find("Controller").GetComponent<Controller>();
			con.endTime = Time.timeSinceLevelLoad - con.startTime;
			UnityEngine.SceneManagement.SceneManager.LoadScene(2);
		}
	}
}
