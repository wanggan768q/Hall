namespace DateDeclare
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInformation
    {
        public int gameType;
        public string versionCode;
        public string downloadVersionCode;
        public bool haveNewVersionIDFlag;
        public int isRunning;
        public string downloadWindows;
        public string downloadAndroid;
        public Ranking[] ranking;
        public DownloadStatus downloadStatus;
    }
}

