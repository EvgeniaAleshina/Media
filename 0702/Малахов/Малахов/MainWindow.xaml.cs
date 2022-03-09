﻿using System;
using System.Windows;
using Малахов.Classes;
using Малахов.Pages;
using Малахов.Pages.EditPages;

namespace Малахов
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new aut());
            ManagerPage.MainFrame = MainFrame;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (ManagerPage.MainFrame.Content.ToString().Contains("aut") || ManagerPage.MainFrame.Content.ToString().Contains("reg"))
            {
                MainMenu.Visibility = Visibility.Collapsed;
                BtnBack.Visibility = Visibility.Hidden;
            }
            else
            {
                MainMenu.Visibility = Visibility.Visible;
                BtnBack.Visibility = Visibility.Visible;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new ClientEditPage());

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new ServiceEditPage());

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new TypesEditPages());

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new aut());

        private void MenuItem_Click_4(object sender, RoutedEventArgs e) => ManagerPage.Navigate(new OrderEditPage());

    }
}
