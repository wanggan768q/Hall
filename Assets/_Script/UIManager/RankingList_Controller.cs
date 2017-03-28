using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;


public class RankingList_Controller : Windows<RankingList_Controller>
{
    private Button mReturnButton;
    private Image mPaihangbang;
    private Toggle mXYLS_Button;
    private Toggle mYQS_Button;
    private Toggle mDT_Button;
    private Toggle mNMW_Button;
    private Toggle mQQMRY_Button;
    private Toggle mQYM_Button;
    private Toggle mSHZ_Button;
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
        mXYLS_Button = Control("XYLS_Button").GetComponent<Toggle>();
        mYQS_Button = Control("YQS_Button").GetComponent<Toggle>();
        mDT_Button = Control("DT_Button").GetComponent<Toggle>();
        mNMW_Button = Control("NMW_Button").GetComponent<Toggle>();
        mQQMRY_Button = Control("QQMRY_Button").GetComponent<Toggle>();
        mQYM_Button = Control("QYM_Button").GetComponent<Toggle>();
        mSHZ_Button = Control("SHZ_Button").GetComponent<Toggle>();
        mDayRanking_Button = Control("DayRanking_Button").GetComponent<Toggle>();
        mZhouRanking_Button = Control("ZhouRanking_Button").GetComponent<Toggle>();
        mTotalRanking_Button = Control("TotalRanking_Button").GetComponent<Toggle>();



        AddEventListener();
    }
    private void AddEventListener()
    {
        mReturnButton.onClick.AddListener(onReturnButton);
      


    }



    private void RemoveEventListener()
    {
        mReturnButton.onClick.RemoveListener(onReturnButton);



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
            //mCoin.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(66).ChineseContent;
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
            //mCoin.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(66).ChineseContent;
        }

    }
    private Sprite loadSprite(string spriteName)
    {
        return Resources.Load<GameObject>("Prefabs/UISprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
    }
}
