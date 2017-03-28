using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITableItem
{
    int Key();
}
public interface ITableManager
{
    string TableName();
    object TableData { get; }
}
public abstract class TableManager<T,U>:Singleton<U>,ITableManager where T:ITableItem
{
    public abstract string TableName();
    public object TableData { get { return mItemArray; } }
    T[] mItemArray;
    Dictionary<int, int> mKeyItemMap = new Dictionary<int, int>();
    /// <summary>
    /// 构造函数
    /// </summary>
    internal TableManager()
    {
        mItemArray = TableParser.Parse<T>(TableName());
        for (int i = 0; i < mItemArray.Length; i++)
        {
            mKeyItemMap[mItemArray[i].Key()] = i;
        }

    }
    /// <summary>
    /// 从表格中获取一行数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public T GetItem(int key)
    {
        int itemIndex;
        if (mKeyItemMap.TryGetValue(key,out itemIndex))
            return mItemArray[itemIndex];
        return default(T);
    }
    /// <summary>
    /// 获取整个表格数据
    /// </summary>
    /// <returns></returns>
    public T[] GetAllItem()
    {
        return mItemArray;
    }
}
