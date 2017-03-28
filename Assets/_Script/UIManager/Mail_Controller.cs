using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;

public class Mail_Controller : Windows<Mail_Controller>
{
    private Button mReturnButtor;
    private Button mSelect_Button;
    private Button mDelete_Button;
    private Image mYoujian;
    private Text mTextMYYJ;

    private static Mail_Controller M_single = null;
    public static Mail_Controller getSingle()
    {
        if (M_single == null)
        {
            return new Mail_Controller();
        }
        else
        {
            return M_single;
        }
    }

    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Mail";
        }
    }
    protected override bool OnOpen()
    {
        M_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    public void Init()
    {
        mReturnButtor = Control("ReturnButton").GetComponent<Button>();
        mSelect_Button = Control("Select_Button").GetComponent<Button>();
        mDelete_Button = Control("Delete_Button").GetComponent<Button>();
        mYoujian = Control("Youjian").GetComponent<Image>();
        mTextMYYJ = Control("TextMYYJ").GetComponent<Text>();

        AddEventListener();
    }
    private void AddEventListener()
    {
        mReturnButtor.onClick.AddListener(onReturnButton);
        mSelect_Button.onClick.AddListener(onSelect_Button);
        mDelete_Button.onClick.AddListener(onDelete_Button);
    }

    private void RemoveEventListener()
    {
        mReturnButtor.onClick.RemoveListener(onReturnButton);
        mSelect_Button.onClick.AddListener(onSelect_Button);
        mDelete_Button.onClick.AddListener(onDelete_Button);
    }
    private void onReturnButton()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    private void onSelect_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onDelete_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        CommonMessageUIController.Instance.Open();
        if (Globle.LanType == 0)
        {
            CommonMessageUIController.Getsingle().ShowText("请请选择要删除的邮件", 1);
        }
        else if (Globle.LanType == 1)
        {
            CommonMessageUIController.Getsingle().ShowText("Please select the mails to be deleted", 1);
        }
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mYoujian.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(59).ChineseContent);
            mTextMYYJ.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(60).ChineseContent;
            mSelect_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(61).ChineseContent;
            mDelete_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(62).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mYoujian.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(59).EnglishContent);
            mTextMYYJ.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(60).EnglishContent;
            mSelect_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(61).EnglishContent;
            mDelete_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(62).EnglishContent;
        }

    }
    private Sprite loadSprite(string spriteName)
    {
        return Resources.Load<GameObject>("Prefabs/UISprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
    }
}
