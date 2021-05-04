using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class GameZoneTrigger : MonoBehaviour
{
    public GameObject Recipient;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            Debug.Log(other.gameObject);
            Recipient.GetComponent<GameController>().BeginGame();
        }
    }
}
