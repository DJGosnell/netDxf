using System.Collections.Generic;
using System.IO;

namespace netDxf.IO
{
    public interface IDxfReader
    {
        /// <summary>
        /// Reads the whole stream.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="supportFolders">List of the document support folders.</param>
        DxfDocument Read(Stream stream, IEnumerable<string> supportFolders);
    }
}