using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Малахов.Entity;
using Малахов.Classes;

namespace Малахов.Pages
{
	public partial class aut : Page
	{
		public aut() => InitializeComponent();

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Login.Text) && MediaGrEntities.GetContext().Users.Any(x => x.login == Login.Text))
			{
				if (MediaGrEntities.GetContext().Users.Any(x => x.login == Login.Text && x.password == Password.Password))
                {
					var user = MediaGrEntities.GetContext().Users.First(x => x.login == Login.Text && x.password == Password.Password);
					Data.Access = user.Access;
					Data.UserID = user.ID;
                    Classes.ManagerPage.MainFrame.Navigate(new Menu());
				}
				else
					MessageBox.Show("Пароль не верный!");
			}
			else
				MessageBox.Show("Такого пользователя не существует!");
		}
		private void Button_Click_1(object sender, RoutedEventArgs e) => Classes.ManagerPage.MainFrame.Navigate(new reg());
	}
}