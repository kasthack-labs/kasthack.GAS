namespace GAS.Core {
    public abstract class IAttacker {
        public enum ReqState {
            Ready,
            Connecting,
            Requesting,
            Downloading,
            Completed,
            Failed
        };

        public ReqState[] States = new ReqState[] {
            ReqState.Ready
        };
        public volatile int ThreadCount; //{ get; set; }
        public volatile int Delay; // { get; set; }
        public volatile int Port; // { get; set; }
        public volatile int Timeout; // { get; set; }
        public volatile int Requested; // { get; set; }
        public volatile int Downloaded; // { get; set; }
        public volatile int Failed; // { get; set; }
        public volatile bool IsFlooding; // { get; set; }
        public volatile bool IsDelayed; //{ get; set; }
        public volatile string Target; // { get; set; }
        public volatile string MethodName; // { get; set; }
        public abstract void Start();
        public abstract void Stop();
    }
}