using System;
using System.IO;
using System.Xml;
using UnityEngine;

public class DateFile : MonoBehaviour
{
    protected static DateFile gSingleton;
    protected string mFileName = "/Data13.xml";
    protected XmlDocument mXmlDoc = new XmlDocument();
    private string strNodeName = "/Data/YaoQianShu";

    protected void _createXmlFile(string strFileName)
    {
        try
        {
            File.Create(strFileName).Close();
            XmlElement newChild = this.mXmlDoc.CreateElement("Data");
            this.mXmlDoc.AppendChild(newChild);
            XmlElement element2 = this.mXmlDoc.CreateElement("YaoQianShu");
            newChild.AppendChild(element2);
            XmlElement element3 = this.mXmlDoc.CreateElement("USERID");
            element2.AppendChild(element3);
            element3.InnerText = "UserId=";
            XmlElement element4 = this.mXmlDoc.CreateElement("LANGUAGE");
            element2.AppendChild(element4);
            element4.InnerText = "Language=1";
            XmlElement element5 = this.mXmlDoc.CreateElement("SoundSource");
            element2.AppendChild(element5);
            element5.InnerText = "SoundSource=1";
            XmlElement element6 = this.mXmlDoc.CreateElement("MusicSource");
            element2.AppendChild(element6);
            element6.InnerText = "MusicSource=1";
            XmlElement element7 = this.mXmlDoc.CreateElement("Password");
            element2.AppendChild(element7);
            element7.InnerText = "Password=";
            XmlElement element8 = this.mXmlDoc.CreateElement("Autologon");
            element2.AppendChild(element8);
            element8.InnerText = "Autologon=0";
            this.mXmlDoc.Save(strFileName);
        }
        catch
        {
            Debug.LogError("DataBase._createXmlFile failed!");
        }
    }

    protected string _readData(string strNodeName, string strName)
    {
        string str = string.Empty;
        if (this.mXmlDoc != null)
        {
            XmlElement element = (XmlElement) this.mXmlDoc.SelectSingleNode(strNodeName);
            str = element.InnerText.Substring((strName + "=").Length);
            this.mXmlDoc.Save(this.mFileName);
            return str;
        }
        Debug.Log("XmlDocument load failed.");
        return str;
    }

    protected int _writeData(string strNodeName, string strName, string strValue)
    {
        if (this.mXmlDoc != null)
        {
            XmlElement element = (XmlElement) this.mXmlDoc.SelectSingleNode(strNodeName);
            element.InnerText = strName + "=" + strValue;
            this.mXmlDoc.Save(this.mFileName);
            return 0;
        }
        Debug.Log("XmlDocument load failed.");
        return 1;
    }

    private void Awake()
    {
        if (gSingleton == null)
        {
            gSingleton = this;
            Debug.Log("DateFile");
        }
        this.mFileName = Application.persistentDataPath + this.mFileName;
        if (!File.Exists(this.mFileName))
        {
            this._createXmlFile(this.mFileName);
        }
        else
        {
            try
            {
                this.mXmlDoc.Load(this.mFileName);
            }
            catch (Exception)
            {
                File.Delete(this.mFileName);
                this._createXmlFile(this.mFileName);
            }
        }
    }

    public static DateFile GetSingleton()
    {
        return gSingleton;
    }

    public string ReadAutologon()
    {
        return this._readData("/Data/YaoQianShu/Autologon", "Autologon");
    }

    public string ReadLanguage()
    {
        return this._readData("/Data/YaoQianShu/LANGUAGE", "Language");
    }

    public string ReadPassword()
    {
        return this._readData("/Data/YaoQianShu/Password", "Password");
    }

    public string ReadUserId()
    {
        return this._readData("/Data/YaoQianShu/USERID", "UserId");
    }

    public string ReadVoice(string audioSource)
    {
        return this._readData("/Data/YaoQianShu/" + audioSource, audioSource);
    }

    public void SaveAutologon(int autologon)
    {
        this._writeData("/Data/YaoQianShu/Autologon", "Autologon", autologon.ToString());
    }

    public void SaveLanguage(string strlanguage)
    {
        this._writeData("/Data/YaoQianShu/LANGUAGE", "Language", strlanguage);
    }

    public void SavePassword(string password)
    {
        this._writeData("/Data/YaoQianShu/Password", "Password", password);
    }

    public void SaveUserId(string strUserId)
    {
        this._writeData("/Data/YaoQianShu/USERID", "UserId", strUserId);
    }

    public void SaveVoice(string audioSource, int status)
    {
        this._writeData("/Data/YaoQianShu/" + audioSource, audioSource, status.ToString());
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
}

