using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using week06.Entities;
using week06.MnbServiceReference;

namespace week06
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();



        public Form1()
        {
            InitializeComponent();

            refreshData();
            
            chartRateData.DataSource = Rates;

            comboBox1.DataSource = Currencies;
            currencyLekerdezes();


        }
        private void refreshData() 
        {
            Rates.Clear();
            GetExchangeRates();
            dataGridView1.DataSource = Rates;
            XmlCreate();
            DataOnChart();


        }
        private void currencyLekerdezes()
        {
            var mnbsrevice_c = new MNBArfolyamServiceSoapClient();
            var request_c = new GetCurrenciesRequestBody();
            var response_c = mnbsrevice_c.GetCurrencies(request_c);
            var result_c = response_c.GetCurrenciesResult;
            var xml_c = new XmlDocument();
            xml_c.LoadXml(result_c);
            foreach (XmlElement element in xml_c.DocumentElement) 
            {
                for(int i = 0; i<element.ChildNodes.Count; i++) 
                {
                    var ChildElement_c = (XmlElement)element.ChildNodes[i];
                    string currency = ChildElement_c.InnerText;
                    Currencies.Add(currency);
                
                
                
                }
            
            
            
            
            }
        
        
        
        }
        private void DataOnChart() 
        {
            

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;




        }
        private void XmlCreate() 
        {
            



        }

        string result;
        private void GetExchangeRates()
        {
            
            // A változó deklarációk jobb oldalán a "var" egy dinamikus változó típus.
            // A "var" változó az első értékadás pillanatában a kapott érték típusát veszi fel, és később nem változtatható.
            // Jelen példa első sora tehát ekvivalens azzal, ha a "var" helyélre a MNBArfolyamServiceSoapClient-t írjuk.
            // Ebben a formában azonban olvashatóbb a kód, és változtatás esetén elég egy helyen átírni az osztály típusát.
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = Convert.ToString(comboBox1.SelectedItem),
                startDate = Convert.ToString(dateTimePicker1.Value),
                endDate = Convert.ToString(dateTimePicker2.Value),
            };

            // Ebben az esetben a "var" a GetExchangeRates visszatérési értékéből kapja a típusát.
            // Ezért a response változó valójában GetExchangeRatesResponseBody típusú.
            var response = mnbService.GetExchangeRates(request);

            // Ebben az esetben a "var" a GetExchangeRatesResult property alapján kapja a típusát.
            // Ezért a result változó valójában string típusú.
            var result = response.GetExchangeRatesResult;

            // XML document létrehozása és az aktuális XML szöveg betöltése
            var xml = new XmlDocument();
            xml.LoadXml(result);

            // Végigmegünk a dokumentum fő elemének gyermekein
            foreach (XmlElement element in xml.DocumentElement)
            {
                // Létrehozzuk az adatsort és rögtön hozzáadjuk a listához
                // Mivel ez egy referencia típusú változó, megtehetjük, hogy előbb adjuk a listához és csak később töltjük fel a tulajdonságait
                var rate = new RateData();
                Rates.Add(rate);

                // Dátum
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                // Valuta
                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                // Érték
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            refreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshData();
        }
    }
}
