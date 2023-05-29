using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapTap.Bootstrap;
using TapTap.Common;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Menu : MonoBehaviour
{
    public Text nickname;
    public Image avatar;

    public void Start()
    {
        SetName();
    }

    public async void SetName()
    {
        var currentUser = await TDSUser.GetCurrent();

        if (null != currentUser)
        {
            nickname.text = currentUser["nickname"].ToString(); // 昵称
            string avatarPath = currentUser["avatar"].ToString(); // 头像
            Debug.Log("头像的 URL 是：" + avatarPath);
            //加载头像
            StartCoroutine(GetTexFromUnityWebRequest(avatarPath));
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public async void Logout()
    {
        var currentUser = await TDSUser.GetCurrent();

        if (null == currentUser)
        {
            Debug.Log("未登录");
        }
        else
        {
            await TDSUser.Logout();
            Debug.Log("已退出登录");
            //退出登录后返回登录页面：
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    //打开公告
    public void openBillboard()
    {
        Debug.Log("打开公告");
    }

    //打开内嵌动态
    public void openForum()
    {
        Debug.Log("打开动态");
    }

    //跳转成就
    public void openAchievement()
    {
        Debug.Log("打开成就");
    }

    //跳转排行榜
    public void openLeaderboard()
    {
        Debug.Log("打开排行榜");
    }

    //加载头像
    IEnumerator GetTexFromUnityWebRequest(string imageUrl)
    {
        using var request = UnityWebRequest.Get(imageUrl);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
            Debug.Log(request.error);
        }
        else
        {
            var texture = new Texture2D(150, 150);
            texture.LoadImage(request.downloadHandler.data);
            var sprite = Sprite.Create(
                texture,
                new Rect(80, 80, 160, 160),
                new Vector2(0.5f, 0.5f)
            );
            avatar.sprite = sprite;
            avatar.SetNativeSize();
            Resources.UnloadUnusedAssets();
        }
    }
}
