using System.Collections.Generic;

namespace BroadridgeTestProject.Cashe
{
    internal class ApplicationCasheService : IApplicationCasheService
    {
        //TODO: may be rewrite System.Web.Caching.Cache
        private readonly IDictionary<ApplicationCasheNames, object> _cashe = new Dictionary<ApplicationCasheNames, object>();

        public object GetValue(ApplicationCasheNames applicationCasheName)
        {
            if (!_cashe.ContainsKey(applicationCasheName))
            {
                return null;
            }

            return _cashe[applicationCasheName];
        }

        public void RemoveValue(ApplicationCasheNames applicationCasheName)
        {
            if (_cashe.ContainsKey(applicationCasheName))
            {
                _cashe.Remove(applicationCasheName);
            }
        }

        public void AddValue(ApplicationCasheNames applicationCasheName, object value)
        {
            _cashe[applicationCasheName] = value;
        }
    }
}