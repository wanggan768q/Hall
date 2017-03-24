using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WindowStyle
{
    WS_Normal,
    WS_Ext,
    WS_CullingMask,
}
public abstract class Windows<T> where T : class, new()
{
    static T msInstance = null;
    static int mCullingMask = 0;
    public static T Instance { get { return msInstance ?? (msInstance = new T()); } }
    public static bool Exist { get { return msInstance != null; } }

    GameObject mCamera = null;
    GameObject mRootUI = null;
    GameObject mWndObject = null;
    GameObject mExtBackground = null;

    public abstract string PrefabName { get; }
    public GameObject WndObject { get { return mWndObject; } }
    protected virtual bool OnOpen() { return true; }
    protected virtual bool OnClose() { return true; }
    protected virtual bool OnShow() { return true; }
    protected virtual bool OnHide() { return true; }
    protected virtual bool OnUpdate() { return true; }

    //public List<string> mPanelName = new List<string> {
    //    "Prefabs/UIPrefab/Lobby/MainViewPanel","Prefabs/UIPrefab/Lobby/CommonBar"
    //};
    public WindowStyle mWindowStyle = WindowStyle.WS_Normal;
    public WindowStyle WinStyle { get { return mWindowStyle; } set { mWindowStyle = value; } }

    public GameObject Control(string name)
    {
        if (msInstance == null)
            return null;

        return Control(name, mWndObject);
    }

    public GameObject Control(string name, GameObject parent)
    {
        if (msInstance == null)
            return null;

        Transform[] children = parent.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.name == name)
                return child.gameObject;
        }
        return null;
    }

    public void Open()
    {
        if (mWndObject)
        {
            GameObject.Destroy(mWndObject);
            if (WindowStyle.WS_Ext <= mWindowStyle)
                GameObject.Destroy(mExtBackground);
        }
        mWndObject = GameObject.Instantiate(Resources.Load(PrefabName)) as GameObject;
        Debug.Log("||||||||||||||||||||||||PERR::"+PrefabName);
        mRootUI = GameObject.Find("GameUILayer");
        if (mRootUI == null)
            mRootUI = GameObject.Find("Canvas");
        WndObject.transform.parent = mRootUI.transform;
        //WndObject.transform.localPosition = Vector3.zero;
        if (PrefabName == "Prefabs/UIPrefab/Lobby/MainViewPanel" || PrefabName == "Prefabs/UIPrefab/Lobby/CommonBar" || PrefabName == "Prefabs/UIPrefab/Lobby/RoleInfopanel"||PrefabName== "Prefabs/UIPrefab/Lobby/LoginViewPanel")
        {
            WndObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            WndObject.transform.localPosition = new Vector3(0, 0, -560);            
        }
        WndObject.transform.localScale = Vector3.one;
        if (PrefabName == "Prefabs/UIPrefab/Lobby/CommonBar")
        {
            WndObject.transform.SetSiblingIndex(WndObject.transform.GetSiblingIndex());
        }
        OnOpen();

        mCamera = GameObject.Find("Camera-CloseUp");
        if (mCamera != null)
            mCullingMask = mCamera.GetComponent<Camera>().cullingMask;

        if (WindowStyle.WS_Ext <= mWindowStyle)
        {
            mExtBackground = GameObject.Instantiate(Resources.Load("BackgroundExtWnd")) as GameObject;
            mExtBackground.transform.parent = mRootUI.transform;
            mExtBackground.transform.localPosition = Vector3.zero + new Vector3(0.0f, 0.0f, 800.0f);
            mExtBackground.transform.localScale = Vector3.one + new Vector3(1500.0f, 1500.0f, 1.0f);
        }

        if (WindowStyle.WS_CullingMask <= mWindowStyle)
        {
            if (mCamera != null)
                mCamera.GetComponent<Camera>().cullingMask = LayerMask.NameToLayer("NGUI");
        }
    }

    public void Close()
    {
        OnClose();
        if (mWndObject != null)
            GameObject.Destroy(mWndObject);

        if (WindowStyle.WS_Ext <= mWindowStyle
            && mExtBackground != null)
            GameObject.Destroy(mExtBackground);

        if (WindowStyle.WS_CullingMask <= mWindowStyle)
            mCamera.GetComponent<Camera>().cullingMask = mCullingMask;

        msInstance = null;
    }

    public virtual void Show()
    {
        if (Exist)
        {
            WndObject.SetActive(true);
            if (WindowStyle.WS_Ext <= mWindowStyle)
                mExtBackground.SetActive(true);
        }
        OnShow();
    }
    void Update()
    {
        OnUpdate();
    }
    public virtual void Hide()
    {
        if (Exist)
        {
            WndObject.SetActive(false);
            if (WindowStyle.WS_Ext <= mWindowStyle)
                mExtBackground.SetActive(false);
        }
        OnHide();
    }
    void OnClickedTimeOutQuit(GameObject go)
    {
        Application.Quit();
    }
    public delegate void FinishFuction();
    IEnumerator waitTodoFunc(float time, FinishFuction function)
    {
        yield return new WaitForSeconds(time);

        if (function != null)
        {
            function();
        }
    }
    //public void Request(RequestCmd request, Client.OnResponse callback)
    //{
    //    MainScript.Instance.Request(request, delegate (string err, Response response)
    //    {
    //        if (!mWndObject)
    //            return;

    //        if (string.Compare(err, "Time out") == 0)
    //            Global.ShowLoadingEnd();

    //        callback(err, response);
    //    });
    //}

}

