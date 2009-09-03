//
// sheet2html.cs: Simple tool to convert dia sheets to HTML fragments
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

public class Sheet2Html
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("USAGE: sheet2html sheetname");
            return;
        }

        XPathNavigator nav =
                new XPathDocument("sheets/" + args[0] + ".sheet").CreateNavigator();

        XPathDocument document = new XPathDocument("sheets/" + args[0] + ".sheet");
        XPathNavigator navi = document.CreateNavigator();
        XPathExpression query = nav.Compile("/dia:sheet/dia:contents/dia:object");
        XmlNamespaceManager manager = new XmlNamespaceManager(nav.NameTable);
        manager.AddNamespace("dia", "http://www.lysator.liu.se/~alla/dia/dia-sheet-ns");
        manager.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
        query.SetContext(manager);
        XPathNodeIterator links = nav.Select(query);

        while (links.MoveNext())
        {
            string sDescription = links.Current.Value;
            string sName = links.Current.GetAttribute("name", "");
            XPathExpression descquery = nav.Compile("/dia:sheet/dia:contents/dia:object[@name='"+sName+"']/dia:description");
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
    }
}


