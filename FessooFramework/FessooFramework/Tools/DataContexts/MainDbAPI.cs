using FessooFramework.Tools.DataContexts.Models;
using FessooFramework.Tools.Web.MainService.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DataContexts
{
    public static class MainDbAPI
    {
        #region Property
        /// <summary>
        /// Базовое время жизни сессиии
        /// </summary>
        public static TimeSpan DefaultTTL = TimeSpan.FromDays(365 * 10);
        #endregion
        #region UserProfile
        /// <summary>
        /// Получаем профиль пользователя по уникальному полю - почта
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static UserProfile UserProfileByEmail(string email)
        {
            var result = default(UserProfile);
            DCT.DCT.Execute(c => result = UserProfile.DbSet().FirstOrDefault(q => q.Email == email));
            return result;
        }
        /// <summary>
        /// Создание нового профиля пользователя
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="secondName"></param>
        /// <param name="middleName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="birthday"></param>
        /// <param name="hashEmail"></param>
        /// <returns></returns>
        public static UserProfile UserProfileCreate(Request_Registration request, Guid applicationId)
        {
            var result = default(UserProfile);
            DCT.DCT.Execute(c =>
            {
                //Создаю новый профиль клиента
                result = new UserProfile()
                {
                    Birthday = request.Birthday,
                    FirstName = request.FirstName,
                    SecondName = request.SecondName,
                    MiddleName = request.MiddleName,
                    Phone = request.Phone,
                    Email = request.Email,
                };
                result.ApplicationId = applicationId;
                result.StateEnum = UserProfileState.Enable;
            });
            return result;
        }
        #endregion
        #region Application
        /// <summary>
        /// Создаю модель приложения и фиксирую в трекере базы
        /// </summary>
        /// <returns></returns>
        public static ApplicationAccess ApplicationAccessCreate()
        {
            var result = default(ApplicationAccess);
            DCT.DCT.Execute(c =>
            {
                result = new ApplicationAccess();
                result.StateEnum = ApplicationState.Enable;
            });
            return result;
        }
        #endregion
        #region UserAccess
        /// <summary>
        /// Создаю модель доступа пользователя и фиксирую в трекере базы
        /// </summary>
        /// <returns></returns>
        public static UserAccess UserAccessCreate(Guid userProfileId, string login, string loginHash)
        {
            var result = default(UserAccess);
            DCT.DCT.Execute(c =>
            {
                result = new UserAccess();
                result.UserProfileId = userProfileId;
                result.Login = login;
                result.LoginHash = loginHash;
                result.StateEnum = UserAccessState.Enable;
            });
            return result;
        }
        /// <summary>
        /// Получить доступ пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static UserAccess UserAccessByLogin(string login)
        {
            var result = default(UserAccess);
            DCT.DCT.Execute(c => result = UserAccess.DbSet().FirstOrDefault(q => q.Login == login));
            return result;
        }
        #endregion
        #region UserSession
        /// <summary>
        /// Создаю модель приложения и фиксирую в трекере базы
        /// </summary>
        /// <returns></returns>
        public static UserSession UserSessionCreate(Guid userProfileId)
        {
            var result = default(UserSession);
            DCT.DCT.Execute(c =>
            {
                result = new UserSession();
                result.UserProfileId = userProfileId;
                result.TTL = DateTime.Now.AddTicks(DefaultTTL.Ticks);
                result.StateEnum = UserSessionState.Enable;
            });
            return result;
        }
        #endregion
        //Внешнее:
        //2. Изменение пароля
        //3. Запрос восстановления пароля - ссылка на почту + закрепеление за учётной системой
        //4. Редактирование профиля - данные по профилю изменены в зависимости от выбранной системы меняем почту через пароль или не меняем почту никогда при редактировании
        //5. Авторизация пользователя
        //6. Проверка сессии пользователя

        //Внутреннее:
        //1. Создание приложения
        //2. Добавить роль
        //3. Блокировка пользователя
        //4. Блокировка приложения

        //Контекст данных:
        //1. По идентификатору сессии получаем сессию
        //2. По доступу пользователя логин+пароль sha512 строке авторизирую пользователя + создаю сессию при не обходимости
        //3. Получаю роли пользователя по профилю
        //4. Получаю токен по профилю
        //5. Профиль получаю только если заходим на вкладку профиль
    }
}
