﻿using GameAssistant.Services;
using GameAssistant.Widgets;
using System.Diagnostics;
using System.Windows;

namespace GameAssistant.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy GeneralSettingsPage.xaml
    /// </summary>
    public partial class GeneralSettingsPage : SettingsPage
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GeneralSettingsPage(ref AllWidgetsContainer allWidgetsContainer)
        {
            InitializeComponent();

            _allWidgetsContainer = allWidgetsContainer;

            LoadWidgets(AllWidgetsContainer);

            SubscriptionWidgetsActiveChangedEvents();

            AutoStart.PropertyValue = AppFileSystem.CheckStartupKeyValue();

            CheckAllWidgetsActiveProperty();
        }

        public void RemovePageMethodsFromWidgetsEvents() => DesubscriptionWidgetsActiveChangedEvents();

        #region WidgetsConteners

        public static readonly DependencyProperty PropertyAllWidgetsContainer = DependencyProperty.Register(
        "AllWidgetsContainer", typeof(AllWidgetsContainer),
        typeof(GeneralSettingsPage)
        );

        private AllWidgetsContainer _allWidgetsContainer;

        /// <summary>
        /// The all widgets containers with widgets.
        /// </summary>
        public AllWidgetsContainer AllWidgetsContainer
        {
            get => _allWidgetsContainer;
            set => SetProperty(ref _allWidgetsContainer, value);
        }

        #endregion

        private void LoadWidgets(AllWidgetsContainer allWidgetsContainer)
        {
            if (allWidgetsContainer.clockWidgetContainer.Widget == null)
            {
                ClockWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                ClockWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.pictureWidgetContainer.Widget == null)
            {
                PictureWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                PictureWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.noteWidgetContainer.Widget == null)
            {
                NoteWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                NoteWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.calculatorWidgetContainer.Widget == null)
            {
                CalculatorWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                CalculatorWidgetActiveProperty.PropertyValue = true;
            }

            if (allWidgetsContainer.browserWidgetContainer.Widget == null)
            {
                BrowserWidgetActiveProperty.PropertyValue = false;
            }
            else
            {
                BrowserWidgetActiveProperty.PropertyValue = true;
            }
        }

        #region Widgets Active Changed Events

        /// <summary>
        /// Add subscription to widgets active changed's events.
        /// </summary>
        private void SubscriptionWidgetsActiveChangedEvents()
        {
            ClockWidget.Events.WidgetActiveChanged += ClockWidgetEvents_WidgetActiveChanged;
            PictureWidget.Events.WidgetActiveChanged += PictureWidgetEvents_WidgetActiveChanged;
            NoteWidget.Events.WidgetActiveChanged += NoteWidgetEvents_WidgetActiveChanged;
            CalculatorWidget.Events.WidgetActiveChanged += CalculatorWidgetEvents_WidgetActiveChanged;
            BrowserWidget.Events.WidgetActiveChanged += BrowserWidgetEvents_WidgetActiveChanged;
        }

        /// <summary>
        /// Remove subscription to widgets active changed's events.
        /// </summary>
        public void DesubscriptionWidgetsActiveChangedEvents()
        {
            ClockWidget.Events.WidgetActiveChanged -= ClockWidgetEvents_WidgetActiveChanged;
            PictureWidget.Events.WidgetActiveChanged -= PictureWidgetEvents_WidgetActiveChanged;
            NoteWidget.Events.WidgetActiveChanged -= NoteWidgetEvents_WidgetActiveChanged;
            CalculatorWidget.Events.WidgetActiveChanged -= CalculatorWidgetEvents_WidgetActiveChanged;
            BrowserWidget.Events.WidgetActiveChanged -= BrowserWidgetEvents_WidgetActiveChanged;
        }

        #region Widgets event's methods

        private void ClockWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (ClockWidgetActiveProperty.PropertyValue != state)
                ClockWidgetActiveProperty.PropertyValue = state;
            CheckAllWidgetsActiveProperty();
        }

        private void PictureWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (PictureWidgetActiveProperty.PropertyValue != state)
                PictureWidgetActiveProperty.PropertyValue = state;
            CheckAllWidgetsActiveProperty();
        }

        private void NoteWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (NoteWidgetActiveProperty.PropertyValue != state)
                NoteWidgetActiveProperty.PropertyValue = state;
            CheckAllWidgetsActiveProperty();
        }

        private void CalculatorWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (CalculatorWidgetActiveProperty.PropertyValue != state)
                CalculatorWidgetActiveProperty.PropertyValue = state;
            CheckAllWidgetsActiveProperty();
        }

        private void BrowserWidgetEvents_WidgetActiveChanged(bool state)
        {
            if (BrowserWidgetActiveProperty.PropertyValue != state)
                BrowserWidgetActiveProperty.PropertyValue = state;

            CheckAllWidgetsActiveProperty();
        }

        #endregion

        #endregion

        /// <summary>
        /// Set or remove startup process.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">True to create autostart, false to delete autostart.</param>
        private void AutoStart_PropertyValueChanged(object sender, bool? e)
        {
            if (e == true)
                AppFileSystem.CreateStartupKey();
            else
                AppFileSystem.DeleteStartupKey();
        }

        #region Active changed checkboxes

        private void ClockWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            ClockWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void PictureWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            PictureWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void NoteWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            NoteWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void CalculatorWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            CalculatorWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        private void BrowserWidgetActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            BrowserWidget.Events.WidgetActiveChanged_Invoke((bool)e);
        }

        #endregion

        // todo poprawić wydajność programu pod względem wykonywania eventów
        private void AllWidgetsActiveProperty_PropertyValueChanged(object sender, bool? e)
        {
            if (!AllWidgetsActiveProperty.IsEnabled)
                return;

            if (ClockWidgetActiveProperty.PropertyValue != e)
                ClockWidgetActiveProperty.PropertyValue = e;

            if (PictureWidgetActiveProperty.PropertyValue != e)
                PictureWidgetActiveProperty.PropertyValue = e;

            if (NoteWidgetActiveProperty.PropertyValue != e)
                NoteWidgetActiveProperty.PropertyValue = e;

            if (CalculatorWidgetActiveProperty.PropertyValue != e)
                CalculatorWidgetActiveProperty.PropertyValue = e;

            if (BrowserWidgetActiveProperty.PropertyValue != e)
                BrowserWidgetActiveProperty.PropertyValue = e;
        }

        // a - czy sprawdzać wartość false
        private void CheckAllWidgetsActiveProperty()
        {
            if (!AllWidgetsActiveProperty.IsEnabled)
                return;

            if (true == ClockWidgetActiveProperty.PropertyValue &&
                ClockWidgetActiveProperty.PropertyValue == PictureWidgetActiveProperty.PropertyValue &&
                PictureWidgetActiveProperty.PropertyValue == NoteWidgetActiveProperty.PropertyValue &&
                NoteWidgetActiveProperty.PropertyValue == CalculatorWidgetActiveProperty.PropertyValue &&
                CalculatorWidgetActiveProperty.PropertyValue == BrowserWidgetActiveProperty.PropertyValue)
            {
                AllWidgetsActiveProperty.PropertyValue = true;
            }
            else if (AllWidgetsActiveProperty.ValueCheckBox.IsChecked != false)
            {
                AllWidgetsActiveProperty.IsEnabled = false;
                AllWidgetsActiveProperty.ValueCheckBox.IsChecked = false;
                AllWidgetsActiveProperty.IsEnabled = true;
            }
        }

        private void ResetAllSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            //if (MessageBox.Show("Should you set widget configuration to default?\n(Warning, if you restore the default settings you will not be able to restore the current data.)", "Setting configuration to default:", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            //{
            //    if (AllWidgetsContainer.clockWidgetContainer.Widget != null)
            //    {
            //        WidgetManager.CloseWidget<ClockWidget, ClockModel>(ref AllWidgetsContainer.clockWidgetContainer.Widget);
            //    }
            //    AllWidgetsContainer.clockWidgetContainer.Widget = new ClockWidget();
            //    AllWidgetsContainer.clockWidgetContainer.Widget.Show();
            //    todo metoda synchronizująca ładowanie widgetu
            //    LoadWidget(ref AllWidgetsContainer.clockWidgetContainer);
            //    WidgetManager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(AllWidgetsContainer.clockWidgetContainer.Widget);
            //}
        }

        private void OpenConfigurationsDireButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Explorer", AppFileSystem.WidgetsConfigurationsMainDire);
        }
    }
}
