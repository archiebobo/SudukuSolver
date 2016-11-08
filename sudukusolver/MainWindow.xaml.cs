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

namespace SudukuSolver
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] num = Suduku_DataReader.GetDataFromFile("E:\\Current\\sudukusolver\\SudukuSolver\\Suduku_file");
            Suduku_Data sd= new Suduku_Data();
            Suduku_Blank sb = new Suduku_Blank(5, 5);
            tb01.Text = num.Length.ToString();
        }
    }
}
