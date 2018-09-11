namespace PutridParrot.Http
{
    public interface ITypeGenerator
    {
        T ToObject<T>(string data);
    }
}
