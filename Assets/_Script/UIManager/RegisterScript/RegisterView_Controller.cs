using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using DateDeclare;


public class RegisterView_Controller : Windows<RegisterView_Controller>
{
    //返回登录按钮
    private Button mBackLogin;
    //注册按钮
    private Button mRegister;
    //游戏账户
    private InputField mGameUserID;
    //电话号码
    private InputField mPhonenumber;
    //游戏昵称
    private InputField mGameName;
    //游戏密码
    private InputField mLoginPassword;
    //确认密码
    private InputField mSurePassword;
    //推荐人
    private InputField mReferrerman;
    //推荐人
    public string Tuijianren;
    //ID
    private string UserID;
    //名字
    private string UserName;
    //电话ID
    private string UserPhoneNum;
    //密码
    private string UserPassWord;
    //头像id
    private int PhotoID;

    private Transform mImage_LBG;
    private Text mText;
    private Text mText_youxizhanghu;
    private Text mText_youxinicheng;
    private Text mText_denglumima;
    private Text mText_querenmima;
    private Text mText_tuijianren;
    private Text mText_dianhuahaoma;
    private Text mText_Phonenumber;
    /// <summary>
    /// 每个脚本的单例模式
    /// </summary>
    private static RegisterView_Controller Register_single = null;
    public static RegisterView_Controller getSingle()
    {
        if (Register_single == null)
        {
            return new RegisterView_Controller();
        }
        else
        {
            return Register_single;
        }
    }
    /// <summary>
    /// 界面初始化的预制件
    /// </summary>
    public override string PrefabName
    {
        get 
        {
            return "Prefabs/UIPrefab/Panel_Zhucezhanghao";
        }
    }

    protected override bool OnOpen()
    {
        Init();
        CheckLanguages();
        return base.OnOpen();
    }

    public void Init()
    {
        mBackLogin = Control("Button_backlogin").GetComponent<Button>();
        mRegister = Control("Button_zhuce").GetComponent<Button>();
        mGameUserID = Control("InputField_youxizhuanghu").GetComponent<InputField>();
        mGameName = Control("InputField_youxinicheng").GetComponent<InputField>();
        mLoginPassword = Control("InputField_denglumima").GetComponent<InputField>();
        mSurePassword = Control("InputField_querenmima").GetComponent<InputField>();
        mReferrerman = Control("InputField_tuijianren").GetComponent<InputField>();
        mPhonenumber = Control("InputField_dianhuahaoma").GetComponent<InputField>();

        mImage_LBG = Control("Image_LBG").GetComponent<Transform>();
        mText = mImage_LBG.FindChild("Text").GetComponent<Text>();
        mText_youxizhanghu = Control("Text_youxizhanghu").GetComponent<Text>();
        mText_youxinicheng = Control("Text_youxinicheng").GetComponent<Text>();
        mText_denglumima = Control("Text_denglumima").GetComponent<Text>();
        mText_querenmima = Control("Text_querenmima").GetComponent<Text>();
        mText_tuijianren = Control("Text_tuijianren").GetComponent<Text>();
        mText_dianhuahaoma = Control("Text_dianhuahaoma").GetComponent<Text>();
        AddEventListener();
    }

    private void AddEventListener()
    {
        mRegister.onClick.AddListener(OnClickRegister);
        mBackLogin.onClick.AddListener(OnClickBackLogin);
    }

    private void  OnClickBackLogin()
    {
        CommonMessageUIController.Instance.Open();
        if (Globle.LanType == 0)
        {
            CommonMessageUIController.Getsingle().ShowText("退出注册？", 2);
        }
        else if (Globle.LanType == 1)
        {
            CommonMessageUIController.Getsingle().ShowText("Exit registration?", 2);
        }
        CommonMessageUIController.Instance.OnClickedOk = OnCLoseTips;
    }
    private void OnCLoseTips()
    {
        Close();
    }

    /// <summary>
    /// 验证正则表达式
    /// </summary>
    /// <param name="regexString"></param>
    /// <param name="validateString"></param>
    /// <returns></returns>
    public static bool RegexValidate(string regexString, string validateString)
    {
        Regex regex = new Regex(regexString);
        return regex.IsMatch(validateString.Trim());
    }

    private void OnClickRegister()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        //判断游戏账号长度
        if (mGameUserID.text.Length < 5 || mGameUserID.text.Length > 15)
        {
            Debug.Log("游戏账号" + mGameUserID.text .ToString());
            CommonMessageUIController.Instance.Open();
            if (Globle.LanType == 0)
            {
                CommonMessageUIController.Getsingle().ShowText("5-15位字母，数字或下划线", 1);
            }
            else if (Globle.LanType == 1)
            {
                CommonMessageUIController.Getsingle().ShowText("The User ID must be 5 to 15 characters", 1);
            }
          
            return;
        }
        UserID = mGameUserID.text;
        //判断游戏昵称是否为空以及长度是否大于10
        if(mGameName.text==null || mGameName.text == "")
        {
            Debug.Log("游戏昵称" + mGameName.text.ToString());
            CommonMessageUIController.Instance.Open();
            if (Globle.LanType == 0)
            {
                CommonMessageUIController.Getsingle().ShowText("10个字符以内，可以使用汉字", 1);
            }
            else if (Globle.LanType == 1)
            {
                CommonMessageUIController.Getsingle().ShowText("The User ID must be 5 to 15 characters", 1);
            }
            return;
        }
        else if(mGameName.text.Length > 10)
        {
            CommonMessageUIController.Instance.Open();
            CommonMessageUIController.Getsingle().ShowText("6-16位字母和数字的组合密码", 1);
            return;
        }
        UserName = mGameName.text;
        if (mPhonenumber.text == null || mGameName.text == "")
        {
            CommonMessageUIController.Instance.Open();
            CommonMessageUIController.Getsingle().ShowText("请填写电话号码", 1);
            return;
        }
        if (mPhonenumber.text.Length < 5 || mPhonenumber.text.Length > 20)
        {
            CommonMessageUIController.Instance.Open();
            CommonMessageUIController.Getsingle().ShowText("请输入正确的手机号码", 1);
            return;
        }
        UserPhoneNum = mPhonenumber.text;


        //游戏登录密码需要多层判断 1.是否为空 2.是否全为数字 3.是否全为英文
        string ispasswordnum;
        ispasswordnum = mLoginPassword.text.ToString();
        if (mLoginPassword.text == null || mLoginPassword.text=="")
        {
            Debug.Log("游戏密码" + mLoginPassword);
            CommonMessageUIController.Instance.Open();
            CommonMessageUIController.Getsingle().ShowText("请再次输入密码", 1);

            return;
        }
        else
        {

            if (mLoginPassword.text.Length < 6 || mLoginPassword.text.Length > 16)
            {
                CommonMessageUIController.Instance.Open();
                CommonMessageUIController.Getsingle().ShowText("游戏密码长度要求为6-16位", 1);
                return;
            }
            if (RegexValidate("^[0-9]*$", mLoginPassword.text))
            {
                CommonMessageUIController.Instance.Open();
                CommonMessageUIController.Getsingle().ShowText("游戏密码不可全为数字", 1);
                return;
            }
            else if (RegexValidate("^[A-Za-z]+$", mLoginPassword.text))
            {
                CommonMessageUIController.Instance.Open();
                CommonMessageUIController.Getsingle().ShowText("游戏密码不可全为字母", 1);
                return;
            }
        }
        //获取输入的密码然后判断是否一致
        string strpassword;
        strpassword = mLoginPassword.text.ToString();
        if (strpassword!= mSurePassword.text)
        {
            Debug.Log("游戏确认密码" + mSurePassword.text.ToString());
            Debug.Log("mmmmmmmmmmmm" + strpassword);
            CommonMessageUIController.Instance.Open();
            CommonMessageUIController.Getsingle().ShowText("两次密码不一致，请重新输入", 1);
            return;
        }
        UserPassWord = strpassword;
        //推荐人账号需要向服务器发送请求判断是否存在
        if (mReferrerman.text == null || mReferrerman.text == "")
        {
            CommonMessageUIController.Instance.Open();
            CommonMessageUIController.Getsingle().ShowText("请输入推荐人账号", 1);
            return;
        }        
        else
        {
            Tuijianren = mReferrerman.text.ToString();
            //需要在这里把推荐人账号以及注册成功的消息发送至服务器
           
            GameManager.Getsingle().RegisterFuc(UserID,UserName,UserPassWord, 2, Tuijianren);
        }
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(10).ChineseContent;
            mText_youxizhanghu.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(11).ChineseContent;
            mText_youxinicheng.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(12).ChineseContent;
            mText_denglumima.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(13).ChineseContent;
            mText_querenmima.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(14).ChineseContent;
            mText_tuijianren.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(15).ChineseContent;
            mText_dianhuahaoma.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(63).ChineseContent;
            mBackLogin.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(16).ChineseContent;
            mRegister.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(17).ChineseContent;
            mGameUserID.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(18).ChineseContent;
            mGameName.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(19).ChineseContent;
            mLoginPassword.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(20).ChineseContent;
            mSurePassword.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(21).ChineseContent;
            mReferrerman.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(22).ChineseContent;
            mPhonenumber.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(64).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(10).EnglishContent;
            mText_youxizhanghu.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(11).EnglishContent;
            mText_youxinicheng.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(12).EnglishContent;
            mText_denglumima.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(13).EnglishContent;
            mText_querenmima.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(14).EnglishContent;
            mText_tuijianren.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(15).EnglishContent;
            mText_dianhuahaoma.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(63).EnglishContent;
            mBackLogin.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(16).EnglishContent;
            mRegister.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(17).EnglishContent;
            mGameUserID.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(18).EnglishContent;
            mGameName.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(19).EnglishContent;
            mLoginPassword.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(20).EnglishContent;
            mSurePassword.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(21).EnglishContent;
            mReferrerman.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(22).EnglishContent;
            mPhonenumber.transform.FindChild("Placeholder").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(64).EnglishContent;
        }
    }
}
