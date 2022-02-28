﻿using GameAssistant.Models;
using GameAssistant.Services;
using GameAssistant.Widgets;
using GameAssistant.WidgetViewModels;
using GameAssistant.Windows;
using System.IO;
using System.Windows;

namespace GameAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Widgets

        #region ClockWidget
        /// <summary>
        /// The clock widget's container.
        /// </summary>
        private WidgetContainer<ClockWidget> clockWidgetContainer = new WidgetContainer<ClockWidget>();
        #endregion

        #region PictureWidget
        /// <summary>
        /// The picture widget.
        /// </summary>
        private WidgetContainer<PictureWidget> pictureWidgetContainer = new WidgetContainer<PictureWidget>();
        #endregion

        #region NoteWidget
        /// <summary>
        /// The note widget.
        /// </summary>
        private WidgetContainer<NoteWidget> noteWidgetContainer = new WidgetContainer<NoteWidget>();
        #endregion

        #endregion

        #region NotifyIcon
        /// <summary>
        /// The notify icon.
        /// </summary>
        private System.Windows.Forms.NotifyIcon NotifyIcon;

        /// <summary>
        /// Invoke when notify icon clicked.
        /// </summary>
        private void NotifyIcon_Click(object sender, System.EventArgs e)
        {
            OpenSettingsWindow();
        }

        /// <summary>
        /// Invoke when settings button clicked.
        /// </summary>
        private void NotifyIcon_MenuItem_Settings_Click(object sender, System.EventArgs e)
        {
            OpenSettingsWindow();
        }

        /// <summary>
        /// Invoke when clock widget button clicked.
        /// </summary>
        private void NotifyIcon_ClockWidget_Settings_Click(object sender, System.EventArgs e)
        {
            WidgetMenager.Widget_ChangeStateAndSave<ClockWidget, ClockViewModel, ClockModel>(ref clockWidgetContainer.Widget);
        }

        /// <summary>
        /// Invoke when picture widget button clicked.
        /// </summary>
        private void NotifyIcon_PictureWidget_Settings_Click(object sender, System.EventArgs e)
        {
            WidgetMenager.Widget_ChangeStateAndSave<PictureWidget, PictureViewModel, PictureModel>(ref pictureWidgetContainer.Widget);
        }

        /// <summary>
        /// Invoke when note widget button clicked.
        /// </summary>
        private void NotifyIcon_NoteWidget_Settings_Click(object sender, System.EventArgs e)
        {
            WidgetMenager.Widget_ChangeStateAndSave<NoteWidget, NoteViewModel, NoteModel>(ref noteWidgetContainer.Widget);
        }

        /// <summary>
        /// Invoke when close app button clicked.
        /// </summary>
        private void NotifyIcon_MenuItem_CloseApp_Click(object sender, System.EventArgs e)
        {
            App.Current.Shutdown();
        }

        #endregion

        #region Settings window

        /// <summary>
        /// Open setting window.
        /// </summary>
        private void OpenSettingsWindow()
        {
            var sw = new SettingsWindow(ref clockWidgetContainer, ref pictureWidgetContainer, ref noteWidgetContainer);
            sw.Show();
            //todo
        }

        #endregion

        #region Ovverides methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SelectDisks();
            AppFileSystem.RegisterFileSystem
            (
                nameof(clockWidgetContainer),
                nameof(pictureWidgetContainer),
                nameof(noteWidgetContainer)
            );

            // Notify icon register:
            NotifyIcon = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                Icon = new System.Drawing.Icon("C:\\Users\\Lenovo\\Desktop\\favicon.ico"),
                ContextMenu = new System.Windows.Forms.ContextMenu(
                    new System.Windows.Forms.MenuItem[]
                    {
                        new System.Windows.Forms.MenuItem("Clock widget", NotifyIcon_ClockWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("Picture widget", NotifyIcon_PictureWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("Note widget", NotifyIcon_NoteWidget_Settings_Click),
                        new System.Windows.Forms.MenuItem("-"),
                        new System.Windows.Forms.MenuItem("Settings", NotifyIcon_MenuItem_Settings_Click),
                        new System.Windows.Forms.MenuItem("-"),
                        new System.Windows.Forms.MenuItem("Close app", NotifyIcon_MenuItem_CloseApp_Click)
                    }
                ),
                //todo notify icon picture's Icon = ,
            };
            NotifyIcon.DoubleClick += NotifyIcon_Click;

            LoadWidgets();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            CloseAndSaveWidgets();
            NotifyIcon.Dispose();
            base.OnExit(e);
        }

        #endregion

        #region Widgets methods

        

        /// <summary>
        /// Load all widgets.
        /// </summary>
        private void LoadWidgets()
        {
            WidgetMenager.LoadWidget<ClockWidget, ClockViewModel, ClockModel>(ref clockWidgetContainer.Widget);
            WidgetMenager.LoadWidget<PictureWidget, PictureViewModel, PictureModel>(ref pictureWidgetContainer.Widget);
            WidgetMenager.LoadWidget<NoteWidget, NoteViewModel, NoteModel>(ref noteWidgetContainer.Widget);
        }

        /// <summary>
        /// Save configuration for all widgets.
        /// </summary>
        private void SaveWidgets()
        {
            WidgetMenager.SaveWidgetConfigurationInFile<ClockWidget, ClockModel>(clockWidgetContainer.Widget);
            WidgetMenager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(pictureWidgetContainer.Widget);
            WidgetMenager.SaveWidgetConfigurationInFile<PictureWidget, PictureModel>(pictureWidgetContainer.Widget);
        }

        /// <summary>
        /// Close all widgets.
        /// </summary>
        private void CloseWidgets()
        {
            WidgetMenager.CloseWidget<ClockWidget, ClockModel>(ref clockWidgetContainer.Widget);
            WidgetMenager.CloseWidget<PictureWidget, PictureModel>(ref pictureWidgetContainer.Widget);
            WidgetMenager.CloseWidget<PictureWidget, PictureModel>(ref pictureWidgetContainer.Widget);
        }

        /// <summary>
        /// Close all widgets.
        /// </summary>
        private void CloseAndSaveWidgets()
        {
            WidgetMenager.CloseAndSaveWidget<ClockWidget, ClockModel>(ref clockWidgetContainer.Widget);
            WidgetMenager.CloseAndSaveWidget<PictureWidget, PictureModel>(ref pictureWidgetContainer.Widget);
            WidgetMenager.CloseAndSaveWidget<NoteWidget, NoteModel>(ref noteWidgetContainer.Widget);
        }

        #endregion

        #region Select global disk

        /// <summary>
        /// Set DiskName to default disk.
        /// </summary>
        private static void SelectDisks()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                foreach (var directory in Directory.GetDirectories(drive.Name))
                {
                    if (directory == Path.Combine(drive.Name, "Users"))
                    {
                        DiskName = drive.Name;
                        goto END;
                    }
                }
            }
        END:;
        }

        /// <summary>
        /// Name of disc that has users dire.
        /// </summary>
        public static string DiskName { get; private set; }

        #endregion

    }
}
