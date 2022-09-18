using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ChangeTarget : MonoBehaviour
{
    public GameObject GO;
    public void setTarget()
    {
        // gameObject = GameObject.Find("GroupMember1");
        GO.GetComponent<Tracking>().changeTarget(GameObject.Find("GM2Pos"));
    }

}

