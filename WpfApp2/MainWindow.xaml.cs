﻿using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly KeyHelper _keyHelper = new KeyHelper();

        private string _decoded = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            _keyHelper.KeyDown += KeysDows;

        }
        private static string ReplaceCode(string code)
        {
            var sb = new StringBuilder(code);
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
        private void KeysDows(object sender, KeyEventArgs e)
        {
            if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) return;
            // Преобразовывем массив в нормальный вид
            textBox1.AppendText(char.ToString((char)(e.KeyCode - Keys.NumPad0 + '0')));
            // 10 символов === 20 символам
            if (textBox1.Text.Length != 20) return;
            for (var i = 6; i < textBox1.Text.Length; i += 2)
            {
                _decoded += ReplaceCode(textBox1.Text.Substring(i, 2));
            }
            // Дальше делаем какую-то работу с <decoded>
            SizeText.Content = _decoded;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _ = System.Windows.MessageBox.Show(_decoded);
        }
    }
}
