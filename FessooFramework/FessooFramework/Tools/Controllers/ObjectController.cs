using FessooFramework.Objects;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Controllers
{
    /// <summary>   A controller for handling objects. 
    ///             Данный котроллер отслеживает изменния объекта и позволяет подписать событие изменения
    ///             Подписка на события можно оформить перед изменением состояния объекта или сразу после этого
    ///             </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>

    public class ObjectController<T> : SystemObject
    {
        #region Property

        /// <summary>   Gets or sets the value.
        ///             Значение объекта </summary>
        ///
        /// <value> The value. </value>

        public T Value
        {
            get
            {
                if (OnlyGet)
                    Refresh();
                return _Value;
            }
            set
            {
                if (IsAsync)
                    SetValueAsync(value);
                else
                    SetValue(value);
            }
        }

        /// <summary>   Gets a value indicating whether this object has value. </summary>
        ///
        /// <value> True if this object has value, false if not. </value>

        public bool HasValue { get { return Value != null; } }

        /// <summary>   Gets or sets a value indicating whether the only get. 
        ///             Объект доступен только для чтения</summary>
        ///
        /// <value> True if only get, false if not. </value>

        public bool OnlyGet { get; private set; }

        /// <summary>   Gets or sets a value indicating whether this object is asynchronous.
        ///             Асинхронно задаёт Value и вызывает события изменения </summary>
        ///
        /// <value> True if this object is asynchronous, false if not. </value>

        public bool IsAsync { get; private set; }

        /// <summary>   Gets or sets the value. </summary>
        ///
        /// <value> The value. </value>

        private T _Value { get; set; }

        /// <summary>   Gets or sets the default value. </summary>
        ///
        /// <value> The default value. </value>

        private T _DefaultValue { get; set; }
        #endregion
        #region Actions

        /// <summary>   The change. 
        ///             Подписка на событие изменения </summary>
        public ActionController Change = new ActionController();

        /// <summary>   The preview change. 
        ///             Подписка на событие перед изменение значения - позволяет воспользоваться старым значением</summary>
        public ActionController PreviewChange = new ActionController();

        /// <summary>   Gets or sets the check value.
        ///             Функция проверки значения, определённая внешней логикой </summary>
        ///
        /// <value> The check value. </value>

        private Func<T, bool> CheckValue { get; set; }

        /// <summary>   Gets or sets the get value.
        ///             Функция объекта доступного только на чтение </summary>
        ///
        /// <value> The get value. </value>

        private Func<T> GetValue { get; set; }
        #endregion
        #region Constructor

        /// <summary>   Constructor. 
        ///             Конструктор инициализирует объект с возоможность Get\Set</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ///
        /// <param name="defaultValue"> . </param>
        /// <param name="_CheckValue">  (Optional) The check value. </param>
        /// <param name="isAsync">      (Optional) True if this object is asynchronous, false if not. </param>

        public ObjectController(T defaultValue, Func<T, bool> _CheckValue = null, bool isAsync = true)
        {
            _DefaultValue = defaultValue;
            _Value = defaultValue;
            CheckValue = _CheckValue;
            IsAsync = isAsync;
        }

        /// <summary>   Constructor. Конструктор инициализирует объект только на чтение - Get. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ///
        /// <param name="defaultValue"> . </param>
        /// <param name="_GetValue">    The get value. </param>

        public ObjectController(T defaultValue, Func<T> _GetValue)
        {
            _DefaultValue = defaultValue;
            _Value = defaultValue;
            OnlyGet = true;
            GetValue = _GetValue;
        }

        #endregion

        #region Public method

        /// <summary>   Sets after change. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ///
        /// <param name="action">   The action. </param>
        /// <param name="priority"> (Optional) The priority. </param>

        public void SetAfterChange(Action action, int priority = 0)
        {
            Change.Set(action, priority);
        }

        /// <summary>   Clears this object to its blank/initial state.
        ///             Очищает объект - присваивает значение по умолчанию переданное в конструктор</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        public void Clear()
        {
            // TODO DCT.Execute((data) =>{
            if (_DefaultValue != null)
                _SetValue(_DefaultValue);
            // TODO }, ExecutionGroup.ObjectController);
        }

        /// <summary>   Clears the asynchronous.  
        ///             Очищает объект - присваивает значение по умолчанию переданное в конструктор  </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        public void ClearAsync()
        {
            /* TODO  DCT.ExecuteAsync((data) =>*/
            Clear()/*, ExecutionGroup.ObjectController)*/;
        }

        /// <summary>   Only clear.
        ///             Очищает объект - без вызова пре и пост методов </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        public void OnlyClear()
        {
            //TODO DCT.Execute((data) =>{
            if (_Value != null)
                ObjectHelper.Dispose(_Value);
            _Value = _DefaultValue;
            //}, ExecutionGroup.ObjectController);
        }

        /// <summary>   Refreshes this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        public void Refresh()
        {
            //TODO DCT.Execute((data) => {
            if (GetValue != null)
            {
                var newValue = GetValue();
                _ComparerValue(newValue);
            }
            else
            {
                Change.Execute();
            }
            //}, ExecutionGroup.ObjectController);
        }

        /// <summary>   Sets value asynchronous. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ///
        /// <param name="newValue"> . </param>

        public void SetValueAsync(T newValue)
        {
            var _nv = newValue;
            // TODO  DCT.ExecuteAsync(data => SetValue(_nv), ExecutionGroup.ObjectController);
        }

        /// <summary>   Sets a value.
        ///             Присвоить значение - аналог Obj.Value = newValue</summary>
        /// 
        /// <param name="newValue"></param> </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="newValue"> . </param>

        public void SetValue(T newValue)
        {
            // TODO DCT.Execute(data => {
            try
            {
                if (OnlyGet)
                    throw new Exception("Read only - для объекта не возможно задать значение");
                if (CheckValue != null && !CheckValue(newValue))
                    return;
                _ComparerValue(newValue);
            }
            catch (Exception ex)
            {
                // TODO  DCT.SendInfo(ex.ToString());
                throw;
            }

            //}, ExecutionGroup.ObjectController);
        }
        #endregion
        #region Private methods

        /// <summary>   Comparer value.
        ///             Проверяем и задаём значение в случае изменения </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ///
        /// <param name="value">    The value. </param>

        private void _ComparerValue(T value)
        {
            if (!EqualityComparer<T>.Default.Equals(_Value, value))
                _SetValue(value);
            else
            {
                var v1 = value;
            }
        }

        /// <summary>   Sets a value.
        ///             Единый метод для присвоение значения </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ///
        /// <param name="value">    The value. </param>

        private void _SetValue(T value)
        {
            PreviewChange.Execute();
            if (_Value != null)
                ObjectHelper.Dispose(_Value);
            _Value = value;
            Change.Execute();
        }
        #endregion
        #region IDisposable realization

        /// <summary>   Finaliser. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        ~ObjectController()
        {
            ReleaseUnmanagedResources();
        }

        /// <summary>   Releases the unmanaged resources. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        private void ReleaseUnmanagedResources()
        {
            ObjectHelper.Dispose(Change);
            ObjectHelper.Dispose(PreviewChange);
            ObjectHelper.Dispose(_Value);
        }

        /// <summary>
        /// Обертка для IDisposable.Dispose в SystemObject Создана для возможности очистки объекта во
        /// всех наследуемых сущностях
        /// 
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом
        /// неуправляемых ресурсов.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        protected override void Dispose()
        {
            ReleaseUnmanagedResources();
        }
        #endregion

    }
}
