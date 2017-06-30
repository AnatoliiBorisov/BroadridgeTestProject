using System.IO;
using System.Xml.Serialization;

namespace BroadridgeTestProject.Services
{
    internal class SerializationService : ISerializationService
    {
        public string SerializeObject<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public T DeserializeObject<T>(string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader textReader = new StringReader(toDeserialize))
            {
                return (T) xmlSerializer.Deserialize(textReader);                
            }        
        }
    }
}