using InternetTechnologies.Server.DAL.Models.Exceptions;
using System.IO;

namespace InternetTechnologies.Server.DAL.Services
{
    public static class Validator
    {
        public static void ValidateXmlPath(this string path)
        {
            path.ValidatePath(".xml");
        }

        private static void ValidatePath(this string path, string format)
        {
            if (path == null || !path.EndsWith(format))
            {
                throw new ValidationException("Incorrect path.");
            }
        }

        public static void ValidateFileExistance(this string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(nameof(path));
            }
        }

        public static void IsNotNull<T>(this T item)
        {
            if (item == null)
            {
                throw new ValidationException(nameof(item));
            }
        }

    }
}
