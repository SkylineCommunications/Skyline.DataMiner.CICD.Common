namespace Skyline.DataMiner.CICD.Common.Events
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Aggregates multiple DeferEvents() calls into a single one.
    /// </summary>
    internal class MultipleDeferEvents : IDisposable
    {
        private readonly Stack<IDisposable> disposables = new Stack<IDisposable>();

        public MultipleDeferEvents(IEnumerable<IDeferEvents> deferrables)
        {
            foreach (var d in deferrables)
            {
                disposables.Push(d.DeferEvents());
            }
        }

        public void Dispose()
        {
            while (disposables.Count > 0)
            {
                disposables.Pop().Dispose();
            }
        }
    }
}
