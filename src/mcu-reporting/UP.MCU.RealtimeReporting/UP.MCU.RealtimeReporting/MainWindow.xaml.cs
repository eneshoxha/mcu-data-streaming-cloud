using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UP.MCU.RealtimeReporting.Providers;
using UP.MCU.RealtimeReporting.Services.Time;

namespace UP.MCU.RealtimeReporting
{
    public partial class MainWindow : Window
    {
        private AndyXProvider andyXProvider;
        private EventInboundService eventInboundService;
        private List<KeyValuePair<string, int>> chartDataList;
        public MainWindow()
        {
            InitializeComponent();

            string url = "https://dev-eu-andyx-buildersoft.azurewebsites.net";
            string tenant = "automatika";
            string product = "mcu-streaming";
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhdXRvbWF0aWthIiwianRpIjoiYTRkOTFiM2EtMWQxYi00YTVjLTgzZWUtODYyOTAyMjk3MDRhIiwiVGVuYW50SWQiOiJkNWFhOTU4My0xMjkxLTQxYmYtYTdjZC05YTM5MmM2NWFjNWQiLCJUZW5hbnQiOiJhdXRvbWF0aWthIiwiU2VjdXJpdHlLZXkiOiJiYmU0NDAwNi1iN2IzLTQzODYtYjA4OS00ZDY3NjRkMzFkNDkiLCJleHAiOjE2MTQ1NDI4NTAsImlzcyI6IkJ1aWxkZXJzb2Z0IiwiYXVkIjoiQW5keVgifQ.WExWilKkKu9CEFeh5Fvqym0T4LoTxRxr25rz6ogJoKE";
            andyXProvider = new AndyXProvider(url, tenant, product, token);
  
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            if (andyXProvider.IsAndyXClientConnected() == true)
            {
                eventInboundService = new EventInboundService(andyXProvider.GetAndyXClient(), chartVonesaKoheore);
            }
        }
    }
}
