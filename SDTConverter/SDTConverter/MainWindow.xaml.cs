using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
        private string SelectedTimeFormat;
        private string SelectedDistanceFormat;
        private string SelectedVelocityFormat;

        private List<TextBox> tbList = new List<TextBox>();

        public MainWindow()
        {
            InitializeComponent();

            SetInitialStates();
        }

        private void SetInitialStates()
        {
            time.SetSecounds(Double.Parse(txt_Time.Text));

            Dist.SetMeters(Double.Parse(txt_Distance.Text));

            Speed.SetMetSec(Double.Parse(txt_Speed.Text));
        }

        private double GetSpeed()
        {
            Speed.SetMetSec(Dist.GetMeters() / time.GetSeconds());
            return Speed.GetMetSec();
        }

        private double GetDistance()
        {
            Dist.SetMeters(Speed.GetMetSec() * time.GetSeconds());
            return Dist.GetMeters();
        }

        private double GetTime()
        {
            time.SetSecounds(Dist.GetMeters() / Speed.GetMetSec());
            return time.GetSeconds();
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
                        if (SelectedTimeFormat.CaseInsensitiveContains("second"))
                        {
                            time.SetSecounds(Double.Parse(tb.Text));
                        }
                        else if (SelectedTimeFormat.CaseInsensitiveContains("minute"))
                        {
                            time.Setminutes(Double.Parse(tb.Text));
                        }
                        else if (SelectedTimeFormat.CaseInsensitiveContains("hour"))
                        {
                            time.SetHours(Double.Parse(tb.Text));
                        }
                    }
                    else if (tb.Name.Contains("Distance"))
                    {
                        if (SelectedDistanceFormat.CaseInsensitiveContains("meter"))
                        {
                            Dist.SetMeters(Double.Parse(tb.Text));
                        }
                        else if (SelectedDistanceFormat.CaseInsensitiveContains("km"))
                        {
                            Dist.SetKm(Double.Parse(tb.Text));
                        }
                    }
                    else if (tb.Name.Contains("Speed"))
                    {
                        if (SelectedVelocityFormat.CaseInsensitiveContains("m/s"))
                        {
                            Speed.SetMetSec(Double.Parse(tb.Text));
                        }
                        else if (SelectedVelocityFormat.CaseInsensitiveContains("km/h"))
                        {
                            Speed.SetKmH(Double.Parse(tb.Text));
                        }
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
            if (selectedValue.CaseInsensitiveContains("km/h"))
            {
                txt_Speed.Text = Speed.GetKmH().ToString("0.###");
            }
            else if (selectedValue.CaseInsensitiveContains("m/s"))
            {
                txt_Speed.Text = Speed.GetMetSec().ToString("0.###");
            }

            SelectedVelocityFormat = selectedValue;
        }

        private void DistanceConversion(string selectedValue)
        {
            if (PrevValue.CaseInsensitiveContains("meter") && selectedValue.CaseInsensitiveContains("km"))
            {
                txt_Distance.Text = Dist.GetKm().ToString("0.###");
            }
            else if (PrevValue.CaseInsensitiveContains("km") && selectedValue.CaseInsensitiveContains("meter"))
            {
                txt_Distance.Text = Dist.GetMeters().ToString("0.###");
            }

            SelectedDistanceFormat = selectedValue;
        }

        private void TimeConvertion(string selectedValue)
        {
            if (selectedValue.CaseInsensitiveContains("minute"))
            {
                txt_Time.Text = time.GetMinutes().ToString("0.###");
            }
            else if (selectedValue.CaseInsensitiveContains("hour"))
            {
                txt_Time.Text = time.GetHours().ToString("0.###");
            }
            else if (selectedValue.CaseInsensitiveContains("second"))
            {
                txt_Time.Text = time.GetSeconds().ToString("0.###");
            }
            SelectedTimeFormat = selectedValue;
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