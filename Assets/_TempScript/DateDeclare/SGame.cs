namespace DateDeclare
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SGame
    {
        public EGameType gameTye;
        public string versionCode;
        public bool haveNewVersionIDFlag;
        public int isRunning;
        public string downloadWindows;
        public string downloadAndroid;
        public Ranking[] ranking;
        public int loadstate;
    }
}

