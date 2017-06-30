namespace BroadridgeTestProject.Services
{
    public interface ISerializationService
    {
        string SerializeObject<T>(T toSerialize);

        T DeserializeObject<T>(string toDeserialize);
    }
}