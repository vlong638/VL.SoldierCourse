using VL.RevitCommon;

namespace VL.SoldierCourse
{
    /// <summary>
    /// CMWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SCWindow : VLWindow
    {
        public new SCViewModel ViewModel { set; get; }

        #region Construction
        public override bool IsNormalClose()
        {
            return ViewModel.ViewType == SCViewType.ViewWindow;
        }
        public override void ViewModelClose()
        {
            ViewModel.ViewType = SCViewType.Close;
            ViewModel.Execute();
        }
        public override void ViewModelClosing()
        {
            ViewModel.ViewType = SCViewType.Closing;
            ViewModel.Execute();
        }
        public override void ViewModelClosed()
        {
            ViewModel.ViewType = SCViewType.Closed;
            ViewModel.Execute();
        }

        public SCWindow(SCViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }
        #endregion
    }
}
