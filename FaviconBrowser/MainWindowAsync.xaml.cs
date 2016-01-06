using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

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
            var allTask = Task.WhenAll(s_Domains.Select(GetFaviconAsync).ToList());

            Image[] images = null;

            try
            {
                images = await allTask;
            }
            catch
            {
                foreach (Exception ex in allTask.Exception.InnerExceptions)
                {
                    // Обработать исключение
                }
            }

            foreach (var image in images)
            {
                m_WrapPanel.Children.Add(image);
            }
        }

        private async Task<Image> GetFaviconAsync(string domain)
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

        private async void GetSum_OnClick(object sender, RoutedEventArgs e)
        {
            var s = await GetSum(1000000);
            LblSum.Content = s;
        }

        private async Task<long> GetSum(long a)
        {
            return await Task.Run(() =>
            {
                long s = 0;
                for (int i = 0; i < a; i++)
                {
                    s += i;
                }

                return s;
            });
        }




        private async Task<int> MyMethod()
        {
            return 5;
        }

        private async Task<string> FetchFirstSuccessfulAsync(IEnumerable<string> urls)
        {
            // ЧТО ДЕЛАТЬ: проверить, что действительно получены адреса URL...
            foreach (string url in urls)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        return await client.GetStringAsync(url);
                    }
                }
                catch (WebException exception)
                {
                    // ЧТО ДЕЛАТЬ: занести в журнал, обновить статистику и т.д.
                }
            }

            throw new WebException("No URLs succeeded"); // URL не получены
        }

        private async Task<string> FetchFirstSuccessfulAsyncMY(IEnumerable<string> urls)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    return await client.GetStringAsync(urls.First());
                }
            }
            catch (WebException exception)
            {
                // ЧТО ДЕЛАТЬ: занести в журнал, обновить статистику и т.д.
            }

            try
            {
                using (var client = new HttpClient())
                {
                    return await client.GetStringAsync(urls.Last());
                }
            }
            catch (WebException exception)
            {
                // ЧТО ДЕЛАТЬ: занести в журнал, обновить статистику и т.д.
            }

            throw new WebException("No URLs succeeded"); // URL не получены
        }
    }
}
