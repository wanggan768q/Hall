using com.miracle9.game.entity;
using DateDeclare;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Transmit : MonoBehaviour
{
	private static Transmit _MyTransmit;

	private Sockets m_CreateSocket;

	public static Transmit GetSingleton()
	{
		return Transmit._MyTransmit;
	}

	private void Awake()
	{
		if (Transmit._MyTransmit == null)
		{
			Debug.Log("_MyTransmit");
			Transmit._MyTransmit = this;
		}
	}

	public void TransmitGetPoint(Sockets MyCreateSocket)
	{
		this.m_CreateSocket = MyCreateSocket;
	}
   
	public void PostMsgControl(Hashtable table)
	{
		string text = table["method"].ToString();
		object[] array = table["args"] as object[];
		Debug.Log("...");
		Debug.Log("Method: " + text);
        if (text == "sendServerTime")
		{
            if (!ConfigGet.ConfigFirstGetFlag && !ConfigGet.ConfigGetFlag)
            {
                ConfigGet.WWWGet();
            }
            GameManager.Getsingle().SendVersion();
            //if (GameManager.Getsingle() && GameManager.Getsingle().m_bOtherStart)
            //{
            //    GameManager.Getsingle().OtherStartHall();
            //}
            //if (GameManager.Getsingle().m_bIsLogin)
            //{
            //    GameManager.Getsingle().NetDownAgainConnect();
            //}
            //GameManager.Getsingle().Autologon();
            //GameManager.Getsingle().ShowNetDown = false;
            Debug.Log("执行完毕");

        }       
		else if (text == "checkVersion")
        {
            Debug.Log("执行开始...checkVersion");
            this.DoCheckVersion(array);
		}
		else if (text == "userLogin")
        {
            Debug.Log("执行开始...userLogin");
            this.DoUserLogin(array);
		}
		else if (text == "checkInformation")
		{
			this.DoCheckInformation(array);
		}
		else if (text == "regist")
		{
            Debug.Log("注册");
			this.DoRegist(array);
		}
		else if (text == "noticeList")
		{
			this.DoNoticeList(array);
		}
		else if (text == "addExpeGold")
		{
			this.DoAddExpeGold(array);
		}
		else if (text == "newGameGold")
		{
			this.DoNewGameGold(array);
		}
		else if (text == "newExpeGold")
		{
			this.DoNewExpeGold(array);
		}
		else if (text == "newUserLevel")
		{
			this.DoNewUserLevel(array);
		}
		else if (text == "gameLoadAddress")
		{
			this.DoGameLoadAddress(array);
		}
		else if (text == "updateRunState")
		{
			this.DoUpdateRunState(array);
		}
		else if (text == "updateNickname")
		{
			this.DoUpdateNickname(array);
		}
		else if (text == "updatePassword")
		{
			this.DoUpdatePassword(array);
		}
		else if (text == "newUserMail")
		{
			this.DoNewUserMail(array);
		}
		else if (text == "mailList")
		{
			this.DoMailList(array);
		}
		else if (text == "deleteMail")
		{
			this.DoDeleteMail(array);
		}
		else if (text == "getMailContent")
		{
			this.DoGetMailContent(array);
		}
		else if (text == "topList")
		{
			this.DoTopList(array);
		}
		else if (text == "inputPassword")
		{
			this.DoInputPassword(array);
		}
		else if (text == "tellServerUserPassword")
		{
			this.DoTellServerUserPassword(array);
		}
		else if (text == "overflow")
		{
			this.DoOverflow(array);
		}
		else if (text == "unOverflow")
		{
			this.DoUnOverflow(array);
		}
		else if (text == "quitToLogin")
		{
			this.DoQuitToLogin(array);
		}
		else if (text == "userAward")
		{
			this.DoUserAward(array);
		}
		else if (text == "chargeOpen")
		{
			this.DoChargeOpen(array);
		}
		else if (text == "recharge")
		{
			this.DoRecharge(array);
		}
		else if (text == "exCharge")
		{
			this.DoExcharge(array);
		}
		else if (text == "chargeNotice")
		{
			this.DoChargeNotice(array);
		}
		else if (text == "NetThread/NetDown")
        {
            Debug.Log("执行开始...NetThread/NetDown");
            GameManager.Getsingle().RecNetDown();
			this.DoNetDown(array);
            Debug.Log("执行完毕...NetThread/NetDown");
        }
		else if (text == "dingFen")
		{
			this.DoDingFen(array);
		}
		else if (text == "lastGameScore")
		{
			this.DoLastGameScore(array);
		}
		else if (text == "scrollMessage")
		{
			this.DoScrollMessage(array);
		}
		else if (text == "addInformation")
		{
			this.DoAddInformation(array);
		}
		else if (text == "checkGameVersions")
		{
			this.DoCheckGameVersions(array);
		}
		else if (text == "cancelWaitingExpiryUser")
		{
			this.DoCancelWaitingExpiryUser(array);
		}
		else
		{
			Debug.Log("No Message Type!");
		}
	}

	private void DoCheckVersion(object[] args)
	{
		Hashtable hashtable = args[0] as Hashtable;
		bool flag = (bool)hashtable["haveNewVersionIDFlag"];
		string text = (string)hashtable["downloadWindows"];
		string text2 = (string)hashtable["downloadAndroid"];
		Debug.Log("haveNewVersionIDFlag: " + flag);
		Debug.Log("downloadWindowsAddr: " + text);
		Debug.Log("downloadAndroidAddr: " + text2);
		if (flag)
		{
            GameManager.Getsingle().RecUpdateMessage();
            //显示版本信息
        }
	}

	private void DoUserLogin(object[] args)
	{
        
		User user = new User();
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["isLogin"];
        Debug.Log("loginHashCount"+hashtable.Count);
        foreach(string hashkey in hashtable.Keys)
        {
            Debug.Log("UserLoginHashtable..." + hashkey);
            Debug.Log("hashkey..." + hashtable[hashkey]);
        }
     //   GameManager.Getsingle().RecLoginMessage(3);    //测试页面
        int num = (int)hashtable["messageStatus"];
		if (flag)
		{
			bool flag2 = (bool)hashtable["isShutup"];
			string hallLoadAddressWindows = (string)hashtable["hallLoadAddressWindows"];
			string hallLoadAddressAndroid = (string)hashtable["hallLoadAddressAndroid"];
			user = (hashtable["user"] as User);
			bool flag3 = (bool)hashtable["special"];
			int length = (hashtable["isRunning"] as Array).Length;
			int[] array = new int[length + 1];
			for (int i = 0; i < length; i++)
			{
				array[i] = (int)(hashtable["isRunning"] as Array).GetValue(i);
			}

            Debug.Log("bIsLoginFlag: " + flag);
            Debug.Log("nMessageStatus: " + num);
            Debug.Log("bIsShutupFlag: " + flag2);
            Debug.Log("hallLoadAddressWindows: " + hallLoadAddressWindows);
            Debug.Log("hallLoadAddressAndroid: " + hallLoadAddressAndroid);
			Debug.Log("bSpecialFlag: " + flag3);
			Debug.Log("MyUser.username: " + user.username);
			Debug.Log("MyUser.promoterName: " + user.promoterName);
			Debug.Log("MyUser.nickname: " + user.nickname);
			Debug.Log("MyUser.sex: " + user.sex);
			Debug.Log("MyUser.level: " + user.level);
			Debug.Log("MyUser.gameGold: " + user.gameGold);
			Debug.Log("MyUser.expeGold: " + user.expeGold);
			Debug.Log("MyUser.Overflow: " + user.overflow);
			Debug.Log("MyUser.photoId:" + user.photoId);
			Debug.Log("MyUser.type:" + user.type);
			Console.Write("isRunning:");
			if (length > 0)
			{
				for (int j = 0; j < length; j++)
				{
					Console.Write(" " + array[j]);
				}
			}
			Debug.Log(".....");

            //显示用户信息
            GlobalModel.HallShareAndroidAddress = hallLoadAddressAndroid;
            GlobalModel.HallShareWindowAddress = hallLoadAddressWindows;
    
            UserInformation.username = user.username;
            UserInformation.nickname = user.nickname;
            UserInformation.sex = user.sex;
            UserInformation.level = user.level;
            UserInformation.gameGold = user.gameGold;
            UserInformation.expeGold = user.expeGold;
            UserInformation.overflow = user.overflow;
            UserInformation.photoId = user.photoId;
            UserInformation.specialFlag = flag3;
            UserInformation.type = user.type;
            UserInformation.ID = user.id;
            UserInformation.ScoreOverflow = false;
            UserInformation.status = 0;
            
           Debug.Log("userid" + UserInformation.username);
			if (length > 0)
			{
				for (int k = 0; k < length; k++)
				{
					if (ConfigGet.GameConfigData[k, 1] == 1)
					{
                        //显示图标
						//GamePanel.GameTypeCache[k].transform.Find("GameIcon").GetComponent<GameControl>().Information.isRunning = array[k];
					}
				}
			}
		   	GameManager.Getsingle().RecLoginMessage(3);
			//if (HallManagerPanel.GetSingleton().Game.userInformation.username == "yunwei1")
			//{
			//	NGUIDebug.Log(new object[]
			//	{
			//		string.Empty
			//	});
			//	NGUIDebug.Log(new object[]
			//	{
			//		string.Empty
			//	});
			//	NGUIDebug.Log(new object[]
			//	{
			//		string.Empty
			//	});
			//	NGUIDebug.Log(new object[]
			//	{
			//		string.Empty
			//	});
			//	NGUIDebug.Log(new object[]
			//	{
			//		"这个包的IP是：" + Constants.IP
			//	});
			//}
		}
		else
		{
			Debug.Log("bIsLoginFlag: " + flag);
			Debug.Log("nMessageStatus: " + num);
			//HallManagerPanel.GetSingleton().RecLoginMessage(num);
		}
	}

	private void DoCheckInformation(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		int num = (int)hashtable["status"];
		Debug.Log("status：" + num);
		//HallManagerPanel.GetSingleton().RecCheckInformation(num);
	}

	private void DoRegist(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["success"];
		int num = (int)hashtable["messageStatus"];
		Debug.Log("success: " + flag);
		Debug.Log("messageStatus: " + num);
		GameManager.Getsingle().RecRegisterMessage(flag, num);
	}

	private void DoNoticeList(object[] args)
	{
		//Notice notice = new Notice();
		//notice = (args[0] as Notice);
		//string title = notice.title;
		//string content = notice.content;
		//string datetime = notice.datetime;
		//string admin = notice.admin;
		//Debug.Log("title: " + title);
		//Debug.Log("content: " + content);
		//Debug.Log("datetime: " + datetime);
		//Debug.Log("admin: " + admin);
		//HallManagerPanel.GetSingleton().Game.SAnouncement.title = notice.title;
		//HallManagerPanel.GetSingleton().Game.SAnouncement.content = notice.content;
		//HallManagerPanel.GetSingleton().Game.SAnouncement.dateTime = notice.datetime;
		//HallManagerPanel.GetSingleton().Game.SAnouncement.admin = notice.admin;
		//HallManagerPanel.GetSingleton().RecAnounceMessage();
	}

	private void DoAddExpeGold(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["success"];
		string text = (string)hashtable["message"];
		Debug.Log("bIsSuccessFlag:" + flag);
		Debug.Log("strMessage:" + text);
		//HallManagerPanel.GetSingleton().RecApplyForExpCoin(flag, text);
	}

	private void DoNewGameGold(object[] args)
	{
		int num = (int)args[0];
		Debug.Log("nGameGold:" + num);
	//	HallManagerPanel.GetSingleton().RecUpdateGameCoin(num);
	}

	private void DoNewExpeGold(object[] args)
	{
		int num = (int)args[0];
		Debug.Log("nExpeGold:" + num);
		//HallManagerPanel.GetSingleton().RecUpdateExpCoin(num);
	}

	private void DoNewUserLevel(object[] args)
	{
		int num = (int)args[0];
		Debug.Log("nUserLevel:" + num);
		//HallManagerPanel.GetSingleton().RecUpdateLevel(num);
	}

	private void DoGameLoadAddress(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		int num = (int)hashtable["gameType"];
		bool flag = (bool)hashtable["haveNewVersionIDFlag"];
		string text = (string)hashtable["versionCode"];
		string text2 = (string)hashtable["downloadWindows"];
		string text3 = (string)hashtable["downloadAndroid"];
		Debug.Log("gameType:" + num);
		Debug.Log("haveNewVersion:" + flag);
		Debug.Log("versionCode:" + text);
		Debug.Log("downloadWindowsAddr:" + text2);
		Debug.Log("downloadAndroidAddr:" + text3);
		//GamePanel.GameTypeCache[num - 1].transform.Find("GameIcon").GetComponent<GameControl>().Information.downloadAndroid = text3;
		//GamePanel.GameTypeCache[num - 1].transform.Find("GameIcon").GetComponent<GameControl>().Information.downloadWindows = text2;
		//GamePanel.GameTypeCache[num - 1].transform.Find("GameIcon").GetComponent<GameControl>().Information.downloadVersionCode = text;
		//HallManagerPanel.GetSingleton().RecGameLoad(num - 1, flag);
	}

	private void DoUpdateRunState(object[] args)
	{
		int length = (args[0] as Array).Length;
		if (length > 0)
		{
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (int)(args[0] as Array).GetValue(i);
			}
			Console.Write("nRunStateList: ");
			for (int j = 0; j < length; j++)
			{
				Console.Write(array[j] + ";");
			}
			Debug.Log("....");
			//HallManagerPanel.GetSingleton().RecUpdateRunState(array, length);
		}
		else
		{
			Debug.Log("大厅中不存在游戏");
		}
	}

	private void DoUpdateNickname(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["success"];
		string text = (string)hashtable["message"];
		Debug.Log("bIsSuccessFlag:" + flag);
		Debug.Log("strMessage:" + text);
		GameManager.Getsingle().RecChangeInfoMessage(flag, text);
	}

	private void DoUpdatePassword(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["success"];
		string text = (string)hashtable["message"];
		Debug.Log("bIsSuccessFlag:" + flag);
		Debug.Log("strMessage:" + text);
		//HallManagerPanel.GetSingleton().RecChangeCodeMessage(flag, text);
	}

	private void DoNewUserMail(object[] args)
	{
		Debug.Log("您有新的邮件，请查收:");
		//HallManagerPanel.GetSingleton().RecNewMailMessage();
	}

	private void DoMailList(object[] args)
	{
		//int length = (args[0] as Array).Length;
		//com.miracle9.game.entity.UserMail[] array = new com.miracle9.game.entity.UserMail[length];
		//MailPanelControl.GetSingleton().MailMessageList.Clear();
		//if (length > 0)
		//{
		//	if (MailPanelControl.GetSingleton().MailMessageList.Count != length)
		//	{
		//		for (int i = 0; i < length; i++)
		//		{
		//			array[i] = (com.miracle9.game.entity.UserMail)(args[0] as Array).GetValue(i);
		//		}
		//		for (int j = 0; j < length; j++)
		//		{
		//			Debug.Log("id:" + array[j].id);
		//			Debug.Log("title:" + array[j].title);
		//			Debug.Log("content:" + array[j].content);
		//			Debug.Log("sender:" + array[j].sender);
		//			Debug.Log("datetime:" + array[j].datetime);
		//			Debug.Log("status:" + array[j].status);
		//		}
		//		for (int k = 0; k < length; k++)
		//		{
		//			DateDeclare.UserMail item;
		//			item.id = array[k].id;
		//			item.title = array[k].title;
		//			item.content = array[k].content;
		//			item.sender = array[k].sender;
		//			item.datetime = array[k].datetime;
		//			item.status = array[k].status;
		//			MailPanelControl.GetSingleton().MailMessageList.Add(item);
		//		}
		//	}
		//}
		//else
		//{
		//	Debug.Log("邮件列表为空");
		//}
		//HallManagerPanel.GetSingleton().RecMailMessage();
	}

	private void DoDeleteMail(object[] args)
	{
		bool flag = (bool)args[0];
		Debug.Log("bIsSuccessFlag:" + flag);
		//HallManagerPanel.GetSingleton().RecDeleteMailMessage(flag);
	}

	private void DoGetMailContent(object[] args)
	{
		string text = null;
		string text2 = null;
		string text3 = null;
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["flag"];
		Debug.Log("bFlag：" + flag);
		if (flag)
		{
			text = (string)hashtable["sender"];
			text2 = (string)hashtable["datetime"];
			text3 = (string)hashtable["content"];
			Debug.Log("sender:" + text);
			Debug.Log("datetime:" + text2);
			Debug.Log("content:" + text3);
		}
		else
		{
			Debug.Log("邮件不存在");
		}
		//HallManagerPanel.GetSingleton().RecDetailMailMessage(text, text2, text3, flag);
	}

	private void DoTopList(object[] args)
	{
		//Hashtable hashtable = new Hashtable();
		//hashtable = (args[0] as Hashtable);
		//int num = (int)hashtable["gameType"];
		//int num2 = (int)hashtable["topListType"];
		//int length = (hashtable["userTop"] as Array).Length;
		//if (length > 0)
		//{
		//	com.miracle9.game.entity.UserTop[] array = new com.miracle9.game.entity.UserTop[length];
		//	for (int i = 0; i < length; i++)
		//	{
		//		array[i] = (com.miracle9.game.entity.UserTop)(hashtable["userTop"] as Array).GetValue(i);
		//	}
		//	Debug.Log("nGameType:" + num);
		//	Debug.Log("nTopListType:" + num2);
		//	for (int j = 0; j < length; j++)
		//	{
		//		Debug.Log("nickname:" + array[j].nickname);
		//		Debug.Log("gold:" + array[j].gold);
		//		Debug.Log("awardName:" + array[j].awardName);
		//		Debug.Log("datetime:" + array[j].datetime);
		//	}
		//	if (num >= 0 && num <= 6)
		//	{
		//		for (int k = 0; k < length; k++)
		//		{
		//			RankingButtonControl.gameRanking[k].nickName = array[k].nickname;
		//			RankingButtonControl.gameRanking[k].gold = array[k].gold;
		//			RankingButtonControl.gameRanking[k].awardName = array[k].awardName;
		//			RankingButtonControl.gameRanking[k].datetime = array[k].datetime;
		//		}
		//		HallManagerPanel.GetSingleton().RecRanking(num, length, num2);
		//	}
		//}
		//else
		//{
		//	Debug.Log("nGameType:" + num);
		//	HallManagerPanel.GetSingleton().RecRanking(num, 0, num2);
		//}
	}

	private void DoInputPassword(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		int num = (int)hashtable["nType"];
		int num2 = (int)hashtable["nGameScore"];
		Debug.Log("nType:" + num);
		Debug.Log("nGameScore:" + num2);
		//HallManagerPanel.GetSingleton().RecInputPassword(num, num2);
	}

	private void DoTellServerUserPassword(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		int num = (int)hashtable["nType"];
		bool flag = (bool)hashtable["bFlag"];
		Debug.Log("nType:" + num);
		Debug.Log("bFlag:" + flag);
		//HallManagerPanel.GetSingleton().RecInputPasswordResult(num, flag);
	}

	private void DoOverflow(object[] args)
	{
		Debug.Log("爆机了。。。");
		//HallManagerPanel.GetSingleton().RecOverflow();
	}

	private void DoUnOverflow(object[] args)
	{
		Debug.Log("爆机状态解除...");
		//HallManagerPanel.GetSingleton().RecOverflowBeDealed();
	}

	private void DoQuitToLogin(object[] args)
	{
		int num = (int)args[0];
		Debug.Log("nQuitType:" + num);
		//HallManagerPanel.GetSingleton().RecQuitToLogin(num);
	}

	private void DoUserAward(object[] args)
	{
		int num = (int)args[0];
		Debug.Log("nGold:" + num);
		//HallManagerPanel.GetSingleton().RecAwardCoin(num);
	}

	private void DoNetDown(object[] args)
	{
		Thread.Sleep(10);
		this.m_CreateSocket.CreateReceiveThread();
	}

	private void DoChargeOpen(object[] args)
	{
		int num = 0;
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		int num2 = (int)hashtable["status"];
		Debug.Log("nStatus: " + num2);
		if (num2 == 0)
		{
			num = (int)hashtable["poportion"];
			Debug.Log("nPoportion:" + num);
		}
		if (num2 == 0)
		{
		//	HallManagerPanel.GetSingleton().RecShowAwarding(true, num);
		}
		else if (num2 == 1)
		{
		//	HallManagerPanel.GetSingleton().RecShowAwarding(false, 1);
		}
	}

	private void DoRecharge(object[] args)
	{
		int num = 0;
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["success"];
		Debug.Log("bSuccess: " + flag);
		if (!flag)
		{
			num = (int)hashtable["messageStatus"];
			Debug.Log("nMessageStatus:" + num);
		}
		//HallManagerPanel.GetSingleton().RecRechargeResult(num, flag);
	}

	private void DoExcharge(object[] args)
	{
		int num = 0;
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["success"];
		Debug.Log("bSuccess: " + flag);
		if (!flag)
		{
			num = (int)hashtable["messageStatus"];
			Debug.Log("nMessageStatus:" + num);
		}
		//HallManagerPanel.GetSingleton().RecAwardingResult(num, flag);
	}

	private void DoChargeNotice(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		string text = (string)hashtable["time"];
		Debug.Log("time: " + text);
		int num = (int)hashtable["gameGold"];
		Debug.Log("nGameGold：" + num);
		int num2 = (int)hashtable["type"];
		Debug.Log("nType：" + num2);
		//HallManagerPanel.GetSingleton().RecRechargeOrExchangeResult(num2, num, text);
	}

	public void DoDingFen(object[] agrs)
	{
		//HallManagerPanel.GetSingleton().RecScoreOverflow();
	}

	public void DoLastGameScore(object[] args)
	{
		int num = (int)args[0];
		Debug.Log("nScore: " + num);
		//HallManagerPanel.GetSingleton().RecLastGameScore(num);
	}

	private void DoScrollMessage(object[] args)
	{
		string text = (string)args[0];
		Debug.Log("ScrollMessage: " + text);
		//HallManagerPanel.GetSingleton().RecScrollingMessage(text);
	}

	public void DoAddInformation(object[] args)
	{
		Hashtable hashtable = new Hashtable();
		hashtable = (args[0] as Hashtable);
		bool flag = (bool)hashtable["success"];
		Debug.Log("bSuccessFlag: " + flag);
		//HallManagerPanel.GetSingleton().RecAddInformation(flag);
	}

	public void DoCheckGameVersions(object[] args)
	{
		int length = (args[0] as Array).Length;
		int[] array = new int[length];
		if (length > 0)
		{
			for (int i = 0; i < length; i++)
			{
				array[i] = (int)(args[0] as Array).GetValue(i);
			}
			Console.Write("nCheckList: ");
			for (int j = 0; j < length; j++)
			{
				Console.Write(array[j] + ";");
			}
			Debug.Log("....");
		}
	//	HallManagerPanel.GetSingleton().RecCheckGameVersions(array);
	}

	public void DoCancelWaitingExpiryUser(object[] args)
	{
		Debug.Log("************DoCancelWaitingExpiryUser************");
	//	HallManagerPanel.GetSingleton().RecCancelWaitingExpiryUser();
	}
}   
