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
        public object Assert { get; private set; }

        private bool TimeChanged = false;
        private bool SpeedChanged = false;
        private bool DistanceChanged = false;

        private string PrevValue;

        private List<TextBox> tbList = new List<TextBox>();

        public MainWindow()
        {
            InitializeComponent();
            SetInitialStates();
        }

        private void SetInitialStates()
        {
            Time = Double.Parse(txt_Time.Text);

            Distance = Double.Parse(txt_Distance.Text);

            Speed = Double.Parse(txt_Speed.Text);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WhatToUpdate()
        {
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

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string whatIsSelected = "You have selected ";

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

            tbList.Add(tb);
            ChangeWhatIsTrue(tb, true);

            foreach (TextBox tbSelected in tbList)
            {
                whatIsSelected += tbSelected.Name + ", ";
            }
            lbl_WhatIsSelected.Content = whatIsSelected;
        }

        private void SpeedConversion(string selectedValue)
        {
            if (PrevValue.CaseInsensitiveContains("m/s") && selectedValue.CaseInsensitiveContains("km/h"))
            {
                Speed *= 3.6;
            }
            else if (PrevValue.CaseInsensitiveContains("km/h") && selectedValue.CaseInsensitiveContains("m/s"))
            {
                Speed /= 3.6;
            }
            txt_Speed.Text = Speed.ToString("0.###");
        }

        private void DistanceConversion(string selectedValue)
        {
            if (PrevValue.CaseInsensitiveContains("meter") && selectedValue.CaseInsensitiveContains("km"))
            {
                Distance /= 1000;
            }
            else if (PrevValue.CaseInsensitiveContains("km") && selectedValue.CaseInsensitiveContains("meter"))
            {
                Distance *= 1000;
            }
            txt_Distance.Text = Distance.ToString("0.###");
        }

        private void TimeConvertion(string selectedValue)
        {
            if (PrevValue.CaseInsensitiveContains("second") && selectedValue.CaseInsensitiveContains("minute") || PrevValue.CaseInsensitiveContains("minute") && selectedValue.CaseInsensitiveContains("hour") || PrevValue.CaseInsensitiveContains("hour") && selectedValue.CaseInsensitiveContains("minute"))
            {
                Time /= 60;
            }
            else if (PrevValue.CaseInsensitiveContains("second") && selectedValue.CaseInsensitiveContains("hour"))
            {
                Time /= 3600;
            }
            else if (PrevValue.CaseInsensitiveContains("minute") && selectedValue.CaseInsensitiveContains("second"))
            {
                Time *= 60;
            }
            else if (PrevValue.CaseInsensitiveContains("hour") && selectedValue.CaseInsensitiveContains("second"))
            {
                Time *= 3600;
            }

            txt_Time.Text = Time.ToString("0.###");
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = (string)((ComboBoxItem)((ComboBox)sender).SelectedItem).Content;

            if (PrevValue != null)
            {
                if (((ComboBox)sender).Name == "cb_Speed")
                {
                    SpeedConversion(selectedValue);
                }
                else if (((ComboBox)sender).Name == "cb_Distance")
                {
                    DistanceConversion(selectedValue);
                }
                else if (((ComboBox)sender).Name == "cb_Time")
                {
                    TimeConvertion(selectedValue);
                }
            }
        }

        private void CB_Got_Focus(object sender, RoutedEventArgs e)
        {
            try
            {
                PrevValue = (string)((ComboBoxItem)((ComboBox)sender).SelectedItem).Content;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}