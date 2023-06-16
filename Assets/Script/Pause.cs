using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapTap.Bootstrap;
using TapTap.Common;
using TapTap.AntiAddiction;
using TapTap.AntiAddiction.Model;
using TapTap.Billboard;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource audioBgm;

    public void PauseGame(){
        //停止上报游戏时长
        AntiAddictionUIKit.LeaveGame();

        //暂停游戏时，停止跑马灯公告
        TapBillboard.StopFetchMarqueeData(true);

        pauseMenu.SetActive(true);
        //暂停游戏
        Time.timeScale = 0f;
        audioBgm.Pause();
    }
    public void ResumeGame(){
        //恢复上报游戏时长
        AntiAddictionUIKit.EnterGame();
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        audioBgm.Play();
    }
    public void GoBackToMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

}
