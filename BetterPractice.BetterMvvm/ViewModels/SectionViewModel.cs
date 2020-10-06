using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BetterPractice.BetterMvvm.ViewModels
{
    public class SectionViewModel<TItem> : List<TItem>, INotifyPropertyChanged
    {
		private string? _header;
		public string? Header
        {
			get => _header;
			set => SetProperty(ref _header, value);
        }

		private string? _description;
		public string? Description
        {
			get => _description;
			set => SetProperty(ref _description, value);
        }

        #region Observable Object Methods

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        /// <param name="backingStore">Backing store.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected virtual bool SetProperty<T>(
			ref T backingStore,
			T value,
			[CallerMemberName] string propertyName = "")
		{
			//if value didn't change
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			RaisePropertyChanged(propertyName);
			return true;
		}

		/// <summary>
		/// Occurs when property changed.
		/// </summary>
		public event PropertyChangedEventHandler? PropertyChanged;

		/// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        #endregion
    }
}
