using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using SampleProject.Backend.Model;
using WebUrlSampleParser.Backend.Parsers;


namespace Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void GetAlbum(object sender, RoutedEventArgs e)
        {
            var request = await AlbumTracklistSelector.Select(LinkBox.Text);
            ListView listView = new ListView();
            BitmapImage bitmap = new BitmapImage();
            StackPanel st = new StackPanel();
            TextBlock tb = new TextBlock();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(request[0].ImageLink, UriKind.Absolute);
            bitmap.EndInit();
            Image image = new Image();
            image.Height = 100;
            image.Width = 100;
            image.Source = bitmap;
            tb.Text = request.First().Album;
            tb.FontSize = 20;
            st.Children.Add(image);
            st.Children.Add(tb);
            foreach (var obj in request)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Content = obj.ToString();
                lvi.Tag = obj;
                listView.Items.Add(lvi);
            }
            st.Children.Add(listView);
            Tabs.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "TrackList" }, // установка заголовка вкладки
                Content = st // установка содержимого вкладки
                
            });
        }
        private async void GetChart(object sender, RoutedEventArgs e)
        {
            var request = await ChartAlbumlistSelector.Select(LinkBox.Text);
            ListView listView = new ListView();
            foreach (var obj in request)
            {
                ListViewItem lvi = new ListViewItem();
                /*BitmapImage bitmap = new BitmapImage();
                StackPanel st = new StackPanel();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(obj.ImageLink, UriKind.Absolute);
                bitmap.EndInit();
                Image image = new Image();
                image.Height = 15;
                image.Width = 15;
                image.Source = bitmap;
                st.Children.Add(image);
                TextBlock albumInfo = new TextBlock();
                albumInfo.Text = obj.ToString();
                st.Children.Add(albumInfo);
                st.Height = 20;
                st.Width = 1440;
                lvi.Content = st;
                lvi.Tag = obj;*/
                lvi.Content = obj.ToString();
                listView.Items.Add(lvi);
            }
            Tabs.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Chart" }, // установка заголовка вкладки
                Content = listView // установка содержимого вкладки
                
            });
        }
        private async void GetList(object sender, RoutedEventArgs e)
        {
            var request = await AlbumlistSelector.Select(LinkBox.Text);
            ListView listView = new ListView();
            StackPanel st = new StackPanel();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(request[0].ListImg, UriKind.Absolute);
            bitmap.EndInit();
            Image image = new Image();
            image.Height = 100;
            image.Width = 100;
            image.Source = bitmap;
            TextBlock tb = new TextBlock();
            tb.Text = request.First().CustomList;
            tb.FontSize = 20;
            st.Children.Add(image);
            st.Children.Add(tb);
            foreach (var obj in request)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Content = obj.ToString();
                listView.Items.Add(lvi);
            }
            st.Children.Add(listView);
            Tabs.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "List" }, // установка заголовка вкладки
                Content = st // установка содержимого вкладки
                
            });
        }
    }
}