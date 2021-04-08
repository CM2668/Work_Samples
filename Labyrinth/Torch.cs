using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
	private float timer = 60000f;
	private GameObject con;
	public GameObject flames;
	public Light source;

	void Start()
	{
		con = GameObject.Find("Controller");
	}
	void Update()
	{
		if (timer > 0 && !con.GetComponent<Controller>().paused)
			timer -= Time.deltaTime;
		else if(timer <= 0)
		{
			if (con != null && con.GetComponent<Controller>().isHard)
			{
				if(source.isActiveAndEnabled)
				{
					timer = 20000f;
					source.enabled = false;
					flames.SetActive(false);
				}
			}
			else if(con != null && con.GetComponent<Controller>().isNormal)
			{
				if (source.isActiveAndEnabled)
				{
					timer = 60000f;
					source.enabled = false;
					flames.SetActive(false);
				}
			}
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.CompareTag("Player"))
		{
			source.enabled = true;
			flames.SetActive(true);
			if (con.GetComponent<Controller>().isNormal)
				timer = 60000;
			else if (con.GetComponent<Controller>().isHard)
				timer = 20000;
		}
	}
}
