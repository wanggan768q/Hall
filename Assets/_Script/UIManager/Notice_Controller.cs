using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;

public class Notice_Controller : Windows<Notice_Controller>
{
    private Button mCloseButton;

    private Transform mNoticeImage;
    private Text mNoticeText;

    private static Notice_Controller N_single = null;
    public static Notice_Controller getSingle()
    {
        if (N_single == null)
        {
            return new Notice_Controller();
        }
        else
        {
            return N_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Notice";
        }
    }
    protected override bool OnOpen()
    {
        N_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
       
    }
    private void Init()
    {
        mNoticeImage = Control("NoticeImage").GetComponent<Transform>();
        mNoticeText = mNoticeImage.FindChild("NoticeText").GetComponent<Text>();
        mCloseButton = Control("CloseButton").GetComponent<Button>();
        mCloseButton.onClick.AddListener(onCloseButton);
    }
    private void onCloseButton()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Hide();
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mNoticeText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(29).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mNoticeText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(29).EnglishContent;
        }


            
        else if (Globle.LanType == 2)
        {
            mNoticeText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(29).EnglishContent;
        }


    }
}