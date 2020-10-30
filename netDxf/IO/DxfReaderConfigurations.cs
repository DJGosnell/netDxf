using netDxf.Logging;

namespace netDxf.IO
{
    public class DxfReaderConfigurations
    {
        /// <summary>
        /// When set to true, block with are not named will be ignored since there is no way
        /// to reference the block in the file.
        /// </summary>
        public bool DropUnnamedBlocks { get; set; }

        /// <summary>
        /// When set to true, attributes which can not be read or can not be added to the attribute
        /// collection are dropped.
        /// </summary>
        public bool DropUnreadableAttributes { get; set; }

        /// <summary>
        /// Set to log any reading errors.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new configuration for DxfReaderConfigured
        /// </summary>
        /// <param name="lenientMode">
        /// If set to true, extra steps are taken to parse dxf files.
        /// Some entities will be dropped if malformed.
        /// </param>
        public DxfReaderConfigurations(bool lenientMode)
        {
            DropUnnamedBlocks = lenientMode;
            DropUnreadableAttributes = lenientMode;
        }
    }
}