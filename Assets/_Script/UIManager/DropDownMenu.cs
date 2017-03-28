using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//public class XialaScript : MonoBehaviour
//{
//    public GameObject panel;
//    void Start()
//    {
//        panel.SetActive(false);
//    }
//    public void HideOrShow()
//    {
//        panel.SetActive(!panel.activeSelf);
//    } 
//}

public class DropDownMenu : MonoBehaviour
{

    Dropdown dropdownItem;
    List<string> tempNames;

    void Awake()
    {
        dropdownItem = GetComponent<Dropdown>();
        tempNames = new List<string>();
    }

    void Start()
    {
        AddNames();
        UpdateDropdownView(tempNames);
    }

    /// <summary>
    /// 刷数据
    /// </summary>
    /// <param name="showNames"></param>
    private void UpdateDropdownView(List<string> showNames)
    {
        dropdownItem.options.Clear();
        Dropdown.OptionData tempData;
        for (int i = 0; i < showNames.Count; i++)
        {
            tempData = new Dropdown.OptionData();
            tempData.text = showNames[i];
            dropdownItem.options.Add(tempData);
        }
        dropdownItem.captionText.text = showNames[0];
    }
    /// <summary>
    /// 模拟数据
    /// </summary>
    private void AddNames()
    {
        string s1 = "请选择安全问题";
        string s2 = "您母亲的姓名是?";
        string s3 = "您父亲的姓名是?";
        string s4 = "您配偶的姓名是?";
        string s5 = "您母亲的生日是?";
        string s6 = "您父亲的生日是?";
        string s7 = "您配偶的生日是?";
        string s8 = "您的出生地是?";
        string s9 = "您的工号是?";
        string s10 = "你最熟悉的朋友姓名是?";
        string s11 = "对您影响最大的人姓名是?";

        tempNames.Add(s1);
        tempNames.Add(s2);
        tempNames.Add(s3);
        tempNames.Add(s4);
        tempNames.Add(s5);
        tempNames.Add(s6);
        tempNames.Add(s7);
        tempNames.Add(s8);
        tempNames.Add(s9);
        tempNames.Add(s10);
        tempNames.Add(s11);
    }
}