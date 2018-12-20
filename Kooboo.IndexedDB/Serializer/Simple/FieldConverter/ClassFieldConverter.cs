//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using Kooboo.IndexedDB.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.IndexedDB.Serializer.Simple.FieldConverter
{
    public class ClassFieldConverter<T> : IFieldConverter<T>
    {
        Type ClassType;
        
        Func<T, object> getValue;
        Action<T, object> setValue;

        public ClassFieldConverter(string FieldName)
        { 
            Type type = typeof(T); 
            this.ClassType = ObjectHelper.GetFieldType(type, FieldName);

            this.Items = new IFieldConverterCollect();
            this.getValue = ObjectHelper.GetGetObjectValue<T>(FieldName); 
            this.setValue = ObjectHelper.GetSetObjectValue<T>(FieldName, ClassType);
            this.FieldNameHash = ObjectHelper.GetHashCode(FieldName);


            var allfields = TypeHelper.GetPublicPropertyOrFields(ClassType);

            foreach (var item in allfields)
            {
                AddFields(ClassType, item.Value, item.Key);
            }
 
        }

        private void AddFields(Type ClassType, Type FieldType,  string FieldName)
        {
           
                var converter = ConverterHelper.GetFieldConverter(ClassType, FieldType, FieldName);

                if (converter != null)
                {
                    Items.Add(converter);
                }
            
        }

        public int ByteLength
        {
            get
            {
                return 0;
            }
        }

        public int FieldNameHash
        {
            get; set;
        }

        public void SetByteValues(T value, byte[] bytes)
        {

            var FieldObject = Activator.CreateInstance(this.ClassType); 

            int startposition = 0;
            int totallength = bytes.Length;
            while (true)
            {

                int FieldNameHash = BitConverter.ToInt32(bytes, startposition);

                startposition += 4;

                int len = BitConverter.ToInt32(bytes, startposition);
                startposition += 4;

                var item = Items.FindNameHash(FieldNameHash);

                if (item != null)
                {
                    byte[] FieldValueBytes = new byte[len];
                    System.Buffer.BlockCopy(bytes, startposition, FieldValueBytes, 0, len);
                    item.SetByteValues(FieldObject, FieldValueBytes);
                }

                startposition += len;

                if (startposition + 8 >= totallength)
                { break; }
            }
               
            this.setValue(value, FieldObject);
        }

        public byte[] ToBytes(T Value)
        {
            object fieldvalue = this.getValue(Value);

            MemoryStream ms = new MemoryStream();

            if (fieldvalue != null)
            {
                foreach (var item in this.Items)
                {
                    byte[] result = item.ToBytes(fieldvalue);
                    if (result == null || result.Length == 0)
                        continue;

                    ms.Write(BitConverter.GetBytes(item.FieldNameHash), 0, 4);

                    int bytelen = item.ByteLength;
                    if (bytelen == 0)
                        bytelen = result.Length;

                    ms.Write(BitConverter.GetBytes(bytelen), 0, 4);

                    ms.Write(result, 0, result.Length);
                }
            }

            byte[] BackValue = ms.ToArray();
            ms.Close();
            return BackValue;
        }

        private IFieldConverterCollect Items
        {
            get; set;
        }

    }

    public class ClassFieldConverter : IFieldConverter
    { 

        Type ClassType;

        Func<object, object> getValue;
        Action<object, object> setValue;

        public ClassFieldConverter(Type ObjectType, string FieldName)
        {
            this.ClassType = ObjectHelper.GetFieldType(ObjectType, FieldName);
        }

        public ClassFieldConverter(Type ObjectType, Type ClassFieldType, string FieldName)
        {
             ClassType = ClassFieldType; 
             Items = new IFieldConverterCollect();
             getValue = ObjectHelper.GetGetObjectValue(FieldName, ObjectType);
             setValue = ObjectHelper.GetSetObjectValue(FieldName, ObjectType, ClassType);
             FieldNameHash = ObjectHelper.GetHashCode(FieldName);

            foreach (var item in ClassType.GetProperties())
            {
                if (item.CanRead && item.CanWrite)
                {
                    AddFields(ClassType, item.PropertyType, item.Name);
                }
            }

            foreach (var item in ClassType.GetFields())
            {
                if (item.IsPublic && !item.IsStatic)
                {
                    AddFields(ClassType, item.FieldType, item.Name);
                }
            }
        }

        private void AddFields(Type ClassType, Type FieldType, string FieldName)
        {
            if (ClassType == FieldType)
            {
                Items.Add(this);  
            }
            else
            {
                var converter = ConverterHelper.GetFieldConverter(ClassType, FieldType, FieldName);

                if (converter != null)
                {
                    Items.Add(converter);
                }
            }
        }

        public int ByteLength
        {
            get
            {
                return 0;
            }
        }

        public int FieldNameHash
        {
            get; set;
        }
          
        public void SetByteValues(object value, byte[] bytes)
        {
            var FieldObject = Activator.CreateInstance(this.ClassType);

            int startposition = 0;
            int totallength = bytes.Length;
            while (true)
            {

                int FieldNameHash = BitConverter.ToInt32(bytes, startposition);

                startposition += 4;

                int len = BitConverter.ToInt32(bytes, startposition);
                startposition += 4;

                var item = Items.FindNameHash(FieldNameHash);

                if (item != null)
                {
                    byte[] FieldValueBytes = new byte[len];
                    System.Buffer.BlockCopy(bytes, startposition, FieldValueBytes, 0, len);
                    item.SetByteValues(FieldObject, FieldValueBytes);
                }

                startposition += len;

                if (startposition + 8 >= totallength)
                { break; }
            }

            this.setValue(value, FieldObject);
        }

        public byte[] ToBytes(object Value)
        {
            object fieldvalue = this.getValue(Value);

            MemoryStream ms = new MemoryStream();

            if (fieldvalue != null)
            { 
                foreach (var item in this.Items)
                {
                    byte[] result = item.ToBytes(fieldvalue);
                    if (result == null || result.Length == 0)
                        continue;

                    var hash = BitConverter.GetBytes(item.FieldNameHash);
                    ms.Write(hash, 0, hash.Length);

                    int bytelen = item.ByteLength;
                    if (bytelen == 0)
                    {
                        bytelen = result.Length;
                    }

                    var length = BitConverter.GetBytes(bytelen);
                    ms.Write(length, 0, length.Length);

                    ms.Write(result, 0, result.Length);
                }
            }

            byte[] BackValue = ms.ToArray();
            ms.Close();
            return BackValue;
        }

        private IFieldConverterCollect Items
        {
            get; set;
        }

    }
}
