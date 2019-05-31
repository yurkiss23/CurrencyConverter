using CurrencyConverter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace CurrencyConverter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> _currList;//, _base_ccyList;
        private string _responce = null;
        public MainWindow()
        {
            InitializeComponent();
            Title = "Converter";
			dpNow.SelectedDate = DateTime.Now;

            _currList = new List<string>();
            //_base_ccyList = new List<string>();
            CurrencyList();

            cbChange.ItemsSource = cbGet.ItemsSource = _currList;
            cbChange.SelectedValue = "UAH";
            cbGet.SelectedValue = "EUR";

            
        }

        public void CurrencyList()
        {
            string url = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";
            using (WebClient wc = new WebClient())
            {
                _responce = wc.DownloadString(url);
                var currencyList = JsonConvert.DeserializeObject<List<CurrencyInfoModel>>(_responce);
                //foreach (var item in currencyList)
                //{
                //    MessageBox.Show(item.base_ccy + ":" + item.ccy + ":" + item.buy + ":" + item.sale);
                //}

                _currList.Add(currencyList.First().base_ccy);
                foreach (var item in currencyList)
                {
                    _currList.Add(item.ccy);
                    //_base_ccyList.Add(item.base_ccy);
                }
            }
        }

        private void mlbd_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("!!!");
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Change");
            var currencyList = JsonConvert.DeserializeObject<List<CurrencyInfoModel>>(_responce);

            if (!string.IsNullOrEmpty(txtChange.Text) && txtChange.Text.All(char.IsDigit))
            {
                switch (cbChange.SelectedIndex)
                {
                    case 0:
                        double digit1 = Convert.ToDouble(txtChange.Text) / double.Parse(currencyList[0].buy.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        txtGet.Text = Convert.ToString(digit1);
                        break;
                    case 1:
                        double digit2 = Convert.ToDouble(txtChange.Text) / double.Parse(currencyList[1].buy.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        txtGet.Text = Convert.ToString(digit2);
                        break;
                    case 2:
                        double digit3 = Convert.ToDouble(txtChange.Text) / double.Parse(currencyList[2].buy.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        txtGet.Text = Convert.ToString(digit3);
                        break;
                }
            }
            else
                txtGet.Text = "...";

            //foreach (var item in currencyList)
            //{
            //    MessageBox.Show(item.base_ccy + ":" + item.ccy + ":" + item.buy + ":" + item.sale);
            //}
            //MessageBox.Show(currencyList.Count().ToString());
            //string condition = cbChange.SelectedValue.ToString() + ":" + cbGet.SelectedValue.ToString();
            //MessageBox.Show(condition);
            //foreach (var itemCng in currencyList)
            //{
            //    //MessageBox.Show("change");
            //    //MessageBox.Show(cbChange.SelectedValue.ToString() + ":" + cbGet.SelectedValue.ToString());
            //    if ((string)cbChange.SelectedValue==itemCng.base_ccy || (string)cbChange.SelectedValue==itemCng.ccy)
            //    {
            //        string left = ((string)cbChange.SelectedValue == itemCng.base_ccy) ? "base_ccy" : "ccy";
            //        string change = ((string)cbChange.SelectedValue == itemCng.base_ccy) ? itemCng.base_ccy : itemCng.ccy;
                    
            //        foreach (var itemGet in currencyList)
            //        {
            //            MessageBox.Show(itemGet.base_ccy+":"+itemGet.ccy);
            //            if ((string)cbGet.SelectedValue == itemGet.base_ccy || (string)cbGet.SelectedValue == itemGet.ccy)
            //            {
            //                string right = ((string)cbGet.SelectedValue == itemGet.base_ccy) ? "base_ccy" : "ccy";
            //                string get = ((string)cbGet.SelectedValue == itemGet.base_ccy) ? itemGet.base_ccy : itemGet.ccy;
                            
            //                string cng = left + ":" + right;
            //                MessageBox.Show(cng);
            //                MessageBox.Show(change + ":" + get);
            //                if ((change + ":" + get) == condition)
            //                {
            //                    switch (cng)
            //                    {
            //                        case "base_ccy:ccy":
            //                            txtGet.Text = Math.Round((Decimal.Parse(txtChange.Text) / itemCng.sale), 2, MidpointRounding.AwayFromZero).ToString();
            //                            break;
            //                        case "base_ccy:base_ccy":
            //                            decimal changer = Math.Round((Decimal.Parse(txtChange.Text) * itemCng.buy), 2, MidpointRounding.AwayFromZero);
            //                            //decimal get = Math.Round((Decimal.Parse(txtGet.Text) / item.sale), 2, MidpointRounding.AwayFromZero);
            //                            txtGet.Text = Math.Round(changer / itemCng.sale, 2, MidpointRounding.AwayFromZero).ToString();
            //                            break;
            //                        case "ccy:base_ccy":
            //                            txtGet.Text = Math.Round((Decimal.Parse(txtChange.Text) * itemCng.buy), 2, MidpointRounding.AwayFromZero).ToString();
            //                            break;
            //                        case "ccy:ccy":
            //                            break;
            //                        default:
            //                            break;
            //                    }
            //                }
            //                //txtGet.Text = Math.Round((Decimal.Parse(txtChange.Text) / item.sale), 2, MidpointRounding.AwayFromZero).ToString();
            //                return;
            //            }
            //        }
            //    }
            //}
        }
    }
}
