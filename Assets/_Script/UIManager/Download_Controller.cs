using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;

public class Download_Controller : Windows<Download_Controller>
{
    private Button mOff_Button;
    private Text mTextFX;
    private Text mSaomiao;
    private Text mXiazai;
    private Button mReplicate;

    private static Download_Controller D_single = null;
    public static Download_Controller getSingle()
    {
        if (D_single == null)
        {
            return new Download_Controller();
        }
        else
        {
            return D_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Download";
        }
    }
    protected override bool OnOpen()
    {
        D_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    private void Init()
    {
        mOff_Button = Control("Off_Button").GetComponent<Button>();
        mTextFX = Control("TextFX").GetComponent<Text>();
        mSaomiao = Control("Saomiao").GetComponent<Text>();
        mXiazai = Control("Xiazai").GetComponent<Text>();
        mReplicate = Control("Replicate").GetComponent<Button>();

        mOff_Button.onClick.AddListener(onOff_Button);
        mReplicate.onClick.AddListener(onReplicate);
    }
    private void onOff_Button()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    private void onReplicate()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    public static implicit operator Download_Controller(SoundLocate_Controller v)
    {
        throw new NotImplementedException();
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mTextFX.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(41).ChineseContent;
            mSaomiao.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(42).ChineseContent;
            mXiazai.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(43).ChineseContent;
            mReplicate.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(44).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mTextFX.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(41).EnglishContent;
            mSaomiao.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(42).EnglishContent;
            mXiazai.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(43).EnglishContent;
            mReplicate.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(44).EnglishContent;
        }
    }
}
