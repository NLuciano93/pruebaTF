namespace Fusap.Common.Swagger
{
    /// <summary>
    /// Indicates the level of exposure of a given Api.
    /// </summary>
    public enum ApiExposureLevel
    {
        /// <summary>
        /// The Api has a private contract and cannot be accessed over the Internet (default).
        /// </summary>
        Internal = 0,

        /// <summary>
        /// The Api has a private contract and can be accessed over the Internet.
        /// </summary>
        External = 1,

        /// <summary>
        /// The Api has a public contract and can accessed over the Internet.
        /// </summary>
        Open = 2
    }
}