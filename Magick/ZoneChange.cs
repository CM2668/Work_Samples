using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChange : MonoBehaviour
{
	public string zone;

    void OnTriggerEnter(Collider obj)
	{
		if(obj.tag == "Player" && PersistentManager.instance.zone != zone)
		{
			PersistentManager.instance.prepreZone = PersistentManager.instance.preZone;
			PersistentManager.instance.preZone = PersistentManager.instance.zone;
			PersistentManager.instance.zone = zone;
		}
	}
}
