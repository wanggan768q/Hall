namespace DateDeclare
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Anouncement
    {
        public string title;
        public string content;
        public string dateTime;
        public string admin;
    }
}

