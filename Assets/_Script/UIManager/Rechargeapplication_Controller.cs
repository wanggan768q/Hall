using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DateDeclare;

public class Rechargeapplication_Controller : Windows<Rechargeapplication_Controller>
{
    private Text mInfoText1;
    private Text mInfoText2;
    private Button mButton_Determine;


    private static Rechargeapplication_Controller R_single = null;
    public static Rechargeapplication_Controller gatSingle()
    {
        if (R_single == null)
        {
            return new Rechargeapplication_Controller();
        }
        else
        {
            return R_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Rechargeapplication";
        }
    }
    protected override bool OnOpen()
    {
        R_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    private void Init()
    {
        mInfoText1 = Control("InfoText1").GetComponent<Text>();
        mInfoText2 = Control("InfoText2").GetComponent<Text>();
        mButton_Determine = Control("Button_Determine").GetComponent<Button>();

        mButton_Determine.onClick.AddListener(onButton_Determine);
    }

    private void onButton_Determine()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mInfoText1.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(113).ChineseContent;
            mInfoText2.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(114).ChineseContent;
            mButton_Determine.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(118).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mInfoText1.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(113).EnglishContent;
            mInfoText2.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(114).EnglishContent;
            mButton_Determine.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(118).EnglishContent;
        }
    }
} 