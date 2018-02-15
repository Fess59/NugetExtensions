using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.ViewModel
{
    public class NotifyObject : SystemObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Обновляет все свойства объкта
        /// </summary>
        public virtual void RaisePropertyChanged()
        {
            RaisePropertyChanged("");
        }
        public void RaisePropertyChanged(string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = ExtractPropertyName(propertyExpression);
            RaisePropertyChanged(propertyName);
        }

        static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The expression is not a member access expression.", nameof(propertyExpression));
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("The member access expression does not access a property.", nameof(propertyExpression));
            }

            //var getMethod = property.GetMethod;
            //if (getMethod.IsStatic)
            //{
            //    throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
            //}

            return memberExpression.Member.Name;
        }
    }
}
