using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrap : MonoBehaviour
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
        if (!GetComponent<Animation>().isPlaying)
		{
			gameObject.GetComponent<AudioSource>().Play();
			GetComponent<Animation>().Play();
		}
            
        yield return new WaitForSeconds(1f);

        player.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);

        GameObject.Find("Controller").GetComponent<Controller>().PlayerDeath(player);
    }
}
