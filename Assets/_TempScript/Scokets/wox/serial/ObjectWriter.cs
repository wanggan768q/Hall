namespace wox.serial
{
    using System;
    using System.Xml;

    public abstract class ObjectWriter : Serial
    {
        protected ObjectWriter()
        {
        }

        public abstract void write(object o, XmlTextWriter writer);
    }
}

