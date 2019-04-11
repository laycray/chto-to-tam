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
using System.Windows.Shapes;

namespace TextInput
{
    /// <summary>
    /// Логика взаимодействия для SetParams.xaml
    /// </summary>
    public partial class SetParams : Window
    {
        public int Index;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Thickness th;
        int speed = 100;
        List<ModelGame> listData;
        public SetParams(List<ModelGame> listData)
        {
            InitializeComponent();
            this.listData = listData;
            for (int i = 0; i < listData.Count; i++)
                comboBox.Items.Add(listData[i].Level);
            th = exampleLable.Margin;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, speed);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            exampleLable.Margin = new Thickness(exampleLable.Margin.Left-1, exampleLable.Margin.Top, exampleLable.Margin.Right+1, exampleLable.Margin.Bottom);
            if(exampleLable.Margin.Left<-5)
            {
                double rightPos = 0 + exampleLable.Width;
                exampleLable.Margin = th;
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                speed = int.Parse(textBox.Text);
                timer.Interval = new TimeSpan(0, 0, 0, 0, speed);
            }
            catch { }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox.Text = listData[comboBox.SelectedIndex].TimeTick.ToString();
            textBox1.Text = listData[comboBox.SelectedIndex].Text;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonGo_Click(object sender, RoutedEventArgs e)
        {
            if(comboBox.SelectedItem!=null)
            {
                Index = comboBox.SelectedIndex;
                try
                {
                    listData[Index].TimeTick = int.Parse(textBox.Text);
                }
                catch { }
                if(string.IsNullOrWhiteSpace(textBox1.Text))
                     listData[Index].Text = textBox1.Text;
            }
            this.Close();
        }
    }
}
