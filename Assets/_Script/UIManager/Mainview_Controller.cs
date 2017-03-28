using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class Mainview_Controller : Windows<Mainview_Controller>
{
    private static Mainview_Controller m_single = null;
    public static Mainview_Controller Getsingle()
    {
        if (m_single == null)
            return new Mainview_Controller();
        else
        {
            return m_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/GuestLogPanel";
        }
    }
    protected override bool OnOpen()
    {
        Init();
        return base.OnOpen();
    }
    private Button close;
    private void Init()
    {
        Languages[] lan= Languages_Manager.Instance.GetAllItem();
        for (int i = 0; i < lan.Length; i++)
        {
            if (lan[i].ID / 2 == 0)
            {
                Debug.Log(lan[i].ChineseContent+lan[i].EnglishContent);
            }
        }
        //Loginview_Controller.getSingle().str;
        //if (Loginview_Controller.Exist)
        //{ 
        //Loginview_Controller.getSingle().Init();
        close = Control("Mail").GetComponent<Button>();
        Control("mail").gameObject.name = Languages_Manager.Instance.GetItem(1).ChineseContent;
        close.onClick.AddListener(OnCloseView);
    }
    private void OnCloseView()
    {
        Close();
        if (!Loginview_Controller.Exist)
        {
            Loginview_Controller.Instance.Open();
        }
        else
        {
            Loginview_Controller.Instance.Show();
        }
    } 
}