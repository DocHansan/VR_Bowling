using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ScoreDrawer : MonoBehaviour
{
    public Text[] Frames;
    public Text[] Throws;
    public GameObject Sender;

    public int[] _Throws;
    private int _SumFrame = 0;
    private bool _IsStrike = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateThrows();
        UpdateFrames(); 
    }

    public void UpdateThrows()
    {
        _Throws = Sender.GetComponent<GameController>().Throws;

        for (int i = 0; i < _Throws.Length; i++)
        {

            if (i == _Throws.Length - 2 && _Throws[i] == 10)
            {
                Throws[i].text = "X";
            }

            else if (_Throws[i] == 10 && i % 2 == 0)
            {
                Throws[i].text = "X";
            }

            else if (i > 0 && _Throws[i - 1] == 10 && i % 2 == 1)
            {
                Throws[i].text = "";
            }

            else if (i > 0 && _Throws[i - 1] + _Throws[i] == 10 && i % 2 == 1)
            {
                Throws[i].text = "/";
            }
            
            else if (_Throws[i] == 0)
            {
                Throws[i].text = "-";
            }

            else
            {
                Throws[i].text = _Throws[i].ToString();
            }
        }
    }

    public void UpdateFrames()
    {
        _SumFrame = 0;
        for (int i = 0; i < _Throws.Length - 3; i++)
        {
            if (i % 2 == 0)
            {
                _SumFrame += _Throws[i];
                _IsStrike = false;
                if (_Throws[i] == 10)
                {
                    _SumFrame += _Throws[i + 2];
                    if (i == _Throws.Length - 5)
                    {
                        _SumFrame += _Throws[i + 2];
                        _SumFrame += _Throws[i + 3];
                        Frames[i / 2].text = _SumFrame.ToString();
                        _IsStrike = true;
                        continue;
                    }
                    if (_Throws[i + 2] == 10)
                    {
                        _SumFrame += _Throws[i + 4];
                    }
                    else
                    {
                        _SumFrame += _Throws[i + 3];
                    }
                    Frames[i / 2].text = _SumFrame.ToString();
                    _IsStrike = true;
                }
            }
            else if (!_IsStrike)
            {
                _SumFrame += _Throws[i];
                if (_Throws[i] + _Throws[i - 1] == 10)
                {
                    _SumFrame += _Throws[i + 1];
                }
                Frames[i / 2].text = _SumFrame.ToString();
            }
        }
        Frames[(_Throws.Length - 3) / 2].text = (_SumFrame + _Throws[_Throws.Length - 1] + _Throws[_Throws.Length - 2] + _Throws[_Throws.Length - 3]).ToString();
    }
}
