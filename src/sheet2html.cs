//
// sheet2html.cs: Convert dia sheets into multiview HTML fragments
//
// Author:
//   Steffen Macke (sdteffen@sdteffen.de)
//
// Copyright (C) 2007,2009 Steffen Macke (http://dia-installer.de)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Xml.XPath;
using System.Xml;
using System.Collections;

public class Sheet2Html
{
    public static void Main(string[] args)
    {
        if (args.Length < 1 || args.Length > 3)
        {
            Console.Error.WriteLine("USAGE: sheet2html [sheetname|-v|--version] [outputdir] [author]");
            return;
        }

        if ("-v" == args[0] || "--version" == args[0])
        {
            Console.Error.WriteLine("sheet2html 0.1.0");
            Console.Error.WriteLine("Copyright (c) 2007, 2009 Steffen Macke");
            return;
        }

        System.IO.DirectoryInfo outputdir = new System.IO.DirectoryInfo(".");

        if (2 <= args.Length)
            outputdir = new System.IO.DirectoryInfo(args[1]);

        string author = "";

        if (3 == args.Length)
            author = args[2];

        XPathDocument document = new XPathDocument("sheets/" + args[0] + ".sheet");
        XPathNavigator nav = document.CreateNavigator();
        XmlNamespaceManager manager = new XmlNamespaceManager(nav.NameTable);
        manager.AddNamespace("dia", "http://www.lysator.liu.se/~alla/dia/dia-sheet-ns");

        // Build language list
        ArrayList languages = new ArrayList();
        languages.Add("en");
        XPathExpression namequery = nav.Compile("/dia:sheet/dia:name");
        namequery.SetContext(manager);
        XPathNodeIterator names = nav.Select(namequery);
        while (names.MoveNext())
        {
            if("" != names.Current.XmlLang)
                languages.Add(names.Current.XmlLang);
        }
        XPathExpression sheetdescquery = nav.Compile("/dia:sheet/dia:description");
        sheetdescquery.SetContext(manager);
        foreach (string language in languages)
        {
            // includes are not available for all languages, fall back to en
            string includelanguage = "en";
            if ("de" == language)
                includelanguage = "de";
            XmlTextWriter output = new XmlTextWriter(outputdir.ToString()+"/index.html."+language,System.Text.Encoding.UTF8);
            output.Formatting = Formatting.Indented;
            output.WriteDocType("html","-//W3C//DTD XHTML 1.0 Transitional//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd","");
            output.WriteStartElement("html");
            output.WriteAttributeString("lang", language);
            output.WriteAttributeString("xml:lang", language);
            output.WriteStartElement("head");
            names = nav.Select(namequery);
            string sheetname = GetValueI18n(language, names);
            output.WriteElementString("title", sheetname);
            
            output.WriteStartElement("meta");
            output.WriteAttributeString("http-equiv", "Content-type");
            output.WriteAttributeString("content", "text/html; charset=utf-8");
            output.WriteEndElement();

            output.WriteComment("#include virtual=\"/include/head_yaml.html\"");            
            XPathNodeIterator sheetdescriptions = nav.Select(sheetdescquery);
            string sheetdescription = GetValueI18n(language, sheetdescriptions);
            
            output.WriteStartElement("meta");
            output.WriteAttributeString("name", "keywords");
            // @todo: Improve keywords
            output.WriteAttributeString("content", "Dia "+sheetname+" "+sheetdescription);
            output.WriteEndElement();

            output.WriteStartElement("meta");
            output.WriteAttributeString("name", "description");
            output.WriteAttributeString("content", sheetdescription);
            output.WriteEndElement();

            // @todo: Canonical URL

            output.WriteEndElement(); // head
            output.WriteStartElement("body");
            output.WriteStartElement("div");
            output.WriteAttributeString("class", "page_margins");
            output.WriteStartElement("div");
            output.WriteAttributeString("class", "page");
            output.WriteComment("#include virtual=\"/include/"+includelanguage+"/header_yaml.html\"");

            output.WriteStartElement("div");
            output.WriteAttributeString("id", "main");
            output.WriteStartElement("div");
            output.WriteAttributeString("id", "col1");
            output.WriteStartElement("div");
            output.WriteAttributeString("id", "col1_content");
            output.WriteAttributeString("class", "clearfix");
            
            output.WriteElementString("h1", sheetname);           

            // @todo: Use gettext
            string example = "Example";
            if("de" == language)
                example = "Beispieldiagramm";
            output.WriteElementString("h2", example);

            output.WriteStartElement("img");
            output.WriteAttributeString("alt", sheetname);
            output.WriteAttributeString("src", "images/" + args[0] + ".png");
            output.WriteEndElement(); // img

            if ("" != author)
            {
                // @todo Use gettext
                string authorheader = "Author";
                if ("de" == language)
                    authorheader = "Autor";
                output.WriteElementString("h2", authorheader);
                output.WriteElementString("div", author);
            }

            if (1 < languages.Count)
            {
                // @todo Use gettext
                string languageheader = "Languages";
                if ("de" == language)
                    languageheader = "Andere Sprachen";
                output.WriteElementString("h2", languageheader);
                output.WriteStartElement("div");
                output.WriteAttributeString("id", "flags");
                foreach (string flag in languages)
                {
                    if (flag == language)
                        continue;
                    output.WriteStartElement("a");
                    output.WriteAttributeString("href", "index.html."+flag);
                    output.WriteStartElement("img");
                    output.WriteAttributeString("alt", flag);
                    // @todo: Use CSS sprites
                    output.WriteAttributeString("src", "../../images/" + flag + ".png");
                    output.WriteEndElement(); // img
                    output.WriteEndElement(); // a
                }
                output.WriteEndElement(); // div
            }
            output.WriteEndElement(); // div col1_content
            output.WriteEndElement(); // div col1

            output.WriteStartElement("div");
            output.WriteAttributeString("id", "col3");
            output.WriteStartElement("div");
            output.WriteAttributeString("id", "col3_content");
            output.WriteAttributeString("class", "clearfix");

            // @todo: Use gettext
            string objectlist = "Object list";
            if ("de" == language)
                objectlist = "Liste der Objekte";
            output.WriteElementString("h2", objectlist);

            output.WriteStartElement("table");

            XPathExpression query = nav.Compile("/dia:sheet/dia:contents/dia:object");
            query.SetContext(manager);
            XPathNodeIterator links = nav.Select(query);

            while (links.MoveNext())
            {
                string objectname = links.Current.GetAttribute("name", "");
                output.WriteStartElement("tr");
                XPathExpression descquery = nav.Compile("/dia:sheet/dia:contents/dia:object[@name='" + objectname + "']/dia:description");
                descquery.SetContext(manager);
                XPathNodeIterator objectdescriptions = nav.Select(descquery);
                string objectdescription = GetValueI18n(language, objectdescriptions);
                output.WriteStartElement("td");
                output.WriteStartElement("img");
                // @todo Verify that image exists
                // @todo Use CSS sprites
                output.WriteAttributeString("alt", objectdescription);
                output.WriteAttributeString("src", "images/" + GetObjectIcon(args[0], objectname));
                output.WriteEndElement();
                output.WriteEndElement();
                output.WriteElementString("td", objectdescription);

                output.WriteEndElement(); // tr
            }
            output.WriteEndElement(); // table

            output.WriteEndElement(); // div col3_content
            output.WriteEndElement(); // div col3

            output.WriteComment("#include virtual=\"/include/"+includelanguage+"/footer_yaml.html\"");
            output.WriteEndElement(); // div main
            output.WriteEndElement(); // div class page
            output.WriteEndElement(); // div class page_margins
            output.WriteEndElement(); // body
            output.WriteEndElement(); // html
            output.Close();
        }
    }

    // Return the value of a given language from an XPath
    public static string GetValueI18n(string language, XPathNodeIterator iterator)
    {
        string result = "";
        if ("en" == language)
            language = "";
        while (iterator.MoveNext())
        {
            if (language == iterator.Current.XmlLang)
            {
                result = iterator.Current.Value;
                break;
            }
            // Fall back to English, if language is not available
            if ("" == result && "" == iterator.Current.XmlLang)
                result = iterator.Current.Value;
        }
        return result;
    }

    // Loop through the shapes and find the icon image name
    public static string GetObjectIcon(string sheet, string objectname)
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("shapes/" + sheet);
        foreach (System.IO.FileInfo f in dir.GetFiles("*.shape"))
        {
            XmlDocument shape = new XmlDocument();
            shape.Load(f.FullName);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(shape.NameTable);
            nsmgr.AddNamespace("", "http://www.daa.com.au/~james/dia-shape-ns");
            XmlNodeList nodeList;
            nodeList = shape.GetElementsByTagName("name", "http://www.daa.com.au/~james/dia-shape-ns");
            foreach (XmlNode name in nodeList)
            {
                if (name.ParentNode.Name == "shape" && name.InnerText == objectname)
                {
                    XmlNodeList iconList;
                    iconList = shape.GetElementsByTagName("icon", "http://www.daa.com.au/~james/dia-shape-ns");
                    foreach (XmlNode icon in iconList)
                    {
                        return icon.InnerText;
                    }
                }
            }
        }
        return "";
    }

}


