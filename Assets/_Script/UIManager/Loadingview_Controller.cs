using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using DateDeclare;

public class Loadingview_Controller : Windows<Loadingview_Controller>
{
    private static Loadingview_Controller m_single = null;
    public static Loadingview_Controller Getsingle()
    {
        if (m_single == null)
            return new Loadingview_Controller();
        else
        {
            return m_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/LoadingPanel";
        }
    }
    protected override bool OnOpen()
    {
        m_single = this;
        //Control("LoadSprite").transform.RotateAroundLocal(WndObject.transform.position,0);
        //Init();
        Loginview_Controller.Instance.Open();
        SoundManager.GetSingleton().PlayMusicSource();

        return base.OnOpen();
    }
    //private void Init()
    //{
    //    Control
    //}
}