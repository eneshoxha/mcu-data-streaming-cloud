using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UP.MCU.Edge.Logic.Providers;
using UP.MCU.Edge.Logic.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UP.MCU.Edge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string machineName;
        private AndyXProvider andyXProvider;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            machineName = Environment.MachineName;
            txtDeviceName.Text = $"Device name: {Environment.MachineName}";

            string url = "https://dev-eu-andyx-buildersoft.azurewebsites.net";
            string tenant = "automatika";
            string product = "mcu-streaming";
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhdXRvbWF0aWthIiwianRpIjoiYTRkOTFiM2EtMWQxYi00YTVjLTgzZWUtODYyOTAyMjk3MDRhIiwiVGVuYW50SWQiOiJkNWFhOTU4My0xMjkxLTQxYmYtYTdjZC05YTM5MmM2NWFjNWQiLCJUZW5hbnQiOiJhdXRvbWF0aWthIiwiU2VjdXJpdHlLZXkiOiJiYmU0NDAwNi1iN2IzLTQzODYtYjA4OS00ZDY3NjRkMzFkNDkiLCJleHAiOjE2MTQ1NDI4NTAsImlzcyI6IkJ1aWxkZXJzb2Z0IiwiYXVkIjoiQW5keVgifQ.WExWilKkKu9CEFeh5Fvqym0T4LoTxRxr25rz6ogJoKE";
            andyXProvider = new AndyXProvider(url, tenant, product, token);
            if (andyXProvider.IsAndyXClientConnected() == true)
            {
                txtConnection.Text = $"Connection: Connected";
                new BaseService(andyXProvider).UseAndyXServices(machineName);
            }
        }
    }
}






