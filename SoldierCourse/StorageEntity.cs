using System;
using System.Collections.Generic;
using VL.RevitCommon;

namespace VL.SoldierCourse
{
    /// <summary>
    /// PipeAnnotationEntityCollection扩展存储对象
    /// </summary>
    public class SCStorageEntity : IExtensibleStorageEntity
    {
        public List<string> FieldNames { get { return new List<string>() { FieldOfData, FieldOfSetting }; } }
        public Guid SchemaId
        {
            get
            {
                return new Guid("B0DAAC24-32EB-478C-B8A3-3D71E3CFC232");
            }
        }
        public string StorageName { get { return "SC_Schema"; } }
        public string SchemaName { get { return "SC_Schema"; } }
        public string FieldOfData { get { return "SC_Collection"; } }
        public string FieldOfSetting { get { return "SC_Settings"; } }
    }
}
