namespace DateDeclare
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Ranking
    {
        public string nickName;
        public string awardName;
        public string datetime;
        public int gold;
    }
}

