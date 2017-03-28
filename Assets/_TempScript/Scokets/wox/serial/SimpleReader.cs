namespace wox.serial
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Reflection;
    using System.Xml;

    public class SimpleReader : ObjectReader
    {
        private Hashtable map = new Hashtable();

        public bool empty(XmlReader xob)
        {
            return (!xob.HasAttributes && xob.IsEmptyElement);
        }

        private static int getDecimalValue(string unicodeValue)
        {
            return HexToInt(unicodeValue.Substring(2, 4));
        }

        public FieldInfo getField(Type typeObject, string name)
        {
            if (typeObject == null)
            {
                return null;
            }
            try
            {
                FieldInfo[] fields = typeObject.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo info = null;
                for (int i = 0; i < fields.Length; i++)
                {
                    if (fields[i].Name.Equals(name))
                    {
                        info = fields[i];
                        break;
                    }
                }
                return info;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
                return null;
            }
        }

        public Type getObjectArrayComponentType(string arrayTypeName)
        {
            Type type = (Type) Serial.mapWOXToCSharp[arrayTypeName];
            if (type != null)
            {
                return type;
            }
            type = (Type) Serial.mapArrayWOXToCSharp[arrayTypeName];
            if (type != null)
            {
                return type;
            }
            if (arrayTypeName.Equals("Object"))
            {
                arrayTypeName = "System.Object";
            }
            Type type2 = Type.GetType(arrayTypeName);
            if (type2 == null)
            {
                type2 = Assembly.GetEntryAssembly().GetType(arrayTypeName);
            }
            return type2;
        }

        private static int HexToInt(string hexString)
        {
            return int.Parse(hexString, NumberStyles.HexNumber, null);
        }

        public bool isArrayList(XmlReader xob)
        {
            return xob.GetAttribute("type").Equals("list");
        }

        public bool isMap(XmlReader xob)
        {
            return xob.GetAttribute("type").Equals("map");
        }

        public bool isObjectArray(XmlReader xob)
        {
            return xob.GetAttribute("type").Equals("array");
        }

        public bool isPrimitiveArray(XmlReader xob)
        {
            if (xob.GetAttribute("type").Equals("array"))
            {
                string attribute = xob.GetAttribute("elementType");
                for (int i = 0; i < base.primitiveArraysWOX.Length; i++)
                {
                    if (base.primitiveArraysWOX[i].Equals(attribute))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public object makeObject(ConstructorInfo cons, object[] args, object key)
        {
            object obj2 = cons.Invoke(args);
            this.map.Add(key, obj2);
            return obj2;
        }

        public override object read(XmlReader xob)
        {
            if (this.empty(xob))
            {
                return null;
            }
            if (this.reference(xob))
            {
                return this.map[xob.GetAttribute("idref")];
            }
            string attribute = xob.GetAttribute("id");
            if (this.isPrimitiveArray(xob))
            {
                return this.readPrimitiveArray(xob, attribute);
            }
            if (this.isObjectArray(xob))
            {
                return this.readObjectArray(xob, attribute);
            }
            if (this.isArrayList(xob))
            {
                return this.readArrayList(xob, attribute);
            }
            if (this.isMap(xob))
            {
                return this.readMap(xob, attribute);
            }
            if (Util.stringable(xob.GetAttribute("type")))
            {
                return this.readStringObject(xob, attribute);
            }
            return this.readObject(xob, attribute);
        }

        public object readArrayList(XmlReader xob, object id)
        {
            Array array = (Array) this.readObjectArrayGeneric(xob, id);
            ArrayList list = new ArrayList();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                list.Add(array.GetValue(i));
            }
            this.map.Add(id, list);
            return list;
        }

        public object readBooleanArray(bool[] a, string[] s)
        {
            int num = 0;
            for (int i = 0; i < a.Length; i++)
            {
                a[num++] = bool.Parse(s[i]);
            }
            return a;
        }

        public object readCharArray(char[] a, string[] s)
        {
            int num = 0;
            for (int i = 0; i < a.Length; i++)
            {
                int num3 = getDecimalValue(s[i]);
                a[num++] = (char) num3;
            }
            return a;
        }

        public object readClassArray(Type[] a, string[] s)
        {
            int num = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (s[i].Equals("null"))
                {
                    a[num++] = null;
                }
                else
                {
                    Type type = (Type) Serial.mapWOXToCSharp[s[i]];
                    if (type == null)
                    {
                        type = (Type) Serial.mapArrayWOXToCSharp[s[i]];
                        if (type == null)
                        {
                            try
                            {
                                Type type2 = Type.GetType(s[i]);
                                if (type2 == null)
                                {
                                    type2 = Assembly.GetEntryAssembly().GetType(s[i]);
                                }
                                a[num++] = type2;
                            }
                            catch (Exception exception)
                            {
                                Console.Out.WriteLine(exception.Message);
                            }
                        }
                        else
                        {
                            a[num++] = type;
                        }
                    }
                    else
                    {
                        a[num++] = type;
                    }
                }
            }
            return a;
        }

        public object readDoubleArray(double[] a, string[] s)
        {
            int num = 0;
            for (int i = 0; i < a.Length; i++)
            {
                a[num++] = double.Parse(s[i]);
            }
            return a;
        }

        public object readFloatArray(float[] a, string[] s)
        {
            int num = 0;
            for (int i = 0; i < a.Length; i++)
            {
                a[num++] = float.Parse(s[i]);
            }
            return a;
        }

        public object readIntArray(int[] a, string[] s)
        {
            int num = 0;
            for (int i = 0; i < a.Length; i++)
            {
                a[num++] = int.Parse(s[i]);
            }
            return a;
        }

        public object readLongArray(long[] a, string[] s)
        {
            int num = 0;
            for (int i = 0; i < a.Length; i++)
            {
                a[num++] = long.Parse(s[i]);
            }
            return a;
        }

        public object readMap(XmlReader xob, object id)
        {
            Hashtable hashtable = new Hashtable();
            XmlReader reader = xob.ReadSubtree();
            reader.Read();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    reader.Read();
                    object key = this.read(reader);
                    reader.Read();
                    object obj3 = this.read(reader);
                    hashtable.Add(key, obj3);
                }
            }
            this.map.Add(id, hashtable);
            return hashtable;
        }

        public object readObject(XmlReader xob, object id)
        {
            try
            {
                Type cl = Type.GetType(xob.GetAttribute("type"));
                if (cl == null)
                {
                    cl = Assembly.GetEntryAssembly().GetType(xob.GetAttribute("type"));
                }
                ConstructorInfo cons = Util.forceDefaultConstructor(cl);
                object ob = this.makeObject(cons, new object[0], id);
                this.setFields(ob, xob);
                return ob;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
                return null;
            }
        }

        public object readObjectArray(XmlReader xob, object id)
        {
            object obj2 = this.readObjectArrayGeneric(xob, id);
            this.map.Add(id, obj2);
            return obj2;
        }

        public object readObjectArrayGeneric(XmlReader xob, object id)
        {
            try
            {
                string attribute = xob.GetAttribute("elementType");
                int length = int.Parse(xob.GetAttribute("length"));
                Array array = Array.CreateInstance(this.getObjectArrayComponentType(attribute), length);
                XmlReader reader = xob.ReadSubtree();
                reader.Read();
                int num2 = 0;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        object obj2 = this.read(reader);
                        array.SetValue(obj2, num2++);
                    }
                }
                return array;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine("Exception is: " + exception.Message);
                return null;
            }
        }

        public object readPrimitiveArray(XmlReader xob, object id)
        {
            try
            {
                Type elementType = (Type) Serial.mapWOXToCSharp[xob.GetAttribute("elementType")];
                int length = int.Parse(xob.GetAttribute("length"));
                Array array = Array.CreateInstance(elementType, length);
                string str = xob.ReadString();
                char[] separator = new char[] { ' ' };
                string[] s = str.Split(separator);
                if (!elementType.Equals(typeof(sbyte)))
                {
                    if (elementType.Equals(typeof(short)))
                    {
                        object obj2 = this.readShortArray((short[]) array, s);
                        this.map.Add(id, obj2);
                        return obj2;
                    }
                    if (elementType.Equals(typeof(int)))
                    {
                        object obj3 = this.readIntArray((int[]) array, s);
                        this.map.Add(id, obj3);
                        return obj3;
                    }
                    if (elementType.Equals(typeof(long)))
                    {
                        object obj4 = this.readLongArray((long[]) array, s);
                        this.map.Add(id, obj4);
                        return obj4;
                    }
                    if (elementType.Equals(typeof(float)))
                    {
                        object obj5 = this.readFloatArray((float[]) array, s);
                        this.map.Add(id, obj5);
                        return obj5;
                    }
                    if (elementType.Equals(typeof(double)))
                    {
                        object obj6 = this.readDoubleArray((double[]) array, s);
                        this.map.Add(id, obj6);
                        return obj6;
                    }
                    if (elementType.Equals(typeof(char)))
                    {
                        object obj7 = this.readCharArray((char[]) array, s);
                        this.map.Add(id, obj7);
                        return obj7;
                    }
                    if (elementType.Equals(typeof(bool)))
                    {
                        object obj8 = this.readBooleanArray((bool[]) array, s);
                        this.map.Add(id, obj8);
                        return obj8;
                    }
                    if (elementType.Equals(typeof(Type)))
                    {
                        object obj9 = this.readClassArray((Type[]) array, s);
                        this.map.Add(id, obj9);
                        return obj9;
                    }
                    return null;
                }
                this.map.Add(id, array);
                return array;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine("The exception is: " + exception.Message);
            }
            return string.Empty;
        }

        public object readShortArray(short[] a, string[] s)
        {
            int num = 0;
            for (int i = 0; i < a.Length; i++)
            {
                a[num++] = short.Parse(s[i]);
            }
            return a;
        }

        public object readStringObject(XmlReader xob, object id)
        {
            try
            {
                Type type = (Type) Serial.mapWOXToCSharp[xob.GetAttribute("type")];
                if (type.Equals(typeof(Type)))
                {
                    Type type2 = (Type) Serial.mapWOXToCSharp[xob.GetAttribute("value")];
                    if (type2 == null)
                    {
                        type2 = (Type) Serial.mapArrayWOXToCSharp[xob.GetAttribute("value")];
                        if (type2 == null)
                        {
                            object obj2 = Type.GetType(xob.GetAttribute("value"));
                            if (obj2 == null)
                            {
                                obj2 = Assembly.GetEntryAssembly().GetType(xob.GetAttribute("value"));
                            }
                            this.map.Add(id, obj2);
                            return obj2;
                        }
                        this.map.Add(id, type2);
                        return type2;
                    }
                    this.map.Add(id, type2);
                    return type2;
                }
                if (type.Equals(typeof(char)))
                {
                    return (char) getDecimalValue(xob.GetAttribute("value"));
                }
                if (type.Equals(typeof(sbyte)))
                {
                    return sbyte.Parse(xob.GetAttribute("value"));
                }
                if (type.Equals(typeof(short)))
                {
                    return short.Parse(xob.GetAttribute("value"));
                }
                if (type.Equals(typeof(int)))
                {
                    return int.Parse(xob.GetAttribute("value"));
                }
                if (type.Equals(typeof(long)))
                {
                    return long.Parse(xob.GetAttribute("value"));
                }
                if (type.Equals(typeof(float)))
                {
                    return float.Parse(xob.GetAttribute("value"));
                }
                if (type.Equals(typeof(double)))
                {
                    return double.Parse(xob.GetAttribute("value"));
                }
                if (type.Equals(typeof(bool)))
                {
                    return bool.Parse(xob.GetAttribute("value"));
                }
                if (type.Equals(typeof(string)))
                {
                    return xob.GetAttribute("value");
                }
                return null;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
                return null;
            }
        }

        public bool reference(XmlReader xob)
        {
            return (xob.GetAttribute("idref") != null);
        }

        public void setFields(object ob, XmlReader xob)
        {
            Type type = ob.GetType();
            XmlReader reader = xob.ReadSubtree();
            reader.Read();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string attribute = reader.GetAttribute("name");
                    try
                    {
                        Type typeObject = type;
                        FieldInfo info = this.getField(typeObject, attribute);
                        object obj2 = null;
                        if (Util.primitive(info.FieldType))
                        {
                            string str2 = reader.GetAttribute("type");
                            if (str2.Equals("char"))
                            {
                                char ch = (char) getDecimalValue(reader.GetAttribute("value"));
                                obj2 = ch;
                            }
                            else if (str2.Equals("byte"))
                            {
                                obj2 = sbyte.Parse(reader.GetAttribute("value"));
                            }
                            else if (str2.Equals("short"))
                            {
                                obj2 = short.Parse(reader.GetAttribute("value"));
                            }
                            else if (str2.Equals("int"))
                            {
                                obj2 = int.Parse(reader.GetAttribute("value"));
                            }
                            else if (str2.Equals("long"))
                            {
                                obj2 = long.Parse(reader.GetAttribute("value"));
                            }
                            else if (str2.Equals("float"))
                            {
                                obj2 = float.Parse(reader.GetAttribute("value"));
                            }
                            else if (str2.Equals("double"))
                            {
                                obj2 = double.Parse(reader.GetAttribute("value"));
                            }
                            else if (str2.Equals("boolean"))
                            {
                                obj2 = bool.Parse(reader.GetAttribute("value"));
                            }
                            else if (str2.Equals("string"))
                            {
                                obj2 = reader.GetAttribute("value");
                            }
                            else if (str2.Equals("class"))
                            {
                                obj2 = Type.GetType(reader.GetAttribute("value"));
                                if (obj2 == null)
                                {
                                    obj2 = Assembly.GetEntryAssembly().GetType(reader.GetAttribute("value"));
                                }
                            }
                            else
                            {
                                obj2 = null;
                            }
                        }
                        else
                        {
                            reader.Read();
                            XmlReader reader2 = reader.ReadSubtree();
                            reader2.Read();
                            obj2 = this.read(reader2);
                        }
                        info.SetValue(ob, obj2);
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }
    }
}

