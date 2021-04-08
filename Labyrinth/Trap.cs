using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		if (player.CompareTag("Player"))
		{
			Debug.Log("Triggered");
			StartCoroutine(Triggered(player.gameObject));
		}
	}

	IEnumerator Triggered(GameObject player)
	{
		//animation
		player.GetComponent<AudioSource>().Play();
		Debug.Log("playing animation and sound");
		yield return new WaitForSeconds(1f);
		GameObject.Find("Controller").GetComponent<Controller>().PlayerDeath(player);
	}
}
