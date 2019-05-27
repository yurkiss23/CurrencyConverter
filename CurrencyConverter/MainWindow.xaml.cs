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
            //foreach (var item in currencyList)
            //{
            //    MessageBox.Show(item.base_ccy + ":" + item.ccy + ":" + item.buy + ":" + item.sale);
            //}

            foreach(var item in currencyList)
            {
                if((string)cbChange.SelectedValue==item.base_ccy || (string)cbChange.SelectedValue==item.ccy)
                {
                    if ((string)cbGet.SelectedValue == item.base_ccy || (string)cbGet.SelectedValue == item.ccy)
                    {
                        MessageBox.Show(cbChange.SelectedValue.ToString() + ":" + cbGet.SelectedValue.ToString());
                        txtGet.Text = Math.Round((Decimal.Parse(txtChange.Text) / item.sale), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    return;
                }
            }
        }
    }
}
