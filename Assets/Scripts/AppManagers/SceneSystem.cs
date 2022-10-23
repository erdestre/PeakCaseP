using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{   //Normally I should use dontDestroyOnLoad(), but for this case there is no need.
    int levelIndex = 0;

    public int _levelIndex //This will be used to change level.
    {
        set
        {
            levelIndex = value;
            LoadLevel();
        }
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
