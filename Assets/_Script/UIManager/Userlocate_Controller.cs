using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using DateDeclare;

public class Userlocate_Controller : Windows<Userlocate_Controller>
{
    private Button mOff_Button;
    private Button mMend_Button;
    private InputField mName_Display;
    private Button mLogOff_Button;
    private Button mAlterPortrait_Button;
    private Transform mTop_User;
    private Text mTextYH;
    private Transform mPersonalData;
    private Text mText;
    private Text mName;
    private Text mAccountNumber;
    private Text mSex;
    private Text mGrade;
    private Text mLabelNV;
    private Text mLabelNAN;

    private Text mUserID;
    private Text mUserName;
    private Text mLevel;
    private Text mGoldScore;
    private Text mTextScore;
    private Text mUserId;
    private Toggle ManToggle;
    private Toggle WomanToggle;


    private static Userlocate_Controller U_single = null;
    public static Userlocate_Controller gatSingle()
    {
        if (U_single == null)
        {
            return new Userlocate_Controller();
        }
        else
        {
            return U_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Userlocate";
        }
    }
    protected override bool OnOpen()
    {
        U_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    private void Init()
    {
        mOff_Button = Control("Off_Button").GetComponent<Button>();
        mMend_Button = Control("Mend_Button").GetComponent<Button>();
        mName_Display = Control("Name_Display").GetComponent<InputField>();
        mLogOff_Button = Control("LogOff_Button").GetComponent<Button>();
        mAlterPortrait_Button = Control("AlterPortrait_Button").GetComponent<Button>();
        mTop_User = Control("Top_User").GetComponent<Transform>();
        mTextYH = mTop_User.FindChild("TextYH").GetComponent<Text>();
        mPersonalData = Control("PersonalData").GetComponent<Transform>();
        mText = mPersonalData.FindChild("Text").GetComponent<Text>();
        mName = Control("Name").GetComponent<Text>();
        mAccountNumber = Control("AccountNumber").GetComponent<Text>();
        mSex = Control("Sex").GetComponent<Text>();
        mGrade = Control("Grade").GetComponent<Text>();
        mLabelNV = Control("LabelNV").GetComponent<Text>();
        mLabelNAN = Control("LabelNAN").GetComponent<Text>();

        mUserID = Control("UserId").GetComponent<Text>();
        mUserName = Control("Placeholder").GetComponent<Text>();
        mLevel = Control("Level").GetComponent<Text>();
        mGoldScore = Control("YXScore").GetComponent<Text>();
        mTextScore = Control("TYScore").GetComponent<Text>();
        mUserId = Control("UserId").GetComponent<Text>();
        ManToggle = Control("Toggle_Man").GetComponent<Toggle>();
        WomanToggle = Control("Toggle_WoMan").GetComponent<Toggle>();
        SetUserInfo();
        AddEventListener();
    }
    private void AddEventListener()
    {
        mOff_Button.onClick.AddListener(onOff_Button);
        mMend_Button.onClick.AddListener(onMend_Button);
        mLogOff_Button.onClick.AddListener(onLogOff_Button);
        mAlterPortrait_Button.onClick.AddListener(onAlterPortrait_Button);
    }
    private void RemoveEventListener()
    {
        mOff_Button.onClick.RemoveListener(onOff_Button);
        mMend_Button.onClick.RemoveListener(onMend_Button);
        mLogOff_Button.onClick.RemoveListener(onLogOff_Button);
        mAlterPortrait_Button.onClick.RemoveListener(onAlterPortrait_Button);
    }
    private void onOff_Button()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Hide();
    }
    private void onMend_Button()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        CommonMessageUIController.Instance.Open();
        if (Globle.LanType == 0)
        {
            CommonMessageUIController.Getsingle().ShowText("游客账号无法修改密码", 1);
        }
        else if (Globle.LanType == 1)
        {
            CommonMessageUIController.Getsingle().ShowText("The password of visitor account cannot be changed!", 1);
        }
       
    }
    private void onLogOff_Button()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        CommonMessageUIController.Instance.Open();
        if (Globle.LanType == 0)
        {
            CommonMessageUIController.Getsingle().ShowText("退出游戏？", 3);
        }
        else if (Globle.LanType == 1)
        {
            CommonMessageUIController.Getsingle().ShowText("Quit The Game?", 3);
        }
        
        CommonMessageUIController.Instance.OnClickedOk = onClose;
        CommonMessageUIController.Instance.OnClickedCancle = onSwitch;
    }
    private void onClose()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        GameManager.Getsingle().CancleLoging();
        GameHallView_Controller.getSingle().Close();
        Loginview_Controller.getSingle().Open();
        Close();
    }
    private void onSwitch()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
        if (!Loginview_Controller.Exist)
        {
            Loginview_Controller.Instance.Open();
        }
        else
        {
            GuestLog_Controller.Instance.Close();
            Loginview_Controller.Instance.Show();
        }

    }
    private void onAlterPortrait_Button()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!AlterPortrait_Panel.Exist)
        {
            AlterPortrait_Panel.Instance.Open();
        }
        else
        {
            AlterPortrait_Panel.Instance.Show();
        }
    }
    private void SetUserInfo()
    {
        mUserID.text = UserInformation.username;
        mUserName.text = UserInformation.nickname;
        mLevel.text = "Lv:" + UserInformation.level + GlobalModel.LeverName[UserInformation.level];
        mGoldScore.text = UserInformation.gameGold.ToString();
        mTextScore.text = UserInformation.expeGold.ToString();
        mUserId.text = UserInformation.username;
        if (UserInformation.sex == '男')
        {
            ManToggle.isOn = true;
            WomanToggle.isOn = false;
        }
        else
        {
            ManToggle.isOn = false;
            WomanToggle.isOn = true;
        }
    }
    public void UpdateUserInifo()
    {
        UserInformation.sex = this.GetPersonSex();
        UserInformation.nickname = mUserName.text;
        UserInformation.photoId = this.GetPhoneId();
        mUserName.text = UserInformation.nickname;
        if (UserInformation.sex == '男')
        {
            ManToggle.isOn = true;
            WomanToggle.isOn = false;
        }
        else
        {
            ManToggle.isOn = false;
            WomanToggle.isOn = true;
        }
    }
    public void ChanePhoto(int photoid)
    {
        mAlterPortrait_Button.image.sprite = loadSprite("Photo_" + photoid);
    }
    private char GetPersonSex()
    {
        if (ManToggle.isOn)
        {
            return '男';
        }
        else
        {
            return '女';
        }
    }
    private int GetPhoneId()
    {
        return 2;
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mTextYH.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(30).ChineseContent;
            mText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(31).ChineseContent;
            mMend_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(32).ChineseContent;
            mName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(33).ChineseContent;
            mAccountNumber.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(34).ChineseContent;
            mSex.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(35).ChineseContent;
            mGrade.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(36).ChineseContent;
            mLabelNAN.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(37).ChineseContent;
            mLabelNV.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(38).ChineseContent;
            mLogOff_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(39).ChineseContent;
            mAlterPortrait_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(40).ChineseContent;    


        }
        else if (Globle.LanType == 1)
        {
            mTextYH.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(30).EnglishContent;
            mText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(31).EnglishContent;
            mMend_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(32).EnglishContent;
            mName.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(33).EnglishContent;
            mAccountNumber.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(34).EnglishContent;
            mSex.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(35).EnglishContent;
            mGrade.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(36).EnglishContent;
            mLabelNAN.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(37).EnglishContent;
            mLabelNV.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(38).EnglishContent;
            mLogOff_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(39).EnglishContent;
            mAlterPortrait_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(40).EnglishContent;
        }
    }
    private Sprite loadSprite(string spriteName)
    {
        return Resources.Load<GameObject>("Prefabs/UISprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
    }
}
