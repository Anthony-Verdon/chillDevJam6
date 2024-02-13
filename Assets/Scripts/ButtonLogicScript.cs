using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogicScript : MonoBehaviour
{
    public void updateScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
