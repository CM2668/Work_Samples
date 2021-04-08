using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamBrain : MonoBehaviour
{
	public NavMeshAgent moveSam;
	public GameObject enemy;
    GameObject tmp = null;
	RaycastHit hit;
	Ray ray;
	int layerMask;
	Vector3 home;
	public string homeZone;

	void Start()
    {
        layerMask = 0 << 8;
		layerMask = ~layerMask;
		moveSam = gameObject.GetComponent<NavMeshAgent>();
		home = transform.position;
		//moveSam.ResetPath();
	}

    void Update()
    {
		//if (enemy != null)
		//{
		//	Debug.Log("I SEE YOU");
		//}
		//if (moveSam.hasPath)
		//{
		//	Debug.Log("MOVING");
		//}
	}

	void FixedUpdate()
	{
		if (enemy != null)
		{
			MoveToTarget();
			if (PersistentManager.instance.zone != homeZone)
				DeAggro();
		}
	}

	void OnTriggerStay(Collider obj)
	{
		if(obj.tag == "Player" && enemy == null)
		{
			Eyes();
		}
	}


	void Eyes()
	{
		RaycastHit hit;
		float distance = 15f;
		float angle = 78;
		float segments = angle - 1;
		Vector3 startPos = transform.position + (Vector3.up * 2);
		Vector3 targetPos = new Vector3();
		float startAngle = -angle * .5f;
		float finishAngle = angle * .5f;
		float increment = angle / segments;

		for(float i = startAngle; i < finishAngle; i += increment)
		{
			targetPos = (Quaternion.Euler(0, i, 0) * transform.forward).normalized * distance;

			if (Physics.Raycast(startPos, targetPos, out hit, distance))
			{
				//Debug.Log("Hit " + hit.collider.gameObject.name);
				if (hit.collider.gameObject.tag == "Player")
				{
					enemy = hit.collider.gameObject;
					//Debug.Log("REEEE");
				}
			}
			//Debug.DrawRay(startPos, targetPos, Color.red);
		}
	}
	/*
	void OnTriggerStay(Collider obj)
	{
		if(obj.tag == "Player")
		{
            if (enemy == null)
            {
                ray = new Ray(transform.position, transform.forward);
                //Debug.DrawRay(ray.origin, ray.direction, Color.red, 5000f);
                GetTarget();
            }
		}
	}

	void GetTarget()
	{
		if (Physics.Raycast(ray, out hit, 50, layerMask))
		{
			if (hit.transform.gameObject.tag == "Player")
			{
				enemy = hit.transform.gameObject;
				gameObject.GetComponent<Wander>().enabled = false;
			}
		}
		
	}
	*/

	void MoveToTarget()
	{
		transform.LookAt(enemy.transform);
		moveSam.SetDestination(enemy.transform.position);
		//Debug.Log(Vector3.Distance(gameObject.transform.position, enemy.transform.position));
		if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) <= 1.5f)
			Shank();
	}

	public void DeAggro()
	{
		enemy = tmp;
        enemy = null;
		gameObject.GetComponent<Wander>().enabled = true;
	}

	void Shank()
	{
		if(enemy.tag == "Player")
		{
			PersistentManager.instance.resetWorld();
		}
	}
}
