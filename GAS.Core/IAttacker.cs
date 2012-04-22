using System;

namespace GAS.Core
{
    public abstract class IAttacker
    {
        public enum ReqState { Ready, Connecting, Requesting, Downloading, Completed, Failed };
        public ReqState[] States =new ReqState[]{ ReqState.Ready};
        public int ThreadCount { get; set; }
        public int Delay { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public int Requested { get; set; }
        public int Downloaded { get; set; }
        public int Failed { get; set; }
        public bool IsFlooding { get; set; }
        public bool IsDelayed { get; set; }
        public string Target { get; set; }
        public string MethodName { get; set; }
        public abstract void Start();
        public abstract void Stop(); 
    }
}
