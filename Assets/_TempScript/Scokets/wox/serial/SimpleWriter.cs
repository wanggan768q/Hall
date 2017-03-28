namespace wox.serial
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Reflection;
    using System.Text;
    using System.Xml;

    public class SimpleWriter : ObjectWriter
    {
        private int count = 0;
        private bool doFinal;
        private bool doStatic = true;
        private Hashtable map = new Hashtable();
        private bool writePrimitiveTypes = true;

        public string arrayString(Array obArray, int len)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                if (i > 0)
                {
                    builder.Append(" ");
                }
                if (obArray is Type[])
                {
                    Type type = (Type) obArray.GetValue(i);
                    if (type != null)
                    {
                        string str = (string) Serial.mapCSharpToWOX[type.ToString()];
                        if (str != null)
                        {
                            builder.Append(str);
                        }
                        else
                        {
                            str = (string) Serial.mapArrayCSharpToWOX[type.ToString()];
                            if (str != null)
                            {
                                builder.Append(str);
                            }
                            else
                            {
                                builder.Append(type.ToString());
                            }
                        }
                    }
                    else
                    {
                        builder.Append("null");
                    }
                }
                else if (obArray is char[])
                {
                    if (obArray.GetValue(i) != null)
                    {
                        char character = (char) obArray.GetValue(i);
                        builder.Append(getUnicodeValue(character));
                    }
                    else
                    {
                        builder.Append("null");
                    }
                }
                else if (obArray is bool[])
                {
                    object obj3 = obArray.GetValue(i);
                    string str2 = string.Empty;
                    if (obj3 != null)
                    {
                        bool flag = (bool) obArray.GetValue(i);
                        if (flag.ToString().Equals("True"))
                        {
                            str2 = "true";
                        }
                        else if (flag.ToString().Equals("False"))
                        {
                            str2 = "false";
                        }
                        builder.Append(str2);
                    }
                    else
                    {
                        builder.Append("null");
                    }
                }
                else
                {
                    object obj4 = obArray.GetValue(i);
                    if (obj4 != null)
                    {
                        builder.Append(obj4.ToString());
                    }
                    else
                    {
                        builder.Append("null");
                    }
                }
            }
            return builder.ToString();
        }

        private static string fillWithZeros(string hexValue)
        {
            switch (hexValue.Length)
            {
                case 1:
                    return ("000" + hexValue);

                case 2:
                    return ("00" + hexValue);

                case 3:
                    return ("0" + hexValue);
            }
            return hexValue;
        }

        public static FieldInfo[] getFields(Type c)
        {
            ArrayList list = new ArrayList();
            while (c != null)
            {
                FieldInfo[] fields = c.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                for (int j = 0; j < fields.Length; j++)
                {
                    list.Add(fields[j]);
                }
                c = null;
            }
            FieldInfo[] infoArray2 = new FieldInfo[list.Count];
            for (int i = 0; i < infoArray2.Length; i++)
            {
                infoArray2[i] = (FieldInfo) list[i];
            }
            return infoArray2;
        }

        private static string getUnicodeValue(char character)
        {
            int number = character;
            string hexValue = IntToHex(number);
            return (@"\u" + fillWithZeros(hexValue));
        }

        private static int HexToInt(string hexString)
        {
            return int.Parse(hexString, NumberStyles.HexNumber, null);
        }

        private static string IntToHex(int number)
        {
            return string.Format("{0:x}", number);
        }

        public bool isPrimitiveArray(string c)
        {
            for (int i = 0; i < base.primitiveArrays.Length; i++)
            {
                if (c.Equals(base.primitiveArrays[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static string stringify(object ob)
        {
            if (ob is Type)
            {
                Type type = (Type) ob;
                string str = (string) Serial.mapCSharpToWOX[type.ToString()];
                if (str != null)
                {
                    return str;
                }
                return type.ToString();
            }
            if (ob is char)
            {
                return getUnicodeValue((char) ob);
            }
            if (!(ob is bool))
            {
                return ob.ToString();
            }
            if (ob.ToString().Equals("True"))
            {
                return "true";
            }
            return "false";
        }

        public override void write(object ob, XmlTextWriter el)
        {
            if (ob == null)
            {
                el.WriteElementString("object", null);
            }
            else if (this.map.ContainsKey(ob))
            {
                el.WriteStartElement("object");
                string str = (string) Serial.mapCSharpToWOX[ob.GetType().ToString()];
                el.WriteAttributeString("type", str);
                el.WriteAttributeString("value", stringify(ob));
                el.WriteAttributeString("id", this.map[ob].ToString());
                el.WriteEndElement();
            }
            else
            {
                this.map.Add(ob, this.count++);
                if (Util.stringable(ob))
                {
                    el.WriteStartElement("object");
                    string str2 = (string) Serial.mapCSharpToWOX[ob.GetType().ToString()];
                    el.WriteAttributeString("type", str2);
                    el.WriteAttributeString("value", stringify(ob));
                    el.WriteAttributeString("id", this.map[ob].ToString());
                    el.WriteEndElement();
                }
                else if (ob is Array)
                {
                    this.writeArray(ob, el);
                }
                else if (ob is ArrayList)
                {
                    this.writeArrayList(ob, el);
                }
                else if (ob is Hashtable)
                {
                    this.writeHashtable(ob, el);
                }
                else
                {
                    el.WriteStartElement("object");
                    el.WriteAttributeString("type", ob.GetType().ToString());
                    el.WriteAttributeString("id", this.map[ob].ToString());
                    this.writeFields(ob, el);
                    el.WriteEndElement();
                }
            }
        }

        public void writeArray(object ob, XmlTextWriter el)
        {
            if (this.isPrimitiveArray(ob.GetType().ToString()))
            {
                this.writePrimitiveArray(ob, el);
            }
            else
            {
                this.writeObjectArray(ob, el);
            }
        }

        public void writeArrayList(object ob, XmlTextWriter el)
        {
            el.WriteStartElement("object");
            el.WriteAttributeString("type", "list");
            object obj2 = ((ArrayList) ob).ToArray();
            this.writeObjectArrayGeneric(ob, obj2, el);
        }

        public void writeFields(object o, XmlTextWriter parent)
        {
            FieldInfo[] infoArray = getFields(o.GetType());
            string name = null;
            for (int i = 0; i < infoArray.Length; i++)
            {
                try
                {
                    name = infoArray[i].Name;
                    object ob = infoArray[i].GetValue(o);
                    parent.WriteStartElement("field");
                    parent.WriteAttributeString("name", name);
                    if (Serial.mapCSharpToWOX.ContainsKey(infoArray[i].FieldType.ToString()))
                    {
                        if (this.writePrimitiveTypes)
                        {
                            parent.WriteAttributeString("type", (string) Serial.mapCSharpToWOX[infoArray[i].FieldType.ToString()]);
                        }
                        if (infoArray[i].FieldType.ToString().Equals("System.Char"))
                        {
                            char character = (char) ob;
                            string str2 = getUnicodeValue(character);
                            parent.WriteAttributeString("value", str2);
                            parent.WriteEndElement();
                        }
                        else if (infoArray[i].FieldType.ToString().Equals("System.Boolean"))
                        {
                            string str3 = string.Empty;
                            if (ob == null)
                            {
                                str3 = string.Empty;
                            }
                            if (ob.ToString().Equals("True"))
                            {
                                str3 = "true";
                            }
                            else if (ob.ToString().Equals("False"))
                            {
                                str3 = "false";
                            }
                            parent.WriteAttributeString("value", str3);
                            parent.WriteEndElement();
                        }
                        else
                        {
                            if (ob == null)
                            {
                                ob = string.Empty;
                            }
                            parent.WriteAttributeString("value", ob.ToString());
                            parent.WriteEndElement();
                        }
                    }
                    else
                    {
                        this.write(ob, parent);
                        parent.WriteEndElement();
                    }
                }
                catch (Exception exception)
                {
                    Console.Out.WriteLine("error: " + exception.Message);
                }
            }
        }

        public void writeHashtable(object ob, XmlTextWriter el)
        {
            el.WriteStartElement("object");
            el.WriteAttributeString("type", "map");
            el.WriteAttributeString("id", this.map[ob].ToString());
            IDictionaryEnumerator enumerator = ((Hashtable) ob).GetEnumerator();
            while (enumerator.MoveNext())
            {
                this.writeMapEntry(enumerator.Entry, el);
            }
            el.WriteEndElement();
        }

        public void writeMapEntry(object ob, XmlTextWriter el)
        {
            el.WriteStartElement("object");
            el.WriteAttributeString("type", "entry");
            DictionaryEntry entry = (DictionaryEntry) ob;
            this.writeMapEntryKey(entry.Key, el);
            this.writeMapEntryKey(entry.Value, el);
            el.WriteEndElement();
        }

        public void writeMapEntryKey(object ob, XmlTextWriter el)
        {
            this.write(ob, el);
        }

        public void writeMapEntryValue(object ob, XmlTextWriter el)
        {
            this.write(ob, el);
        }

        public void writeObjectArray(object ob, XmlTextWriter el)
        {
            el.WriteStartElement("object");
            el.WriteAttributeString("type", "array");
            this.writeObjectArrayGeneric(ob, ob, el);
        }

        public void writeObjectArrayGeneric(object obStored, object ob, XmlTextWriter el)
        {
            string str = (string) Serial.mapCSharpToWOX[ob.GetType().GetElementType().ToString()];
            if (str == null)
            {
                str = (string) Serial.mapArrayCSharpToWOX[ob.GetType().GetElementType().ToString()];
                if (str != null)
                {
                    el.WriteAttributeString("elementType", str);
                }
                else if (ob.GetType().GetElementType().ToString().Equals("System.Object"))
                {
                    el.WriteAttributeString("elementType", "Object");
                }
                else
                {
                    el.WriteAttributeString("elementType", ob.GetType().GetElementType().ToString());
                }
            }
            else
            {
                el.WriteAttributeString("elementType", str);
            }
            Array array = (Array) ob;
            int length = array.GetLength(0);
            el.WriteAttributeString("length", string.Empty + length);
            el.WriteAttributeString("id", this.map[obStored].ToString());
            for (int i = 0; i < length; i++)
            {
                this.write(array.GetValue(i), el);
            }
            el.WriteEndElement();
        }

        public void writePrimitiveArray(object ob, XmlTextWriter el)
        {
            el.WriteStartElement("object");
            el.WriteAttributeString("type", "array");
            string str = (string) Serial.mapCSharpToWOX[ob.GetType().GetElementType().ToString()];
            el.WriteAttributeString("elementType", str);
            Array obArray = (Array) ob;
            int length = obArray.GetLength(0);
            if (!(obArray is sbyte[]))
            {
                el.WriteAttributeString("length", string.Empty + length);
                el.WriteAttributeString("id", this.map[ob].ToString());
                el.WriteString(this.arrayString(obArray, length));
                el.WriteEndElement();
            }
        }
    }
}

