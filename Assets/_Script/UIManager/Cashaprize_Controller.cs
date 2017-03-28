using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DateDeclare;

public class Cashaprize_Controller : Windows<Cashaprize_Controller>
{
    private InputField mInput_Quota;
    private InputField mInput_Rightkey;
    private InputField mInput_Gamepassword;

    private Button mButton_Cancel;
    private Button mButton_Determine;

    private Text mTextduijian;
    private Text mCashaprizeQuota;
    private Text mCurrency;
    private Text mvalueQuota;
    private Text mRightkeyName;
    private Text mGamepassword;


    private static Cashaprize_Controller C_single = null;
    public static Cashaprize_Controller gatSingle()
    {
        if (C_single == null)
        {
            return new Cashaprize_Controller();
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
            return "Prefabs/UIPrefab/Panel_Cashaprize";
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
        mInput_Quota = Control("Input_Quota").GetComponent<InputField>();
        mInput_Rightkey = Control("Input_Rightkey").GetComponent<InputField>();
        mInput_Gamepassword = Control("Input_Gamepassword").GetComponent<InputField>();
        mButton_Cancel = Control("Button_Cancel").GetComponent<Button>();
        mButton_Determine = Control("Button_Determine").GetComponent<Button>();

        mTextduijian = Control("Textduijian").GetComponent<Text>();
        mCashaprizeQuota = Control("CashaprizeQuota").GetComponent<Text>();
        mCurrency = Control("Currency").GetComponent<Text>();
        mvalueQuota = Control("valueQuota").GetComponent<Text>();
        mRightkeyName = Control("RightkeyName").GetComponent<Text>();
        mGamepassword = Control("Gamepassword").GetComponent<Text>();

        mButton_Cancel.onClick.AddListener(onButton_Cancel);
        mButton_Determine.onClick.AddListener(onButton_Determine);

    }

    private void onButton_Cancel()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    private void onButton_Determine()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (mInput_Quota.text == null || mInput_Quota.text == "")
        {
            Debug.Log("游戏密码" + mInput_Quota);
            CommonMessageUIController.Instance.Open();
            if (Globle.LanType == 0)
            {
                CommonMessageUIController.Getsingle().ShowText("兑奖额度输入不能为空或者零", 1);
            }
            else if (Globle.LanType == 1)
            {
                CommonMessageUIController.Getsingle().ShowText("The amount cannot be blank or 0", 1);
            }
            

            return;
        }
    }


    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mTextduijian.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(79).ChineseContent;
            mCashaprizeQuota.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(80).ChineseContent;
            mCurrency.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(81).ChineseContent;
            mvalueQuota.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(82).ChineseContent;
            mRightkeyName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(83).ChineseContent;
            mGamepassword.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(84).ChineseContent;
            mButton_Cancel.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(85).ChineseContent;
            mButton_Determine.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(116).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mTextduijian.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(79).EnglishContent;
            mCashaprizeQuota.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(80).EnglishContent;
            mCurrency.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(81).EnglishContent;
            mvalueQuota.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(82).EnglishContent;
            mRightkeyName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(83).EnglishContent;
            mGamepassword.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(84).EnglishContent;
            mButton_Cancel.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(85).EnglishContent;
            mButton_Determine.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(116).EnglishContent;
        }
    }
}