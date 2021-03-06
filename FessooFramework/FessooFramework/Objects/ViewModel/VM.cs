﻿using FessooFramework.Objects.Delegate;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FessooFramework.Objects.ViewModel
{
    public abstract class VM : NotifyObject
    {
        #region Properties
        public Guid Id { get; set; }
        //public bool IsAsync = true;
        #endregion
        #region Commands
        public ICommand CommandRaiseDone { get; set; }
        #endregion
        #region Constructor
        protected VM()
        {
            Id = Guid.NewGuid();
            Loaded();
            DCT.Execute(data => Initialization());
        }
        #endregion
        #region Abstractions
        /// <summary>
        /// Все реализация логики при инициализации - тут
        /// Вызываеться автоматически при создании
        /// </summary>
        public virtual void Initialization()
        {
            CommandRaiseDone = new DelegateCommand(RaiseDone);
            //if (IsAsync)
            //    DCT.ExecuteAsync(data => { InitializationAsync(); RaiseDone(); });
        }
        /// <summary>
        /// Асинхронная инициализация
        /// </summary>
        public virtual void InitializationAsync()
        {

        }
        public void Dispose()
        {
            Unloaded();
        }
        public virtual void RaiseDone()
        {
            RaisePropertyChanged();
        }
        protected virtual void Loaded() { }
        protected virtual void Unloaded() { }
        #endregion

        #region IDisposable

        ~VM()
        {
            ObjectHelper.Dispose(this);
        }

        #endregion
    }
}
