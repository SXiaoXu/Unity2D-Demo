using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListViewCell : MonoBehaviour
{
    public Text text;

    void Start()
    {
        GetComponent<UnityEngine.UI.Image>().color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    public void Refresh(int index)
    {
        text.text = index.ToString();
    }
}
