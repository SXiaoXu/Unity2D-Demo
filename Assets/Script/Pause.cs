using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapTap.Bootstrap;
using TapTap.Common;
public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource audioBgm;

    public void PauseGame(){
        pauseMenu.SetActive(true);
        //暂停游戏
        Time.timeScale = 0f;
        audioBgm.Pause();
    }
    public void ResumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        audioBgm.Play();
    }
    public void GoBackToMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

}
