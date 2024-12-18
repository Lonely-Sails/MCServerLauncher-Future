﻿using System;
using System.Windows;
using iNKORE.UI.WPF.Modern.Controls;
using MCServerLauncher.WPF.Modules;
using ConsoleWindow = MCServerLauncher.WPF.InstanceConsole.Window;

namespace MCServerLauncher.WPF.View.Pages
{
    /// <summary>
    ///    HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void ShowConsoleWindow(object sender, RoutedEventArgs e)
        {
            new ConsoleWindow().Show();
        }
        private void ShowExceptionWindow(object sender, RoutedEventArgs e)
        {
            throw new Exception("Test Exception");
        }

        private void PushSimpleNotification(object sender, RoutedEventArgs e)
        {
            Notification.PushNotification("Title", "Message", true, InfoBarSeverity.Informational);
        }
    }
}