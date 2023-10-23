namespace Skyline.DataMiner.CICD.Common.Events
{
    using System;

    /// <summary>
    /// The implementing class provides the option to hold off raising events until the given object is disposed.
    /// </summary>
    public interface IDeferEvents
    {
        IDisposable DeferEvents();
    }
}