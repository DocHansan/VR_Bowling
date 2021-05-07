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
    public int Throw = -1;
    public int PlayerTry = 0;
    public int DroppedPinsCount;

    private bool _IsGameBegin = false;
    private float _timer = 0;
    private Vector3 _PinRotation;

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
                Throws[Throw] = DroppedPinsCount;
                Recipient.GetComponent<ScoreDrawer>().UpdateThrows();
                Recipient.GetComponent<ScoreDrawer>().UpdateFrames();

                if (PlayerTry == 2 || DroppedPinsCount == 10)
                {
                    if (PlayerTry == 1 && Throw < Throws.Length - 3)
                    {
                        Throw += 1;
                    }
                    RespawnPins();
                    PlayerTry = 0;
                }
            }
        }
    }

    //Calls when ball in trigger zone
    public void BeginGame()
    {
        if (!_IsGameBegin && Throw < Throws.Length - 2)
        {
            PlayerTry += 1;
            _IsGameBegin = true;
            Throw += 1;
        }
        else if (Throw == Throws.Length - 2 && (Throws[Throws.Length - 3] == 10 || Throws[Throws.Length - 3] + Throws[Throws.Length - 2] == 10))
        {
            PlayerTry += 1;
            _IsGameBegin = true;
            Throw += 1;
        }
    }

    public void RespawnPins()
    {
        foreach (GameObject Pins in GameObject.FindGameObjectsWithTag("Pin"))
        {
            Destroy(Pins);
        }
        Instantiate(Pins, PinsPosition.transform.position, Quaternion.identity);
    }

    public void RespawnBalls()
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
        DroppedPinsCount = 0;
        foreach (GameObject Pins in GameObject.FindGameObjectsWithTag("Pin"))
        {
            foreach (Transform PinRotation in Pins.GetComponentsInChildren<Transform>())
            {
                if (PinRotation != Pins.GetComponentsInChildren<Transform>()[0])
                {
                    _PinRotation = PinRotation.rotation.eulerAngles;
                    if (_PinRotation[0] <= 250 || _PinRotation[0] >= 290)
                    {
                        DroppedPinsCount += 1;
                        Destroy(PinRotation.gameObject);
                    }
                }
            }
        }
    }

}
