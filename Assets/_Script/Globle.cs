using UnityEngine;
using System.Collections;

public class Globle
{
    private static Globle m_single=null;
    public static Globle Getsingle()
    {
        if (m_single == null)
            m_single = new Globle();
        return m_single;
    }
    //语言类型 0中文 1英文
    public static int LanType = 0;
    public bool OperationalVersion;//banben
}
