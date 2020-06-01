using System.IO;

namespace ADSTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                var sourceFileName = args[0];
                var targetFileName = args[1];
                using (FileStream targetStream = FileStreamHelper.OpenWithStream(targetFileName, FileMode.Create, FileAccess.Write))
                {
                    using (FileStream sourceStream = FileStreamHelper.OpenWithStream(sourceFileName, FileMode.Open, FileAccess.Read))
                    {
                        var buff = new byte[sourceStream.Length];
                        var length = (int) sourceStream.Length;
                        sourceStream.Read(buff, 0, length);
                        targetStream.Write(buff, 0, length);
                    }
                }
            }
        }
    }
}
