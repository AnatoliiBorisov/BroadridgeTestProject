namespace BroadridgeTestProject.Cashe
{
    public interface IApplicationCasheService
    {
        object GetValue(ApplicationCasheNames applicationCasheName);

        void RemoveValue(ApplicationCasheNames applicationCasheName);

        void AddValue(ApplicationCasheNames applicationCasheName, object value);
    }
}