using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Entity;

namespace Малахов.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для OrderEditPage.xaml
    /// </summary>
    public partial class OrderEditPage : Page
    {
        private Order _currentServiceOrder;
        public OrderEditPage()
        {
            InitializeComponent();
            _currentServiceOrder = new Order();
            _currentServiceOrder.Date = DateTime.Now;
            _currentServiceOrder.Manager = MediaGrEntities.GetContext().Managers.First(x => x.IDUser == Data.UserID);
            getComboBox();
            DataContext = _currentServiceOrder;
        }
        public OrderEditPage(Order sv)
        {
            InitializeComponent();
            _currentServiceOrder = sv;
            getComboBox();
            DataContext = _currentServiceOrder;
        }
        public void getComboBox()
        {
            CBClient.ItemsSource = MediaGrEntities.GetContext().Clients.ToList();
            CBService.ItemsSource = MediaGrEntities.GetContext().Services.ToList();
            CBManager.ItemsSource = MediaGrEntities.GetContext().Managers.ToList();
        }

        private void BntSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBCount.Text) || CBService.SelectedItem == null || string.IsNullOrWhiteSpace(TbDuration.Text)) return;
            if(_currentServiceOrder.ID == 0)
            MediaGrEntities.GetContext().Orders.Add(_currentServiceOrder);
            try
            {
                MediaGrEntities.GetContext().SaveChanges();
                MessageBox.Show("Вы добавили заказ");
                ManagerPage.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void TBCountInSecond_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBCount.Text) || CBService.SelectedItem == null || string.IsNullOrWhiteSpace(TbDuration.Text)) return;
            var cost = MediaGrEntities.GetContext().Services.ToList().First(x => x.Title == (CBService.SelectedItem as Service).Title).Cost;
            var count = int.Parse(TBCount.Text);
            var duration = int.Parse(TbDuration.Text);
            var sum = (cost * duration * count).ToString().Replace(',', '.');
            TBSum.Text = $"{sum}";
            _currentServiceOrder.Price = int.Parse(sum.Split('.')[0]);

        }
    }
}
