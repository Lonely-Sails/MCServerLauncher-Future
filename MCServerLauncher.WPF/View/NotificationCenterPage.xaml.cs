﻿using System.Windows;
using iNKORE.UI.WPF.Modern.Controls;

namespace MCServerLauncher.WPF.View
{
    /// <summary>
    ///     NotificationCenterPage.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationCenterPage
    {
        public NotificationCenterPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Send a notification.
        /// </summary>
        /// <param name="title">Title of the notification.</param>
        /// <param name="message">Content of the notification.</param>
        /// <param name="isClosable">Controls whether notifications can be turned off.</param>
        /// <param name="severity">Level.</param>
        public void PushNotification(string title, string message, bool isClosable, InfoBarSeverity severity)
        {
            NotificationContainer.Children.Insert(
                index: 0,
                element: new InfoBar
                    {
                        Title = title,
                        Message = message,
                        Severity = severity,
                        IsClosable = isClosable,
                        IsOpen = true
                    }
                );

        }

        private void TestNotification(object sender, RoutedEventArgs e)
        {
            PushNotification(title: "Test", message: "Test", isClosable: true, severity: InfoBarSeverity.Success);
        }
    }
}
