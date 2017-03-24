using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using DateDeclare;

public class AlterPortrait_Panel : Windows<AlterPortrait_Panel>
{
    private Button mPortraitZhu;
    private Button mPortrait;
    private Button mPortrait01;
    private Button mPortrait02;
    private Button mPortrait03;
    private Button mbg;
    private static AlterPortrait_Panel A_single = null;
    public static AlterPortrait_Panel gatSingle()
    {
        if (A_single == null)
        {
            return new AlterPortrait_Panel();
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
            return "Prefabs/UIPrefab/AlterPortrait_Panel";
        }
    }
    protected override bool OnOpen()
    {
        A_single = this;
        Init();
        return base.OnOpen();
    }
    private void Init()
    {
        mPortraitZhu = Control("PortraitZhu").GetComponent<Button>();
        mbg = Control("bg").GetComponent<Button>();
        mPortrait = Control("Portrait").GetComponent<Button>();
        mPortrait01 = Control("Portrait01").GetComponent<Button>();
        mPortrait02 = Control("Portrait02").GetComponent<Button>();
        mPortrait03 = Control("Portrait03").GetComponent<Button>();
        AddEventListener();
    }
    private void AddEventListener()
    {
        mPortraitZhu.onClick.AddListener(onPortraitZhu);
        mPortrait.onClick.AddListener(onPortrait);
        mPortrait01.onClick.AddListener(onPortrait01);
        mPortrait02.onClick.AddListener(onPortrait02);
        mPortrait03.onClick.AddListener(onPortrait03);
        mbg.onClick.AddListener(onbg);
    }
    private void RemoveEventListener()
    {
        mPortraitZhu.onClick.RemoveListener(onPortraitZhu);
        mPortrait.onClick.RemoveListener(onPortrait);
        mPortrait01.onClick.RemoveListener(onPortrait01);
        mPortrait02.onClick.RemoveListener(onPortrait02);
        mPortrait03.onClick.RemoveListener(onPortrait03);
        mbg.onClick.RemoveListener(onbg);
    }
    private void onPortraitZhu()
    {
        SoundManager.GetSingleton().playButtonSound(MusicType.Type_Button);
        Close();
    }
    private void onbg()
    {
        Close();
    }
    private void onPortrait()
    {
        Userlocate_Controller.gatSingle().ChanePhoto(1);
        GameManager.Getsingle().UpdateUserInfo(UserInformation.nickname, UserInformation.sex,1);
        Close();
    }
    private void onPortrait01()
    {
        Debug.Log("123");
        Userlocate_Controller.gatSingle().ChanePhoto(2);
        GameManager.Getsingle().UpdateUserInfo(UserInformation.nickname, UserInformation.sex, 2);
        Close();
    }
    private void onPortrait02()
    {
        Userlocate_Controller.gatSingle().ChanePhoto(3);
        GameManager.Getsingle().UpdateUserInfo(UserInformation.nickname, UserInformation.sex,3);
        Close();
    }
    private void onPortrait03()
    {
        Userlocate_Controller.gatSingle().ChanePhoto(4);
        GameManager.Getsingle().UpdateUserInfo(UserInformation.nickname, UserInformation.sex, 4);
        Close();
    }
    
}