namespace ENINET.TransparentPortal.API.Services.Storage
{
    public class LocalStorageManager : IStorageManager
    {


        public LocalStorageManager()
        {

        }

        /// <summary>
        /// Cancella il file
        /// </summary>
        /// <param name="root"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<bool> DeleteFile(string root, string fileName)
        {
            var segmenti = Utility.ReportUtility.SplitFileNameToPath(fileName);
            var percorso = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(root, segmenti.Site), segmenti.Area), segmenti.Year), segmenti.Month), fileName);
            if (File.Exists(percorso))
            {
                File.Delete(percorso);
                return Task.FromResult(true);
            }
            throw new FileNotFoundException();
        }

        /// <summary>
        /// Scarica il File
        /// </summary>
        /// <param name="root"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public async Task<byte[]> Download(string root, string fileName)
        {
            var segmenti = Utility.ReportUtility.SplitFileNameToPath(fileName);
            var percorso = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(root, segmenti.Site), segmenti.Area), segmenti.Year), segmenti.Month), fileName);
            if (File.Exists(percorso))
            {
                var payload = await File.ReadAllBytesAsync(percorso);
                return payload;
            }
            throw new FileNotFoundException();
        }

        public Task<bool> Exists(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveFile(string root, string fileName, byte[] fileContent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Salva il file da uno stream
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        public async Task<bool> SaveFile(string root, string fileName, Stream stream)
        {
            var percorsi = Utility.ReportUtility.SplitFileNameToPath(fileName);
            var percorso = Path.Combine(Path.Combine(Path.Combine(Path.Combine(root, percorsi.Site), percorsi.Area), percorsi.Year), percorsi.Month);
            EnsurePathExists(percorso);
            using (var fileStream = File.Create(Path.Combine(percorso, fileName)))
            {
                await stream.CopyToAsync(fileStream);
            }
            return true;

        }

        private void EnsurePathExists(string path)
        {
            var paths = path.Split("\\");
            var percorso = "";
            foreach (var p in paths)
            {

                percorso = Path.Combine(percorso, p);
                if (!Directory.Exists(percorso)) Directory.CreateDirectory(percorso);
            }
        }
    }
}
