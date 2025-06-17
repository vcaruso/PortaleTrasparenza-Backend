namespace ENINET.TransparentPortal.API.Utility
{
    public class ReportUtility
    {
        /// <summary>
        /// Splitta il file in percorsi, il file da downlodare ha in più il sito formato: SITE-AREA-YEAR-MONTH-PROGRESSIVE
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        public static (string Site, string Area, string Year, string Month, string Progressive) SplitFileNameToPath(string name)
        {
            /// Formato RO-NUMEROFILE-MESE-ANNO.ESTENSIONE
            var percorsi = name.Split("-");
            if (percorsi.Length != 5)
            {
                throw new BadHttpRequestException("Bad File Format!");
            }
            var site = percorsi[0];
            var area = percorsi[1];
            var progressive = percorsi[2];
            var month = percorsi[3];
            var year = percorsi[4].Split(".")[0];
            return (site, area, year, month, progressive);
        }
    }
}
