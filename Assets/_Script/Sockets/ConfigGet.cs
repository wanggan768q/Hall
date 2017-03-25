using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class ConfigGet : MonoBehaviour
{
	public static string url = "http://" + Constants.IP + ":8080/game/Version.txt";

	public static WWW www;

	public static bool ConfigGetFlag = false;

	public static bool ConfigFirstGetFlag = true;

	public static string[] ConfigData = new string[6];

	public static int[,] GameConfigData;

	private string text;

	private void Awake()
	{
		ConfigGet.WWWGet();
	}

	private void Start()
	{
		ConfigGet.GameConfigData = new int[6, 3];
	}

	public static void WWWGet()
	{
		ConfigGet.url = "http://" + "183.2.246.50" + ":8080/game/Version.txt";
		ConfigGet.www = new WWW(ConfigGet.url);
	}

	private void Update()
	{
		if (ConfigGet.www == null || ConfigGet.ConfigGetFlag)
		{
			return;
		}
		if ((Time.frameCount % 500 == 0 || ConfigGet.ConfigFirstGetFlag) && !ConfigGet.ConfigGetFlag && ConfigGet.www.isDone)
		{
			ConfigGet.ConfigFirstGetFlag = false;
			this.text = ConfigGet.www.text;
			if (!string.IsNullOrEmpty(this.text))
			{
				RSA rSA_ = new RSA();
				this.text = rSA_.Decrypt(this.text);
				this.StringSplit(this.text, "\\b\\#", ConfigGet.ConfigData);
				this.StringSplitToInt(ConfigGet.ConfigData[5]);
				ConfigGet.ConfigGetFlag = true;
                GameManager.Getsingle().HallVersion = ConfigData[0];
                Debug.Log(www.text);
			}
          
            for (int i = 0; i < ConfigData.Length; i++)
            {
                Debug.Log("config" + ConfigData[i]);
            }
		}
	}

	private void StringSplit(string strInput, string pattern, string[] strOutput)
	{
		int num = 0;
		string[] array = Regex.Split(strInput, pattern);
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			if (!string.IsNullOrEmpty(text))
			{
				strOutput[num++] = text;
			}
		}
	}

	private void StringSplitToInt(string strInput)
	{
		int num = 0;
		int num2 = 0;
		string pattern = "\\b\\,";
		string[] array = Regex.Split(strInput, pattern);
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			if (!string.IsNullOrEmpty(text))
			{
				ConfigGet.GameConfigData[num, num2++] = int.Parse(text);
				if (num2 == 3)
				{
					num++;
					num2 = 0;
				}
			}
		}
	}
}
