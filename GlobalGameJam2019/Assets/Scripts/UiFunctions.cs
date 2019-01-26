using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UiFunctions : MonoBehaviour
{
    public void ChangeScene(int s){
        SceneManager.LoadScene(s);
        }

    public void ExitGame()
    {
        Application.Quit();
    }
}
