using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.Web.MainService.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web.MainService
{
    public abstract class MainServiceClient : BaseServiceClient
    {
        protected override IEnumerable<ServiceResponseConfigBase> Configurations => new ServiceResponseConfigBase[] {  };
        public void Registration(Action<bool> callback, string email, string phone, string password, string firstName, string secondName, string middleName,  DateTime? birthday = null)
        {
            DCT.DCT.ExecuteAsyncQueue<bool>(c=> 
            {
                var result = false;
                birthday = birthday == null ? new DateTime(1900, 01, 01) : birthday;
                var request = new Request_Registration()
                {
                    Email = email,
                    Birthday = birthday.Value,
                    Phone = phone,
                    Password = password,
                    FirstName = firstName,
                    SecondName = secondName,
                    MiddleName = middleName,
                };
                var response = Execute<Request_Registration, Response_Registration>(request);
                switch (response.StateEnum)
                {
                    case Objects.Message.MessageState.Succesfull:
                        result = true;
                        SetHash(response.HashUID, response.SessionUID);
                        break;
                    default:
                        break;
                }
                return result;
            }, complete: (c, r) => {
                if (callback == null)
                    ConsoleHelper.Send("MainServiceClient", "В методе Registration не реализован Callback");
                else
                    callback(r);
            });
        }
        public void SignIn(Action<bool> callback, string login, string password)
        {
            DCT.DCT.ExecuteAsyncQueue<bool>(c =>
            {
                var result = false;
                var request = new Request_SignIn()
                {
                    Login = login,
                    Password = password
                };
                var response = Execute<Request_SignIn, Response_SignIn>(request);
                switch (response.StateEnum)
                {
                    case Objects.Message.MessageState.Succesfull:
                        result = true;
                        SetHash(response.HashUID, response.SessionUID);
                        break;
                    default:
                        break;
                }
                return result;
            }, complete:(c,r) => {
                if (callback == null)
                    ConsoleHelper.Send("MainServiceClient", "В методе SignIn не реализован Callback");
                else
                    callback(r);
            });
        }
        public void SessionCheck(Action<bool> callback, string hashUID, string sessionUID)
        {
            DCT.DCT.ExecuteAsyncQueue(c =>
            {
                var result = false;
                var request = new Request_SessionCheck()
                {
                    HashUID = hashUID,
                    SessionUID = sessionUID
                };
                var response = Execute<Request_SessionCheck, Response_SessionCheck>(request);
                switch (response.StateEnum)
                {
                    case Objects.Message.MessageState.Succesfull:
                        result = true;
                        SetHash(response.HashUID, response.SessionUID);
                        break;
                    default:
                        break;
                }
                return result;
            }, complete: (c,r) => {
                if (callback == null)
                    ConsoleHelper.Send("MainServiceClient", "В методе SessionCheck не реализован Callback");
                else
                    callback(r);
            });
        }
        private void SetHash(string hashUID, string sessionUID)
        {
            DCT.DCT.Execute(c =>
            {
                c._SessionInfo.HashUID = hashUID;
                c._SessionInfo.SessionUID = sessionUID;
            });
        }
    }
}
