using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;
using VL.RevitCommon;

namespace VL.SoldierCourse
{
    public class SCContext
    {
        #region statics for Init
        public static Document Doc { private set; get; }
        public static UIDocument UIDoc { private set; get; }
        public static View3D ViewSC { private set; get; }
        public static Guid ProjectId { private set; get; }
        public static bool InitByUIDocument(UIDocument uidoc)
        {
            UIDoc = uidoc;
            Doc = UIDoc.Document;
            string viewName = "土方分块";
            ViewSC = VLDocumentHelper.GetElementByNameAs<View3D>(Doc, viewName);
            ProjectId = VLRevitProject.GetProjectId(Doc);
            if (ViewSC == null)
            {
                using (var transaction = new Transaction(Doc, "EarthworkBlocking." + nameof(InitByUIDocument)))
                {
                    transaction.Start();
                    try
                    {
                        var viewFamilyType = new FilteredElementCollector(Doc).OfClass(typeof(ViewFamilyType)).ToElements().FirstOrDefault(c => (c as ViewFamilyType).FamilyName == "三维视图" || (c as ViewFamilyType).FamilyName == "3D View");
                        if (viewFamilyType == null)
                        {
                            TaskDialog.Show("消息", "该功能需在三维视图下操作");
                            return false;
                        }
                        ViewSC = View3D.CreateIsometric(Doc, viewFamilyType.Id);
                        ViewSC.Name = viewName;
                        transaction.Commit();
                        TaskDialog.Show("消息", "三维视图(土方分块)新建成功");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.RollBack();
                        TaskDialog.Show("消息", "视图(土方分块)新建失败,错误详情:" + ex.ToString());
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }
        #endregion
        
        public static OverrideGraphicSettings DefaultCPSettings = new OverrideGraphicSettings();//默认的颜色透明度方案














        #region Creator
        private static SCCreator _Creator = null;
        public static SCCreator Creator { get { return _Creator ?? (_Creator = new SCCreator()); } }

        #endregion

        #region OverrideGraphicSettings
        static ElementId _DefaultFillPatternId = null;
        /// <summary>
        /// 获取填充模式
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ElementId GetDefaultFillPatternId(Document doc)
        {
            if (_DefaultFillPatternId != null)
                return _DefaultFillPatternId;

            _DefaultFillPatternId = new FilteredElementCollector(doc).OfClass(typeof(FillPatternElement)).ToElements().First(c => (c as FillPatternElement).GetFillPattern().IsSolidFill).Id;
            return _DefaultFillPatternId;
        }
        /// <summary>
        /// 获取淡显的颜色透明度设置
        /// </summary>
        public static OverrideGraphicSettings _TingledOverrideGraphicSettings = null;
        public static OverrideGraphicSettings GetTingledOverrideGraphicSettings(Document doc)
        {
            if (_TingledOverrideGraphicSettings == null)
            {
                _TingledOverrideGraphicSettings = VLOverrideGraphicSettingsHelper.GetOverrideGraphicSettings(new Autodesk.Revit.DB.Color(185, 185, 185), GetDefaultFillPatternId(doc), 80);
            }
            return _TingledOverrideGraphicSettings;
        }
        #endregion
    }
}
