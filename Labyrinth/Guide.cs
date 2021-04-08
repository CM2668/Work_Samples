using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public List<GameObject> goals;
    public GameObject exit;
    public GameObject currentGoal;
    public GameObject player;
    public bool forceUpdate;

    void Awake()
	{
        UpdateGoal();
	}

    void Update()
    {
        gameObject.transform.position = player.transform.position + new Vector3(0, 1.25f, -.5f);
        gameObject.transform.LookAt(currentGoal.transform);
        gameObject.transform.Rotate(new Vector3(0f, transform.rotation.eulerAngles.y, 0f), Space.World);
    }

    public void UpdateGoal()
	{
        if(goals.Count > 0)
		{
            int i = Random.Range(0, goals.Count);
            currentGoal = goals[i];
            goals.RemoveAt(i);
		}
        else if (goals.Count == 0)
            currentGoal = exit;
	}
}
