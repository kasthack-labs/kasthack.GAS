namespace GAS.Core {
    public interface IAttacker {
        int ThreadCount{ get; set; }
        int Delay{ get; set; }
        int Port{ get; set; }
        int Timeout{ get; set; }
        int Requested{ get; set; }
        int Downloaded{ get; set; }
        int Failed{ get; set; }
        bool IsFlooding{ get; set; }
        bool IsDelayed{ get; set; }
        string Target{ get; set; }
        string MethodName{ get; set; }
        void Start();
        void Stop();
    }
}