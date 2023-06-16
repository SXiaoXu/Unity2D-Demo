using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapTap.Bootstrap;
using TapTap.Common;
using UnityEngine.UI;
using UnityEngine.Networking;
using TapTap.AntiAddiction;
using TapTap.AntiAddiction.Model;
using TapTap.Billboard;

public class Menu : MonoBehaviour
{
    public Text nickname;
    public Text age;
    public Image avatar;
    //获取玩家年龄段
    public void Start()
    {
        SetName();
        SetAge();

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
    public void SetAge()
    {
        //展示三种年龄：未认证、未成年、18+
        int ageRange = AntiAddictionUIKit.AgeRange;
        if (ageRange == -1)
        {
            //未认证
            age.text = "未认证";
        }
        else if (ageRange == 18)
        {
            //成年
            age.text = "18+";
        }
        else
        {
            // ageRange = 0,8,16 未成年
            age.text = "未成年";
        }
    }

    public void PlayGame()
    {
        //上报游戏时长
        AntiAddictionUIKit.EnterGame();
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
            //退出登录时，退出防沉迷
            AntiAddictionUIKit.Exit();
            //退出登录后返回登录页面：
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    //打开公告
    public void openBillboard()
    {
        Debug.Log("打开公告");
        OpenBillboard();
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

    public void OpenBillboard()
    {
        TapBillboard.OpenPanel((any, error) =>
        {
            if (error != null)
            {
                // 打开公告失败，可以根据 error.code 和 error.errorDescription 来判断错误原因
                Debug.Log($"打开开屏公告失败: {error.code}, {error.errorDescription}");
            }
            else
            {
                Debug.Log("打开公告成功");
            }
        }, () =>
        {
            Debug.Log("公告已关闭");
        });


        //监听公告中的跳转
        TapBillboard.RegisterCustomLinkListener(url =>
        {
            // 这里返回的 url 地址和游戏在公告系统内配置的地址是一致的

        });
        //刷新小红点
        TapBillboard.QueryBadgeDetails((badgeDetails, error) =>
        {
            if (error != null)
            {
                // 获取小红点信息失败，可以根据 error.code 和 error.errorDescription 来判断错误原因
                Debug.Log($"打开开屏公告失败: {error.code}, {error.errorDescription}");
            }
            else
            {
                // 获取小红点信息成功
                if (badgeDetails.showRedDot == 1)
                {
                    // 有新的公告信息
                    Debug.Log("有新的公告信息");
                }
                else
                {
                    // 没有新的公告信息
                    Debug.Log("刷新小红点，没有新的公告信息");
                }
            }
        });
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
