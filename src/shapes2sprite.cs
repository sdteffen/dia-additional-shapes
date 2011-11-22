//
// shapes2sprite.cs: Convert dia shapes into CSS sprites
//
// Author:
//   Steffen Macke (sdteffen@sdteffen.de)
//
// Copyright (C) 2007, 2009 - 2011 Steffen Macke (http://dia-installer.de)
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

public class Shapes2Sprite
{
    public static void Main(string[] args)
    {
        if ((args.Length == 1 && ("-h" == args[0] || "--help" == args[0])) || args.Length > 1)
        {
            Console.Error.WriteLine("USAGE: shapes2sprite [Options]");
            Console.Error.WriteLine("Options:");
            Console.Error.WriteLine("-h, --help                 Display help and exit");
            Console.Error.WriteLine("-v, --version              Display version and exit");

            return;
        }

        if (1 == args.Length && ("-v" == args[0] || "--version" == args[0]))
        {
            Console.Error.WriteLine("shapes2sprite 0.2.1");
            Console.Error.WriteLine("Copyright (c) 2007, 2009 - 2011 Steffen Macke");
            return;
        }

	string montagecmd = "/* montage -geometry +0+0 -tile ";
	int objectcount = 0;
	string files = "";
        int x=0;
	
	Console.WriteLine(".icon { width: 22px; height: 22px; }");
	System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("sheets");
        foreach (System.IO.FileInfo f in dir.GetFiles("*.sheet"))
        {
		string sheet = f.Name.Replace(".sheet", "");
		XPathDocument document = new XPathDocument(f.FullName);
		XPathNavigator nav = document.CreateNavigator();
		XmlNamespaceManager manager = new XmlNamespaceManager(nav.NameTable);
		manager.AddNamespace("dia", "http://www.lysator.liu.se/~alla/dia/dia-sheet-ns");

	   	XPathExpression query = nav.Compile("/dia:sheet/dia:contents/dia:object");
		query.SetContext(manager);
		XPathNodeIterator links = nav.Select(query);

	 	while (links.MoveNext())
		{
			string objectname = links.Current.GetAttribute("name", "");
			string icon =  GetObjectIcon(sheet, objectname);
	      		files += icon + " ";
			objectcount++;
			Console.WriteLine("." + sheet + "_" + icon.Replace(".png","") + " {background: transparent url(s.png) -"+x+"px 0px no-repeat;}");
			x += 22;
		}
	} 
	montagecmd += objectcount + "x1 " + files + " s.png */";
	Console.WriteLine(montagecmd);
    }

    // Loop through the shapes and find the icon image name
    // @todo support icons from Dia itself
    public static string GetObjectIcon(string sheet, string objectname)
    {
	try
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
	} catch (Exception e) {}
        return "";
    }

}

