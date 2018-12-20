//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using Kooboo.IndexedDB.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace Kooboo.IndexedDB.Serializer.Simple
{
 public   class ClassConverter
    { 
        Type ClassType; 
      
        public ClassConverter(Type ClassType)
        {
            this.ClassType = ClassType;
            Items = new IFieldConverterCollect(); 
        }

        public void InitFields()
        {

            var allfields = TypeHelper.GetPublicPropertyOrFields(ClassType);

            foreach (var item in allfields)
            {
                AddFields(ClassType, item.Value, item.Key);
            } 
        }

        private void AddFields(Type ClassType, Type FieldType, string FieldName)
        {
            var converter = ConverterHelper.GetFieldConverter(ClassType, FieldType, FieldName);

            if (converter != null)
            {
                Items.Add(converter);
            }
        }
          
        public object FromBytes(byte[] bytes)
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

            return FieldObject; 
        }

        public byte[] ToBytes(object Value)
        {
            MemoryStream ms = new MemoryStream();

            foreach (var item in this.Items)
            {
                byte[] result = item.ToBytes(Value);
                if (result == null || result.Length == 0)
                {
                    continue;
                }

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
