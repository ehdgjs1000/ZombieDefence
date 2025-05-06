using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNames { IntroScene = 0, LoginScene, };
public static class Utils
{
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void LoadScene(string sceneName = "")
    {
        if (sceneName =="")
        { 
            SceneManager.LoadScene(GetActiveScene());
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public static void LoadScene(SceneNames sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

}
