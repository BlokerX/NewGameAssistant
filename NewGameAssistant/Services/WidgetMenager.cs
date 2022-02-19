﻿using NewGameAssistant.Models;
using NewGameAssistant.Widgets;
using NewGameAssistant.WidgetViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NewGameAssistant.Services
{
    /// <summary>
    /// Widget's helper class.
    /// </summary>
    internal static class WidgetMenager
    {
        /// <summary>
        /// List of widget type's unit.
        /// </summary>
        public static List<WidgetTypeUnit> WidgetsList = new List<WidgetTypeUnit>()
        {
            new WidgetTypeUnit()
            {
                WidgetType = typeof(ClockWidget),
                WidgetViewModelType = typeof(ClockViewModel),
                WidgetModelType = typeof(ClockModel)
            },
            new WidgetTypeUnit()
            {
                WidgetType = typeof(PictureWidget),
                WidgetViewModelType = typeof(PictureViewModel),
                WidgetModelType = typeof(PictureModel)
            },
            new WidgetTypeUnit()
            {
                WidgetType = typeof(NoteWidget),
                WidgetViewModelType = typeof(NoteViewModel),
                WidgetModelType = typeof(NoteModel)
            },
        };

        /// <summary>
        /// Convert widget type to view model type. 
        /// </summary>
        /// <param name="widgetType">Widget type.</param>
        /// <returns>View model type.</returns>
        public static Type GetViewModelTypeOfWidgetType(Type widgetType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetType == widgetType)
                    return wtu.WidgetViewModelType;
            }
            return null;
        }

        /// <summary>
        /// Convert view model type to widget type.
        /// </summary>
        /// <param name="viewModelType">View model type.</param>
        /// <returns>Widget type.</returns>
        public static Type GetWidgetTypeOfViewModelType(Type viewModelType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetType == viewModelType)
                    return wtu.WidgetType;
            }
            return null;
        }

        /// <summary>
        /// Coonvert view model type to model type.
        /// </summary>
        /// <param name="viewModelType">View model type.</param>
        /// <returns>Models type.</returns>
        public static Type GetModelTypeOfViewModelType(Type viewModelType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetViewModelType == viewModelType)
                    return wtu.WidgetModelType;
            }
            return null;
        }

        /// <summary>
        /// Convert model type to view model type.
        /// </summary>
        /// <param name="modelType">Model type.</param>
        /// <returns>View model type.</returns>
        public static Type GetViewModelTypeOfModelType(Type modelType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetModelType == modelType)
                    return wtu.WidgetViewModelType;
            }
            return null;
        }

        /// <summary>
        /// Convert widget type to model type. 
        /// </summary>
        /// <param name="widgetType">Widget type.</param>
        /// <returns>Model type.</returns>
        public static Type GetModelTypeOfWidgetType(Type widgetType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetType == widgetType)
                    return wtu.WidgetModelType;
            }
            return null;
        }

        /// <summary>
        /// Convert model type to widget type.
        /// </summary>
        /// <param name="modelType">Model type.</param>
        /// <returns>Widget type.</returns>
        public static Type GetWidgetTypeOfModelType(Type modelType)
        {
            foreach (var wtu in WidgetsList)
            {
                if (wtu.WidgetModelType == modelType)
                    return wtu.WidgetType;
            }
            return null;
        }

        /// <summary>
        /// Return new widget.
        /// </summary>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="WidgetViewModelType">Type of widget's view model.</typeparam>
        /// <typeparam name="WidgetModelType">Type of widget model.</typeparam>
        /// <returns>Widget.</returns>
        public static WidgetType CreateWidget<WidgetType, WidgetViewModelType, WidgetModelType>()
            where WidgetType : WidgetBase, new()
            where WidgetViewModelType : class, IWidgetViewModel<WidgetModelType>, new()
            where WidgetModelType : WidgetModelBase, new()
        {
            AppFileSystem.CheckDiresArchitecture();
            return new WidgetType();
        }

        /// <summary>
        /// Save widget model to file by JSON way.
        /// </summary>
        /// <param name="widget">Widget with widget configuration (in data context) to save.</param>
        /// <param name="path">Path to file where it save widget configuration.</param>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool SaveWidgetConfigurationInFile<WidgetType, ModelType>(WidgetType widget, string path)
            where WidgetType : WidgetBase, new()
        {
            if (widget == null)
            {
                return false;
            }

            var widgetModel = (widget.DataContext as IWidgetViewModel<ModelType>).WidgetModel;

            var directoryPath = path.TrimStart(Path.DirectorySeparatorChar);
            if (Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(path.TrimStart(Path.DirectorySeparatorChar));
            }

            using (var sw = File.CreateText(path))
            {
                sw.Write(JsonConvert.SerializeObject(widgetModel));
            }

            return true;
        }

        /// <summary>
        /// Save widget model to file by JSON way.
        /// </summary>
        /// <param name="widget">Widget with widget configuration (in data context) to save.</param>
        /// <typeparam name="WidgetType">Type of widget.</typeparam>
        /// <typeparam name="ModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool SaveWidgetConfigurationInFile<WidgetType, ModelType>(WidgetType widget)
            where WidgetType : WidgetBase, new()
        {
            Directory.CreateDirectory(AppFileSystem.GetSaveDireConfigurationPath(typeof(WidgetType).Name));
            return SaveWidgetConfigurationInFile<WidgetType, ModelType>(widget, AppFileSystem.GetSaveFileConfigurationPath(typeof(WidgetType).Name));
        }

        /// <summary>
        /// Read widget configuration from path, save it in widgetModel and return operation's result.
        /// </summary>
        /// <param name="widgetModel">Widget with downloaded configuration.</param>
        /// <param name="path">Path to json file with save of widget configuration.</param>
        /// <typeparam name="WidgetModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool DownloadWidgetConfigurationFromFile<WidgetModelType>(out WidgetModelType widgetModel, string path)
            where WidgetModelType : WidgetModelBase, new()
        {
            widgetModel = null;

            if (!File.Exists(path))
            {
                return false;
            }

            using (var sr = new StreamReader(path))
            {
                widgetModel = JsonConvert.DeserializeObject<WidgetModelType>(sr.ReadToEnd());
            }
            return true;
        }

        /// <summary>
        /// Read widget configuration from path, save it in widgetModel and return operation's result.
        /// </summary>
        /// <param name="widgetModel">Widget with downloaded configuration.</param>
        /// <typeparam name="WidgetModelType">Type of widget model.</typeparam>
        /// <returns>Operation result.</returns>
        public static bool DownloadWidgetConfigurationFromFile<WidgetModelType>(out WidgetModelType widgetModel)
            where WidgetModelType : WidgetModelBase, new()
        {
            return DownloadWidgetConfigurationFromFile(out widgetModel, AppFileSystem.GetSaveFileConfigurationPath(GetWidgetTypeOfModelType(typeof(WidgetModelType)).Name));
        }

    }
}
