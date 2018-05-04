using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace VL.SoldierCourse
{
    public class SCExe
    {
        public static Result Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            SCSet set = new SCSet(commandData.Application);
            if (set.DoCmd())
                return Result.Succeeded;
            else
                return Result.Failed;
        }
    }
}
