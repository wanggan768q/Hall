using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DateDeclare;
using System.IO;

public class Loginview_Controller : Windows<Loginview_Controller>
{
    private Button mButton_jinrudating;
    private Button mButton_youkedenglu;
    private Button mButton_zhucezhanghao;
    private Button mButton_wangjimima;
    private Button mButton_English;
    private Toggle RemaindAccount;
    private Image imgLogin;
    private InputField mUserId;
    private InputField mPassWord;

    private Text mText_TOP;
    private Text mText_zhanghao;
    private Text mText_mima;




    /// <summary>
    /// 每个脚本的单例模式
    /// </summary>
    private static Loginview_Controller L_single = null;
    public static Loginview_Controller getSingle()
    {
        if (L_single == null)
        {
            return new Loginview_Controller();
        }
        else
        {
            return L_single;
        }
    }
    /// <summary>
    /// 界面初始化的预制件
    /// </summary>
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Lgoin";
        }
    }

    /// <summary>
    /// 每个界面打开的open方法 相当于Mono的start();
    /// </summary> 
    /// <returns></returns>
    protected override bool OnOpen()
    {
        L_single = this;
        if (Loadingview_Controller.Exist)
        {
            Loadingview_Controller.Instance.Close();
        }
        Init();

        return base.OnOpen();
    }
    /// <summary>
    /// 初始化界面元素
    /// </summary>
    public void Init()
    {
        mButton_jinrudating = Control("Button_jinrudating").GetComponent<Button>();
        mButton_youkedenglu = Control("Button_youkedenglu").GetComponent<Button>();
        mButton_zhucezhanghao = Control("Button_zhucezhanghao").GetComponent<Button>();
        mButton_wangjimima = Control("Button_wangjimima").GetComponent<Button>();
        mButton_English = Control("Button_English").GetComponent<Button>();
        mUserId = Control("Input_zhanghao").GetComponent<InputField>();
        mPassWord = Control("Input_mima").GetComponent<InputField>();
        mText_TOP = Control("Text_TOP").GetComponent<Text>();
        mText_zhanghao = Control("Text_zhanghao").GetComponent<Text>();
        mText_mima = Control("Text_mima").GetComponent<Text>();
        AddEventListener();
        RemaindAccount = Control("Toggle_jizhuzhanghao").GetComponent<Toggle>();
        if (GameManager.RegisterUserID != null)
        {

            mUserId.text = GameManager.RegisterUserID;
            mPassWord.text = GameManager.RegisterPassword;
        }
        else
        {
            string SaceUserId = ReadUserid();
            if (SaceUserId != null)
            {
                mUserId.text = SaceUserId;

            }
        }
      

    }
    

    //添加按钮的监听事件
    private void AddEventListener()
    {
        mButton_jinrudating.onClick.AddListener(OnEnterDating);
        mButton_youkedenglu.onClick.AddListener(OnEnterYouKe);
        mButton_zhucezhanghao.onClick.AddListener(OnEnterzhucezhanghao);
        mButton_wangjimima.onClick.AddListener(OnEnterwangjimima);
        mButton_English.onClick.AddListener(OnClickLanguageButton);
       
    }

    //移出监听事件
    private void RemoveEventListener()
    {
        mButton_jinrudating.onClick.RemoveListener(OnEnterDating);
        mButton_youkedenglu.onClick.RemoveListener(OnEnterYouKe);
        mButton_zhucezhanghao.onClick.RemoveListener(OnEnterzhucezhanghao);
        mButton_wangjimima.onClick.RemoveListener(OnEnterwangjimima);
        mButton_English.onClick.RemoveListener(OnClickLanguageButton);
    }

    //进入大厅
    private void OnEnterDating()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);

        //CommonMessageUIController.Getsingle().ShowText("请输入游戏账号和密码", 1);
        //需要连接服务器请求账号密码是否正确
        if (mUserId.text == null || mUserId.text == "")
        {
            CommonMessageUIController.Instance.Open();
            if (Globle.LanType == 0)
            {
                CommonMessageUIController.Getsingle().ShowText("请输入游戏账号", 1);
            }
            else if (Globle.LanType == 1)
            {
                CommonMessageUIController.Getsingle().ShowText("Please enter your user ID", 1);
            }
        }
        else if (mPassWord.text == null || mPassWord.text == "")
        {
            CommonMessageUIController.Instance.Open();
            if (Globle.LanType == 0)
            {
                CommonMessageUIController.Getsingle().ShowText("请输入游戏密码", 1);
            }
            else if (Globle.LanType == 1)
            {
                CommonMessageUIController.Getsingle().ShowText("Please enter the password", 1);
            }
        }
        else
        {

            GameManager.Getsingle().LoginRequest(mUserId.text, mPassWord.text, "1.0.0");
            
            if (RemaindAccount.isOn)
            {

                string  str = Application.persistentDataPath + @"\UserID.txt";
                if(File.Exists(str))
                {
                    File.Delete(str);
                }              
                File.AppendAllText(str,  mUserId.text+ "\n");
            }
        }
    }

    //游客登录
    private void OnEnterYouKe()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Hide();
        //RemoveEventListener();
        if(!GuestLog_Controller.Exist)
        {
            GuestLog_Controller.Instance.Open();
            Notice_Controller.Instance.Open();
        }
        else
        {
            GuestLog_Controller.Instance.Show();
            Notice_Controller.Instance.Show();
        }
    }

    //注册账号
    private void OnEnterzhucezhanghao()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!RegisterView_Controller.Exist)
        {
            RegisterView_Controller.Instance.Open();
        }
        else
        {
            RegisterView_Controller.Instance.Show();
        }
    }
    private void OnClickLanguageButton()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (Globle.LanType == 0)
        {
            Globle.LanType = 1;

        }
        else
        {
            Globle.LanType = 0;
        }
        //ShowViewText();
        CheckLanguages();
    }


    /// <summary>
    /// 忘记密码
    /// </summary>
    private void OnEnterwangjimima()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        CommonMessageUIController.Instance.Open();
        if (Globle.LanType == 0)
        {
            CommonMessageUIController.Getsingle().ShowText("请直接与前台联系，重置密码并及时修改", 1);
        }
        else if (Globle.LanType == 1)
        {
            CommonMessageUIController.Getsingle().ShowText("Please contact with the receptionist,resetting passwords and modify", 2);
        }
        //else if (Globle.LanType == 2)
        //{
        //    CommonMessageUIController.Getsingle().ShowText("Please contact",2);
        //}



        //CommonMessageUIController.Instance.OnClickedOk = OnLickExit;
        //CommonMessageUIController.Instance.OnClickedCancle = OnCLickMain;
    }
    private string ReadUserid()
    {

        FileInfo t = new FileInfo(Application.persistentDataPath + @"\UserID.txt");
        if (!t.Exists)
        {
            return "error";
        }
        else
        {
            StreamReader sr = null;
            sr = File.OpenText(Application.persistentDataPath + @"\UserID.txt");
            string line;
            line = sr.ReadLine();
            sr.Close();
            sr.Dispose();
            return line;
        }
    
    }
    //private void OnLickExit()
    //{
    //    Application.Quit();
    //}

    //private void OnCLickMain()
    //{
    //    Close();
    //    GuestLog_Controller.Instance.Open();
    //}
    //public void ShowViewText()
    //{
    //      //if(Globle.Getsingle().OperationalVersion==tr)
    //    if (Globle.LanType == 0)
    //    {
    //        mButton_English.transform.Find("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(6).ChineseContent;
    //        Control("ss").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(6).ChineseContent;
    //    }
    //    else if (Globle.LanType == 1)
    //    {
    //        mButton_English.transform.Find("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(6).EnglishContent;
    //        Control("ss").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(6).EnglishContent;
    //    }
    //}

    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mButton_English.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(1).ChineseContent;
            mText_TOP.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(2).ChineseContent;
            mText_zhanghao.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(3).ChineseContent;
            mText_mima.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(4).ChineseContent;
            RemaindAccount.transform.FindChild("Label").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(5).ChineseContent;
            mButton_jinrudating.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(6).ChineseContent;
            mButton_youkedenglu.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(7).ChineseContent;
            mButton_zhucezhanghao.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(8).ChineseContent;
            mButton_wangjimima.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(9).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mButton_English.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(1).EnglishContent;
            mText_TOP.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(2).EnglishContent;
            mText_zhanghao.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(3).EnglishContent;
            mText_mima.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(4).EnglishContent;
            RemaindAccount.transform.FindChild("Label").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(5).EnglishContent;
            mButton_jinrudating.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(6).EnglishContent;
            mButton_youkedenglu.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(7).EnglishContent;
            mButton_zhucezhanghao.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(8).EnglishContent;
            mButton_wangjimima.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(9).EnglishContent;
        }

    }

}
 