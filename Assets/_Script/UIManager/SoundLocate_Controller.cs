using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DateDeclare;

public class SoundLocate_Controller : Windows<SoundLocate_Controller>
{
    private Button mAbout;
    private Button mDefinite;

    private Text mTextSZ;
    private Text mGameSound;
    private Text mBackdropMusic;
    

    private static SoundLocate_Controller S_single = null;
    public static SoundLocate_Controller getSingle()
    {
        if (S_single == null)
        {
            return new SoundLocate_Controller();
        }
        else
        {
            return S_single;
        }
    }
    public override string PrefabName
    {
        get
        {
            return "Prefabs/UIPrefab/Panel_SoundLocate";
        }
    }
    
    protected override bool OnOpen()
    {
        S_single = this;
        Init();
        CheckLanguages();
        return base.OnOpen();
    }
    public void Init()
    {
        mAbout = Control("About").GetComponent<Button>();
        mDefinite = Control("Definite").GetComponent<Button>();
        mTextSZ = Control("TextSZ").GetComponent<Text>();
        mGameSound = Control("GameSound").GetComponent<Text>();
        mBackdropMusic = Control("BackdropMusic").GetComponent<Text>();

        AddEventListener();
    }
    private void AddEventListener()
    {
        mAbout.onClick.AddListener(onAbout);
        mDefinite.onClick.AddListener(onDefinite);
    }
    private void RemoveEventListener()
    {
        mAbout.onClick.RemoveListener(onAbout);
        mDefinite.onClick.RemoveListener(onDefinite);
    }
    private void onDefinite()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    private void onAbout()
    {
        //播放按钮音效
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        if (!Anenst_Controller.Exist)
        {
            Anenst_Controller.Instance.Open();
        }
        else
        {
            Anenst_Controller.Instance.Show();
        }
    }
    public void CheckLanguages()
    {
        if (Globle.LanType == 0)
        {
            mTextSZ.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(50).ChineseContent;
            mGameSound.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(51).ChineseContent;
            mBackdropMusic.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(52).ChineseContent;
            mAbout.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(53).ChineseContent;
            mDefinite.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(54).ChineseContent;
        }
        else if (Globle.LanType == 1)
        {
            mTextSZ.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(50).EnglishContent;
            mGameSound.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(51).EnglishContent;
            mBackdropMusic.GetComponent<Text>().text = Languages_Manager.Instance.GetItem(52).EnglishContent;
            mAbout.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(53).EnglishContent;
            mDefinite.transform.FindChild("Text").GetComponent<Text>().text = Languages_Manager.Instance.GetItem(54).EnglishContent;
            
        }

    }


}
