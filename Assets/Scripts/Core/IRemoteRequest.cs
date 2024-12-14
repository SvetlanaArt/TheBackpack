namespace BackpackUnit.Core
{
    public interface IRemoteRequest
    {
        public void SendToServer(string objectName, string act);
    }
}

