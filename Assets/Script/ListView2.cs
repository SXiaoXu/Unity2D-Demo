using System.Collections;
using System.Collections.Generic;
using TapTap.UI.AillieoTech;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListView2 : MonoBehaviour
{
  public struct RankItemData
    {
        // 名次
        public int rank;
        // 名字
        public string name;
        // 分数
        public string score;
    }

    List<RankItemData> testData = new List<RankItemData>();

    public ScrollView scrollView;

    private void Start()
    {
        // 构造测试数据
        InitData();


        scrollView.SetUpdateFunc((index, rectTransform) =>
        {
            // 更新item的UI元素
            RankItemData data = testData[index];
            rectTransform.gameObject.SetActive(true);
            rectTransform.Find("rankText2").GetComponent<Text>().text = data.rank.ToString();
            rectTransform.Find("nameText2").GetComponent<Text>().text = data.name;
             rectTransform.Find("scoreText2").GetComponent<Text>().text = data.score;

            // RectTransform  bg = rectTransform.Find("bg").GetComponent<RectTransform>();
            // bg.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - 4);
        });
        scrollView.SetItemSizeFunc((index) =>
        {
            // 返回item的尺寸
            RankItemData data = testData[index];
            if(data.rank <= 3)
            {
                return new Vector2(1350, 140);
            }
            else
            {
                return new Vector2(1350, 120);
            }
        });
        scrollView.SetItemCountFunc(() =>
        {
            // 返回数据列表item的总数
            return testData.Count;
        });

        scrollView.UpdateData(false);
        
    }

    private void InitData()
    {
        // 构建50000个排名数据
        for (int i = 1; i <= 500; ++i)
        {
            RankItemData data = new RankItemData();
            data.rank = i;
            data.name = "Name_" + i;
            data.score = "Score_" + i;
            testData.Add(data);
        }
    }
}