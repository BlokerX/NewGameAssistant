﻿using NewGameAssistant.Core;
using NewGameAssistant.Models;
using System.Windows.Media;

namespace NewGameAssistant.WidgetViewModels
{
    /// <summary>
    /// View model that contains bindings for PictureWidget.
    /// </summary>
    internal class PictureViewModel : BindableObject, IWidgetViewModel<PictureModel>
    {

        private PictureModel _widgetModel = new PictureModel();
        /// <summary>
        /// Picture widget properties.
        /// </summary>
        public PictureModel WidgetModel
        {
            get => _widgetModel;
            set => SetProperty(ref _widgetModel, value);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PictureViewModel()
        {
            // Set title:
            WidgetModel.Title = "Picture widget";

            // Set widget size:
            WidgetModel.Width = 400;
            WidgetModel.Height = 250;

            // Set widget position:
            WidgetModel.ScreenPositionY += 70;

            // Set widget background color:
            WidgetModel.BackgroundColor = new SolidColorBrush(Colors.Transparent);
        }
    }
}
