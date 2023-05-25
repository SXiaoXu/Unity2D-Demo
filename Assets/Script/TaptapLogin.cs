using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using TapTap.Bootstrap;
using TapTap.Common;
using UnityEngine.SceneManagement;

public class TaptapLogin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var config = new TapConfig.Builder()
            .ClientID("mlbfoduqiglbdugddp") // 必须，开发者中心对应 Client ID
            .ClientToken("3wROiubU8Dkv5c3h5K6bsawFYMjoSqBxXN0A55Hm") // 必须，开发者中心对应 Client Token
            .ServerURL("https://mlbfoduq.cloud.tds1.tapapis.cn") // 必须，开发者中心 > 你的游戏 > 游戏服务 > 基本信息 > 域名配置 > API
            .RegionType(RegionType.CN) // 非必须，CN 表示中国大陆，IO 表示其他国家或地区
            .ConfigBuilder();
        TapBootstrap.Init(config);
        Debug.Log("测试一下");
    }

    public async void taptapLogin()
    {
        var currentUser = await TDSUser.GetCurrent();

        if (null == currentUser)
        {
            Debug.Log("当前未登录");
            // 开始登录
            try
            {
                // 在 iOS、Android 系统下会唤起 TapTap 客户端或以 WebView 方式进行登录
                // 在 Windows、macOS 系统下显示二维码（默认）和跳转链接（需配置）
                var tdsUser = await TDSUser.LoginWithTapTap();
                Debug.Log($"login success:{tdsUser}");
                // 获取 TDSUser 属性
                var objectId = tdsUser.ObjectId; // 用户唯一标识
                var nickname = tdsUser["nickname"]; // 昵称
                var avatar = tdsUser["avatar"]; // 头像

                Debug.Log("当前登录成功的用户是：");
                Debug.Log(nickname);
                // 进入游戏
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            catch (Exception e)
            {
                if (e is TapException tapError) // using TapTap.Common
                {
                    Debug.Log($"encounter exception:{tapError.code} message:{tapError.message}");
                    // if (tapError.code == TapErrorCode.ERROR_CODE_BIND_CANCEL) // 取消登录
                    // {
                    //     Debug.Log("登录取消");
                    // }
                    Debug.Log("登录失败");
                }
            }
        }
        else
        {
            Debug.Log("已登录");
            // 进入游戏
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
