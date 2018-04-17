using FT.Helper.Helper;
using System;
using System.IO;
using System.Text;

namespace FTP.Helper.Helper
{
    public static class EncodeFile
    {
        public static bool Encode(string path)
        {
            bool result = false;

            if (string.IsNullOrEmpty(path))
            {
                Logger.log.Error("Path is empty.");
                result = false;
            }

            try
            {
                var fileBytes = File.ReadAllBytes(path);
                string encodedFile = Convert.ToBase64String(fileBytes);
                byte[] bytes = Encoding.ASCII.GetBytes(encodedFile);
                File.WriteAllBytes(path, bytes);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.log.Error("File coudnt be encoded" + '\n', ex);
                result = false;
            }
            return result;
        }
    }
}
