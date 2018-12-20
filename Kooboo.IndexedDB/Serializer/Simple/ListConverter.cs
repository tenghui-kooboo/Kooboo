//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using Kooboo.IndexedDB.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.IndexedDB.Serializer.Simple
{
  public class ListConverter
    {  
        private Type DataType;
        private Type ListType;

        private int FieldLength;

        private Func<object, byte[]> GetObjectBytes;
        private Func<byte[], object> GetObjectValue;
   
        public ListConverter(Type ListType)
        {
            this.ListType = ListType;
            this.DataType = ObjectHelper.GetEnumberableType(ListType);
  
            this.FieldLength = ConverterHelper.GetTypeLength(this.DataType);
 
            this.GetObjectBytes = ConverterHelper.GetValueToBytes(this.DataType);
            this.GetObjectValue = ConverterHelper.GetBytesToValue(this.DataType);

            if (this.GetObjectBytes == null || this.GetObjectValue == null)
            {
                throw new Exception(this.DataType.Name + " is not yet supported.");
            }
        } 
   
        public object FromBytes(byte[] bytes)
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

            return list; 

        }

        public byte[] ToBytes(object value)
        { 
            if (value == null)
                return null;

            MemoryStream ms = new MemoryStream();

            foreach (var item in (IEnumerable)value)
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
}
