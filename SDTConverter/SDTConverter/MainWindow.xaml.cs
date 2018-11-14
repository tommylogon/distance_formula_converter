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

namespace SDTConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double Speed { get; set; } = 0.0;
        private double Time { get; set; } = 0.0;
        private double Distance { get; set; } = 0.0;

        private bool TimeChanged = false;
        private bool SpeedChanged = false;
        private bool DistanceChanged = false;

        private List<TextBox> tbList = new List<TextBox>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private double GetSpeed()
        {
            Speed = Distance / Time;
            return Speed;
        }

        private double GetDistance()
        {
            Distance = Speed * Time;
            return Distance;
        }

        private double GetTime()
        {
            Time = Distance / Speed;
            return Time;
        }

        private void HandleData(object sender, TextChangedEventArgs e)
        {
            SetValues();
        }

        private void ChangeWhatIsTrue(TextBox tb, bool isTrue)
        {
            if (tb.Name.Contains("Time"))
            {
                TimeChanged = isTrue;
            }
            else if (tb.Name.Contains("Distance"))
            {
                DistanceChanged = isTrue;
            }
            else if (tb.Name.Contains("Speed"))
            {
                SpeedChanged = isTrue;
            }
        }

        private void SetValues()
        {
            try
            {
                foreach (TextBox tb in tbList)
                {
                    if (tb.Name.Contains("Time"))
                    {
                        Time = Double.Parse(tb.Text);
                    }
                    else if (tb.Name.Contains("Distance"))
                    {
                        Distance = Double.Parse(tb.Text);
                    }
                    else if (tb.Name.Contains("Speed"))
                    {
                        Speed = Double.Parse(tb.Text);
                    }
                }
                if (TimeChanged & DistanceChanged)
                {
                    txt_Speed.Text = GetSpeed().ToString();
                }
                else if (DistanceChanged & SpeedChanged)
                {
                    txt_Time.Text = GetTime().ToString();
                }
                else if (SpeedChanged & TimeChanged)
                {
                    txt_Distance.Text = GetDistance().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tbList.Count == 2)
            {
                foreach (TextBox tbinlist in tbList)
                {
                    if (tb.Name == tbinlist.Name)
                    {
                        return;
                    }
                }
                ChangeWhatIsTrue(tbList[0], false);
                tbList.RemoveAt(0);
            }
            else
            {
                tbList.Add(tb);
                ChangeWhatIsTrue(tb, true);
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string content = (string)((ComboBoxItem)((ComboBox)sender).SelectedItem).Content;
            if (content?.Contains("m/s") == true)
            {
                txt_Speed.Text = (Speed * 3600).ToString();
            }
            else if (content?.Contains("Km/H") == true)
            {
                txt_Speed.Text = (Speed / 3600).ToString();
            }
            else if (content?.Contains("Sec") == true)
            {
                txt_Time.Text = (Time * 60).ToString();
            }
            else if (content?.Contains("Min") == true)
            {
                txt_Time.Text = (Time / 60).ToString();
            }
            else if (content?.Contains("Hour") == true)
            {
                txt_Time.Text = (Time / 60).ToString();
            }
            else if (content?.Contains("Met") == true)
            {
                txt_Distance.Text = (Distance * 1000).ToString();
            }
            else if (content?.Contains("Km") == true)
            {
                txt_Distance.Text = (Distance / 1000).ToString();
            }
        }
    }
}