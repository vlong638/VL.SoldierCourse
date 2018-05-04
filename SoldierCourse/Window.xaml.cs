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
            return ViewModel.ViewType == SCViewType.Idle;
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

        private void Btn_FilterDate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!ViewModel.SelectedDate.HasValue)
            {
                MainContext.ShowMessage("请选中日期");
                return;
            }

            //TODO
            //ViewModel.ViewTypeValue=


        }
    }
}
