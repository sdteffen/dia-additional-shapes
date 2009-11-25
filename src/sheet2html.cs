//
// sheet2html.cs: Convert dia sheets into multiview HTML documents
//
// Author:
//   Steffen Macke (sdteffen@sdteffen.de)
//
// Copyright (C) 2007, 2009 Steffen Macke (http://dia-installer.de)
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
        if (args.Length < 1 || (args.Length == 1 && ("-h" == args[0] )|| "--help" == args[0]))
        {
            Console.Error.WriteLine("USAGE: sheet2html [options] [sheetname]");
            Console.Error.WriteLine("Options:");
            Console.Error.WriteLine("--output-directory=DIR     Specify output directory");
            Console.Error.WriteLine("--author=AUTHOR            Specify sheet creator");
	    Console.Error.WriteLine("--montage                  Create montage commandline (CSS sprites)");
            Console.Error.WriteLine("--ssi                      Include SSI comments");
            Console.Error.WriteLine("--comes-with-dia           Sheet is part of the Dia distribution");
            Console.Error.WriteLine("-v, --version              Display version and exit");
            Console.Error.WriteLine("-h, --help                 Display help and exit");
            return;
        }

        if ("-v" == args[0] || "--version" == args[0])
        {
            Console.Error.WriteLine("sheet2html 0.1.0");
            Console.Error.WriteLine("Copyright (c) 2007, 2009 Steffen Macke");
            return;
        }

        // Defaults
        System.IO.DirectoryInfo outputdir = new System.IO.DirectoryInfo(".");
        string author = "";
        bool output_ssi = false;
        bool comes_with_dia = false;
        bool montage = false;

        // Parse commandline arguments
        for (int i = 0; i < args.Length; i++)
        {
            if (19 < args[i].Length && "--output-directory=" == args[i].Substring(0, 19))
                outputdir = new System.IO.DirectoryInfo(args[i].Substring(19));
                
            if (9 < args[i].Length && "--author=" == args[i].Substring(0, 9))
                author = args[i].Substring(9);

            if ("--ssi" == args[i])
                output_ssi = true;

            if ("--comes-with-dia" == args[i])
                comes_with_dia = true;
	    
	   if ("--montage" == args[i])
              montage = true;
        }

        XPathDocument document = new XPathDocument("sheets/" + args[args.Length-1] + ".sheet");
        XPathNavigator nav = document.CreateNavigator();
        XmlNamespaceManager manager = new XmlNamespaceManager(nav.NameTable);
        manager.AddNamespace("dia", "http://www.lysator.liu.se/~alla/dia/dia-sheet-ns");

        if(montage)
	{
   	    XPathExpression query = nav.Compile("/dia:sheet/dia:contents/dia:object");
            query.SetContext(manager);
            XPathNodeIterator links = nav.Select(query);
 	    string montagecmd = "montage -geometry +0+0 -tile ";
            int objectcount = 0;
            string files = "";
 	    while (links.MoveNext())
            {
                string objectname = links.Current.GetAttribute("name", "");
      		files += GetObjectIcon(args[args.Length-1], objectname) + " ";
		objectcount++;
            }
            montagecmd += objectcount + "x1 " + files + " " + args[args.Length-1] + "-sprite.png";
            Console.WriteLine(montagecmd);
	    return;	
        } 

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
            output.WriteAttributeString("xmlns", "http://www.w3.org/1999/xhtml");
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

            if (output_ssi)
            {
                output.WriteComment("#include virtual=\"/include/head_yaml.html\"");
                output.WriteStartElement("script");
                output.WriteAttributeString("type", "text/javascript");
                output.WriteComment("#include virtual=\"/include/js/helper.js\"");
                output.WriteEndElement(); // script
                output.WriteStartElement("link");
                output.WriteAttributeString("rel", "canonical");
                output.WriteAttributeString("href", "http://dia-installer.de/shapes/" + args[args.Length - 1] + "/index.html." + language);
                output.WriteEndElement(); // link
            }
            
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

            // CSS sprites
 	    output.WriteStartElement("style");
	    output.WriteString(".icon { width: 22px; height: 22px; }");
	    XPathExpression query = nav.Compile("/dia:sheet/dia:contents/dia:object");
            query.SetContext(manager);
            XPathNodeIterator links = nav.Select(query);
            int x=0;
            while (links.MoveNext())
            {
                string objectname = links.Current.GetAttribute("name", "");
      		GetObjectIcon(args[args.Length-1], objectname);
		output.WriteString("." + GetObjectIcon(args[args.Length-1], objectname).Replace(".png","") + " {background: transparent url(images/"+args[args.Length-1]+"-sprite.png) -"+x+"px 0px no-repeat;}\n");
		x += 22;
            }
	    output.WriteEndElement(); // style

            output.WriteEndElement(); // head
            output.WriteStartElement("body");
            output.WriteStartElement("div");
            output.WriteAttributeString("class", "page_margins");
            output.WriteStartElement("div");
            output.WriteAttributeString("class", "page");
            
            if(output_ssi)
                output.WriteComment("#include virtual=\"/include/"+includelanguage+"/header_yaml.html\"");

            output.WriteStartElement("div");
            output.WriteAttributeString("id", "main");
            output.WriteStartElement("div");
            output.WriteAttributeString("id", "col1");
            output.WriteStartElement("div");
            output.WriteAttributeString("id", "col1_content");
            output.WriteAttributeString("class", "clearfix");

            if (output_ssi)
                output.WriteComment("#include virtual=\"/include/block.html\"");

            output.WriteElementString("h1", sheetname);
            output.WriteStartElement("div");
            output.WriteString(sheetdescription);
            output.WriteString(". ");
            // @todo: Use gettext
            if ("de" == language)
                output.WriteString("Diese Objekte können sie zu Ihrer Dia-Werkzeugkiste hinzufügen.");
            else
                output.WriteString("These objects can be added to your Dia toolbox.");
            output.WriteEndElement(); // div
            // @todo: Use gettext
            string example = "Example";
            if("de" == language)
                example = "Beispieldiagramm";
            output.WriteElementString("h2", example);

            output.WriteStartElement("img");
            output.WriteAttributeString("alt", sheetname);
            output.WriteAttributeString("src", "images/" + args[args.Length-1] + ".png");
            output.WriteEndElement(); // img

            // @todo: Use gettext
            output.WriteElementString("h2", "Download");
            output.WriteStartElement("ul");
            output.WriteStartElement("li");
            output.WriteStartElement("a");
            output.WriteAttributeString("href", args[args.Length - 1] + ".zip");
            output.WriteAttributeString("class", "track");
            output.WriteString(args[args.Length - 1] + ".zip");
            output.WriteEndElement(); // a
            output.WriteString(" ");
            // @todo: Use gettext
            if ("de" == language)
                output.WriteString("Objekte und Objektbogen, als Zip-Archiv gepackt");
            else
                output.WriteString("sheet and objects, zipped");
            output.WriteEndElement(); // li
            output.WriteStartElement("li");
            output.WriteStartElement("a");
            output.WriteAttributeString("href", args[args.Length - 1] + ".dia");
            output.WriteAttributeString("class", "track");
            output.WriteString(args[args.Length - 1] + ".dia");
            output.WriteEndElement(); // a
            output.WriteString(" ");
            // @todo: Use gettext
            if ("de" == language)
                output.WriteString("Beispieldiagramm im Dia-Format");
            else
                output.WriteString("example diagram in Dia format");
            output.WriteEndElement(); // li
            output.WriteEndElement(); // ul

            // @todo: Use gettext
            output.WriteElementString("h2", "Installation");
            output.WriteStartElement("ul");
            output.WriteStartElement("li");
            // @todo: Use gettext
            if ("de" == language)
                output.WriteString("Automatisierte, wizard-basierte Installation:");
            else
                output.WriteString("Automated, wizard-based installation:");
            output.WriteString(" ");
            output.WriteStartElement("a");
            output.WriteAttributeString("href", "http://dia-installer.de/diashapes/index.html");
            output.WriteString("diashapes");
            output.WriteEndElement(); // a
            output.WriteEndElement(); // li
            output.WriteStartElement("li");
            // @todo: Use gettext
            if ("de" == language)
                output.WriteString("Manuelle Installation: Um die neuen Objekte zu benutzen, entpacken Sie diese Dateien in Ihren .dia Ordner und starten sie Dia neu.");
            else
                output.WriteString("Manual installation: extract the files to your .dia folder and restart Dia.");
            output.WriteEndElement(); // li
            output.WriteEndElement(); // ul

            if ("" != author && !comes_with_dia)
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

            if (output_ssi)
                output.WriteComment("#include virtual=\"/include/block.html\"");

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

            query = nav.Compile("/dia:sheet/dia:contents/dia:object");
            query.SetContext(manager);
            links = nav.Select(query);

            while (links.MoveNext())
            {
                string objectname = links.Current.GetAttribute("name", "");
                output.WriteStartElement("tr");
                XPathExpression descquery = nav.Compile("/dia:sheet/dia:contents/dia:object[@name='" + objectname + "']/dia:description");
                descquery.SetContext(manager);
                XPathNodeIterator objectdescriptions = nav.Select(descquery);
                string objectdescription = GetValueI18n(language, objectdescriptions);
                output.WriteStartElement("td");
		output.WriteStartElement("div");
		output.WriteAttributeString("class", "icon "+GetObjectIcon(args[args.Length-1], objectname).Replace(".png",""));
		output.WriteString(" ");
		output.WriteEndElement(); // div 
                output.WriteEndElement(); // td
                output.WriteElementString("td", objectdescription);

                output.WriteEndElement(); // tr
            }
            output.WriteEndElement(); // table

            output.WriteEndElement(); // div col3_content
            output.WriteEndElement(); // div col3

            if(output_ssi)
	    {
                output.WriteComment("#include virtual=\"/include/"+includelanguage+"/footer_yaml.html\"");
		output.WriteComment("#incude virtual=\"include/tail_yaml.html\"");
	    }
            
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


