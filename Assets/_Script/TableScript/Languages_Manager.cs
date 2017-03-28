using UnityEngine;
using System.Collections;


public class Languages : ITableItem
{
    public int ID;
    public string ChineseContent;
    public string EnglishContent;
    public int Key() { return ID; }
}
public class Languages_Manager : TableManager<Languages, Languages_Manager>
{
    public override string TableName()
    {
        return "Language";
    }
}
