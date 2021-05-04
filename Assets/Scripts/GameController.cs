using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject PinsPosition;
    public GameObject Pins;
    public GameObject[] BallsPosition;
    public GameObject[] Balls;

    private int _PlayerTry = 0;
    private bool _IsGameBegin = false;
    private float _timer = 0;
    private Vector3 _PinRotation;
    private int _DroppedPinsCount;

    // Start is called before the first frame update
    void Start()
    {
        RespawnBalls();
        RespawnPins();
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsGameBegin)
        {
            _timer += Time.deltaTime;
            if (_timer >= 5f)
            {
                _timer = 0;
                Debug.Log(ÑheckDroppedPins());
                RespawnBalls();
                _IsGameBegin = false;
                if (_PlayerTry == 2 || ÑheckDroppedPins() == 10)
                {
                    RespawnPins();
                    _PlayerTry = 0;
                }
            }
        }
        //Debug.Log(_IsGameBegin);
        //Debug.Log(_PlayerTry);
        //Debug.Log(_timer);
    }

    //Calls when ball in trigger zone
    public void BeginGame()
    {
        _PlayerTry += 1;
        _IsGameBegin = true;
    }
    
    void RespawnPins()
    {
        foreach (GameObject Pins in GameObject.FindGameObjectsWithTag("Pin"))
        {
            Destroy(Pins);
        }
        Instantiate(Pins, PinsPosition.transform.position, Quaternion.identity);
    }

    void RespawnBalls()
    {
        foreach (GameObject Ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            Destroy(Ball);
        }
        for (int i = 0; i < Balls.Length; i++)
        {
            Instantiate(Balls[i], BallsPosition[i].transform.position, Quaternion.identity);
        }
    }

    int ÑheckDroppedPins()
    {
        _DroppedPinsCount = -1;
        foreach (GameObject Pins in GameObject.FindGameObjectsWithTag("Pin"))
        {
            foreach (Transform PinRotation in Pins.GetComponentsInChildren<Transform>())
            {
                _PinRotation = PinRotation.rotation.eulerAngles;
                if (_PinRotation[0] <= 250 || _PinRotation[0] >= 290)
                {
                    _DroppedPinsCount += 1;
                }
            }
        }
        return _DroppedPinsCount;
    }
}
