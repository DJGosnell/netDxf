using System;
using netDxf.Entities;
using netDxf.Tables;

namespace netDxf
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets the hyperlink for the current object.  Returns null if no hyperlink is set on the entity.
        /// </summary>
        /// <remarks>
        /// Sets xData directly.
        /// Full xData including hyperlink descriptor:
        /// {
        ///   new XDataRecord(XDataCode.String, @"URL"),
        ///   XDataRecord.OpenControlString,
        ///   new XDataRecord(XDataCode.String, @"DESCRIPTOR TEXT"),
        ///   XDataRecord.OpenControlString,
        ///   new XDataRecord(XDataCode.Int32, 1)
        ///   XDataRecord.CloseControlString
        ///   XDataRecord.CloseControlString
        /// }
        /// </remarks>
        public static string GetHyperlink(this EntityObject entityObject)
        {
            // Check the xData for the existence.
            if (entityObject.XData.ContainsAppId("PE_URL"))
            {
                var record = entityObject.XData["PE_URL"].XDataRecord;
                return record.Count > 0 ? record[0].Value as string : null;
            }

            // Nothing was previously set for the hyperlink.
            return null;
        }

        /// <summary>
        /// Sets the hyperlink for the current object.
        /// </summary>
        /// <remarks>
        /// Sets xData directly.
        /// Full xData including hyperlink descriptor:
        /// {
        ///   new XDataRecord(XDataCode.String, @"URL"),
        ///   XDataRecord.OpenControlString,
        ///   new XDataRecord(XDataCode.String, @"DESCRIPTOR TEXT"),
        ///   XDataRecord.OpenControlString,
        ///   new XDataRecord(XDataCode.Int32, 1)
        ///   XDataRecord.CloseControlString
        ///   XDataRecord.CloseControlString
        /// }
        /// </remarks>
        public static void SetHyperlink(this EntityObject entityObject, string value)
        {
            // Ensure the hyperlink is not over the limit.
            if (value.Length > 255)
                throw new ArgumentException("Hyperlink can not be longer than 255 characters.");

            var hyperlinkRecord = new XDataRecord(XDataCode.String, value);

            // Check if the xdata already exists.
            if (entityObject.XData.ContainsAppId("PE_URL"))
            {
                var record = entityObject.XData["PE_URL"].XDataRecord;

                // If there are no records for this xData, create the required record.
                // Otherwise update the existing record.
                if (record.Count < 1)
                    record.Add(hyperlinkRecord);
                else
                    record[0] = hyperlinkRecord;
            }
            else
            {
                // Create a new xData record.
                entityObject.XData.Add(new XData(new ApplicationRegistry("PE_URL"))
                {
                    XDataRecord =
                    {
                        hyperlinkRecord
                    }
                });
            }
        }
    }
}