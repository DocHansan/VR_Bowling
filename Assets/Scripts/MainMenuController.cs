using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    public GameObject SaveObject;

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        Save SaveClass = CreateSaveClass();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Save.save");
        bf.Serialize(file, SaveClass);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/Save.save", FileMode.Open);

        Save SaveClass = (Save)bf.Deserialize(file);
        file.Close();

        SaveObject.GetComponent<GameController>().Throws = SaveClass.Throws;
        SaveObject.GetComponent<GameController>().Throw = SaveClass.Throw;
        SaveObject.GetComponent<GameController>().PlayerTry = SaveClass.PlayerTry;
        SaveObject.GetComponent<GameController>().DroppedPinsCount = SaveClass.DroppedPinsCount;

        SaveObject.GetComponent<GameController>().Recipient.GetComponent<ScoreDrawer>().UpdateThrows();
        SaveObject.GetComponent<GameController>().Recipient.GetComponent<ScoreDrawer>().UpdateFrames();
        SaveObject.GetComponent<GameController>().RespawnPins();
        SaveObject.GetComponent<GameController>().RespawnBalls();

        if (SaveClass.Throw % 2 == 0)
        {
            foreach (Transform PinOnScene in GameObject.FindGameObjectsWithTag("Pin")[1].GetComponentsInChildren<Transform>())
            {
                Debug.Log("Try");
                Debug.Log(SaveClass.DroppedPinsCount);
                if (PinOnScene != GameObject.FindGameObjectsWithTag("Pin")[1].GetComponentsInChildren<Transform>()[0])
                {
                    if (SaveClass.DroppedPinsCount != 0)
                    {
                        SaveClass.DroppedPinsCount -= 1;
                        Destroy(PinOnScene.gameObject);
                    }
                }
            }
        }
    }

    private Save CreateSaveClass()
    {
        Save SaveClass = new Save();

        SaveClass.Throws = SaveObject.GetComponent<GameController>().Throws;
        SaveClass.Throw = SaveObject.GetComponent<GameController>().Throw;
        SaveClass.PlayerTry = SaveObject.GetComponent<GameController>().PlayerTry;
        SaveClass.DroppedPinsCount = SaveObject.GetComponent<GameController>().DroppedPinsCount;
        
        return SaveClass;
    }
}
