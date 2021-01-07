using Buildersoft.Andy.X.Client;
using Buildersoft.Andy.X.Streams;
using Buildersoft.Andy.X.Streams.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using UP.MCU.RealtimeReporting.Models.Time;

namespace UP.MCU.RealtimeReporting.Services.Time
{
    public class EventInboundService
    {
        private readonly Source<Event> source;
        private readonly Chart vonesaKoheore;
        private readonly ListBox listOfResultsBox;
        private readonly Label lblVkMes;
        private readonly Label lblVkMax;
        private readonly Label lblVkMin;
        private List<KeyValuePair<string, int>> chartDataList;
        private List<double> datas;

        public EventInboundService(AndyXClient andyXClient, Chart vonesaKoheore, ListBox listOfResultsBox, Label lblVkMes, Label lblVkMax, Label lblVkMin)
        {
            this.vonesaKoheore = vonesaKoheore;
            this.listOfResultsBox = listOfResultsBox;
            this.lblVkMes = lblVkMes;
            this.lblVkMax = lblVkMax;
            this.lblVkMin = lblVkMin;
            chartDataList = new List<KeyValuePair<string, int>>();
            ((LineSeries)vonesaKoheore.Series[0]).ItemsSource = chartDataList;
            datas = new List<double>();

            var reader = new Reader<Event>(andyXClient.GetClient())
               .Component(Event.ComponentName)
               .Book(Event.BookName)
               .ReaderType(Buildersoft.Andy.X.Client.Model.ReaderTypes.Exclusive)
               .ReaderName("reporting-vonesa-koheore-pc")
               .ReaderAs(Buildersoft.Andy.X.Client.Model.ReaderAs.Subscription)
               .Build();

            source = new Source<Event>(reader)
                .Configure(new Buildersoft.Andy.X.Streams.Settings.SourceConfigurationSettings())
                .Throttle(1, 1);

            source.Integration += Source_Integration;
            source.InitializeReader();
        }
        int k = 0;
        private void Source_Integration(object sender, Data<Event> e)
        {
            k++;
            chartDataList.Add(new KeyValuePair<string, int>(k.ToString(), Convert.ToInt32(Math.Abs(e.RawData.DiferencaNeMilisekonda))));
            listOfResultsBox.Items.Add($"Vk({k}) = {Math.Abs(e.RawData.DiferencaNeMilisekonda)} ms");
            listOfResultsBox.ScrollIntoView(listOfResultsBox.Items[listOfResultsBox.Items.Count - 1]);

            datas.Add(Math.Abs(e.RawData.DiferencaNeMilisekonda));
            if (chartDataList.Count > 30)
                chartDataList.RemoveAt(0);

            ((LineSeries)vonesaKoheore.Series[0]).ItemsSource = null;
            ((LineSeries)vonesaKoheore.Series[0]).ItemsSource = chartDataList;

            lblVkMes.Content = $"Vk(mes) = {datas.Average()} ms";
            lblVkMax.Content = $"Vk(max) = {datas.Max()} ms";
            lblVkMin.Content = $"Vk(min) = {datas.Min()} ms";
        }
    }
}
