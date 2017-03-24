namespace wox.serial
{
    using System;
    using System.Xml;

    public abstract class ObjectReader : Serial
    {
        protected ObjectReader()
        {
        }

        public abstract object read(XmlReader reader);
    }
}

