using System.IO;

namespace 記帳.Utils
{
    internal class PathUtils
    {

        public static void CheckPathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string GetPathFromImagePath(string input)
        {

            string[] imagePath = input.Split('\\');
            string path = "";
            for (int i = 0; i < imagePath.Length - 1; i++)
            {
                path += imagePath[i] + "\\";
            }
            return path;
        }
    }
}
