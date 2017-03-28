using System;
using System.Collections;
using UnityEngine;

public class PostMessageThread : MonoBehaviour
{
	private static PostMessageThread _MyThread;

	private bool m_flag;

	private Hashtable m_MyTable;

	private MessageControl m_MyMessageControl;

	private Transmit m_MyTransmit;

	public int num;

	public static PostMessageThread GetSingleton()
	{
		return PostMessageThread._MyThread;
	}

	private void Awake()
	{
		if (PostMessageThread._MyThread == null)
		{
			Debug.Log("_MyThread");
			PostMessageThread._MyThread = this;
		}
	}

	public void PostMessageThreadParaInit()
	{
		this.m_flag = false;
		this.m_MyTable = new Hashtable();
	}

	public void PostMessageThreadGetPoint(MessageControl MyMessageControl, Transmit MyTransmit)
	{
		this.m_MyMessageControl = MyMessageControl;
		this.m_MyTransmit = MyTransmit;
	}

	public void PostThread()
	{
		int num = 0;
        if (this.m_MyMessageControl != null)
        {
            while (this.m_MyMessageControl.PostMessage(ref this.m_MyTable) && num < 11)
            {
                this.num++;
                this.m_MyTransmit.PostMsgControl(this.m_MyTable);
                num++;            
            }
            Debug.Log("num" + this.num);
        }
        else
        {
            Debug.Log("this is null");
        }
    }
}
