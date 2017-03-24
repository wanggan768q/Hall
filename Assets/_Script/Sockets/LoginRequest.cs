using UnityEngine;
using System.Collections;
using LitJson;

public class LoginRequest : MonoBehaviour {

    private static LoginRequest mLoginRequest;
    public static LoginRequest GetSingleton()
    {
        return LoginRequest.mLoginRequest;
    }
    private string deviceid;
    private string mUserid;
    private string mPassword;
    private string mVersion;
    private void Awake()
    {
        if (LoginRequest.mLoginRequest == null)
        {
            Debug.Log("mLoginRequest");
            LoginRequest.mLoginRequest = this;
        }
        deviceid = SystemInfo.deviceUniqueIdentifier;
    }
    public void StartRequest(string userid,string passwrod,string version)
    {
        mUserid = userid;
        mPassword = passwrod;
        mVersion = version;
        StartCoroutine(LoginRequestFuc(userid, deviceid));
    }
    public void StartCancle()
    {
        StartCoroutine(UserCancel(mUserid, deviceid));
    }
    public  IEnumerator LoginRequestFuc(string userid, string deviceid)
    {
        string testUrl = "http://47.90.125.190:8080/auth/verfiy/" + userid + "/" + deviceid;// 21213 / 12312";
        WWW www = new WWW(testUrl);
        yield return www;
        Debug.Log("postrequest 3");
        if (www.isDone)
        {
            Debug.Log("LoginRequest" + www.text);
            if (www.text == null || www.text == "null")
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
                        else
                        {
                            CommonMessageUIController.Instance.Open();
                            CommonMessageUIController.Instance.ShowText("此用户以登录，请勿重复登录", 1);
                            CommonMessageUIController.Instance.OnClickedOk = RequestFailure;
                        }

                    }
                }
            }

        }


        Debug.Log("postrequest 4");
    }
    private IEnumerator LoginFuc(string userid, string deviceid)
    {
        string testUrl = "http://47.90.125.190:8080/auth/record/" + userid + "/" + deviceid;
        WWW www = new WWW(testUrl);
        yield return www;
        Debug.Log("postrequest 3");
        if (www.isDone)
        {
            Debug.Log("LoginFuc:" + www.text);
            GameManager.Getsingle().CheckDeviceid = true;
            GameManager.Getsingle().LoginFuc(mUserid,mPassword, "1.0.0");
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
    private void RequestFailure()
    {
        CommonMessageUIController.Instance.Close();

    }
}
