using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject PinsPosition;
    public GameObject Pins;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Pins, PinsPosition.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
