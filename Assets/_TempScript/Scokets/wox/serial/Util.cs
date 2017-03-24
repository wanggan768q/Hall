namespace wox.serial
{
    using System;
    using System.Reflection;

    internal class Util
    {
        public static ConstructorInfo forceDefaultConstructor(Type cl)
        {
            return cl.GetConstructor(Type.EmptyTypes);
        }

        public static void main(string[] args)
        {
            object obj2 = Type.GetType("serializer.Student").GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        public static bool primitive(Type typeOb)
        {
            for (int i = 0; i < Serial.primitives.Length; i++)
            {
                if (Serial.primitives[i].Equals(typeOb))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool stringable(object o)
        {
            return ((((((o is sbyte) || (o is double)) || ((o is float) || (o is int))) || (((o is long) || (o is short)) || ((o is bool) || (o is char)))) || (o is Type)) || (o is string));
        }

        public static bool stringable(string name)
        {
            try
            {
                return (((Type) Serial.mapWOXToCSharp[name]) != null);
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine("Exception: " + exception.Message);
                return false;
            }
        }

        public static void testReflection(object testObject)
        {
            testReflection(testObject.GetType());
        }

        public static void testReflection(Type objectType)
        {
            ConstructorInfo constructor = objectType.GetConstructor(Type.EmptyTypes);
            ConstructorInfo[] constructors = objectType.GetConstructors();
            MethodInfo[] methods = objectType.GetMethods();
            object obj2 = constructor.Invoke(new object[0]);
            foreach (ConstructorInfo info2 in constructors)
            {
            }
            foreach (MethodInfo info3 in methods)
            {
            }
        }
    }
}

