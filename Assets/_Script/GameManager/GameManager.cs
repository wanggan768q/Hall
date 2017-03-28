using LitJson;
using System;
using System.Collections;
using System.Net;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;

public class GameManager : MonoBehaviour
{
    public string HallVersion="1.2.12";
    public static bool CommunicationEnable = true;
    public bool m_bWrite = true;
    public bool m_bOtherStart;
    public bool m_bEnglish = true;
    private string domainName = string.Empty;
    private static GameManager m_single = null;
    public bool m_bIsLogin;
    public bool ShowNetDown;
    private bool m_bStartDownload;
    private long[] lsize = new long[2];
    private long m_lCurrenSize;
    public AndroidJavaClass jc;
    public AndroidJavaObject jo;
    public string Language;
    public bool m_bYouKe;
    public static string RegisterUserID;
    public static string RegisterPassword;
    public UserInformation UserInfo;
    public static string url = "http://47.91.142.202:8200/auth/verfiy/";
    public static WWW www;
    private string username = "ggggg";
    private string deviceid = "zzzz";
    public bool CheckDeviceid;
    public static GameManager Getsingle()
    {
        if (m_single == null)
            m_single = new GameManager();
        return m_single;
    }
    private string Yuming(string hostname)
    {
        IPAddress iPAddress;
        if (IPAddress.TryParse(hostname, out iPAddress))
        {
            return iPAddress.ToString();
        }
        return Dns.GetHostEntry(hostname).AddressList[0].ToString();
    }

    private void ChangeIpToYming(string DomainName)
    {
        string text = this.Yuming(DomainName);
        Constants.IP = text.ToString();

    }
    private void Awake()
    {
#if UNITY_ANDROID
        if (this.domainName != string.Empty)
        {
            this.ChangeIpToYming(this.domainName);
        }
        this.jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        this.jo = this.jc.GetStatic<AndroidJavaObject>("currentActivity");
#endif
    }

    public void RegisterFuc(string UserID, string UserName, string PassWord, int PhoneNum, string TuiJianren)
    {
        if(NetMain.GetSingleton())
        {
            if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
            {
                bool msg = NetMain.GetSingleton().MyCreateSocket.SendRegistInFo(UserID, UserName, PassWord, PhoneNum, 0, TuiJianren);
                if (msg)
                {
                    RegisterUserID = UserID;
                    RegisterPassword = PassWord;
                    Debug.Log("userid" + RegisterUserID);
                   Debug.Log("注册成功");
                }
            }
        }


        

    }
    public void LoginRequest(string Userid,string userpassword,string version)
    {
        NetMain.GetSingleton().mLoginRequest.StartRequest(Userid,userpassword,version);

    }
    public void CancleLoging()
    {
        NetMain.GetSingleton().mLoginRequest.StartCancle();

    }
    public void LoginFuc(string UserID, string UserPassWord, string Version)
    {
        Debug.Log(this.CheckDeviceid);
        if (NetMain.GetSingleton())
        {
            if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
            {
                bool msg = NetMain.GetSingleton().MyCreateSocket.SendUserLogin(UserID, UserPassWord, HallVersion);
                if (this.m_bWrite)
                {
                }
                if (msg)
                {
                    //this.Game.ShowGetNetDateTip();
                    CommunicationEnable = false;
					UserInformation.password = UserPassWord;
                    Debug.Log("发送成功");
                }
                //if (this.Login.GetToggleStartState() || HallManagerPanel.GetSingleton().OperationalVersion)
                //{
                //    DateFile.GetSingleton().SaveUserId(this.Game.userInformation.username);
                //}
                //else
                //{
                //    DateFile.GetSingleton().SaveUserId(string.Empty);
                //}
            }
            else
            {
                Debug.Log("服务没启动");
            }
        }
    }
    public void SendVersion()
    {
        if (NetMain.GetSingleton())
        {
            if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
            {
                bool flag = NetMain.GetSingleton().MyCreateSocket.SendCheckVersion(HallVersion);
         
            }
            else
            {
             //出错
            }
        }
    }
    public void RecUpdateMessage()
    {
        if (this.m_bWrite)
        {
        }
     //   this.Game.HideNetDateTip();
        if (this.m_bOtherStart || this.m_bIsLogin)
        {
            //this.LoadingPanel.gameObject.SetActive(false);
            //this.TotalTime = 0f;
            //this.PageTye = HallManagerPanel.EPageTye.EPageTye_Login;
            //this.Login.ShowLoginPanel(false);
            //this.Game.ShowGamePanelAsBackground();
        }
       
        GameManager.CommunicationEnable = true;
    }
    public void NetDownAgainConnect()
    {
        Debug.Log("执行开始netdown");
        if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag() && this.m_bIsLogin)
        {
            bool flag = NetMain.GetSingleton().MyCreateSocket.SendUserLogin("zwq1234", "abcc456", HallVersion);
            if (flag)
            {
                Debug.Log("执行完毕netdown");
              //  this.Game.ShowGetNetDateTip();
                GameManager.CommunicationEnable = false;
            }
        }
    }
    public void OtherStartHall()
    {
        if (this.m_bOtherStart)
        {
           // this.Show = true;
            if (NetMain.GetSingleton())
            {
                if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
                {
                    Debug.Log("otherstar");
                   // ShadowPanel.ShadowListClear();
                    NetMain.GetSingleton().MyCreateSocket.SendUserLogin("zwq1234", "abcc456", HallVersion);
                 //   this.Game.userInformation.username = this.Game.jo.Get<string>("mUserId");
                  //  this.Game.userInformation.password = this.Game.jo.Get<string>("mPwd");
                  //  this.Language = Localization.language;
                  //  this.m_bEnglish = (this.Language == "English");
                }
                else
                {
                 //   this.TipManager.ShowTipManagerPanel(EGameTipType.Net_ConnectionError, 0, true);
                }
            }
        }
    }
    public void Autologon()
    {
        //if (this.OperationalVersion && this.PageTye == HallManagerPanel.EPageTye.EPageTye_None)
        //{
        //    this.AutoLogonEnable = (DateFile.GetSingleton().ReadAutologon() == "1");
        //}
        //if (this.OperationalVersion && this.AutoLogonEnable && !this.m_bOtherStart && !this.m_bIsLogin && NetMain.GetSingleton())
        //{
        if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
        {
            bool flag = NetMain.GetSingleton().MyCreateSocket.SendUserLogin("zwq1234", "abcc456", HallVersion);
            if (flag)
            {
                Debug.Log("自动登录");
                //this.Game.ShowGetNetDateTip();
                CommunicationEnable = false;
            }
        }
        else
        {
            // this.TipManager.ShowTipManagerPanel(EGameTipType.Net_ConnectionError, 0, true);
            //    }
        }
        //}
    }
    public void RecNetDown()
    {
        //if (!this.ShowNetDown && this.TipManager.GameTipType != EGameTipType.IsExitHall && (this.TipManager.GameTipType < EGameTipType.Game_UserIdFrozen || this.TipManager.GameTipType > EGameTipType.HasBeDeleted))
        //{
            //this.Game.HideNetDateTip();
            this.ShowNetDown = true;
           // this.ShowNetConnectError();
       // }
    }



    protected void OnClickNoLongerPrompt()
    {

        if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
        {
            NetMain.GetSingleton().MyCreateSocket.SendIgnoreCheckInformation((true) ? 1 : 0);  // true是 UIToggle 里面的值
        }
        else
        {
            //this.ShowNetConnectError();
        }
    }

    protected void DownLoadUpdate(int type)
    {
        Debug.Log( "--Tag-- DownLoadUpdate" );
#if UNITY_ANDROID
        if (this.m_bStartDownload)
        {
            if ((float)(this.lsize[0] - this.m_lCurrenSize) >= 0.1f)
            {
             //   this.ShowProgress(float.Parse(((float)this.lsize[0] * 1f / (float)this.lsize[1]).ToString("0.00")));
            }
            if (this.jo.Get<bool>("StartDown"))
            {
                this.m_lCurrenSize = this.lsize[0];
                this.lsize = this.jo.Call<long[]>("DownLoadThread[type].GetPercentage", new object[0]);
                if (this.lsize[0] <= 0L)
                {
                   // this.ShowProgress(0f);
                }
                else
                {
                  //  this.ShowProgress(float.Parse(((float)this.lsize[0] * 1f / (float)this.lsize[1]).ToString("0.00")));
                }
            }
            if (this.jo.Get<bool>("DownOk"))
            {
                this.m_bStartDownload = false;
                //this.ShowProgress(1f);
                //this.GameObj.transform.FindChild("GameType/Progress Bar").gameObject.SetActive(false);
                //this.DownloadGame = EGameType.GameType_Unknown;
            }
        }
#endif
    }
    public void RecRegisterMessage(bool success, int failmessage)
    {
       
       
        //if (!this.m_bYouKe)
        //{
        //    if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
        //    {
        //        GlobalModel.Getsingle().UserInfo.username = "123456";
        //        GlobalModel.Getsingle().UserInfo.password = string.Empty;
        //        bool flag = NetMain.GetSingleton().MyCreateSocket.SendUserLogin(GlobalModel.Getsingle().UserInfo.username, GlobalModel.Getsingle().UserInfo.password,"");
        //        if (flag)
        //        {
        //           //登录
        //        }
        //    }
        //    else
        //    {
        //       //没连接上
        //    }
        //    this.m_bYouKe = false;
        //}
        //else
        //{
            if (success)
            {
                
                
                RegisterView_Controller.getSingle().Close();
                Loginview_Controller.getSingle().Open();
                //this.TipManager.ShowTipManagerPanel(EGameTipType.Register_Success, 0, false);
            }
            else if (failmessage != 1)
            {
                if (failmessage == 2)
                {
                   // this.TipManager.ShowTipManagerPanel(EGameTipType.Recommender_NoExit, 0, false);
                }
            }
            else
            {
              //  this.TipManager.ShowTipManagerPanel(EGameTipType.Register_IDRepetitive, 0, false);
            }
            GameManager.CommunicationEnable = true;
      //  }
    }
    public void RecLoginMessage(int type)
    {
      
        if (type >= 0 && type <= 3)
        {
            Debug.Log("显示开始" + type);
            // this.Game.HideNetDateTip();    隐藏NetDate页面
            ///  this.TipManager.HideTipManagerPanel(); 隐藏提示页面
            // this.Register.ActiveRegister(false);  隐藏注册页面
            if (this.m_bWrite)
            {
            }
            // this.PageTye = HallManagerPanel.EPageTye.EPageTye_Login;  更改页面 枚举为当前页面
            //this.LoadingPanel.gameObject.SetActive(false);  隐藏登录页面
            // this.TotalTime = 0f; 设置时间
            //自动登录获取名称和密码
            //if (this.OperationalVersion && this.AutoLogonEnable)
            //{
            //    this.Game.userInformation.username = DateFile.GetSingleton().ReadUserId();
            //    this.Game.userInformation.password = DateFile.GetSingleton().ReadPassword();
            //}
            if (type == 0)
            {
                //this.Login.ShowLoginPanel(false);
                //this.Game.ShowGamePanelAsBackground();
                //this.Game.userInformation.password = string.Empty;
                //this.Login.SetPassword();
                //this.PageTye = HallManagerPanel.EPageTye.EPageTye_Login;
                //this.TipManager.ShowTipManagerPanel(EGameTipType.Login_IDMismatchingPwd, 0, false);
                //if (this.Game.userInformation.type == 0)
                //{
                //    this.Login.SetName(this.Game.userInformation.username);
                //}
                //this.m_bOtherStart = false;
                //if (this.OperationalVersion)
                //{
                //    DateFile.GetSingleton().SavePassword(string.Empty);
                //    DateFile.GetSingleton().SaveAutologon(0);

            }
            else if (type == 1)
            {
                //this.Login.ShowLoginPanel(false);
                //this.Game.ShowGamePanelAsBackground();
                //this.Game.userInformation.password = string.Empty;
                //this.Login.SetPassword();
                //this.PageTye = HallManagerPanel.EPageTye.EPageTye_Login;
                //this.TipManager.ShowTipManagerPanel(EGameTipType.Game_UserIdFrozen, 0, false);
                //if (this.Game.userInformation.type == 0)
                //{
                //    this.Login.SetName(this.Game.userInformation.username);
                //}
                //this.m_bOtherStart = false;
                //if (this.OperationalVersion)
                //{
                //    DateFile.GetSingleton().SavePassword(string.Empty);
                //    DateFile.GetSingleton().SaveAutologon(0);
                // }
            }
            else if (type == 2)
            {
                //this.Login.ShowLoginPanel(false);
                //this.Game.ShowGamePanelAsBackground();
                //this.Game.userInformation.password = string.Empty;
                //this.Login.SetPassword();
                //this.PageTye = HallManagerPanel.EPageTye.EPageTye_Login;
                //this.TipManager.ShowTipManagerPanel(EGameTipType.ServerUpdate, 0, false);
                //if (this.Game.userInformation.type == 0)
                //{
                //    this.Login.SetName(this.Game.userInformation.username);
                //}
                //this.m_bOtherStart = false;
            }
            else if (type == 3)
            {
                Debug.Log("显示开始 type3");
                Loginview_Controller.getSingle().Hide();
                if (!GameHallView_Controller.Exist)
                {
                    GameHallView_Controller.Instance.Open();
                    Notice_Controller.Instance.Open();
                }
                else
                {
                    GameHallView_Controller.Instance.Show();
                    Notice_Controller.Instance.Show();
                }
            }
            // this.LoginAnnouncement = true;  公告
            //if (this.m_bOtherStart)
            //{
            //    this.LoginAnnouncement = false;  //公告
            //}
            //      this.PageTye = HallManagerPanel.EPageTye.EPageTye_GameHall;  设置界面枚举
            if (!this.m_bIsLogin)
                {
                  //  this.Login.HideLoginPanel();  隐藏login 页面
                   // this.Game.ShowGamePanel();   显示大厅页面
                }
                this.m_bIsLogin = true;
            //if (this.OperationalVersion)
            //{
            //    if (this.AutoLogonEnable)
            //    {
            //        this.Show = true;
            //        this.Login.HideLoginPanel();
            //        this.Game.ShowGamePanel();
            //    }
            //    else
            //    {
            //        if (base.transform.Find("LoginPanel/Login/Toggle").GetComponent<UIToggle>().value)
            //        {
            //            DateFile.GetSingleton().SavePassword(base.transform.Find("LoginPanel/Login/CodeInput/Input").GetComponent<UIInput>().value);
            //        }
            //        else
            //        {
            //            DateFile.GetSingleton().SavePassword(string.Empty);
            //        }
            //        DateFile.GetSingleton().SaveAutologon((!base.transform.Find("LoginPanel/Login/Toggle1").GetComponent<UIToggle>().value) ? 0 : 1);
            //    }
            
            //if (this.Game.userInformation.overflow == 1)
            //{
            //    this.TipManager.ShowTipManagerPanel(EGameTipType.CoinOverFlow, 0, false);
            //    this.AnnouncementEnable = false;
            //}
            //else
            //{
            //    this.AnnouncementEnable = true;
            //}
            //if (this.Game.userInformation.type == 1)
            //{
            //    this.m_bCanBeExchanged = false;
            //}
        
            //if (this.OperationalVersion)
            //{
            //    this.AutoLogonEnable = false;
            //}
        }
           
    }
	[DllImportAttribute("__Internal")]  
	public static extern void OpenGame (string gameName);

    public void startFish(int type)
    {
        if (type < 0 || type > 5)
        {
            return;
        }
        int num;
        if (GameManager.Getsingle().Language == "English")
        {
            num = 1;
        }
        else
        {
            num = 0;
        }
		#if UNITY_ANDROID
        // 发送名字 密码 类型 ip 和 多语言id
        //this.jo.Call("startFish");
        this.jo.Call("startFish", new object[]
        {
            UserInformation.username,
            UserInformation.password,
            type,
            Constants.IP,
            num.ToString()
        });
		#elif UNITY_STANDALONE_OSX || UNITY_IPHONE
		//userMessage
		//string aaa = UserInformation.username + " " + UserInformation.password + " " + "com.xingli.hall" + " " + "183.2.246.50" + " " + num + " " + DateTime.Now.Millisecond / 1000;

		//aaa = AES.AESEncrypt(aaa,"sadkjfdashfhegdgfdhjfghfdgdgdgoiewquqreqopacvoppodfdf");

		string str = "MoneyFish://www.xingli.com?";
//		string text = string.Concat(new string[]
//			{
//				"+NAME=",
//				UserInformation.username,
//				"+PWD=",
//				UserInformation.password,
//				"+IP=",
//				"183.2.246.50",
//				"+LAN=",
//				num.ToString()
//			});
		string text = string.Concat(new string[]
			{
				"+NAME=",
				UserInformation.username,
				"+PWD=",
				UserInformation.password,
				"+IP=",
				"183.2.246.50",
				"+LAN=",
				num.ToString()
			});
		text = str + text;
		Application.OpenURL(text);
		Application.Quit();
		//string url = string.Format("MoneyFish://?userMessage=" + aaa);
		//OpenGame(url);
		#endif
    }

    public void UpdateUserInfo(string nickname,char personsex,int photoid)
    {
        if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
        {
            if (this.m_bWrite)
            {
            }
            bool flag = NetMain.GetSingleton().MyCreateSocket.SendUpdateNickname(nickname, personsex, photoid);
            if (flag)
            {
               
                GameManager.CommunicationEnable = false;
            }
        }
        else
        {
           
        }
    }
    public void RecChangeInfoMessage(bool success, string reason)
    {

        if (success)
        {
            Userlocate_Controller.gatSingle().UpdateUserInifo();
        }
        else
        {
            //this.TipManager.ShowTipManagerPanel(EGameTipType.SystemError, 0, false);
        }
        GameManager.CommunicationEnable = true;
    }

	#if UNITY_ANDROID
    private void OnGUI()
    {
        GUILayout.Label("bbbb");
        GUI.Label(new Rect(200,200,200,200),"ssssss" + jo.ToString()+"aaaa");
    }
	#endif
    //[DebuggerHidden]
    //private IEnumerator DownloadVersion()
    //{
    //    HallManagerPanel.DownloadVersion DownloadVersion = new HallManagerPanel.DownloadVersion();
    //    DownloadVersion = this;
    //    return DownloadVersion;
    //}
    //public void OtherStartHall()
    //{
    //    if (this.m_bOtherStart)
    //    {
    //        this.Show = true;
    //        if (NetMain.GetSingleton())
    //        {
    //            if (NetMain.GetSingleton().MyCreateSocket.GetSocketStartFlag())
    //            {
    //                ShadowPanel.ShadowListClear();
    //                NetMain.GetSingleton().MyCreateSocket.SendUserLogin(this.Game.jo.Get<string>("mUserId"), this.Game.jo.Get<string>("mPwd"), this.HallVersion);
    //                this.Game.userInformation.username = this.Game.jo.Get<string>("mUserId");
    //                this.Game.userInformation.password = this.Game.jo.Get<string>("mPwd");
    //                this.Language = Localization.language;
    //                this.m_bEnglish = (this.Language == "English");
    //            }
    //            else
    //            {
    //                this.TipManager.ShowTipManagerPanel(EGameTipType.Net_ConnectionError, 0, true);
    //            }
    //        }
    //    }
    //}

}
