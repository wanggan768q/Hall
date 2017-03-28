using UnityEngine;
using System.Collections;
using LitJson;
using System;

public class TempTestWWW : MonoBehaviour {

    public static string url = "http://47.91.142.202:8200/auth/verfiy/";

    public static WWW www;

    private string username="ggggg";
    private string deviceid="zzzz";
    // Use this for initialization
    void Start () {
       // 1c659919062866fd96cb0a9466267cdaa1ac3536
         deviceid = SystemInfo.deviceUniqueIdentifier;
        StartCoroutine( LoginRequest(username, deviceid));
       // StartCoroutine(LoginFuc(username, password));
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public  IEnumerator LoginRequest(string userid, string deviceid)
    {
        string testUrl = "http://47.90.125.190:8080/auth/verfiy/" + userid + "/" + deviceid;// 21213 / 12312";
        WWW www = new WWW(testUrl);
        yield return www;
        Debug.Log("postrequest 3");
        if (www.isDone)
        {
            Debug.Log("LoginRequest"+www.text);
            if (www.text == null||www.text=="null")
            {
               
                StartCoroutine(LoginFuc(userid, deviceid));
            }
            else
            {
                if (www.error != null)
                {
                    Debug.Log("error code = " + www.error);
                }
                else
                {
                    Debug.Log("content = " + www.text);
                    JsonData jsonData = JsonMapper.ToObject(www.text);
                    if (jsonData == null)
                    {
                        Debug.LogError("LoginRequest jsonData == null");
                        yield break;
                    }
                    if (www.text.Contains("\"deviceId\":"))
                    {
                        Debug.Log("deviceId: " + jsonData["deviceId"]);
                        string getdeviceid = jsonData["deviceId"].ToString();
                        if (getdeviceid == deviceid)
                        {
                            StartCoroutine(LoginFuc(userid, deviceid));
                        }

                    }
                }
            }
            
        }

        
        Debug.Log("postrequest 4");
    }
    private IEnumerator LoginFuc(string userid, string deviceid)
    {
        string testUrl = "http://47.90.125.190:8080/auth/record/" + userid + "/"+ deviceid;
        WWW www = new WWW(testUrl);
        yield return www;
        Debug.Log("postrequest 3");
        if (www.isDone)
        {
            Debug.Log("LoginFuc:"+www.text);
           // StartCoroutine(UserCancel(username, password));
        }
    }
    private IEnumerator UserCancel(string userid, string deviceid)
    {
        string testUrl = "http://47.90.125.190:8080/auth/cancel/" + userid + "/" + deviceid;
        WWW www = new WWW(testUrl);
        yield return www;
        Debug.Log("postrequest 3");
        if (www.isDone)
        {
            Debug.Log("UserCancel:" + www.text);
        }
        if (www.error != null)
        {
            Debug.LogError(www.error);
        }

    }
    private void OnGUI()
    {
        if (GUILayout.Button("1"))
        {
            StartCoroutine(UserCancel(username, deviceid));
        }
    }

}
