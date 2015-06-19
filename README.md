# XmlFormatter

Format Xml Documents

## Features

* Indent and format Xml
* Attribute wrapping
* Standard Input/Output

## Usage

    XmlFormatter v1.0.0.0

    Must use either /stdin or /f:<file> switch.

    XmlFormatter.exe /f:<file> /o:<file> /Indent

     - OPTIONS -

    /stdin                  Use stdin/stdout mode. (/s)
    /File:<string>          XML file to be formatted. (/f)
    /Output:<string>        Formated XML file. (/o)
    /Indent                 Indent XML elements. (/I)
    /NewLineOnAttributes    Write attributes on a new line. (/N)
    /OmitDeclaration        Omit XML declaration. (/O)
    /CheckCharacters        Do character checking. (/C)
    /NewLineChars:<string>  Character string to use for line breaks.
    /IndentChars:<string>   Character string to use when indenting.
    