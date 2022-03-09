using System;
using System.Collections.Generic;
using System.Linq;
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
using Малахов.Entity;
using Малахов.Pages.EditPages;
using Малахов.Classes;

namespace Малахов.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            CBFilter.ItemsSource = MediaGrEntities.GetContext().Services.GroupBy(x => x.Title).Select(x => x.Key).ToList();
            CBSort.ItemsSource = DGridOrder.Columns.Select(x=> x.Header).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage.MainFrame.Navigate(new OrderEditPage((sender as Button).DataContext as Order));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage.MainFrame.Navigate(new OrderEditPage());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var orderForRemoving = DGridOrder.SelectedItems.Cast<Order>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие { orderForRemoving.Count()} элементов ? ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MediaGrEntities.GetContext().Orders.RemoveRange(orderForRemoving);
                    MediaGrEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridOrder.ItemsSource = MediaGrEntities.GetContext().Orders.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + ex.StackTrace.ToString());
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridOrder.ItemsSource = MediaGrEntities.GetContext().Orders.ToList();
        }

        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGridOrder.ItemsSource = MediaGrEntities.GetContext().Orders.Where(x => x.Service.Title == CBFilter.SelectedItem.ToString()).ToList();
        }



        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = DGridOrder.ItemsSource.Cast<Order>().ToList();
            switch (CBSort.SelectedItem.ToString())
            {
                case "Клиент":
                   DGridOrder.ItemsSource = list.OrderBy(x => x.Client.Fullname).ToList();
                    break;
                case "Сервис":
                    DGridOrder.ItemsSource = list.OrderBy(x => x.Service.Title).ToList();
                    break;
                case "Сумма":
                    DGridOrder.ItemsSource = list.OrderBy(x => x.Price).ToList();
                   break;
                case "Длительность":
                    DGridOrder.ItemsSource = list.OrderBy(x => x.DurationInSecond).ToList();
                    break;
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = TbSearch.Text.ToLower();
            var list = new List<Order>();
            MediaGrEntities.GetContext().Orders.ToList().ForEach(x =>
            {
                if (x.Service.Title.ToLower().Contains(searchText) ||
                x.Client.Fullname.ToLower().Contains(searchText) ||
                x.Manager.Fullname.ToLower().Contains(searchText) ||
                x.Date.ToString().Contains(searchText))
                    list.Add(x);
            });
            DGridOrder.ItemsSource = list;
        }
    }
}
