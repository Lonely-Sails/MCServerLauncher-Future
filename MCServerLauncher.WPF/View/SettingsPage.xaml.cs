﻿using iNKORE.UI.WPF.Modern;
using MCServerLauncher.WPF.Helpers;
using MCServerLauncher.WPF.View.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MCServerLauncher.WPF.View
{
    /// <summary>
    ///     SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            DataContext = this;

            # region Initialize nums
            InitDownloadSourceSelection();
            ResDownload_DownloadThreadCnt.SettingSlider.Value = BasicUtils.AppSettings.Download.ThreadCnt;
            ResDownload_ActionWhenDownloadError.SettingComboBox.SelectedIndex = _actionWhenDownloadErrorList.IndexOf(BasicUtils.AppSettings.Download.ActionWhenDownloadError);
            Instance_ActionWhenDeleteConfirm.SettingComboBox.SelectedIndex = _actionWhenDeleteConfirmList.IndexOf(BasicUtils.AppSettings.Instance.ActionWhenDeleteConfirm);
            More_LauncherTheme.SettingComboBox.SelectedIndex = _themeList.IndexOf(BasicUtils.AppSettings.App.Theme);
            #endregion

            # region Event handler binding
            InstanceCreation_MinecraftJavaAutoAgreeEula.SettingSwitch.Toggled += OnMinecraftJavaAutoAcceptEulaChanged;
            InstanceCreation_MinecraftJavaAutoDisableOnlineMode.SettingSwitch.Toggled += OnMinecraftJavaAutoSwitchOnlineModeChanged;
            InstanceCreation_MinecraftBedrockAutoDisableOnlineMode.SettingSwitch.Toggled += OnMinecraftBedrockAutoSwitchOnlineModeChanged;
            InstanceCreation_UseMirrorForMinecraftForgeInstall.SettingSwitch.Toggled += OnUseMirrorForMinecraftForgeInstallChanged;
            InstanceCreation_UseMirrorForMinecraftNeoForgeInstall.SettingSwitch.Toggled += OnUseMirrorForMinecraftNeoForgeInstallChanged;
            InstanceCreation_UseMirrorForMinecraftFabricInstall.SettingSwitch.Toggled += OnUseMirrorForMinecraftFabricInstallChanged;
            InstanceCreation_UseMirrorForMinecraftQuiltInstall.SettingSwitch.Toggled += OnUseMirrorForMinecraftQuiltInstallChanged;

            ResDownload_DownloadThreadCnt.SettingSlider.ValueChanged += OnDownloadThreadValueChanged;
            ResDownload_ActionWhenDownloadError.SettingComboBox.SelectionChanged += OnActionWhenDownloadErrorSelectionChanged;

            Instance_ActionWhenDeleteConfirm.SettingComboBox.SelectionChanged += OnActionWhenDeleteConfirmIndexSelectionChanged;

            More_LauncherTheme.SettingComboBox.SelectionChanged += OnLauncherThemeIndexSelectionChanged;
            More_FollowStartupForLauncher.SettingSwitch.Toggled += OnFollowStartupForLauncherChanged;
            More_AutoCheckUpdateForLauncher.SettingSwitch.Toggled += OnAutoCheckUpdateForLauncherChanged;
            # endregion

            AboutVersionReplacer.Text = $"Developer Version {Assembly.GetExecutingAssembly().GetName().Version}";
        }

        # region MinecraftJavaAutoAcceptEula
        public bool MinecraftJavaAutoAcceptEula
        {
            get => (bool)GetValue(MinecraftJavaAutoAcceptEulaProperty);
            set => SetValue(MinecraftJavaAutoAcceptEulaProperty, value);
        }
        public static readonly DependencyProperty MinecraftJavaAutoAcceptEulaProperty =
            DependencyProperty.Register(
                "MinecraftJavaAutoAcceptEula",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.InstanceCreation.MinecraftJavaAutoAcceptEula)
            );
        private void OnMinecraftJavaAutoAcceptEulaChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("InstanceCreation.MinecraftJavaAutoAcceptEula", InstanceCreation_MinecraftJavaAutoAgreeEula.SettingSwitch.IsOn);
        }
        # endregion

        # region MinecraftJavaAutoSwitchOnlineMode
        public bool MinecraftJavaAutoSwitchOnlineMode
        {
            get => (bool)GetValue(MinecraftJavaAutoSwitchOnlineModeProperty);
            set => SetValue(MinecraftJavaAutoSwitchOnlineModeProperty, value);
        }
        public static readonly DependencyProperty MinecraftJavaAutoSwitchOnlineModeProperty =
            DependencyProperty.Register(
                "MinecraftJavaAutoSwitchOnlineMode",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.InstanceCreation.MinecraftJavaAutoSwitchOnlineMode)
            );
        private void OnMinecraftJavaAutoSwitchOnlineModeChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("InstanceCreation.MinecraftJavaAutoSwitchOnlineMode", InstanceCreation_MinecraftJavaAutoDisableOnlineMode.SettingSwitch.IsOn);
        }
        # endregion

        # region MinecraftBedrockAutoSwitchOnlineMode
        public bool MinecraftBedrockAutoSwitchOnlineMode
        {
            get => (bool)GetValue(MinecraftBedrockAutoSwitchOnlineModeProperty);
            set => SetValue(MinecraftBedrockAutoSwitchOnlineModeProperty, value);
        }
        public static readonly DependencyProperty MinecraftBedrockAutoSwitchOnlineModeProperty =
            DependencyProperty.Register(
                "MinecraftBedrockAutoSwitchOnlineMode",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.InstanceCreation.MinecraftBedrockAutoSwitchOnlineMode)
            );
        private void OnMinecraftBedrockAutoSwitchOnlineModeChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("InstanceCreation.MinecraftBedrockAutoSwitchOnlineMode", InstanceCreation_MinecraftBedrockAutoDisableOnlineMode.SettingSwitch.IsOn);
        }
        #endregion

        #region UseMirrorForMinecraftForgeInstall
        public bool UseMirrorForMinecraftForgeInstall
        {
            get => (bool)GetValue(UseMirrorForMinecraftForgeInstallProperty);
            set => SetValue(UseMirrorForMinecraftForgeInstallProperty, value);
        }
        public static readonly DependencyProperty UseMirrorForMinecraftForgeInstallProperty =
            DependencyProperty.Register(
                "UseMirrorForMinecraftForgeInstall",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.InstanceCreation.UseMirrorForMinecraftForgeInstall)
            );
        private void OnUseMirrorForMinecraftForgeInstallChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("InstanceCreation.UseMirrorForMinecraftForgeInstall", InstanceCreation_UseMirrorForMinecraftForgeInstall.SettingSwitch.IsOn);
        }
        # endregion

        #region UseMirrorForMinecraftNeoForgeInstall
        public bool UseMirrorForMinecraftNeoForgeInstall
        {
            get => (bool)GetValue(UseMirrorForMinecraftNeoForgeInstallProperty);
            set => SetValue(UseMirrorForMinecraftNeoForgeInstallProperty, value);
        }
        public static readonly DependencyProperty UseMirrorForMinecraftNeoForgeInstallProperty =
            DependencyProperty.Register(
                "UseMirrorForMinecraftNeoForgeInstall",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.InstanceCreation.UseMirrorForMinecraftNeoForgeInstall)
            );
        private void OnUseMirrorForMinecraftNeoForgeInstallChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("InstanceCreation.UseMirrorForMinecraftNeoForgeInstall", InstanceCreation_UseMirrorForMinecraftNeoForgeInstall.SettingSwitch.IsOn);
        }
        # endregion

        #region UseMirrorForMinecraftFabricInstall
        public bool UseMirrorForMinecraftFabricInstall
        {
            get => (bool)GetValue(UseMirrorForMinecraftFabricInstallProperty);
            set => SetValue(UseMirrorForMinecraftFabricInstallProperty, value);
        }
        public static readonly DependencyProperty UseMirrorForMinecraftFabricInstallProperty =
            DependencyProperty.Register(
                "UseMirrorForMinecraftFabricInstall",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.InstanceCreation.UseMirrorForMinecraftFabricInstall)
            );
        private void OnUseMirrorForMinecraftFabricInstallChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("InstanceCreation.UseMirrorForMinecraftFabricInstall", InstanceCreation_UseMirrorForMinecraftFabricInstall.SettingSwitch.IsOn);
        }
        # endregion

        #region UseMirrorForMinecraftQuiltInstall
        public bool UseMirrorForMinecraftQuiltInstall
        {
            get => (bool)GetValue(UseMirrorForMinecraftQuiltInstallProperty);
            set => SetValue(UseMirrorForMinecraftQuiltInstallProperty, value);
        }
        public static readonly DependencyProperty UseMirrorForMinecraftQuiltInstallProperty =
            DependencyProperty.Register(
                "UseMirrorForMinecraftQuiltInstall",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.InstanceCreation.UseMirrorForMinecraftQuiltInstall)
            );
        private void OnUseMirrorForMinecraftQuiltInstallChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("InstanceCreation.UseMirrorForMinecraftQuiltInstall", InstanceCreation_UseMirrorForMinecraftQuiltInstall.SettingSwitch.IsOn);
        }
        # endregion

        # region ResDownloadSource

        private void InitDownloadSourceSelection()
        {
            FastMirrorSrc.IsChecked = BasicUtils.AppSettings.Download.DownloadSource == "FastMirror";
            PolarsMirrorSrc.IsChecked = BasicUtils.AppSettings.Download.DownloadSource == "PolarsMirror";
            ZCloudFileSrc.IsChecked = BasicUtils.AppSettings.Download.DownloadSource == "ZCloudFile";
            MSLAPISrc.IsChecked = BasicUtils.AppSettings.Download.DownloadSource == "MSLAPI";
            MCSLSyncSrc.IsChecked = BasicUtils.AppSettings.Download.DownloadSource == "MCSLSync";
        }
        private void OnResDownloadSourceSelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                BasicUtils.SaveSetting("ResDownload.DownloadSource", ((RadioButton)sender).GetType().GetProperty("Name")?.GetValue(sender).ToString().Replace("Src", ""));
            }
            catch (ArgumentOutOfRangeException)
            {
                // ignored due to mtfk error
            }
        }
        # endregion
        
        #region DownloadThreadValue
        public int DownloadThreadValue
        {
            get => (int)GetValue(DownloadThreadValueProperty);
            set => SetValue(DownloadThreadValueProperty, value);
        }
        public static readonly DependencyProperty DownloadThreadValueProperty =
            DependencyProperty.Register(
                "DownloadThreadValue",
                typeof(int),
                typeof(RangeSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.Download.ThreadCnt)
            );
        private void OnDownloadThreadValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BasicUtils.SaveSetting("ResDownload.ThreadCnt", (int)ResDownload_DownloadThreadCnt.SettingSlider.Value);
        }
        #endregion

        #region ActionWhenDownloadErrorIndex
        private static readonly List<string> _actionWhenDownloadErrorList = new() { "stop", "retry1", "retry3" };
        public static IEnumerable<string> ActionWhenDownloadError { get; } = new List<string>
        {
            "直接停止下载",
            "重试下载 1 次",
            "重试下载 3 次"
        };
        public int ActionWhenDownloadErrorIndex
        {
            get => (int)GetValue(ActionWhenDownloadErrorIndexProperty);
            set => SetValue(ActionWhenDownloadErrorIndexProperty, value);
        }
        public static readonly DependencyProperty ActionWhenDownloadErrorIndexProperty =
            DependencyProperty.Register(
                "ActionWhenDownloadErrorIndex",
                typeof(int),
                typeof(ComboSettingCard),
                new PropertyMetadata(_actionWhenDownloadErrorList.IndexOf(BasicUtils.AppSettings.Download.ActionWhenDownloadError))
            );
        private void OnActionWhenDownloadErrorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BasicUtils.SaveSetting("ResDownload.ActionWhenDownloadError", _actionWhenDownloadErrorList[ResDownload_ActionWhenDownloadError.SettingComboBox.SelectedIndex]);
            }
            catch (ArgumentOutOfRangeException)
            {
                // ignored due to mtfk error
            }
        }
        #endregion

        #region ActionWhenDeleteConfirm
        public static IEnumerable<string> ActionWhenDeleteConfirm { get; } = new List<string>
        {
            "实例名称输入验证",
            "守护进程密钥验证"
        };
        private static readonly List<string> _actionWhenDeleteConfirmList = new() { "name", "key" };
        public int ActionWhenDeleteConfirmIndex
        {
            get => (int)GetValue(ActionWhenDeleteConfirmIndexProperty);
            set => SetValue(ActionWhenDeleteConfirmIndexProperty, value);
        }
        public static readonly DependencyProperty ActionWhenDeleteConfirmIndexProperty =
            DependencyProperty.Register(
                "ActionWhenDeleteConfirmIndex",
                typeof(int),
                typeof(ComboSettingCard),
                new PropertyMetadata(_actionWhenDeleteConfirmList.IndexOf(BasicUtils.AppSettings.Instance.ActionWhenDeleteConfirm))
            );
        private void OnActionWhenDeleteConfirmIndexSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BasicUtils.SaveSetting("Instance.ActionWhenDeleteConfirm", _actionWhenDeleteConfirmList[Instance_ActionWhenDeleteConfirm.SettingComboBox.SelectedIndex]);
            }
            catch (ArgumentOutOfRangeException)
            {
                // ignored due to mtfk error
            }
        }
        #endregion

        #region LauncherTheme
        public static IEnumerable<string> ThemeForApp { get; } = new List<string>
        {
            "跟随系统",
            "浅色",
            "深色"
        };
        private static readonly List<string> _themeList = new() { "auto", "light", "dark" };
        public int LauncherThemeIndex
        {
            get => (int)GetValue(LauncherThemeIndexProperty);
            set => SetValue(LauncherThemeIndexProperty, value);
        }
        public static readonly DependencyProperty LauncherThemeIndexProperty =
            DependencyProperty.Register(
                "LauncherThemeIndex",
                typeof(int),
                typeof(ComboSettingCard),
                new PropertyMetadata(_themeList.IndexOf(BasicUtils.AppSettings.App.Theme))
            );
        private void OnLauncherThemeIndexSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BasicUtils.SaveSetting("App.Theme", _themeList[More_LauncherTheme.SettingComboBox.SelectedIndex]);
                ThemeManager.Current.ApplicationTheme = More_LauncherTheme.SettingComboBox.SelectedIndex switch
                {
                    0 => null,
                    1 => ApplicationTheme.Light,
                    2 => ApplicationTheme.Dark,
                    _ => ThemeManager.Current.ApplicationTheme
                };
            }
            catch (ArgumentOutOfRangeException)
            {
                // ignored due to mtfk error
            }
        }
        #endregion

        # region FollowStartupForLauncher
        public bool FollowStartupForLauncher
        {
            get => (bool)GetValue(FollowStartupForLauncherProperty);
            set => SetValue(FollowStartupForLauncherProperty, value);
        }
        public static readonly DependencyProperty FollowStartupForLauncherProperty =
            DependencyProperty.Register(
                "FollowStartupForLauncher",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.App.FollowStartup)
            );
        private void OnFollowStartupForLauncherChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("App.FollowStartup", More_FollowStartupForLauncher.SettingSwitch.IsOn);
        }
        #endregion

        #region AutoCheckUpdateForLauncher
        public bool AutoCheckUpdateForLauncher
        {
            get => (bool)GetValue(AutoCheckUpdateForLauncherProperty);
            set => SetValue(AutoCheckUpdateForLauncherProperty, value);
        }
        public static readonly DependencyProperty AutoCheckUpdateForLauncherProperty =
            DependencyProperty.Register(
                "AutoCheckUpdateForLauncher",
                typeof(bool),
                typeof(SwitchSettingCard),
                new PropertyMetadata(BasicUtils.AppSettings.App.AutoCheckUpdate)
            );
        private void OnAutoCheckUpdateForLauncherChanged(object sender, RoutedEventArgs e)
        {
            BasicUtils.SaveSetting("App.AutoCheckUpdate", More_AutoCheckUpdateForLauncher.SettingSwitch.IsOn);
        }
        # endregion

    }

}