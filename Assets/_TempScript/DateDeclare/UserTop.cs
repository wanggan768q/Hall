namespace DateDeclare
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct UserTop
    {
        public string nickname;
        public int gold;
        public string awardName;
        public string datetime;
    }
}

