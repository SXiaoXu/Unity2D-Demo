using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapTap.Bootstrap;
using TapTap.Common;
public class Menu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }
    public void QuitGame(){
        Application.Quit();
    }
    public  async void Logout(){
         var currentUser = await TDSUser.GetCurrent();

        if (null == currentUser)
        {
        Debug.Log("未登录");
        }else{
            await TDSUser.Logout();
             Debug.Log("已退出登录");
             //退出登录后返回登录页面：
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }

    }

    //打开公告
    public void openBillboard(){
        Debug.Log("打开公告");
    }
    //打开内嵌动态
    public void openForum(){
        Debug.Log("打开动态");
    }
    //跳转成就
    public void openAchievement(){
       Debug.Log("打开成就");
    }
    //跳转排行榜
    public void openLeaderboard(){
        Debug.Log("打开排行榜");
    }
   
}
