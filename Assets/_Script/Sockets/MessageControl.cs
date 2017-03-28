using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class MessageControl : MonoBehaviour
{
	private Mutex m_MessageControlMutex;

	private Queue m_MsgQueue;

	private static MessageControl _MyMessageControl;

	public static MessageControl GetSingleton()
	{
		return MessageControl._MyMessageControl;
	}

	private void Awake()
	{
		if (MessageControl._MyMessageControl == null)
		{
			Debug.Log("_MyMessageControl");
			MessageControl._MyMessageControl = this;
		}
	}

	public void MessageControlParaInit()
	{
		this.m_MessageControlMutex = new Mutex();
		this.m_MsgQueue = new Queue();
	}

	public void AddMessage(Hashtable myTable)
	{
		this.m_MessageControlMutex.WaitOne();
		this.m_MsgQueue.Enqueue(myTable);
		this.m_MessageControlMutex.ReleaseMutex();
	}

	public bool PostMessage(ref Hashtable myTable)
	{
		this.m_MessageControlMutex.WaitOne();
		if (this.m_MsgQueue.Count <= 0)
		{
			this.m_MessageControlMutex.ReleaseMutex();
			return false;
		}
		myTable = (Hashtable)this.m_MsgQueue.Dequeue();
		this.m_MessageControlMutex.ReleaseMutex();
		return true;
	}
}
