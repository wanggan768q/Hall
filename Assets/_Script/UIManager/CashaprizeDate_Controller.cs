using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DateDeclare;

public class CashaprizeDate_Controller : Windows<CashaprizeDate_Controller>
{
    private Button mButton_Skip;
    private Button mButton_Goperfect;



    private Text mInfoText;
    private Text mInfoText2;
    private Text mLabel;
    private Button mBG;

    private static CashaprizeDate_Controller C_single = null;
    public static CashaprizeDate_Controller gatSingle()
    {
        if (C_single == null)
        {
            return new CashaprizeDate_Controller();
        }
        else
        {
            return C_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_CashaprizeDate";
        }
    }
    protected override bool OnOpen()
    {
        C_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    private void Init()
    {
        mButton_Skip = Control("Button_Skip").GetComponent<Button>();
        mButton_Goperfect = Control("Button_Goperfect").GetComponent<Button>();
        mInfoText = Control("InfoText").GetComponent<Text>();
        mInfoText2 = Control("InfoText2").GetComponent<Text>();
        mLabel = Control("Label").GetComponent<Text>();
        mBG = Control("BG").GetComponent<Button>();


        mButton_Skip.onClick.AddListener(onButton_Skip);
        mButton_Goperfect.onClick.AddListener(onButton_Goperfect);
        mBG.onClick.AddListener(onBG);
    }
    //按钮跳过  事件
    private void onButton_Skip()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!Cashaprize_Controller.Exist)
        {
            Close();
            Cashaprize_Controller.Instance.Open();
        }
        else
        {
            Cashaprize_Controller.Instance.Show();
        }
    }
    //按钮 前去完善 事件
    private void onButton_Goperfect()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!Perfectinformation_Controller.Exist)
        {
            CashaprizeDate_Controller.Instance.Close();
            Perfectinformation_Controller.Instance.Open();
        }
        else
        {
            Perfectinformation_Controller.Instance.Show();
        }
    }
    private void onBG()
    {
        Close();
    }

    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mInfoText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(74).ChineseContent;
            mInfoText2.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(75).ChineseContent;
            mLabel.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(76).ChineseContent;
            mButton_Skip.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(77).ChineseContent;
            mButton_Goperfect.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(78).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mInfoText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(74).EnglishContent;
            mInfoText2.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(75).EnglishContent;
            mLabel.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(76).EnglishContent;
            mButton_Skip.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(77).EnglishContent;
            mButton_Goperfect.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(78).EnglishContent;
        }
    }
}