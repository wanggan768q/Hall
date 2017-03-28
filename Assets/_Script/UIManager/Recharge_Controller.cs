using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DateDeclare;

public class Recharge_Controller : Windows<Recharge_Controller>
{
    private InputField mInput_Quota;
    private InputField mInput_Rightkey;

    private Text mTextChonzhi;
    private Text mRechargelimit;
    private Text mvalueQuota;
    private Text mRightkeyName;

    private Button mButton_Cancel;
    private Button mButton_Determine;

    private static Recharge_Controller R_single = null;
    public static Recharge_Controller gatSingle()
    {
        if (R_single == null)
        {
            return new Recharge_Controller();
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
            return "Prefabs/UIPrefab/Panel_Recharge";
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
        mInput_Quota = Control("Input_Quota").GetComponent<InputField>();
        mInput_Rightkey = Control("Input_Rightkey").GetComponent<InputField>();

        mButton_Cancel = Control("Button_Cancel").GetComponent<Button>();
        mButton_Determine = Control("Button_Determine").GetComponent<Button>();
        mTextChonzhi = Control("TextChonzhi").GetComponent<Text>();
        mRechargelimit = Control("Rechargelimit").GetComponent<Text>();
        mvalueQuota = Control("valueQuota").GetComponent<Text>();
        mRightkeyName = Control("RightkeyName").GetComponent<Text>();
        mRightkeyName = Control("Placeholder").GetComponent<Text>();


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
        //其余两个没有判断....
        if (mInput_Quota.text == null || mInput_Quota.text == "")
        {
            CommonMessageUIController.Instance.Open();
            if (Globle.LanType == 0)
            {
                CommonMessageUIController.Getsingle().ShowText("请输入充值金额", 1);
            }
            else if (Globle.LanType == 1)
            {
                CommonMessageUIController.Getsingle().ShowText("Please enter the recharge amount", 1);
            }
            return;
        }
        else
        {
            
        }
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mTextChonzhi.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(109).ChineseContent;
            mRechargelimit.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(110).ChineseContent;
            mvalueQuota.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(111).ChineseContent;
            mRightkeyName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(83).ChineseContent;
            mButton_Cancel.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(85).ChineseContent;
            mButton_Determine.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(116).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mTextChonzhi.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(109).EnglishContent;
            mRechargelimit.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(110).EnglishContent;
            mvalueQuota.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(111).EnglishContent;
            mRightkeyName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(83).ChineseContent;
            mButton_Cancel.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(85).EnglishContent;
            mButton_Determine.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(116).EnglishContent;
        }
    }
}
