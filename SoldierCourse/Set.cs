using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using VL.RevitCommon;

namespace VL.SoldierCourse
{
    /// <summary>
    /// 命令对象
    /// </summary>
    public class SCSet : VLRevitCommand
    {

        public UIApplication UIApplication;
        public Document Document { get { return UIApplication.ActiveUIDocument.Document; } }
        public SCViewModel ViewModel { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="app"></param>
        public SCSet(UIApplication app) : base(app)
        {
            UIApplication = app;
        }


        protected override bool DoMain()
        {
            try
            {
                SQLiteHelper.PrepareTables(false);
                SCContext.InitByUIDocument(uiDoc);
                ViewModel.Execute();
                return true;
            }
            catch (Exception ex)
            {
                VLLogger.Log(ex);
                return false;
            }
        }

        protected override bool Prepare()
        {
            throw new NotImplementedException();
        }
    }
}
