using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class Client_DataTable : MonoBehaviour {

    private static Client_DataTable m_single;
    public static Client_DataTable GetSingle()
    {
        if (m_single == null)
            return new Client_DataTable();
        return m_single;
    }
    public void Table_Write(string name,string m_value1,string m_value2)
    {
        // 这里加载txt表格文件
        TextAsset textAsset = (TextAsset)Resources.Load(name);
        if (textAsset == null)
        {
            Debug.LogError("无法加载表格文件：" + name);
            return;
        }
        Client_Data[] c_ds= Client_DataManager.Instance.GetAllItem();
        //解析每一行的数据
        string[] lines = textAsset.text.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        //清除数据
        for (int i = 0; i < lines.Length; i++)
        {
            textAsset.text.Remove(i);
        }
        
        for (int i = 0; i < lines.Length; i++)
        {
            if (m_value1.ToString() == lines[i].ToString())
            {
                break;
            }
        }
        FileStream fs = new FileStream(Application.dataPath + "/Table/Resources/Client_Data.txt",FileMode.Open);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);
        Debug.Log("-------------------yixieru");
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                sw.Write("表id"+"\t"+"玩家账号"+"\n");
            }
            else if (i == 1)
            {
                sw.Write("ID"+"\t"+"account_id"+"\n");
            }
            else if (i > 1 && i < lines.Length - 1)
            {
                sw.Write(c_ds[i-2].ID.ToString()+"\t"+c_ds[i-2].account_id.ToString()+"\n");
                Debug.Log("-------------------yihuanhang");
            }
            else
            {
                sw.Write(lines[i]+"\n");
                sw.Write(m_value1+"\t"+m_value2);
                sw.Close();
                Debug.Log("-------------------yicunchushuju");
            }
        }
    }
}
