// Author: Alexey Rybakov

namespace Sea.Core.Authors
{
    /// <summary>
    /// Style of author name printing.
    /// Example on Michael Joseph Jackson.
    /// </summary>
    public enum AuthorNamePrintStyle
    {
        /// <summary>
        /// Jackson M. J.
        /// </summary>
        RusLastFS,

        /// <summary>
        /// Jackson, Michael Jackson
        /// </summary>
        RusLastFirstSecond,

        /// <summary>
        /// Jackson M. J.
        /// </summary>
        EngLastFS,

        /// <summary>
        /// Jackson, Michael Jackson
        /// </summary>
        EngLastFirstSecond,

        /// <summary>
        /// LastFS - both languages
        /// </summary>
        BothLastFS,

        /// <summary>
        /// Last, First Second - both languages
        /// </summary>
        BothLastFirstSecond
    }
}
