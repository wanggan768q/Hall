using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;


public class RankingList_Controller : Windows<RankingList_Controller>
{
    private Button mReturnButton;
    private Image mPaihangbang;
    private Button mXYLS_Button;
    private Button mYQS_Button;
    private Button mDT_Button;
    private Button mNMW_Button;
    private Button mQQMRY_Button;
    private Button mQYM_Button;
    private Button mSHZ_Button;
    private Toggle mDayRanking_Button;
    private Toggle mZhouRanking_Button;
    private Toggle mTotalRanking_Button;
    


    private static RankingList_Controller R_single = null;
    public static RankingList_Controller getSingle()
    {
        if (R_single == null)
        {
            return new RankingList_Controller();
        }
        else
        {
            return R_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_Ranking List";
        }
    }
    protected override bool OnOpen()
    {
        R_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    public void Init()
    {
        mReturnButton = Control("ReturnButton").GetComponent<Button>();
        mPaihangbang = Control("Paihangbang").GetComponent<Image>();
        mXYLS_Button = Control("XYLS_Button").GetComponent<Button>();
        mYQS_Button = Control("YQS_Button").GetComponent<Button>();
        mDT_Button = Control("DT_Button").GetComponent<Button>();
        mNMW_Button = Control("NMW_Button").GetComponent<Button>();
        mQQMRY_Button = Control("QQMRY_Button").GetComponent<Button>();
        mQYM_Button = Control("QYM_Button").GetComponent<Button>();
        mSHZ_Button = Control("SHZ_Button").GetComponent<Button>();
        mDayRanking_Button = Control("DayRanking_Button").GetComponent<Toggle>();
        mZhouRanking_Button = Control("ZhouRanking_Button").GetComponent<Toggle>();
        mTotalRanking_Button = Control("TotalRanking_Button").GetComponent<Toggle>();

        AddEventListener();
    }
    private void AddEventListener()
    {
        mReturnButton.onClick.AddListener(onReturnButton);
        mXYLS_Button.onClick.AddListener(onXYLS_Button);
        mYQS_Button.onClick.AddListener(onYQS_Button);
        mDT_Button.onClick.AddListener(onDT_Button);
        mNMW_Button.onClick.AddListener(onNMW_Button);
        mQQMRY_Button.onClick.AddListener(onQQMRY_Button);
        mQYM_Button.onClick.AddListener(onQYM_Button);
        mSHZ_Button.onClick.AddListener(onSHZ_Button);
    }

    private void RemoveEventListener()
    {
        mReturnButton.onClick.RemoveListener(onReturnButton);
        mXYLS_Button.onClick.RemoveListener(onXYLS_Button);
        mYQS_Button.onClick.RemoveListener(onYQS_Button);
        mDT_Button.onClick.RemoveListener(onDT_Button);
        mNMW_Button.onClick.RemoveListener(onNMW_Button);
        mQQMRY_Button.onClick.RemoveListener(onQQMRY_Button);
        mQYM_Button.onClick.RemoveListener(onQYM_Button);
        mSHZ_Button.onClick.RemoveListener(onSHZ_Button);
    }
    private void onReturnButton()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    private void onXYLS_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onYQS_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onDT_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onNMW_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onQQMRY_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onQYM_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onSHZ_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onDayRanking_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onZhouRanking_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
    private void onTotalRanking_Button()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
    }
   





    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mPaihangbang.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(55).ChineseContent);
            mXYLS_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(23).ChineseContent;
            mYQS_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(24).ChineseContent;
            mDT_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(25).ChineseContent;
            mNMW_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(26).ChineseContent;
            mQQMRY_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(27).ChineseContent;
            mQYM_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(28).ChineseContent;
            mSHZ_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(65).ChineseContent;
            mDayRanking_Button.transform.FindChild("RText").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(56).ChineseContent;
            mZhouRanking_Button.transform.FindChild("ZText").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(57).ChineseContent;
            mTotalRanking_Button.transform.FindChild("ZoText").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(58).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mPaihangbang.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(55).EnglishContent);
            mXYLS_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(23).EnglishContent;
            mYQS_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(24).EnglishContent;
            mDT_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(25).EnglishContent;
            mNMW_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(26).EnglishContent;
            mQQMRY_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(27).EnglishContent;
            mQYM_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(28).EnglishContent;
            mSHZ_Button.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(65).EnglishContent;
            mDayRanking_Button.transform.FindChild("RText").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(56).EnglishContent;
            mZhouRanking_Button.transform.FindChild("ZText").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(57).EnglishContent;
            mTotalRanking_Button.transform.FindChild("ZoText").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(58).EnglishContent;
        }

    }
    private Sprite loadSprite(string spriteName)
    {
        return Resources.Load<GameObject>("Prefabs/UISprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
    }
}
