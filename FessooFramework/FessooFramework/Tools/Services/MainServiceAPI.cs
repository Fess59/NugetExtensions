using FessooFramework.Tools.DataContexts;
using FessooFramework.Tools.DataContexts.Models;
using FessooFramework.Tools.DataContexts.ServiceModels;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Services
{
    public static class MainServiceAPI
    {
        public static Response_Registration User_Registration(Request_Registration request)
        {
            var result = new Response_Registration();
            DCT.DCT.Execute(c => {
                var emailHash = CryptographyHelper.StringToSha256String(request.Email, request.Password);
                //Проверка полученных данных
                if (string.IsNullOrWhiteSpace(emailHash) || string.IsNullOrWhiteSpace(request.Email))
                {
                    result.Comment = "Указаны не корректные для регистрации логин или пароль";
                    result.StateEnum = Objects.Message.MessageState.NotSuccesfull;
                    return;
                }
                //Проверка регистрации пользователя по почте
                var up = MainDbAPI.UserProfileByEmail(request.Email);
                if (up != null)
                {
                    result.Comment = "Пользователь с указанным Email уже зарегистрирован";
                    result.StateEnum = Objects.Message.MessageState.NotSuccesfull;
                    return;
                }
                //Создаю приложение 
                var application = MainDbAPI.ApplicationAccessCreate();
                //Создаю профиль
                var profile = MainDbAPI.UserProfileCreate(request, application.Id);
                //Создаю ключи авторизации - телефон, почта, email(базовый)
                //- Email
                var emailAccess = MainDbAPI.UserAccessCreate(profile.Id, profile.Email, emailHash);
                //- Phone var phoneAccess = MainDbAPI.UserAccessCreate(profile.Id, profile.Phone, phoneHash
                //- Login var loginAccess = MainDbAPI.UserAccessCreate(profile.Id, profile.Login, loginHash);
                //Создаю сессию
                var userSession = MainDbAPI.UserSessionCreate(profile.Id);
                c.SaveChanges();
                result.Comment = "Пользователь успешно зарегистрирован!";
                result.StateEnum = Objects.Message.MessageState.Succesfull;
                result.SessionUID = userSession.Id.ToString();
                result.HashUID = emailHash;
            }, continueExceptionMethod: (x, ex) =>
            {
                result.Error = ex.ToString();
                result.StateEnum = Objects.Message.MessageState.Error;
            });
            return result;
        }
        public static Response_SignIn User_SignIn(Request_SignIn request)
        {
            var result = new Response_SignIn();
            DCT.DCT.Execute(c => {
                var emailHash = CryptographyHelper.StringToSha256String(request.Login, request.Password);
                //Проверка полученных данных
                if (string.IsNullOrWhiteSpace(emailHash) || string.IsNullOrWhiteSpace(request.Login))
                {
                    result.Comment = "Указан не корректный логин для авторизации";
                    result.StateEnum = Objects.Message.MessageState.NotSuccesfull;
                    return;
                }
                var access = MainDbAPI.UserAccessByLogin(request.Login);
                if (access.LoginHash == emailHash)
                {
                    result.Comment = "Пользователь успешно авторизирован!";
                    result.HashUID = emailHash;
                    var session = MainDbAPI.UserSessionCreate(access.UserProfileId);
                    result.SessionUID = session.Id.ToString();
                    result.StateEnum = Objects.Message.MessageState.Succesfull;
                }
                else
                {
                    result.Comment = "Указаны не корректные логин или пароль";
                    result.StateEnum = Objects.Message.MessageState.NotSuccesfull;
                }
            }, continueExceptionMethod: (x, ex) =>
            {
                result.Error = ex.ToString();
                result.StateEnum = Objects.Message.MessageState.Error;
            });
            return result;
        }
        public static Response_SignIn User_SessionCheck(Request_SignIn request)
        {
            var result = new Response_SignIn();
            DCT.DCT.Execute(c => {
                //var emailHash = CryptographyHelper.StringToSha256String(request.Login, request.Password);
                ////Проверка полученных данных
                //if (string.IsNullOrWhiteSpace(emailHash) || string.IsNullOrWhiteSpace(request.Login))
                //{
                //    result.Comment = "Указана не корректная сессия или хэш клиента";
                //    result.StateEnum = Objects.Message.MessageState.NotSuccesfull;
                //    return;
                //}
                //var access = MainDbAPI.UserAccessByLogin(request.Login);
                //if (access.LoginHash == emailHash)
                //{
                //    result.Comment = "Пользователь успешно авторизирован!";
                //    result.HashUID = emailHash;
                //    result.SessionUID = request.SessionUID;
                //    result.StateEnum = Objects.Message.MessageState.Succesfull;
                //}
                //else
                //{
                //    result.Comment = "Указаннная ссессия просрочена сессия требуется вызов метода SingIn и получение но";
                //    result.StateEnum = Objects.Message.MessageState.NotSuccesfull;
                //}
            }, continueExceptionMethod: (x, ex) =>
            {
                result.Error = ex.ToString();
                result.StateEnum = Objects.Message.MessageState.Error;
            });
            return result;
        }
    }
}
