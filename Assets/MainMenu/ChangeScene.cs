using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneIndex;

    public void Load()
    {
        if (sceneIndex >= 0)
            SceneManager.LoadScene(sceneIndex);
        else
            Application.Quit();
    }
}
