﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MCServerLauncher.WPF.Helpers;
using MCServerLauncher.WPF.View.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCServerLauncher.WPF.View.CreateInstanceProvider
{
    /// <summary>
    ///     CreateMinecraftQuiltInstanceProvider.xaml 的交互逻辑
    /// </summary>
    public partial class CreateMinecraftQuiltInstanceProvider
    {
        private List<QuiltMinecraftVersion> SupportedAllMinecraftVersions { get; set; }
        private List<string> QuiltLoaderVersions { get; set; }
        public CreateMinecraftQuiltInstanceProvider()
        {
            InitializeComponent();
            ToggleStableMinecraftVersionCheckBox.Checked += ToggleStableMinecraftVersion;
            FetchMinecraftVersionsButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            FetchQuiltVersionButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
        private class QuiltMinecraftVersion
        {
            public string MinecraftVersion { get; set; }
            public bool IsStable { get; set; }
        }

        private void GoPreCreateInstance(object sender, RoutedEventArgs e)
        {
            var parent = this.TryFindParent<CreateInstancePage>();
            parent.CurrentCreateInstance.GoBack();
        }

        private void FinishSetup(object sender, RoutedEventArgs e)
        {
        }

        private void AddJvmArgument(object sender, RoutedEventArgs e)
        {
            JVMArgumentListView.Items.Add(new JVMArgumentItem());
        }
        private async void FetchMinecraftVersions(object sender, RoutedEventArgs e)
        {
            FetchMinecraftVersionsButton.IsEnabled = false;
            MinecraftVersionComboBox.IsEnabled = false;
            var response = await NetworkUtils.SendGetRequest("https://meta.quiltmc.org/v3/versions/game", useBrowserUserAgent: true);
            var content = await response.Content.ReadAsStringAsync();
            var allSupportedVersionsList = JsonConvert.DeserializeObject<JToken>(content);
            SupportedAllMinecraftVersions = allSupportedVersionsList!.Select(mcVersion => new QuiltMinecraftVersion
            {
                MinecraftVersion = mcVersion.SelectToken("version")!.ToString(),
                IsStable = mcVersion.SelectToken("stable")!.ToObject<bool>()
            }).ToList();
            ToggleStableMinecraftVersionCheckBox.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
            FetchMinecraftVersionsButton.IsEnabled = true;
        }
        private void ToggleStableMinecraftVersion(object sender, RoutedEventArgs e)
        {
            MinecraftVersionComboBox.IsEnabled = false;
            MinecraftVersionComboBox.ItemsSource = ResDownloadUtils.SequenceMinecraftVersion(
                ToggleStableMinecraftVersionCheckBox.IsChecked.GetValueOrDefault(true) ?
                SupportedAllMinecraftVersions.Where(mcVersion => mcVersion.IsStable).ToList().Select(mcVersion => mcVersion.MinecraftVersion).ToList() :
                SupportedAllMinecraftVersions.Select(mcVersion => mcVersion.MinecraftVersion).ToList()
            );
            MinecraftVersionComboBox.IsEnabled = true;
        }
        private async void FetchQuiltVersions(object sender, RoutedEventArgs e)
        {
            FetchQuiltVersionButton.IsEnabled = false;
            QuiltVersionComboBox.IsEnabled = false;
            var response = await NetworkUtils.SendGetRequest("https://meta.quiltmc.org/v3/versions/loader");
            var apiData = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());
            QuiltLoaderVersions = apiData!.Select(version => version.SelectToken("version")!.ToString()).ToList();
            QuiltVersionComboBox.ItemsSource = QuiltLoaderVersions;
            QuiltVersionComboBox.IsEnabled = true;
            FetchQuiltVersionButton.IsEnabled = true;
        }
    }
}