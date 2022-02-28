﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameAssistant.ControlViewModels
{
    internal class SettingPropertyViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged (Implemented)

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Set property with invoke PropertyChangedEvent.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="storage">Property reference.</param>
        /// <param name="value">New property value.</param>
        /// <param name="propertyname">The name of property.</param>
        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyname = null)
        {
            storage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion

    }
}
