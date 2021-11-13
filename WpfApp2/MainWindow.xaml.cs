using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly KeyHelper keyHelper = new KeyHelper();

        string code = string.Empty;

        string decoded = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            keyHelper.KeyDown += KeysDows;

        }
        private string ReplaceCode(string code)
        {
            StringBuilder sb = new StringBuilder(code);
            sb.Replace("48", "0");
            sb.Replace("49", "1");
            sb.Replace("50", "2");
            sb.Replace("51", "3");
            sb.Replace("52", "4");
            sb.Replace("53", "5");
            sb.Replace("54", "6");
            sb.Replace("55", "7");
            sb.Replace("56", "8");
            sb.Replace("57", "9");

            return sb.ToString().Trim();
        }

        // Для RFId карт через USB считыватель smartec
        private void KeysDows(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            textBox1.AppendText(e.KeyCode.ToString());
            
            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                textBox1.AppendText(char.ToString((char)(e.KeyCode - Keys.NumPad0 + '0')));
                // Записываем в массив
                code += char.ToString((char)(e.KeyCode - Keys.NumPad0 + '0'));

                // 10 символов === 20 символам
                for (int i = 0; i < code.Length; i += 2)
                {
                    decoded += ReplaceCode(code.Substring(i, 2));
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = "48484850494853535048";
            string s2 = "";
            // Пропускаю первые три символа 
            for (int i = 6; i < input.Length; i += 2)
            {
                s2 += ReplaceCode(input.Substring(i, 2));
            }
            //Output: 2105520
            _ = System.Windows.MessageBox.Show(s2);
        }
    }
}
