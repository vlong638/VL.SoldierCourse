using Autodesk.Revit.DB;
using System;
using System.IO;
using System.Text;
using VL.RevitCommon;

namespace VL.SoldierCourse
{
    /// <summary>
    /// 首先这是一个ES存储对象,在这个基础上额外增加了DB存储对象
    /// </summary>
    public class SCModel : VLModel_ES
    {
        #region ES存储
        public DateTime? SelectedDate { set; get; }
        #endregion

        public SCModel() : base("")
        {
        }
        public SCModel(string data) : base(data)
        {
        }

        #region ES
        public override bool LoadData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return false;
            try
            {
                StringReader sr = new StringReader(data);
                SelectedDate = sr.ReadFormatStringAsDateTime();
                return true;
            }
            catch (Exception ex)
            {
                VLLogger.Log(ex);
                return false;
            }
        }
        public override string ToData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendItem(SelectedDate);
            return sb.ToString();
        }
        public void SaveESData(Document doc)
        {
            SCStorageEntity StorageEntity = new SCStorageEntity();
            var data = ToData();
            VLTransactionHelper.DelegateRevitTransaction(doc, nameof(SaveESData), () =>
            {
                VLDelegateHelper.DelegateTryCatch(
                    () =>
                    {
                        ExtensibleStorageHelper.SetData(doc, StorageEntity, StorageEntity.FieldOfData, data);
                        return true;
                    },
                    () =>
                    {
                        ExtensibleStorageHelper.RemoveStorage(doc, StorageEntity);
                        ExtensibleStorageHelper.SetData(doc, StorageEntity, StorageEntity.FieldOfData, data);
                        return false;
                    }
                );
                return true;
            });
        }
        public void LoadESData(Document doc)
        {
            SCStorageEntity StorageEntity = new SCStorageEntity();
            VLTransactionHelper.DelegateRevitTransaction(doc, nameof(LoadESData), () =>
            {
                LoadData(ExtensibleStorageHelper.GetData(doc, StorageEntity, StorageEntity.FieldOfData));
                return true;
            });
        }
        #endregion
    }
}
