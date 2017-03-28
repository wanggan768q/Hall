using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;

public class Anenst_Controller : Windows<Anenst_Controller>
{
    private Button mCloseButton;
    private Text mNoticeText;

    private static Anenst_Controller A_single = null;
    public static Anenst_Controller getSingle()
    {
        if (A_single == null)
        {
            return new Anenst_Controller();
        }
        else
        {
            return A_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Anenst";
        }
    }
    protected override bool OnOpen()
    {
        A_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    private void Init()
    {
        mCloseButton = Control("CloseButton").GetComponent<Button>();
        mNoticeText = Control("NoticeText").GetComponent<Text>();

        mCloseButton.onClick.AddListener(onCloseButton);
    }
    private void onCloseButton()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mNoticeText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(53).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mNoticeText.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(53).EnglishContent;
        }
    }
}
