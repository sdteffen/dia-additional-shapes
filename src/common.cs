//
// common.cs: Convert dia shapes into CSS sprites
//
// Author:
//   Steffen Macke (sdteffen@sdteffen.de)
//
// Copyright (C) 2007, 2009 - 2012 Steffen Macke (http://dia-installer.de)
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
using System.Collections.Generic;
using System.IO;

// Deal with all dia icons
class DiaIcons
{
	public const string SHEET_SPECIFIC = "SHEET_SPECIFIC";
	public SortedDictionary<string,string> icons;
	
	// Parse the available icons
	public DiaIcons()
	{
		icons = new SortedDictionary<string, string>();
		string[] folders = {"sheets", "shapes"};
		var fullpath = new DirectoryInfo(".").FullName;
		foreach(string folder in folders)
		{
			DirectoryInfo dir = new DirectoryInfo(folder);
    		foreach (DirectoryInfo d in dir.GetDirectories())
			{
				foreach(FileInfo f in d.GetFiles("*.png"))
				{
					var path = f.FullName.Replace(fullpath+"/", "");
					string duplicatevalue;
					if(icons.TryGetValue(f.Name, out duplicatevalue))
					{
						if(SHEET_SPECIFIC != duplicatevalue)
						{
							icons.Add(DiaCss.CanonicalizePath(duplicatevalue), duplicatevalue);
							icons.Add(DiaCss.CanonicalizePath(path), path);
							icons[f.Name] = SHEET_SPECIFIC;
						}
						else
						{
							icons.Add(DiaCss.CanonicalizePath(path), path);
						}
					}
					else
					{
						icons.Add(f.Name, path);
					}
				}
			}
		}
	}
}

// Dia CSS sprite related functionality
class DiaCss
{
	// Convert a relative path into a canonical CSS class
	public static string CanonicalizePath(string path)
	{
		return path.Replace("shapes/", "").Replace("sheets/", "").Replace("/", "");
	}
}

// Find icons in sheets and shapes
class DiaIconFinder
{
	DiaIcons icons;
	// objectname is key, pngname value
	Dictionary<string,string> objecticons;
	// objectname is key, sheet value
	Dictionary<string,string> objectsheets;
	
	public DiaIconFinder()
	{
		icons = new DiaIcons();
		objecticons = new Dictionary<string, string>();
		objectsheets = new Dictionary<string, string>();
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
				try
				{
					string objectname = links.Current.GetAttribute("name", "");					
					string iconpath = DiaIconFinder.GetPathFromNode(sheet, objectname, nav);
					if("" != iconpath)
						objecticons.Add(objectname, iconpath);
					objectsheets.Add(objectname, sheet);
				} catch {}
			}
		}
		System.IO.DirectoryInfo sdir = new System.IO.DirectoryInfo("shapes");
		foreach (System.IO.DirectoryInfo idir in sdir.GetDirectories())
		{
			foreach (System.IO.FileInfo f in idir.GetFiles("*.shape"))
			{
			    XmlDocument shape = new XmlDocument();
			    shape.Load(f.FullName);
			    XmlNamespaceManager nsmgr = new XmlNamespaceManager(shape.NameTable);
			    nsmgr.AddNamespace("", "http://www.daa.com.au/~james/dia-shape-ns");
			    XmlNodeList nodeList;
			    nodeList = shape.GetElementsByTagName("name", "http://www.daa.com.au/~james/dia-shape-ns");
			    foreach (XmlNode name in nodeList)
			    {
				string dummy;
			        if ((name.ParentNode.Name == "shape") && !(objecticons.TryGetValue(name.InnerText, out dummy)))
			        {
			            XmlNodeList iconList;
			            iconList = shape.GetElementsByTagName("icon", "http://www.daa.com.au/~james/dia-shape-ns");
			            foreach (XmlNode icon in iconList)
			            {
			                objecticons.Add(name.InnerText, icon.InnerText);
			            }
			        }
			    }
			    
			}
		}
	}
	
	// For a given Dia object, return the matching CSS class
	public string GetClassForObjectName(string name)
	{
		string cssclass = name;
		string path = "";
		string sheet = "";

		switch (name)
		{
			case "UML - Class":
				cssclass = "dumlclass";
			break;
			case "UML - Note":
				cssclass = "dc2sheetnote";
			break;
			case "UML - Dependency":
				cssclass ="dc2sheetdependency";
		     break;
   case "UML - Realizes":
      cssclass = "drealizes";
    break;
   case "UML - Generalization":
      cssclass = "dgeneralization";
    break;
   case "UML - Association":
      cssclass = "dassociation";
    break;
   case "UML - Implements":
      cssclass = "dimplements";
    break;
   case "UML - Constraint":
      cssclass = "dconstraint";
    break;
   case "UML - SmallPackage":
      cssclass = "dsmallpackage";
    break;
   case "UML - LargePackage":
      cssclass = "dlargepackage";
    break;
   case "UML - Actor":
      cssclass = "dactor";
    break;
   case "UML - Usecase":
      cssclass = "dcase";
    break;
   case "UML - Lifeline":
      cssclass = "dlifeline";
    break;
   case "UML - Object":
      cssclass = "dobject";
    break;
   case "UML - Message":
      cssclass = "dmessage";
    break;
   case "UML - Component":
      cssclass = "dc2sheetcomponent";
    break;
   case "UML - Component Feature":
      cssclass = "dfacet";
    break;
   case "UML - Node":
      cssclass = "dnode";
    break;
   case "UML - Classicon":
      cssclass = "dclassicon";
    break;
   case "UML - State Term":
      cssclass = "dstate_term";
    break;
   case "UML - State":
      cssclass = "dstate";
    break;
   case "UML - Activity":
       cssclass = "dactivity";
    break;
   case "UML - Branch":
      cssclass = "dbranch";
    break;
   case "UML - Fork":
      cssclass = "dfork";
    break;
   case "UML - Transition":
	cssclass = "dtransition";
	break;
			default:
				if(objecticons.TryGetValue(name, out cssclass))
				{
					if(icons.icons.TryGetValue(cssclass, out path))
					{
						if(DiaIcons.SHEET_SPECIFIC == path)
						{
							if(objectsheets.TryGetValue(name, out sheet))
							{
								cssclass = "d"+sheet+DiaCss.CanonicalizePath(cssclass).Replace(".png", "");
							}
						}
						else 
							cssclass = "d"+DiaCss.CanonicalizePath(cssclass).Replace(".png", "");
					}
				}
			break;
		}
	
		return cssclass;
	}
	
    // Returns the relative path to the icon file
	// @todo make non-static, nav as member
    public static string GetPathFromNode(string sheet, string objectname, XPathNavigator nav)
    {
		XmlNamespaceManager manager = new XmlNamespaceManager(nav.NameTable);
		manager.AddNamespace("dia", "http://www.lysator.liu.se/~alla/dia/dia-sheet-ns");
	
		XPathExpression iconquery = nav.Compile("/dia:sheet/dia:contents/dia:object[@name='" + objectname + "']/dia:icon");
		iconquery.SetContext(manager);
	        XPathNodeIterator objectdescriptions = nav.Select(iconquery);
		while(objectdescriptions.MoveNext())
		{
			return objectdescriptions.Current.Value;
		}
		
		return GetPathFromObjectName(sheet, objectname);
    }

    // Loop through the shapes and find the icon image name
    public static string GetPathFromObjectName(string sheet, string objectname)
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
		} catch {}
        return "";
    }
}
