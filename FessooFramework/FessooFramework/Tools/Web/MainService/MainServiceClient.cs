using FessooFramework.Objects.Data;
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
        protected override IEnumerable<ServiceResponseConfigBase> Configurations => new ServiceResponseConfigBase[] { };
        public bool Registration(string email, string phone, string password, string firstName, string secondName, string middleName,  DateTime? birthday = null)
        {
            var result = false;
            DCT.DCT.Execute(c=> 
            {
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
            });
            return result;
        }
        public bool SignIn(string login, string password)
        {
            var result = false;
            DCT.DCT.Execute(c =>
            {
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
            });
            return result;
        }
        public bool SessionCheck(string hashUID, string sessionUID)
        {
            var result = false;
            DCT.DCT.Execute(c =>
            {
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
            });
            return result;
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
