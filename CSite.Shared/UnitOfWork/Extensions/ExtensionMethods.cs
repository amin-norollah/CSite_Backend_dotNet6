namespace CSite.Shared.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Convert DateTime to "yyyy-MM-dd HH:mm:ss" DateTime String Format
        /// </summary>
        /// <param name="dateTime">System.DateTime</param>
        /// <returns>"yyyy-MM-dd HH:mm:ss" DateTime String</returns>
        public static string ToAmChartFormatStringDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm");

        }
    }
}
