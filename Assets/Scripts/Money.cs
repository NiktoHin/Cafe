using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public RectTransform rt;
    public BoxCollider2D col;
    public int money;
    public Sprite[] TextureMoney = new Sprite[9];

    void Start()
    {
        //money = 30;
        
    }

    void Update()
    {
        rt = GetComponent<RectTransform>();
        col = gameObject.GetComponent<BoxCollider2D>();
        TestMoney();
    }

    public void TestMoney()
    {
        switch (money)
        {
            case <= 10:
                GetComponent<Image>().sprite = TextureMoney[0];
                col.size = new Vector2(29, 11.5f);
                rt.sizeDelta = new Vector2(29, 11.5f);
                break;

            case <= 40:
                GetComponent<Image>().sprite = TextureMoney[1];
                col.size = new Vector2(29, 22);
                rt.sizeDelta = new Vector2(29, 22);
                break;

            case <= 90:
                GetComponent<Image>().sprite = TextureMoney[2];
                col.size = new Vector2(54, 27);
                rt.sizeDelta = new Vector2(54, 27);
                break;

            case <= 100:
                GetComponent<Image>().sprite = TextureMoney[3];
                col.size = new Vector2(67, 14);
                rt.sizeDelta = new Vector2(67, 14);
                break;

            case <= 120:
                GetComponent<Image>().sprite = TextureMoney[4];
                col.size = new Vector2(67, 20);
                rt.sizeDelta = new Vector2(67, 20);
                break;

            case <= 150:
                GetComponent<Image>().sprite = TextureMoney[5];
                col.size = new Vector2(67, 29);
                rt.sizeDelta = new Vector2(67, 29);
                break;

            case <= 200:
                GetComponent<Image>().sprite = TextureMoney[6];
                col.size = new Vector2(67, 16);
                rt.sizeDelta = new Vector2(67, 16);
                break;

            case <= 250:
                GetComponent<Image>().sprite = TextureMoney[7];
                col.size = new Vector2(67, 31);
                rt.sizeDelta = new Vector2(67, 31);
                break;

            case <= 300:
                GetComponent<Image>().sprite = TextureMoney[8];
                col.size = new Vector2(67, 17);
                rt.sizeDelta = new Vector2(67, 17);
                break;
        }
    }
}
