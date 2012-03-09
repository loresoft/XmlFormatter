using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlFormatter.CommandLine;

namespace XmlFormatter
{
    public class FormatOptions
    {
        [Argument(ArgumentType.AtMostOnce,
           LongName = "stdin",
           HelpText = "Use stdin/stdout mode.")]
        public bool InputOutput = false;

        [Argument(ArgumentType.AtMostOnce,
            ShortName = "f",
            LongName = "File",
            HelpText = "XML file to be formatted.")]
        public string File;

        [Argument(ArgumentType.AtMostOnce,
            ShortName = "o",
            LongName = "Output",
            HelpText = "Formated XML file.")]
        public string OutputFile;


        [Argument(ArgumentType.AtMostOnce,
           LongName = "Indent",
           HelpText = "Indent XML elements.")]
        public bool Indent = true;

        [Argument(ArgumentType.AtMostOnce,
           LongName = "NewLineOnAttributes",
           HelpText = "Write attributes on a new line.")]
        public bool NewLineOnAttributes = false;

        [Argument(ArgumentType.AtMostOnce,
           LongName = "OmitDeclaration",
           HelpText = "Omit XML declaration.")]
        public bool OmitXmlDeclaration = false;

        [Argument(ArgumentType.AtMostOnce,
           LongName = "CheckCharacters",
           HelpText = "Do character checking.")]
        public bool CheckCharacters = true;

        [Argument(ArgumentType.AtMostOnce,
           LongName = "NewLineChars",
           HelpText = "Character string to use for line breaks.")]
        public string NewLineChars = Environment.NewLine;

        [Argument(ArgumentType.AtMostOnce,
           LongName = "IndentChars",
           HelpText = "Character string to use when indenting.")]
        public string IndentChars = "  ";

    }
}
