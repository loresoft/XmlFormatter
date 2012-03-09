using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using XmlFormatter.CommandLine;

namespace XmlFormatter
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (Parser.ParseHelp(args))
                {
                    OutputHeader();
                    OutputUsageHelp();
                    return 0;
                }

                var errorBuffer = new StringBuilder();
                var arguments = new FormatOptions();

                if (!Parser.ParseArguments(args, arguments, s => errorBuffer.AppendLine(s)))
                {
                    OutputHeader();
                    Console.Error.WriteLine(errorBuffer.ToString());
                    OutputUsageHelp();
                    return 1;
                }

                if ((arguments.InputOutput && !string.IsNullOrEmpty(arguments.File))
                    || (!arguments.InputOutput && string.IsNullOrEmpty(arguments.File)))
                {
                    OutputHeader();
                    Console.Error.WriteLine("Must use either /stdin or /f:<file> switch.");
                    OutputUsageHelp();
                    return 1;
                }

                if (arguments.InputOutput)
                {
                    FormatConsole(arguments);
                    return 0;
                }

                FormatFile(arguments);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error trying to format XML file.");
                Console.Error.WriteLine(ex);
                return 1;
            }
        }

        private static void FormatFile(FormatOptions options)
        {
            string inputFile = Path.GetFullPath(options.File);
            string outputFile = string.IsNullOrEmpty(options.OutputFile) ? inputFile : Path.GetFullPath(options.OutputFile);

            bool isSameFile = string.Equals(options.File, outputFile);

            string writeFile = isSameFile ? Path.GetTempFileName() : outputFile;

            var readerSettings = CreateReaderSettings();
            var writerSettings = CreateWriterSettings(options);

            using (var inputStream = File.OpenText(inputFile))
            using (var xmlReader = XmlReader.Create(inputStream, readerSettings))
            using (var outputStream = File.CreateText(writeFile))
            using (var xmlWriter = XmlWriter.Create(outputStream, writerSettings))
            {
                xmlWriter.WriteNode(xmlReader, false);
                xmlWriter.Flush();
            }

            if (!isSameFile)
                return;

            File.Copy(writeFile, outputFile, true);
            File.Delete(writeFile);
        }

        private static void FormatConsole(FormatOptions options)
        {
            var readerSettings = CreateReaderSettings();
            var writerSettings = CreateWriterSettings(options);

            string xml = Console.In.ReadToEnd();
            // remove null
            xml = xml.Replace("\0", "");

            using (var inputStream = new StringReader(xml))
            using (var xmlReader = XmlReader.Create(inputStream, readerSettings))
            using (var xmlWriter = XmlWriter.Create(Console.Out, writerSettings))
            {
                xmlWriter.WriteNode(xmlReader, false);
                xmlWriter.Flush();
            }
        }

        private static XmlReaderSettings CreateReaderSettings()
        {
            var readerSettings = new XmlReaderSettings
            {
                IgnoreWhitespace = true
            };
            return readerSettings;
        }

        private static XmlWriterSettings CreateWriterSettings(FormatOptions options)
        {
            var writerSettings = new XmlWriterSettings
            {
                Indent = options.Indent,
                CheckCharacters = options.CheckCharacters,
                NewLineOnAttributes = options.NewLineOnAttributes,
                OmitXmlDeclaration = options.OmitXmlDeclaration,
                IndentChars = options.IndentChars,
                NewLineChars = options.NewLineChars,
                Encoding = Encoding.UTF8
            };

            return writerSettings;
        }

        private static void OutputUsageHelp()
        {
            Console.WriteLine();
            Console.WriteLine("XmlFormatter.exe /f:<file> /o:<file> /Indent");
            Console.WriteLine();
            Console.WriteLine(" - OPTIONS -");
            Console.WriteLine();
            Console.WriteLine(Parser.ArgumentsUsage(typeof(FormatOptions)));
        }

        private static void OutputHeader()
        {
            Console.WriteLine("XmlFormatter v{0}", ThisAssembly.AssemblyInformationalVersion);
            Console.WriteLine();
        }

    }
}
