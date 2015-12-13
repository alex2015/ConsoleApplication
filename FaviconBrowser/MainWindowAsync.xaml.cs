using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FaviconBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowAsync : Window
    {
        private static readonly List<string> s_Domains = new List<string>
        {
            "google.com",
            "bing.com",
            "oreilly.com",
            "simple-talk.com",
            //"microsoft.com",
            "facebook.com",
            "twitter.com",
            "reddit.com",
            "baidu.com",
            "bbc.co.uk"
        };

        public MainWindowAsync()
        {
            InitializeComponent();
        }

        private async void GetButton_OnClick(object sender, RoutedEventArgs e)
        {
            var images = await Task.WhenAll(s_Domains.Select(GetFavicon).ToList());

            foreach (var image in images)
            {
                m_WrapPanel.Children.Add(image);
            }
        }

        private async Task<Image> GetFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = await webClient.DownloadDataTaskAsync("http://" + domain + "/favicon.ico");
            return MakeImageControl(bytes);
        }

        private static Image MakeImageControl(byte[] bytes)
        {
            Image imageControl = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            imageControl.Source = bitmapImage;
            imageControl.Width = 16;
            imageControl.Height = 16;
            return imageControl;
        }
    }
}
