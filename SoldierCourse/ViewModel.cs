using System;
using System.Collections.Generic;
using VL.RevitCommon;

namespace VL.SoldierCourse
{
    public enum SCViewType
    {
        Idle,
        Close,//主动触发
        Closing,//关闭前触发
        Closed,//关闭后触发
        CheckWarn,
    }

    public class SCViewModel : VLViewModel<SCViewType>
    {
        SCModel Model;
        SCWindow View;

        //选中日期
        public DateTime? SelectedDate
        {
            get
            {
                return Model.SelectedDate;
            }
            set
            {
                Model.SelectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }

        #region Construction
        public SCViewModel() : base()
        {
            Model = new SCModel();
            Model.LoadESData(SCContext.Doc);
            View = new SCWindow(this);
        }
        #endregion

        public override void Execute()
        {
            switch (ViewType)
            {
                case SCViewType.Idle:
                    View = new SCWindow(this);
                    View.ShowDialog();
                    break;
                case SCViewType.Close:
                    Model.SaveESData(SCContext.Doc);
                    View.Close();
                    break;
                case SCViewType.Closing:
                    Model.SaveESData(SCContext.Doc);
                    break;
                case SCViewType.CheckWarn:

                    break;
                default:
                    throw new NotImplementedException("功能未实现");
            }
        }
    }
}
