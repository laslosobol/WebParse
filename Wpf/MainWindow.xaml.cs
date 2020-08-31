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
            var gridView = new GridView();
            listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Title", DisplayMemberBinding = new Binding("Title") });
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Artist", DisplayMemberBinding = new Binding("Artist") });
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Album", DisplayMemberBinding = new Binding("Album") });
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Duration", DisplayMemberBinding = new Binding("Duration") });
            foreach (var obj in request)
            {
                listView.Items.Add(obj);
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
            var gridView = new GridView();
            listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Title", DisplayMemberBinding = new Binding("Title") });
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Artist", DisplayMemberBinding = new Binding("Artist") });
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Release Date", DisplayMemberBinding = new Binding("ReleaseDate") });
            foreach (var obj in request)
            {
                listView.Items.Add(obj);
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
            var gridView = new GridView();
            listView.View = gridView;
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Title", DisplayMemberBinding = new Binding("Title") });
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Artist", DisplayMemberBinding = new Binding("Artist") });
            gridView.Columns.Add(new GridViewColumn { 
                Header = "Release Date", DisplayMemberBinding = new Binding("ReleaseDate") });
            foreach (var obj in request)
            {
                listView.Items.Add(obj);
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