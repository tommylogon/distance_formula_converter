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
        private Velocity Speed = new Velocity();
        private Distance Dist = new Distance();
        private Time time = new Time();

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
            time.Value = Double.Parse(txt_Time.Text);

            Dist.Value = Double.Parse(txt_Distance.Text);

            Speed.SetMetSec(Double.Parse(txt_Speed.Text));
        }

        private double GetSpeed()
        {
            Speed.SetMetSec(Dist.Value / time.Value);
            return Speed.GetMetSec();
        }

        private double GetDistance()
        {
            return Dist.Value = Speed.GetMetSec() * time.Value;
        }

        private double GetTime()
        {
            return time.Value = Dist.Value / Speed.GetMetSec();
        }

        private void HandleData(object sender, TextChangedEventArgs e)
        {
            SetValues();

            UpdateWithSelectedvalues();
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
                        time.Value = Double.Parse(tb.Text);
                    }
                    else if (tb.Name.Contains("Distance"))
                    {
                        Dist.Value = Double.Parse(tb.Text);
                    }
                    else if (tb.Name.Contains("Speed"))
                    {
                        Speed.SetMetSec(Double.Parse(tb.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateWithSelectedvalues()
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
                txt_Speed.Text = Speed.GetKmH().ToString("0.###");
            }
            else if (PrevValue.CaseInsensitiveContains("km/h") && selectedValue.CaseInsensitiveContains("m/s"))
            {
                txt_Speed.Text = Speed.GetMetSec().ToString("0.###");
            }

            Speed.Format = selectedValue;
        }

        private void DistanceConversion(string selectedValue)
        {
            if (PrevValue.CaseInsensitiveContains("meter") && selectedValue.CaseInsensitiveContains("km"))
            {
                Dist.Value /= 1000;
            }
            else if (PrevValue.CaseInsensitiveContains("km") && selectedValue.CaseInsensitiveContains("meter"))
            {
                Dist.Value *= 1000;
            }

            Dist.Format = selectedValue;

            txt_Distance.Text = Dist.Value.ToString("0.###");
        }

        private void TimeConvertion(string selectedValue)
        {
            if (PrevValue.CaseInsensitiveContains("second") && selectedValue.CaseInsensitiveContains("minute") || PrevValue.CaseInsensitiveContains("minute") && selectedValue.CaseInsensitiveContains("hour") || PrevValue.CaseInsensitiveContains("hour") && selectedValue.CaseInsensitiveContains("minute"))
            {
                time.Value /= 60;
            }
            else if (PrevValue.CaseInsensitiveContains("second") && selectedValue.CaseInsensitiveContains("hour"))
            {
                time.Value /= 3600;
            }
            else if (PrevValue.CaseInsensitiveContains("minute") && selectedValue.CaseInsensitiveContains("second"))
            {
                time.Value *= 60;
            }
            else if (PrevValue.CaseInsensitiveContains("hour") && selectedValue.CaseInsensitiveContains("second"))
            {
                time.Value *= 3600;
            }

            time.Format = selectedValue;

            txt_Time.Text = time.Value.ToString("0.###");
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