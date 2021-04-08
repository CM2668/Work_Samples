using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTask : MonoBehaviour
{
    void OnTriggerEnter(Collider obj)
    {
        GameObject tmp = obj.gameObject;
        if(tmp.name == "Plank")
        {
            tmp.GetComponent<Rigidbody>().isKinematic = false;
            PersistentManager.instance.repairBridge = true;
            PersistentManager.instance.repairBridgeCB.SetActive(true);
            PersistentManager.instance.TaskComplete(true, "Bridge to the forest has been repaired.");
        }
    }

    void OnTriggerExit(Collider obj)
    {
        GameObject tmp = obj.gameObject;
        if (tmp.name == "Plank")
        {
            tmp.GetComponent<Rigidbody>().isKinematic = true;
            PersistentManager.instance.repairBridge = false;
            PersistentManager.instance.repairBridgeCB.SetActive(false);
        }
    }
}
