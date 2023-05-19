using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Login : MonoBehaviour
{
   public void GotoMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }
    public void QuitGame(){
        Application.Quit();
    }
}
