using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            double ind = double.Parse(txtInput.Text);
            WSSample.ServiceClient client = new WSSample.ServiceClient();
            btn.IsEnabled = false;
            // 调用服务操作
            double retval = await client.SqrAsync(ind);
            // 显示结果
            tbResult.Text = $"计算结果：{retval}";
            btn.IsEnabled = true;
        }
    }
}
