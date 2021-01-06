using Buildersoft.Andy.X.Client;
using Buildersoft.Andy.X.Streams;
using Buildersoft.Andy.X.Streams.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using UP.MCU.RealtimeReporting.Models.Time;

namespace UP.MCU.RealtimeReporting.Services.Time
{
    public class EventInboundService
    {
        private readonly Source<Event> source;
        private readonly Chart vonesaKoheore;
        private List<KeyValuePair<string, int>> chartDataList;

        public EventInboundService(AndyXClient andyXClient, Chart vonesaKoheore)
        {
            this.vonesaKoheore = vonesaKoheore;

            chartDataList = new List<KeyValuePair<string, int>>();
            ((LineSeries)vonesaKoheore.Series[0]).ItemsSource = chartDataList;

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
            chartDataList.Add(new KeyValuePair<string, int>(k.ToString(), Convert.ToInt32(e.RawData.DiferencaNeMilisekonda)));
            if (chartDataList.Count > 30)
                chartDataList.RemoveAt(0);

            ((LineSeries)vonesaKoheore.Series[0]).ItemsSource = null;
            ((LineSeries)vonesaKoheore.Series[0]).ItemsSource = chartDataList;
        }
    }
}
