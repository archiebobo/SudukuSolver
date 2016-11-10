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
        int[] init_num = Suduku_DataReader.GetDataFromFile("E:\\Current\\sudukusolver\\SudukuSolver\\Suduku_file");
        TextBox[] firstbox = new TextBox[81];
        TextBox[] secondbox = new TextBox[81];
        TextBox[] thirdbox = new TextBox[81];
        Suduku_Model sd = new Suduku_Model("E:\\Current\\sudukusolver\\SudukuSolver\\Suduku_file");
        public MainWindow()
        {
            InitializeComponent();
            load_text_box(gl01, firstbox,true);
            SetTextBoxValue(firstbox, init_num);
            load_text_box(gl02, secondbox,false);
            SetTextBoxValue(secondbox, sd);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sd.Solve_Suduku();
            SetTextBoxValue(secondbox, sd);
            if (sd.Check_Result())
            {
                tb01.Text = "Success!";
            }
            else
            {
                tb01.Text = "Failed!";
            }
        }
        private void load_text_box(Grid gd,TextBox[] tb,bool IfEdit)
        {
            for (int i = 0; i < 81; i++)
            {
                tb[i] = new TextBox();
                tb[i].Focusable =IfEdit;
                tb[i].FontSize = 20;
                tb[i].FontFamily = new FontFamily("Arial Black");
                tb[i].TextAlignment = TextAlignment.Center;
                tb[i].TextWrapping = TextWrapping.Wrap;
                gd.Children.Add(tb[i]);
                Grid.SetRow(tb[i], i / 9);
                Grid.SetColumn(tb[i], i % 9);
            }
        }
        private void SetTextBoxValue(TextBox[] tb, int[] val)
        {
            for (int i = 0; i < 81; i++)
            {
                if (val[i] != 0)
                {
                    tb[i].Text = val[i].ToString();
                }
            }
        }
        private void SetTextBoxValue(TextBox[] tb, Suduku_Model su)
        {
            for (int i = 0; i < 81; i++)
            {
                if (su.Blanks[i].Value != 0)
                {
                    tb[i].FontSize = 20;
                    tb[i].Text = su.Blanks[i].Value.ToString();
                }
                else
                {
                    tb[i].FontSize = 10;
                    string txt = "";
                    for (int j = 0; j < su.Blanks[i].iValues.Count; j++)
                    {
                        txt += su.Blanks[i].iValues[j];
                    }
                    tb[i].Text = txt;
                }
            }
        }
    }
}
