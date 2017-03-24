using UnityEngine;
using System.Collections;


public class Client_Data : ITableItem
{
    public int ID;
    public string account_id;
    public int Key() { return ID; }
}
public class Client_DataManager : TableManager<Client_Data, Client_DataManager>
{
    public override string TableName()
    {
        return "Client_Data";
    }
}
