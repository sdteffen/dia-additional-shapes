//
// shapes2sprite.cs: Convert dia shapes and sheets into JSON
//
// Author:
//   Steffen Macke (sdteffen@sdteffen.de)
//
// Copyright (C) 2014 Steffen Macke (http://dia-installer.de)
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

// JSON data shapes and objects
public class Shapes2Json:DiaCommon
{
  public static void Main (string[]args)
  {
    if ((args.Length == 1 && ("-h" == args[0] || "--help" == args[0]))
	|| args.Length > 3)
      {
	Console.Error.WriteLine ("USAGE: shapes2json [Options]");
	Console.Error.WriteLine ("Options:");
	Console.Error.WriteLine
	  ("--datadir=datadir          Path where sheets and shapes reside");
	Console.Error.WriteLine
	  ("--language=language        2-letter language code, default is 'en'");
	Console.Error.WriteLine
	  ("--sheets                   Write sheet data");
	Console.Error.WriteLine
	  ("-h, --help                 Display help and exit");
	Console.Error.WriteLine
	  ("-v, --version              Display version and exit");
	return;
      }

    if (1 == args.Length && ("-v" == args[0] || "--version" == args[0]))
      {
	Console.Error.WriteLine ("shapes2json 0.1.0");
	Console.Error.WriteLine ("Copyright (c) 2011 - 2014 Steffen Macke");
	return;
      }

    string language = "en";
    bool sheets = false;
    for (int i = 0; i < args.Length; i++)
      {
	if (10 < args[i].Length && "--datadir=" == args[i].Substring (0, 10))
	  Directory.SetCurrentDirectory (args[i].Substring (10));
	if (11 < args[i].Length && "--language=" == args[i].Substring (0, 11))
	  language = args[i].Substring (11);
	if ("--sheets" == args[i])
	  sheets = true;
      }

    bool first = true;
    string o = "";
    int c = 0;
    DiaIconFinder iconfinder = new DiaIconFinder ();

    Console.WriteLine ("{");

    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo ("sheets");
    foreach (System.IO.FileInfo f in dir.GetFiles ("*.sheet"))
    {
      string sheet = f.Name.Replace (".sheet", "");
      XPathDocument document = new XPathDocument (f.FullName);
      XPathNavigator nav = document.CreateNavigator ();
      XmlNamespaceManager manager = new XmlNamespaceManager (nav.NameTable);
      manager.AddNamespace ("dia",
			    "http://www.lysator.liu.se/~alla/dia/dia-sheet-ns");
      if (sheets)
	{
	  if (first)
	    {
	      first = false;
	      o = "";
	    }
	  else
	    o = ",";
	  XPathExpression namequery = nav.Compile ("/dia:sheet/dia:name");
	  namequery.SetContext (manager);
	  XPathExpression descquery =
	    nav.Compile ("/dia:sheet/dia:description");
	  descquery.SetContext (manager);
	  Console.WriteLine (o + "\"" + sheet + "\":{\"n\":\"" +
			     GetValueI18n (language,
					   nav.Select (namequery)).
			     Replace ("\"",
				      "\\\"") + "\",\"d\":\"" +
			     GetValueI18n (language,
					   nav.Select (descquery)).
			     Replace ("\"", "\\\"") + "\"}");
	}
      else
	{
	  XPathExpression query =
	    nav.Compile ("/dia:sheet/dia:contents/dia:object");
	  query.SetContext (manager);
	  XPathNodeIterator links = nav.Select (query);

	  while (links.MoveNext ())
	    {
	      try
	      {
		string objectname = links.Current.GetAttribute ("name", "");
		if (first)
		  {
		    first = false;
		    o = "";
		  }
		else
		  o = ",";
		XPathExpression descquery =
		  nav.Compile ("/dia:sheet/dia:contents/dia:object[@name='" +
			       objectname + "']/dia:description");
		descquery.SetContext (manager);
		XPathNodeIterator objectdescriptions = nav.Select (descquery);
		Console.WriteLine (o + "\"" + c.ToString () + "\":{\"n\":\"" +
				   objectname.Replace ("\"",
						       "\\\"") +
				   "\",\"c\":\"" +
				   iconfinder.
				   GetClassForObjectName (objectname).
				   Substring (1) + "\",\"d\":\"" +
				   GetValueI18n (language,
						 objectdescriptions).
				   Replace ("\"",
					    "\\\"") + "\",\"s\":\"" + sheet +
				   "\"}");
		c++;
	      }
	      catch
	      {
	      }
	    }
	}
    }
    Console.WriteLine ("}");
  }
}
