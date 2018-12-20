//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using Kooboo.IndexedDB.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Kooboo.IndexedDB.Serializer.Simple.FieldConverter
{
    public class ListFieldConverter<T> : IFieldConverter<T>
    {
        private Type DataType;
        private Type ListType;

        private int FieldLength;

        private Func<object, byte[]> GetObjectBytes;
        private Func<byte[], object> GetObjectValue;

        private Func<T, object> GetFieldValue;
        private Action<T, object> SetFieldValue;

        private string FieldName { get; set; }

        public ListFieldConverter(Type ListType, string FieldName)
        {
            this.ListType = ListType;
            this.DataType = ObjectHelper.GetEnumberableType(ListType);
            this.FieldName = FieldName;
            this.FieldNameHash = ObjectHelper.GetHashCode(FieldName);
            this.FieldLength = ConverterHelper.GetTypeLength(this.DataType);

            this.GetFieldValue = ObjectHelper.GetGetObjectValue<T>(FieldName);
            this.SetFieldValue = ObjectHelper.GetSetObjectValue<T>(FieldName, this.ListType);
            
            this.GetObjectBytes = ConverterHelper.GetValueToBytes(this.DataType);
            this.GetObjectValue = ConverterHelper.GetBytesToValue(this.DataType);

            if (this.GetObjectBytes == null || this.GetObjectValue == null)
            {
                throw new Exception(this.DataType.Name + " is not yet supported.");
            }
        }

        public int FieldNameHash
        {
            get; set;
        }

        public int ByteLength
        {
            get
            {
                return 0;
            }
        }

        public void SetByteValues(T value, byte[] bytes)
        {
            var list = Activator.CreateInstance(this.ListType) as System.Collections.IList;

            int startposition = 0;
            int totallength = bytes.Length;

            while (true)
            {
                if (this.FieldLength > 0)
                {

                    byte[] FieldValueBytes = new byte[this.FieldLength];
                    System.Buffer.BlockCopy(bytes, startposition, FieldValueBytes, 0, this.FieldLength);
                    startposition += this.FieldLength;
                    var objectvalue = this.GetObjectValue(FieldValueBytes);
                    list.Add(objectvalue);
                }
                else
                {
                    int len = BitConverter.ToInt32(bytes, startposition);
                    startposition += 4;

                    if (len > 0)
                    {
                        byte[] FieldValueBytes = new byte[len];
                        System.Buffer.BlockCopy(bytes, startposition, FieldValueBytes, 0, len);
                        startposition += len;
                        var objectvalue = this.GetObjectValue(FieldValueBytes);
                        list.Add(objectvalue);
                    }
                    else
                    {
                        list.Add(null); 
                    }
                }

                if (startposition >= totallength)
                { break; }
            }

            this.SetFieldValue(value, list);

        }

        public byte[] ToBytes(T value)
        {
            var listvalue = GetFieldValue(value);

            if (listvalue == null)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream();

            foreach (var item in (IEnumerable)listvalue)
            {
                var result = this.GetObjectBytes(item);

                if (this.FieldLength > 0)
                    ms.Write(result, 0, result.Length);
                else
                {
                    if (result != null)
                    {
                        ms.Write(BitConverter.GetBytes(result.Length), 0, 4);
                        ms.Write(result, 0, result.Length);
                    }
                    else
                        ms.Write(BitConverter.GetBytes(0), 0, 4);
                }
            }

            byte[] BackValue = ms.ToArray();
            ms.Close();
            return BackValue;
        }
    }

    public class ListFieldConverter : IFieldConverter
    {
        Func<object, object> getValue;
        Action<object, object> setValue;

        ListConverter converter;

        public ListFieldConverter(string FieldName, Type ObjectType, Type ListType)
        {
            this.getValue = ObjectHelper.GetGetObjectValue(FieldName, ObjectType);
            this.setValue = ObjectHelper.GetSetObjectValue(FieldName, ObjectType, ListType);
            this.FieldNameHash = ObjectHelper.GetHashCode(FieldName);
            converter = new ListConverter(ListType);
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
            object fieldvalue = converter.FromBytes(bytes);
            setValue(value, fieldvalue);
        }

        public byte[] ToBytes(object Value)
        {
            object fieldvalue = this.getValue(Value);
            return converter.ToBytes(fieldvalue);
        }
    }
}
