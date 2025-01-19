using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelection : MonoBehaviour
{
    public void IndustrialAreaV1()
    {
        SceneManager.LoadScene(sceneName: "Map 1");
    }

    public void IndustrialAreaV2()
    {
        SceneManager.LoadScene(sceneName: "Map 2");
    }

    public void BacktoHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


}
