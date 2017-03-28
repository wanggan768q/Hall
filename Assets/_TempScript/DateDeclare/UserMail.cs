namespace DateDeclare
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct UserMail
    {
        public string title;
        public string content;
        public string datetime;
        public string sender;
        public int status;
        public int id;
    }
}

