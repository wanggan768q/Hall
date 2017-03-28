namespace DateDeclare
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct UserInformation
    {
        public string username;
        public string nickname;
        public char sex;
        public int photoId;
        public int status;
        public int overflow;
        public int gameGold;
        public int expeGold;
        public double levelScore;
        public int level;
        public string password;
        public bool specialFlag;
        public int type;
        public bool ScoreOverflow;
        public int ID;
    }
}

