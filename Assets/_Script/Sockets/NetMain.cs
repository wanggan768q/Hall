using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class NetMain : MonoBehaviour
{
	public static NetMain G_NetMngr;

	public Sockets MyCreateSocket;

    public LoginRequest mLoginRequest;

	private Transmit MyTransmit;

	private MessageControl MyMessageControl;

	public PostMessageThread MyPostMessageThread;

	private DataEncrypt MyDataEncrypt;

	private Thread _ConnectThread;

	private float fSendTime;

	public static NetMain GetSingleton()
	{
		return NetMain.G_NetMngr;
	}

	private void Awake()
	{
		if (NetMain.G_NetMngr == null)
		{
			UnityEngine.Debug.Log("G_NetMngr");
			NetMain.G_NetMngr = this;
		}
	}

    void Start()
	{
        ConnectSvr();
	}

	public void ConnectSvr()
	{
		this.MyCreateSocket = Sockets.GetSingleton();
		this.MyTransmit = Transmit.GetSingleton();
		this.MyMessageControl = MessageControl.GetSingleton();
		this.MyPostMessageThread = PostMessageThread.GetSingleton();
		this.MyDataEncrypt = DataEncrypt.GetSingleton();
		this.MyMessageControl.MessageControlParaInit();
		this.MyPostMessageThread.PostMessageThreadParaInit();
		this.MyCreateSocket.CreateSocketGetPoint(this.MyMessageControl, this.MyDataEncrypt);
		this.MyTransmit.TransmitGetPoint(this.MyCreateSocket);
		this.MyPostMessageThread.PostMessageThreadGetPoint(this.MyMessageControl, this.MyTransmit);
        this.mLoginRequest = LoginRequest.GetSingleton();

    }

	public void CreateRcvThread()
	{
		if (this._ConnectThread == null || !this._ConnectThread.IsAlive)
		{
			this._ConnectThread = new Thread(new ThreadStart(this.MyCreateSocket.SocketConnect));
			this._ConnectThread.Start();
		}
	}

	private void Update()
	{
		if (this.MyPostMessageThread != null)
		{           
			this.MyPostMessageThread.PostThread();
		}
	}

	private void OnDestroy()
	{
		this.MyCreateSocket.SocketClose();
		if (this._ConnectThread != null && this._ConnectThread.IsAlive)
		{
			this._ConnectThread.Abort();
		}
	}
}
