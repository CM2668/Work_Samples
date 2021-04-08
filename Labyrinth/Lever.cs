using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
	public bool hasWalls = true;
	public GameObject[] walls;
	public bool trapOn = true;
	public GameObject[] badFloors;
	public GameObject[] goodFloors;
	public GameObject interactKey;
	public bool isGoal = false;
	public float timer;

	void OnTriggerStay(Collider player)
	{
		if (player.CompareTag("Player"))
		{
			interactKey.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E) && timer <=0)
			{
				timer = 1;
				//lever animation
				gameObject.transform.Rotate(0f, 180f, 0f);
				//wait for animation
				if (hasWalls)
				{
					for (int i = 0; i < walls.Length; i++)
					{
						walls[i].GetComponent<AudioSource>().Play();
						walls[i].transform.Rotate(0f, 90f, 0f, Space.World);
						Debug.Log("Rotated");
					}
				}
				if (trapOn)
				{
					//gameObject.GetComponent<AudioSource>().Play();
					trapOn = false;
					//trap deactivate sound
					for(int i = 0; i < badFloors.Length; i++)
					{
						badFloors[i].SetActive(false);
						goodFloors[i].SetActive(true);
					}
				}
				if (isGoal)
				{
					GameObject.Find("Guide").GetComponent<Guide>().UpdateGoal();
					isGoal = false;
				}
			}
		}
		if(timer > 0)
		{
			timer -= Time.deltaTime;
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.CompareTag("Player"))
		{
			interactKey.SetActive(false);
		}
	}
}
