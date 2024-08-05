using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace base64.net
{
    class Program
    {
        static int Main(string[] args)
        {
            var IS_INPUT_REDIR = false;
            try
            {
                var key = Console.KeyAvailable; //Console.ReadKey();
            }
            //catch (Exception ex) { Console.WriteLine(ex); }
            catch
            {
                IS_INPUT_REDIR = true;
            }

            if (args.Length == 0 && !IS_INPUT_REDIR)
            {
                Console.WriteLine("Usage:  base64.net [file]");
                Console.WriteLine("        type [file 1] | base64.net");
                Console.WriteLine("Decode: base64.net --decode [file]");
                Console.WriteLine("        type [file 1] | base64.net --decode");

                Console.WriteLine();

                Console.WriteLine("Advance: base64.net [file 1] ... [file n]");
                Console.WriteLine("         concats all files, produces 1 base64 pack");
                Console.WriteLine("         type [file] | base64.net [file 1] ... [file n]");
                Console.WriteLine("         concats all files, produces 1 base64 pack");
                Console.WriteLine("Decode:  base64.net --decode [file 1] ... [file n]");
                Console.WriteLine("         decodes each file, then concat in output");
                Console.WriteLine("         type [file] | base64.net --decode [file 1] ... [file n]");
                Console.WriteLine("         decode stdin, then each files, then concat in output");
                return 1;
            }
            if (args.Contains("--decode"))
            { // begin of encode
                if(IS_INPUT_REDIR)
                    using (Stream inputStream = Console.OpenStandardInput())
                    {
                        using (MemoryStream outStream = new MemoryStream())
                        {
                            inputStream.CopyTo(outStream);
                            var txt = Encoding.ASCII.GetString(outStream.ToArray());
                            try
                            {
                                var decoded = Convert.FromBase64String(txt);
                                Console.WriteLine(Encoding.ASCII.GetString(decoded));
                            }
                            catch
                            {
                                Console.Error.WriteLine("redirected stdin, is not base64");
                            }
                        }
                    }
                foreach (var filename in args)
                {
                    if (filename != "--decode")
                        if (File.Exists(filename))
                        {
                            var data = File.ReadAllText(filename);
                            try
                            {
                                var decoded = Convert.FromBase64String(data);
                                Console.WriteLine(Encoding.ASCII.GetString(decoded));
                            }
                            catch
                            {
                                Console.Error.WriteLine("file " + filename + ", is not base64");
                            }
                        }
                        else
                            Console.Error.WriteLine("cannot find, skipping " + filename);
                }
                return 0;
            } // end of decode
            else
            { //begin of encode
                //using(Stream outStream = Console.OpenStandardOutput())
                using (MemoryStream outStream = new MemoryStream())
                {
                    if(IS_INPUT_REDIR)
                        using (Stream inputStream = Console.OpenStandardInput())
                        {
                            int len = 0;
                            const int BUFFER_SIZE = 4000;
                            byte[] bytes = new byte[BUFFER_SIZE];
                            int outputLength = inputStream.Read(bytes, 0, BUFFER_SIZE);
                            outStream.Write(bytes, len, outputLength);
                            len += outputLength;
                            while (outputLength == BUFFER_SIZE)
                            {
                                outputLength = inputStream.Read(bytes, 0, BUFFER_SIZE);
                                outStream.Write(bytes, len, outputLength);
                                len += outputLength;
                            }
                        }
                    foreach (var filename in args)
                    {
                        if (File.Exists(filename))
                        {
                            using (var fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                                fs.CopyTo(outStream);
                        } else
                            Console.Error.WriteLine("cannot find, skipping " + filename);
                    }
                    Console.WriteLine(Convert.ToBase64String(outStream.ToArray()));
                }
                return 0;
            } //end of encode
        }
    }
}
