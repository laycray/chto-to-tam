using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TextInput
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //таймер
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();
        //коллекция вопросов
        List<ModelGame> listLevels = new List<ModelGame>();
        //параметры бегущей строки
        double top, bottom;
        //выбранный индекс из коллекции
        int index;
        //текст взятый из коллекции для редактирования
        string text;
        //параметры проверки
        int mistake = 0, successful = 0, speedInput = 0, time = 0;
        Thickness th;

        public MainWindow()
        {
            InitializeComponent();
            AddData _getData = new AddData();
            _getData.listLevels(listLevels);
            SetParams _setParams = new SetParams(listLevels);
            _setParams.ShowDialog();
            index = _setParams.Index;
            mainGrid.Children.Remove(buttonRestart);
            label.Content = text = listLevels[index].Text;
            labelLevel.Content = listLevels[index].Level;
            labelSpeedTick.Content = listLevels[index].TimeTick;
            th = label.Margin;
            top = label.Margin.Top;
            bottom = label.Margin.Bottom;

            timer1.Tick += Timer1_Tick;
            timer1.Interval = new TimeSpan(0, 0, 1);

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0,listLevels[index].TimeTick);
        }

        private void GetResult()
        {
            labelTime.Content = DateTime.Parse("00:00:00").AddSeconds((double)time).ToString("mm:ss");
            labelMist.Content = mistake;
            try
            {
                labelSpeedInput.Content = successful * 60 / time;
            }
            catch
            {
                labelSpeedInput.Content = 0;
            }
                labelSucc.Content = successful;
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timer1.Stop();
            label.Margin = th;
            label.Content = text = listLevels[index].Text;
            mistake = successful = time = 0;
            GetResult();
            mainGrid.Children.Add(buttonStart);
            mainGrid.Children.Remove(buttonRestart);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            time++;
            GetResult();
        }

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == text[0].ToString())
            {
                successful++;
                text = text.Remove(0, 1);
                label.Content = text;
                label.Margin = new Thickness(label.Margin.Left + 5, top, label.Margin.Right - 5, bottom);
            }
            else
                mistake++;


            if(text.Length==0)
            {
                timer.Stop();
                timer1.Stop();
                MessageBox.Show("Вы Выиграли:)\n Вы успешно справились с поставленной задачей!", "Радостное известие!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            timer1.Start();
            timer.Start();
            mainGrid.Children.Remove(buttonStart);
            mainGrid.Children.Add(buttonRestart);
        }

        private void timerTick(object sender, EventArgs e)
        {
            Thread thread = new Thread(ChangePosition);
            thread.Start();
                if (label.Margin.Left<=0)
            {
                timer.Stop();
                timer1.Stop();
                MessageBox.Show("Вы проиграли:(\n Вы не справились с поставленной задачей..", "Печальная новость", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void ChangePosition()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                label.Margin = new Thickness(label.Margin.Left - 1, top, label.Margin.Right + 1, bottom);
            }));
        }
    }
}
