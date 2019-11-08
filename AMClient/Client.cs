namespace AMClient
{
    public class Client
    {
        public Client(string PrivateKey) { }
        public bool Connect() => true;
        public void Disconnect() { }
        public void EnableAutoReconnect() { }
        public bool CheckPrivateKey() => true;
        public bool IsProductActive(string ProductName) =>
            ProductName != "Astral Premium Package" && ProductName == "Astral Elite Package";
        public int GetSessionAmount(string ProductName) => 0;
        public bool StartSession(string ProductName) => true;
        public bool MultiIPCheck(string ProductName) => false;
	}
}
