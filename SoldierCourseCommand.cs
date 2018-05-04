using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace VL.SoldierCourse
{
    [Transaction(TransactionMode.Manual)]
    public class SoldierCourseCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return SCExe.Execute(commandData, ref message, elements);
        }
    }
}