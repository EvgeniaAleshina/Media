using System;
using System.Windows;
using System.Windows.Controls;
using Малахов.Classes;
using Малахов.Entity;

namespace Малахов
{
    /// <summary>
    /// Логика взаимодействия для ServiceEditPage.xaml
    /// </summary>
    public partial class ServiceEditPage : Page
	{
		private readonly Service _currentService;
		public ServiceEditPage()
		{
			InitializeComponent();
			_currentService = new Service();
			DataContext = _currentService;
        }
		public ServiceEditPage(Service sv)
		{
			InitializeComponent();
			_currentService = sv;
			DataContext = _currentService;
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
            if (string.IsNullOrWhiteSpace(TbTitle.Text) || string.IsNullOrWhiteSpace(TbCost.Text))
                MessageBox.Show("Заполните пожалуйста все поля", "", MessageBoxButton.OK);
            else
            {
                if (_currentService.ID == 0) MediaGrEntities.GetContext().Services.Add(_currentService);
                try
                {
                    MediaGrEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                { MessageBox.Show($"{ex.Message}"); }
                MessageBox.Show("Вы успешно добавили/изменили заказчика");
                ManagerPage.MainFrame.GoBack();
            }
        }
	}
}
