using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambush : MonoBehaviour
{
    public GameObject[] aims;
    public GameObject[] goblins;
    public int goblinsToSpawn;
    public int deadGoblins;
    public bool ambushStart = false;
    public bool ambushFinished = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (ambushStart && !ambushFinished)
        {
            if(PersistentManager.instance.zone != "Forest")
            {
                for(int i = 0; i < goblinsToSpawn; i++)
                {
                    goblins[i].GetComponent<SamBrain>().DeAggro();
                }
                AmbushOver();
            }
            else if (GoblinsGone())
            {
                AmbushOver();
            }
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "Player")
        {
            for (int i = 0; i < goblinsToSpawn; i++)
            {
                goblins[i] = (GameObject)Instantiate(Resources.Load("Sam"), aims[i].transform);
                goblins[i].GetComponent<SamBrain>().enemy = GameObject.FindGameObjectWithTag("Player");
            }
            PersistentManager.instance.totalGoblins += goblinsToSpawn;
            ambushStart = true;
        }
    }

    bool GoblinsGone()
    {
        for(int i = 0; i < goblinsToSpawn; i++)
        {
            if (goblins[i] == null)
                deadGoblins++;
        }
        if (deadGoblins == goblinsToSpawn)
            return true;
        else
            return false;
    }

    void AmbushOver()
    {
        ambushFinished = true;
        PersistentManager.instance.TaskComplete(true, "You have survived the goblin ambush.");
        PersistentManager.instance.surviveAmbushCB.SetActive(true);
        for (int i = 0; i < goblinsToSpawn; i++)
            Destroy(aims[i].gameObject);
        Destroy(gameObject);
    }
}
