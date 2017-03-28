using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using DateDeclare;

public class Perfectinformation_Controller : Windows<Perfectinformation_Controller>
{
    private Button mOff_Button;
    private Button mButton_Skip;
    private Button mButton_Submit;
    private InputField mInput_IDnumber;
    private InputField mInput_Rightkey;

    private Text mTextFX;
    private Text mnumberName;
    private Text mSafetyproblem;
    private Text mRightkeyName;

    private static Perfectinformation_Controller P_single = null;
    public static Perfectinformation_Controller gatSingle()
    {
        if (P_single == null)
        {
            return new Perfectinformation_Controller();
        }
        else
        {
            return P_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Perfectinformation";
        }
    }
    protected override bool OnOpen()
    {
        P_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    private void Init()
    {
        mOff_Button = Control("Off_Button").GetComponent<Button>();
        mButton_Skip = Control("Button_Skip").GetComponent<Button>();
        mButton_Submit = Control("Button_Submit").GetComponent<Button>();
        mInput_IDnumber = Control("Input_IDnumber").GetComponent<InputField>();
        mInput_Rightkey = Control("Input_Rightkey").GetComponent<InputField>();
        mTextFX = Control("TextFX").GetComponent<Text>();
        mnumberName = Control("numberName").GetComponent<Text>();
        mSafetyproblem = Control("Safetyproblem").GetComponent<Text>();
        mRightkeyName = Control("RightkeyName").GetComponent<Text>();

        mOff_Button.onClick.AddListener(onOff_Button);
        mButton_Skip.onClick.AddListener(onButton_Skip);
        mButton_Submit.onClick.AddListener(onButton_Submit);
    }
    //关闭按钮
    private void onOff_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    //跳过按钮
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
    //提交按钮   
    private void onButton_Submit()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (mInput_IDnumber.text.Length < 5 || mInput_IDnumber.text.Length > 15)
        {
            
            CommonMessageUIController.Instance.Open();
            if (Globle.LanType == 0)
            {
                CommonMessageUIController.Getsingle().ShowText("请填写身份证号码", 1);
            }
            else if (Globle.LanType == 1)
            {
                CommonMessageUIController.Getsingle().ShowText("Please enter your ID card number", 1);
            }

            return;
        }
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mTextFX.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(106).ChineseContent;
            mnumberName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(89).ChineseContent;
            mInput_IDnumber.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(90).ChineseContent;
            mSafetyproblem.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(91).ChineseContent;
            mRightkeyName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(103).ChineseContent;
            mInput_Rightkey.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(104).ChineseContent;
            mButton_Skip.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(77).ChineseContent;
            mButton_Submit.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(105).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mTextFX.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(106).EnglishContent;
            mnumberName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(89).EnglishContent;
            mInput_IDnumber.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(90).EnglishContent;
            mSafetyproblem.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(91).EnglishContent;
            mRightkeyName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(103).EnglishContent;
            mInput_Rightkey.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(104).EnglishContent;
            mButton_Skip.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(77).EnglishContent;
            mButton_Submit.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(105).EnglishContent;
        }
    }
}