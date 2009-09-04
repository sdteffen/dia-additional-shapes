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
        if (args.Length != 1 && args.Length != 2)
        {
            Console.Error.WriteLine("USAGE: sheet2html [sheetname|-v|--version] [outputdir]");
            return;
        }

        if ("-v" == args[0] || "--version" == args[0])
        {
            Console.Error.WriteLine("sheet2html 0.1.0");
            Console.Error.WriteLine("Copyright (c) 2007, 2009 Steffen Macke");
            return;
        }

        System.IO.DirectoryInfo outputdir = new System.IO.DirectoryInfo(".");

        if (2 == args.Length)
            outputdir = new System.IO.DirectoryInfo(args[1]);

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
            output.WriteStartElement("body");
            
            output.WriteElementString("h1", sheetname);

            // @todo: Use gettext
            string example = "Example";
            if("de" == language)
                example = "Beispieldiagramm";
            output.WriteElementString("h2", example);

            output.WriteStartElement("img");
            output.WriteAttributeString("alt", sheetname);
            output.WriteAttributeString("src", "images/" + args[0] + ".png");
            output.WriteEndElement();

            // @todo: Use gettext
            string objectlist = "Object list";
            if ("de" == language)
                objectlist = "Liste der Objekte";
            output.WriteElementString("h2", objectlist);

            XPathExpression query = nav.Compile("/dia:sheet/dia:contents/dia:object");
            query.SetContext(manager);
            XPathNodeIterator links = nav.Select(query);

            while (links.MoveNext())
            {
                string sDescription = links.Current.Value;
                string sName = links.Current.GetAttribute("name", "");
                XPathExpression descquery = nav.Compile("/dia:sheet/dia:contents/dia:object[@name='" + sName + "']/dia:description");
                descquery.SetContext(manager);
                XPathNodeIterator descriptions = nav.Select(descquery);
                while (descriptions.MoveNext())
                {
                    Console.WriteLine(descriptions.Current.XmlLang);
                }
                Console.WriteLine("<!--" + sName + "-->");
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("shapes/" + args[0]);
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
                        if (name.ParentNode.Name == "shape" && name.InnerText == sName)
                        {
                            XmlNodeList iconList;
                            iconList = shape.GetElementsByTagName("icon", "http://www.daa.com.au/~james/dia-shape-ns");
                            foreach (XmlNode icon in iconList)
                            {
                                Console.WriteLine("<tr><td><img src=\"images/" + icon.InnerText + "\" /></td><td>" + sDescription + "</td></tr>");
                            }
                        }
                    }

                }
            }
            output.WriteEndElement(); // body
            output.WriteEndElement(); // html
            output.Close();
        }
    }

    public static string GetValueI18n(string language, XPathNodeIterator iterator)
    {
        string result = "";
        if ("en" == language)
            language = "";
        while (iterator.MoveNext())
        {
            if (language == iterator.Current.XmlLang)
                result = iterator.Current.Value;
        }
        return result;
    }
}


