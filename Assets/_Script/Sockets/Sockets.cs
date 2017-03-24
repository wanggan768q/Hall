using com.miracle9.game.entity;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;
using UnityEngine;
using wox.serial;

public class Sockets : MonoBehaviour
{
	private static Sockets _MyCreateSocket;

	private MessageControl m_MyMessageControl;

	private DataEncrypt m_DataEncrypt;

	public Socket MySocket;

	private Thread m_nListenerThread;

	private Thread m_nReceiveThread;

	private bool m_nSocketStartFlag;

	private long m_recriveHeartTime;

	private long m_sendHeartTime;

	public static Sockets GetSingleton()
	{
		return Sockets._MyCreateSocket;
	}

	private void Awake()
	{
		if (Sockets._MyCreateSocket == null)
		{
			Debug.Log("Sockets");
			Sockets._MyCreateSocket = this;
		}
	}

	public void CreateSocketGetPoint(MessageControl MyMessageControl, DataEncrypt MyDataEncrypt)
	{
		this.CreateReceiveThread();
		this.CreateListenerThread();
		this.m_MyMessageControl = MyMessageControl;
		this.m_DataEncrypt = MyDataEncrypt;
	}

	private void CreateListenerThread()
	{
		try
		{
			this.m_nListenerThread.Abort();
			this.m_nListenerThread = null;
		}
		catch (Exception ex)
		{
			Console.WriteLine("ListenerThread" + ex.Message);
		}
		this.m_nListenerThread = new Thread(new ThreadStart(this.socketListener));
		this.m_nListenerThread.Start();
	}

	public void CreateReceiveThread()
	{
		try
		{
			this.m_nReceiveThread.Abort();
			this.m_nReceiveThread = null;
            Debug.Log("aaaaaaaaaaaaaaa----------------");

        }
		catch (Exception ex)
		{
			Console.WriteLine("ReceiveThread" + ex.Message);
		}
		this.m_nReceiveThread = new Thread(new ThreadStart(this.SocketConnect));
		this.m_nReceiveThread.Start();
	}

	public bool GetSocketStartFlag()
	{
		return this.m_nSocketStartFlag;
	}

	public void SocketClose()
	{
		if (this.MySocket != null)
		{
			this.MySocket.Close();
			this.MySocket = null;
		}
		try
		{
			this.m_nReceiveThread.Abort();
			this.m_nReceiveThread = null;
		}
		catch (Exception ex)
		{
			Console.WriteLine("ReceiveThread" + ex.Message);
		}
		try
		{
			this.m_nListenerThread.Abort();
			this.m_nListenerThread = null;
		}
		catch (Exception ex2)
		{
			Console.WriteLine("ListenerThread" + ex2.Message);
		}
	}

	public void SocketConnect()
	{
		Thread.Sleep(100);
		this.m_nSocketStartFlag = false;
		this.m_nSocketStartFlag = this._socketStart();
		if (!this.m_nSocketStartFlag)
		{
			return;
		}
		this.SendPublicKey();
		this._myReceiveControl();
	}

	private bool _socketStart()
	{
		this.MySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("101.37.85.230"), 8899);
		bool result;
		try
		{
			this.MySocket.Connect(remoteEP);
			this.m_recriveHeartTime = this._getCurTime();
			this.m_sendHeartTime = this._getCurTime();
			result = true;
		}
		catch (SocketException ex)
		{
			Console.WriteLine("connect:" + ex.Message);
			this._netDownControl();
			result = false;
		}
		return result;
	}

	private void _myReceiveControl()
	{
		int num = 0;
		MemoryStream memoryStream = new MemoryStream();
		while (true)
		{
			byte[] buffer = new byte[1024];
			try
			{
				num = this.MySocket.Receive(buffer);
                Debug.Log("get msg recive" + num);
            }
			catch (Exception ex)
			{
				Console.WriteLine("Receive:" + ex.Message);
				this._netDownControl();
				return;
			}
			if (num <= 0)
			{
				break;
			}
			if (num > 0)
			{
				memoryStream.Position = memoryStream.Length;
				memoryStream.Write(buffer, 0, num);
			}
			while (memoryStream.Length >= 4L)
			{
				memoryStream.Position = 0L;
				byte[] array = new byte[4];
				memoryStream.Read(array, 0, 4);
				Array.Reverse(array);
				int num2 = BitConverter.ToInt32(array, 0);
				if (memoryStream.Length - 4L < (long)num2)
				{
					break;
				}
				memoryStream.Position = 4L;
				byte[] array2 = new byte[num2];
				memoryStream.Read(array2, 0, num2);
				int num3 = (int)(memoryStream.Length - (long)num2 - 4L);
				byte[] buffer2 = new byte[num3];
				memoryStream.Position = (long)(num2 + 4);
				memoryStream.Read(buffer2, 0, num3);
				memoryStream = new MemoryStream();
				memoryStream.Position = 0L;
				memoryStream.Write(buffer2, 0, num3);
				string @string = Encoding.UTF8.GetString(array2);
                if (@string != null && @string != "")
                {
                    this._receiveNetMessage(@string);
                }
               
            }
		}
        
		this._netDownControl();
	}

	private void _receiveNetMessage(string msg)
	{
        if (msg == null || msg == "")
        {
            return;
        }
		if (this.m_DataEncrypt.GetKey() != "none")
		{
			msg = this.m_DataEncrypt.Decrypt(msg);
		}
		XmlReader xmlReader = XmlReader.Create(new StringReader(msg));
		xmlReader.Read();
		SimpleReader simpleReader = new SimpleReader();
        if (xmlReader == null)
        {
            return;
        }

        Hashtable hashtable = simpleReader.read(xmlReader) as Hashtable;

		string text = hashtable["method"].ToString();
		object[] array = hashtable["args"] as object[];
        Debug.Log("  ");
		Debug.Log("receiceMsg>>>Net:" + text);
		if (text == "sendServerTime")
		{
			Hashtable hashtable2 = new Hashtable();
			hashtable2 = (array[0] as Hashtable);
			long serverTime = (long)hashtable2["time"];
			string str = (string)hashtable2["key"];
			this.m_DataEncrypt.setServerTime(serverTime);
			this.m_DataEncrypt.DecryptKey(str);
		}
		else if (text == "heart")
		{
			this.m_recriveHeartTime = this._getCurTime();
			return;
		}
        Debug.Log("arr"+array[0]);
		this.m_MyMessageControl.AddMessage(hashtable);
	}

	private long _getCurTime()
	{
		DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
		DateTime now = DateTime.Now;
		return (long)Math.Round((now - d).TotalMilliseconds, MidpointRounding.AwayFromZero);
	}

	public void socketListener()
	{
		Thread.Sleep(5000);
		long num = 0L;
		while (true)
		{
			num = this._getCurTime();
			if (num - this.m_recriveHeartTime > 20000L)
			{
				try
				{
					this.m_nReceiveThread.Abort();
					this.m_nReceiveThread = null;
				}
				catch (Exception ex)
				{
					Console.WriteLine("ReceiveThread" + ex.Message);
				}
				this._netDownControl();
				this.m_recriveHeartTime = num;
			}
			else
			{
				if (num - this.m_sendHeartTime >= 5000L && this.MySocket != null)
				{
					this.SendHeart();
					this.m_sendHeartTime = num;
				}
				Thread.Sleep(500);
			}
		}
	}

    private bool _sendMsg(string strMethod, object[] args)
    {
        SimpleWriter simpleWriter = new SimpleWriter();
        StringWriter stringWriter = new StringWriter();
        XmlTextWriter el = new XmlTextWriter(stringWriter);
        if (simpleWriter == null)
        {
            Debug.Log("simpleWriter null");
        }
        if (strMethod == null)
        {
            Debug.Log("strMethod null");
        }
        if (args == null)
        {
            Debug.Log("args null");
        }
        if (this.m_DataEncrypt == null)
        {
            Debug.Log("m_DataEncrypt null");
        }
        simpleWriter.write(new Hashtable
		{
			{
				"method",
				strMethod
			},
			{
				"args",
				args
			},
			{
				"time",
				this.m_DataEncrypt.GetUnixTime()
			}
		}, el);
		return this.m_DataEncrypt.GetKey() != "none" && this._send(this.m_DataEncrypt.Encrypt(stringWriter.ToString()));
	}

	private bool _send(string msg)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(msg);
		byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
		byte[] array = new byte[bytes.Length + 4];
		Array.Reverse(bytes2);
		Buffer.BlockCopy(bytes2, 0, array, 0, 4);
		Buffer.BlockCopy(bytes, 0, array, 4, bytes.Length);
		bool result;
		try
		{
			int num = this.MySocket.Send(array, array.Length, SocketFlags.None);
			if (num == array.Length)
			{
				result = true;
			}
			else
			{
				result = false;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			result = false;
		}
		return result;
	}

	private void _netDownControl()
    {
        if (this.MySocket != null)
		{
			this.MySocket.Close();
			this.MySocket = null;
		}
		this.m_DataEncrypt.KeyReset();
		this._tellUINetDown();
	}

	private void _tellUINetDown()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("method", "NetThread/NetDown");
		hashtable.Add("args", new object[0]);    
		this.m_MyMessageControl.AddMessage(hashtable);
	}

	public bool SendCheckVersion(string versionCode)
	{
		string strMethod = "versionService/checkVersion";
		object[] args = new object[]
		{
			versionCode
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendcheckInformation()
	{
		string strMethod = "userService/checkInformation";
		object[] args = new object[0];
		return this._sendMsg(strMethod, args);
	}

	public bool SendIgnoreCheckInformation(int nStatus)
	{
		string strMethod = "userService/ignoreCheckInformation";
		object[] args = new object[]
		{
			nStatus
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendRegistInFo(string username, string nickname, string password, int photoId, int type, string promoterName)
	{
		User user = new User();
		user.username = username;
		user.nickname = nickname;
		user.password = password;
		user.photoId = photoId;
		user.type = type;
		user.promoterName = promoterName;
		string strMethod = "userService/regist";
		object[] args = new object[]
		{
			user
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendRequestNotice()
	{
		string strMethod = "noticeService/noticeList";
		object[] args = new object[0];
		return this._sendMsg(strMethod, args);
	}

	public bool SendUserLogin(string username, string password, string checkVersion)
	{
		string strMethod = "userService/userLogin";
		object[] args = new object[]
		{
			username,
			password,
			checkVersion
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendGetExpeGold()
	{
		string strMethod = "userService/addExpeGold";
		object[] args = new object[0];
		return this._sendMsg(strMethod, args);
	}

	public bool SendGameVersionCode(int nGameType, string versionCode)
	{
		string strMethod = "userService/gameLoadAddress";
		object[] args = new object[]
		{
			nGameType,
			versionCode
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendUpdateNickname(string nickname, char sex, int photoId)
	{
		string strMethod = "userService/updateNickname";
		object[] args = new object[]
		{
			nickname,
			sex,
			photoId
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendUpdatePassword(string oldPassword, string newPassword)
	{
		string strMethod = "userService/updatePassword";
		object[] args = new object[]
		{
			oldPassword,
			newPassword
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendMailList()
	{
		string strMethod = "userMailService/mailList";
		object[] args = new object[0];
		return this._sendMsg(strMethod, args);
	}

	public bool SendDeleteMail(string ids)
	{
		string strMethod = "userMailService/deleteMail";
		object[] args = new object[]
		{
			ids
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendGetMailContent(int MailId)
	{
		string strMethod = "userMailService/getMailContent";
		object[] args = new object[]
		{
			MailId
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendTopList(int nGameType, int topListType)
	{
		string strMethod = "userTopService/topList";
		object[] args = new object[]
		{
			nGameType + 1,
			topListType
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendExitGame()
	{
		string strMethod = "userService/exitGame";
		object[] args = new object[0];
		return this._sendMsg(strMethod, args);
	}

	public bool SendUserPassword(int nType, string userPassword)
	{
		string strMethod = "userService/tellServerUserPassword";
		object[] args = new object[]
		{
			nType,
			userPassword
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendHeart()
	{
		string strMethod = "userService/heart";
		object[] args = new object[0];
		return this._sendMsg(strMethod, args);
	}

	public bool SendReCharge(int nRechargeAmount, string strRemarks)
	{
		string strMethod = "userService/recharge";
		object[] args = new object[]
		{
			nRechargeAmount,
			strRemarks
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendExcharge(int exchargeGold, string strRemarks, string strPassword)
	{
		string strMethod = "userService/exCharge";
		object[] args = new object[]
		{
			exchargeGold,
			strRemarks,
			strPassword
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendCheckGameVersions(string luckVersion, string fishVersion, string cardVersion, string bulletFishVersion, string mermaidVersion, string lackVersion)
	{
		string strMethod = "userService/checkGameVersions";
		string[] array = new string[]
		{
			luckVersion,
			fishVersion,
			cardVersion,
			bulletFishVersion,
			mermaidVersion,
			lackVersion
		};
		object[] args = new object[]
		{
			array
		};
		return this._sendMsg(strMethod, args);
	}

	public bool SendAddInformation(string card, string phone, string question, string answer)
	{
		string strMethod = "userService/addInformation";
		object[] args = new object[]
		{
			card,
			phone,
			question,
			answer
		};
		return this._sendMsg(strMethod, args);
	}

	private bool SendPublicKey()
	{
		string[] array = new string[2];
		array = this.m_DataEncrypt.NetConnectGetRsaKey();
		string value = "userService/publicKey";
		object[] value2 = new object[]
		{
			array[0],
			array[1]
		};
		SimpleWriter simpleWriter = new SimpleWriter();
		StringWriter stringWriter = new StringWriter();
		XmlTextWriter el = new XmlTextWriter(stringWriter);
		simpleWriter.write(new Hashtable
		{
			{
				"method",
				value
			},
			{
				"args",
				value2
			}
		}, el);
		string s = stringWriter.ToString();
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
		byte[] array2 = new byte[bytes.Length + 4];
		Array.Reverse(bytes2);
		Buffer.BlockCopy(bytes2, 0, array2, 0, 4);
		Buffer.BlockCopy(bytes, 0, array2, 4, bytes.Length);
		bool result;
		try
		{
			int num = this.MySocket.Send(array2, array2.Length, SocketFlags.None);
			if (num == array2.Length)
			{
				result = true;
			}
			else
			{
				result = false;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			result = false;
		}
		return result;
	}

	public bool SendCancelExpiryPwd(int userId)
	{
		string strMethod = "userService/cancelExpiryPwd";
		object[] args = new object[]
		{
			userId
		};
		return this._sendMsg(strMethod, args);
	}
}
