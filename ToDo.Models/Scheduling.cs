namespace ToDo
{
    using ReactiveUI;
    using System.Reactive.Concurrency;
    using System.Threading;

    public static class Scheduling
    {
        public static IScheduler MainThreadScheduler = Platform.IsWasm ?
            new SynchronizationContextScheduler(SynchronizationContext.Current)
            : RxApp.MainThreadScheduler;
    }
}
