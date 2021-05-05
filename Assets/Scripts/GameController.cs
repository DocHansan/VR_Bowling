using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject PinsPosition;
    public GameObject Pins;
    public GameObject[] BallsPosition;
    public GameObject[] Balls;
    public GameObject Recipient;
    public int[] Throws = new int[21];

    private int _PlayerTry = 0;
    private bool _IsGameBegin = false;
    private float _timer = 0;
    private Vector3 _PinRotation;
    private int _DroppedPinsCount;
    private int _Throw = -1;

    // Start is called before the first frame update
    void Start()
    {
        RespawnBalls();
        RespawnPins();

        for (int i = 0; i < Throws.Length; i++)
        {
            Throws[i] = 0;
        }
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
                RespawnBalls();
                _IsGameBegin = false;
                ÑheckDroppedPins();
                Throws[_Throw] = _DroppedPinsCount;
                Recipient.GetComponent<ScoreDrawer>().UpdateThrows();
                Recipient.GetComponent<ScoreDrawer>().UpdateFrames();

                if (_PlayerTry == 2 || _DroppedPinsCount == 10)
                {
                    if (_PlayerTry == 1 && _Throw < 18)
                    {
                        _Throw += 1;
                    }
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
        if (!_IsGameBegin && _Throw < 19)
        {
            _PlayerTry += 1;
            _IsGameBegin = true;
            _Throw += 1;
        }
        else if (_Throw == 19 && (Throws[18] == 10 || Throws[18] + Throws[19] == 10))
        {
            _PlayerTry += 1;
            _IsGameBegin = true;
            _Throw += 1;
        }
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

    void ÑheckDroppedPins()
    {
        _DroppedPinsCount = 0;
        foreach (GameObject Pins in GameObject.FindGameObjectsWithTag("Pin"))
        {
            foreach (Transform PinRotation in Pins.GetComponentsInChildren<Transform>())
            {
                if (PinRotation != Pins.GetComponentsInChildren<Transform>()[0])
                {
                    _PinRotation = PinRotation.rotation.eulerAngles;
                    if (_PinRotation[0] <= 250 || _PinRotation[0] >= 290)
                    {
                        _DroppedPinsCount += 1;
                        Destroy(PinRotation.gameObject);
                    }
                }
            }
        }
    }

}
