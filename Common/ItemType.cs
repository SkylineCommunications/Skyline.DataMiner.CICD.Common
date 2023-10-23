namespace Skyline.DataMiner.CICD.Common
{
    /// <summary>
    /// Defines DataMiner item types.
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// Automation script.
        /// </summary>
        Automation,

        /// <summary>
        /// Visio.
        /// </summary>
        Visio,

        /// <summary>
        /// Dashboard.
        /// </summary>
        Dashboard,
        ////Protocol,   // Not needed: done via .dmprotocol package and not .dmapp package
        ////Function,   // For now, there was no consensus on a generic way to make such packages. So for now, it's up to users to provide Git with a .dmapp package right away.
        ////Example,    // Not needed for now.
        ////Files,      // Not yet ready, will be tackled later on.
    }
}
