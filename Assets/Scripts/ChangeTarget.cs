using UnityEngine;

public class ChangeTarget : MonoBehaviour
{
    public GameObject GO;

    public void setTarget()
    {
        // gameObject = GameObject.Find("GroupMember1");
        GO.GetComponent<Tracking>().changeTarget(GameObject.Find("GM2Pos"));
    }
}