using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;

public class GuestLog_Controller : Windows<GuestLog_Controller>
{
    private Button mNameBgButton;
    private Button mYXButton;
    private Button mTYButton;
    private Button mXinyunliushi;
    private Button mYaoqianshu;
    private Button mDantiao;
    private Button mNiumowang;
    private Button mQQmeirenyu;
    private Button mQueyimen;
    private Button mShuihuzhuan;
    private Button mGonggao;
    private Button mShare;
    private Button mSetting;
    private Button mRank;
    private Button mMail;

    private Transform mTopPanel;


    private static GuestLog_Controller G_single = null;
    public static GuestLog_Controller getSingle()
    {
        if (G_single == null)
        {
            return new GuestLog_Controller();
        }
        else
        {
            return G_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/GuestLogPanel";
        }
    }
    protected override bool OnOpen()
    {
        G_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    public void Init()
    {
        
        mTopPanel = Control("TopPanel").transform;
        //找到TopPanel下的NameBgButton
        mNameBgButton = mTopPanel.FindChild("NameBgButton").GetComponent<Button>();

        mNameBgButton = Control("NameBgButton").GetComponent<Button>();
        mYXButton = Control("YXButton").GetComponent<Button>();
        mTYButton = Control("TYButton").GetComponent<Button>();
        mXinyunliushi = Control("xinyunliushi").GetComponent<Button>();
        mYaoqianshu = Control("yaoqianshu").GetComponent<Button>();
        mDantiao = Control("dantiao").GetComponent<Button>();
        mNiumowang = Control("niumowang").GetComponent<Button>();
        mQQmeirenyu = Control("QQmeirenyu").GetComponent<Button>();
        mQueyimen = Control("queyimen").GetComponent<Button>();
        mShuihuzhuan = Control("Shuihuzhuan").GetComponent<Button>();
        mGonggao = Control("Gonggao").GetComponent<Button>();
        mShare = Control("Share").GetComponent<Button>();
        mSetting = Control("Setting").GetComponent<Button>();
        mRank = Control("Rank").GetComponent<Button>();
        mMail = Control("Mail").GetComponent<Button>();
        AddEventListener();
    }

    private void AddEventListener()
    {
        mNameBgButton.onClick.AddListener(onUserlocate);
        mYXButton.onClick.AddListener(onYXButton);
        mTYButton.onClick.AddListener(onTYButton);
        mXinyunliushi.onClick.AddListener(onXinyunliushi);
        mYaoqianshu.onClick.AddListener(onYaoqianshu);
        mDantiao.onClick.AddListener(onDantiao);
        mNiumowang.onClick.AddListener(onNiumowang);
        mQQmeirenyu.onClick.AddListener(onQQmeirenyu);
        mQueyimen.onClick.AddListener(onQueyimen);
        mShuihuzhuan.onClick.AddListener(onShuihuzhuan);

        mGonggao.onClick.AddListener(onGonggao);
        mShare.onClick.AddListener(onShare);
        mSetting.onClick.AddListener(onSetting);
        mRank.onClick.AddListener(onRank);
        mMail.onClick.AddListener(onMail);
    }

    private void RemoveEventListener()
    {
        mNameBgButton.onClick.RemoveListener(onUserlocate);
        mYXButton.onClick.RemoveListener(onYXButton);
        mTYButton.onClick.RemoveListener(onTYButton);
        mXinyunliushi.onClick.RemoveListener(onXinyunliushi);
        mYaoqianshu.onClick.RemoveListener(onYaoqianshu);
        mDantiao.onClick.RemoveListener(onDantiao);
        mNiumowang.onClick.RemoveListener(onNiumowang);
        mQQmeirenyu.onClick.RemoveListener(onQQmeirenyu);
        mQueyimen.onClick.RemoveListener(onQueyimen);
        mShuihuzhuan.onClick.RemoveListener(onShuihuzhuan);

        mGonggao.onClick.RemoveListener(onGonggao);
        mShare.onClick.RemoveListener(onShare);
        mSetting.onClick.RemoveListener(onSetting);
        mRank.onClick.RemoveListener(onRank);
        mMail.onClick.RemoveListener(onMail);


    }
    /// <summary>
    /// 信息窗口
    /// </summary>
    private void onUserlocate()
    {
        if (!Userlocate_Controller.Exist)
        {
            Userlocate_Controller.Instance.Open();
        }
        else
        {
            Userlocate_Controller.Instance.Show();
        }
        
    }
    private void onYXButton()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        CommonMessageUIController.Instance.Open();
        if (Globle.LanType == 0)
        {
            CommonMessageUIController.Getsingle().ShowText("请直接到前台充值/兑换，给您带来的不便请谅解", 1);
        }
        else if (Globle.LanType == 1)
        {   
            CommonMessageUIController.Getsingle().ShowText("Please go directly to the front desk to recharge or exchange", 1);
        }
    }
    private void onTYButton()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        CommonMessageUIController.Instance.Open();
        if (Globle.LanType == 0)
        {
            CommonMessageUIController.Getsingle().ShowText("体验币低于10000可申请，每次申请不足10000币", 4);
        }
        else if (Globle.LanType == 1)
        {
            CommonMessageUIController.Getsingle().ShowText("Tickets less than 10,000 may apply for each application make up 10,000 tickets", 4);
        }
       
        //CommonMessageUIController.Instance.OnClickedOk = onClose;

    }
    private void onXinyunliushi()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        GameManager.Getsingle().startFish(0);
    }
    private void onYaoqianshu()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        GameManager.Getsingle().startFish(1);
    }
    private void onDantiao()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        GameManager.Getsingle().startFish(2);
    }
    private void onNiumowang()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        GameManager.Getsingle().startFish(3);
    }
    private void onQQmeirenyu()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        GameManager.Getsingle().startFish(4);
    }
    private void onQueyimen()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        GameManager.Getsingle().startFish(5);
    }
    private void onShuihuzhuan()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        GameManager.Getsingle().startFish(6);
    }
    /// <summary>
    /// 公告
    /// </summary>
    private void onGonggao()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!Notice_Controller.Exist)
        {
            Notice_Controller.Instance.Open();
        }
        else
        {
            Notice_Controller.Instance.Show();
        }
    }
    /// <summary>
    /// 分享
    /// </summary>
    private void onShare()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!Download_Controller.Exist)
        {
            Download_Controller.Instance.Open();
        }
        else
        {
            Download_Controller.Instance.Show();
        }
    }
    /// <summary>
    /// 设置
    /// </summary>
    private void onSetting()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!SoundLocate_Controller.Exist)
        {
            SoundLocate_Controller.Instance.Open();
        }
        else
        {
            SoundLocate_Controller.Instance.Show();
        }
    }
    /// <summary>
    /// 排行榜
    /// </summary>
    private void onRank()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!RankingList_Controller.Exist)
        {
            
            RankingList_Controller.Instance.Open();
        }
        else
        {
            RankingList_Controller.Instance.Show();

        }
    }
    /// <summary>
    /// 邮箱
    /// </summary>
    private void onMail()
    {
        //播放音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!Mail_Controller.Exist)
        {
            Mail_Controller.Instance.Open();
        }
        else
        {
            Mail_Controller.Instance.Show();
        }
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mXinyunliushi.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(23).ChineseContent;
            mYaoqianshu.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(24).ChineseContent;
            mDantiao.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(25).ChineseContent;
            mNiumowang.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(26).ChineseContent;
            mQQmeirenyu.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(27).ChineseContent;
            mQueyimen.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(28).ChineseContent;
            mShuihuzhuan.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(65).ChineseContent;
            mGonggao.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(45).ChineseContent);
            mShare.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(46).ChineseContent);
            mSetting.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(47).ChineseContent);
            mRank.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(48).ChineseContent);
            mMail.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(49).ChineseContent);
        }
        else if (Globle.LanType == 1)
        {
            mXinyunliushi.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(23).EnglishContent;
            mYaoqianshu.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(24).EnglishContent;
            mDantiao.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(25).EnglishContent;
            mNiumowang.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(26).EnglishContent;
            mQQmeirenyu.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(27).EnglishContent;
            mQueyimen.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(28).EnglishContent;
            mShuihuzhuan.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(65).EnglishContent;
            mGonggao.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(45).EnglishContent);
            mShare.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(46).EnglishContent);
            mSetting.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(47).EnglishContent);
            mRank.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(48).EnglishContent);
            mMail.GetComponent<Image>().sprite = loadSprite(Languages_Manager.Instance.GetItem(49).EnglishContent);
        }
    
    }
    private Sprite loadSprite(string spriteName)
    {
        return Resources.Load<GameObject>("Prefabs/UISprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
    }

}
