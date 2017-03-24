using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;

public class CommonMessageUIController : Windows<CommonMessageUIController> {

    public override string PrefabName
    {
        get { return "Prefabs/UIPrefab/Panel_tips"; } 
    }
    private static CommonMessageUIController m_single = null;
    public static CommonMessageUIController Getsingle()
    {
        if (m_single == null)
            return new CommonMessageUIController();
        return m_single;
    }
    public delegate void OnClickOK();
    public delegate void OnClickCancle();
    public OnClickOK OnClickedOk;
    public OnClickCancle OnClickedCancle;
    private Text infoText;
    private Button btnConfirm;
    private Button btnCancel;
    protected override bool OnOpen()
    {
        Init();
        return base.OnOpen();
    }
    public void Init()
    {
        m_single = this;
        infoText = Control("InfoText").GetComponent<Text>();
        btnConfirm= Control("Button_quit").GetComponent<Button>();
        btnCancel = Control("Button_quxiao").GetComponent<Button>();
        btnConfirm.onClick.AddListener(OnClickIbtnSure);
        btnCancel.onClick.AddListener(OnClickIbtnCancel);
    }
    /// <summary>
    /// 显示提示文字
    /// </summary>
    /// <param name="id"></param>
    public void Show(int id)
    {
        infoText.text = Languages_Manager.Instance.GetItem(id).ChineseContent;
    }
    public void ShowText(string mText,int mType)
    {
        infoText.text = mText;
        switch (mType)
        {
            case 1:
                if(Globle.LanType==0)
                {
                    btnConfirm.transform.Find("Text").GetComponent<Text>().text = "确定";
                }
                else if(Globle.LanType == 1)
                {
                    btnConfirm.transform.Find("Text").GetComponent<Text>().text = "OK";
                }
                
                btnConfirm.gameObject.transform.localPosition = new Vector3(0, -59, 0);
                btnCancel.gameObject.SetActive(false);
                break;
            case 2:
                if (Globle.LanType == 0)
                {
                    btnConfirm.transform.Find("Text").GetComponent<Text>().text = "退出";
                    btnCancel.transform.Find("Text").GetComponent<Text>().text = "取消";
                    
                }
                else if (Globle.LanType == 1)
                {
                    btnConfirm.transform.Find("Text").GetComponent<Text>().text = "Confirm";
                    btnCancel.transform.Find("Text").GetComponent<Text>().text = "Cancel";
                }
                SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
                break;
            case 3:
                if (Globle.LanType == 0)
                {
                    btnConfirm.transform.Find("Text").GetComponent<Text>().text = "退出";
                    btnCancel.transform.Find("Text").GetComponent<Text>().text = "切换账户";
                }
                else if (Globle.LanType == 1)
                {
                    btnConfirm.transform.Find("Text").GetComponent<Text>().text = "Quit";
                    btnCancel.transform.Find("Text").GetComponent<Text>().text = "SwitchUser";
                }
                SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
                break;
            case 4:
                if (Globle.LanType == 0)
                {
                    btnConfirm.transform.Find("Text").GetComponent<Text>().text = "确认";
                    btnCancel.transform.Find("Text").GetComponent<Text>().text = "取消";
                }
                else if (Globle.LanType == 1)
                {
                    btnConfirm.transform.Find("Text").GetComponent<Text>().text = "OK";
                    btnCancel.transform.Find("Text").GetComponent<Text>().text = "Cancel";
                }
                SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
                break;
            default:
                break;
        }        
    }

    public void ShowOnLyConfirm()
    {
        btnConfirm.gameObject.transform.localPosition = new Vector3(0,-59,0);
        btnCancel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 移除监听事件
    /// </summary>
    public void RemoveAllListener()
    {
        btnConfirm.onClick.RemoveListener(OnClickIbtnSure);
        btnCancel.onClick.RemoveListener(OnClickIbtnCancel);
    }
    private void OnClickIbtnSure()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (OnClickedOk != null)
        {
            OnClickedOk();
            OnClickedOk = null;
        }
        Close();
    }
    private void OnClickIbtnCancel()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (OnClickedCancle != null)
        {
            OnClickedCancle();
            OnClickedCancle = null;
        }
        Close();
    }
}
