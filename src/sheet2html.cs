//
// sheet2html.cs: Convert dia sheets into multiview HTML documents
//
// Author:
//   Steffen Macke (sdteffen@sdteffen.de)
//
// Copyright (C) 2007, 2009 - 2014 Steffen Macke (http://dia-installer.de)
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

public class Sheet2Html : DiaCommon
{
  public static void Main (string[]args)
  {
    if (args.Length < 1
	|| (args.Length == 1 && ("-h" == args[0]) || "--help" == args[0]))
      {
	Console.Error.WriteLine ("USAGE: sheet2html [options] [sheetname]");
	Console.Error.WriteLine ("Options:");
	Console.Error.WriteLine
	  ("--author=AUTHOR            Specify sheet creator");
	Console.Error.WriteLine
	  ("--comes-with-dia           Sheet is part of the Dia distribution");
	Console.Error.WriteLine
	  ("--datadir=datadir          Path where sheets and shapes reside");
	Console.Error.WriteLine
	  ("--example-author=AUTHOR     Specify example author");
	Console.Error.WriteLine
	  ("-h, --help                 Display help and exit");
	Console.Error.WriteLine
	  ("--original-example=URL     Link to the original example file");
	Console.Error.WriteLine
	  ("--output-directory=DIR     Specify output directory");
	Console.Error.WriteLine
	  ("--noads                    Add noads tags to template");
	Console.Error.WriteLine
	  ("--tpl                      Create Smarty Template");
	Console.Error.WriteLine
	  ("-v, --version              Display version and exit");

	return;
      }

    if ("-v" == args[0] || "--version" == args[0])
      {
	Console.Error.WriteLine ("sheet2html 0.9.3");
	Console.Error.
	  WriteLine ("Copyright (c) 2007, 2009 - 2014 Steffen Macke");
	return;
      }

    // Defaults
    System.IO.DirectoryInfo outputdir = new System.IO.DirectoryInfo (".");
    string author = "";
    string exampleauthor = "";
    bool output_tpl = false;
    bool comes_with_dia = false;
    string noads = "";
    string output_suffix = "html";
    string originalexample = "";
    string sheet_path_fragment = args[args.Length - 1];

    // Parse commandline arguments
    for (int i = 0; i < args.Length; i++)
      {
	if (19 < args[i].Length
	    && "--output-directory=" == args[i].Substring (0, 19))
	  outputdir = new System.IO.DirectoryInfo (args[i].Substring (19));

	if (9 < args[i].Length && "--author=" == args[i].Substring (0, 9))
	  author = args[i].Substring (9);

	if (10 < args[i].Length && "--datadir=" == args[i].Substring (0, 10))
	  System.IO.Directory.SetCurrentDirectory (args[i].Substring (10));

	if (17 < args[i].Length
	    && "--example-author=" == args[i].Substring (0, 17))
	  exampleauthor = args[i].Substring (17);

	if (19 < args[i].Length
	    && "--original-example=" == args[i].Substring (0, 19))
	  originalexample = args[i].Substring (19);

	if ("--tpl" == args[i])
	  {
	    output_tpl = true;
	    output_suffix = "tpl";
	  }

	if ("--comes-with-dia" == args[i])
	  comes_with_dia = true;

	if ("--noads" == args[i])
	  noads = " noads=1 ";
      }

    DiaIconFinder iconfinder = new DiaIconFinder ();

    XPathDocument document =
      new XPathDocument ("sheets/" + args[args.Length - 1] + ".sheet");
    XPathNavigator nav = document.CreateNavigator ();
    XmlNamespaceManager manager = new XmlNamespaceManager (nav.NameTable);
    manager.AddNamespace ("dia",
			  "http://www.lysator.liu.se/~alla/dia/dia-sheet-ns");

    // Build language list
    ArrayList languages = new ArrayList ();
    languages.Add ("en");
    XPathExpression namequery = nav.Compile ("/dia:sheet/dia:name");
    namequery.SetContext (manager);
    XPathNodeIterator names = nav.Select (namequery);
    while (names.MoveNext ())
      {
	if ("" != names.Current.XmlLang)
	  languages.Add (names.Current.XmlLang);
      }
    XPathExpression sheetdescquery =
      nav.Compile ("/dia:sheet/dia:description");
    sheetdescquery.SetContext (manager);
    foreach (string language in languages)
    {
      // includes are not available for all languages, fall back to en
      string includelanguage = "en";
      if (("de" == language) || ("es" == language) || ("fr" == language))
	includelanguage = language;
      string outputfilename =
	outputdir.ToString () + "/index." + output_suffix;
      if ("en" != language)
	outputfilename = outputfilename + "." + language;
      XmlTextWriter output =
	new XmlTextWriter (outputfilename, System.Text.Encoding.UTF8);
      output.Formatting = Formatting.Indented;
      if (output_tpl)
	{
	  output.WriteRaw ("{include file='header.tpl' language='" +
			   includelanguage + "'}");
	}
      else
	{
	  output.WriteStartElement ("html");
	  output.WriteAttributeString ("xmlns",
				       "http://www.w3.org/1999/xhtml");
	  output.WriteAttributeString ("lang", language);
	  output.WriteAttributeString ("xml:lang", language);
	  output.WriteStartElement ("head");
	}
      names = nav.Select (namequery);
      string sheetname = GetValueI18n (language, names);
      XPathNodeIterator sheetdescriptions = nav.Select (sheetdescquery);
      string sheetdescription = GetValueI18n (language, sheetdescriptions);
      output.WriteElementString ("title",
				 "{t}Dia Sheet{/t} " + sheetname + ": " +
				 sheetdescription);

      output.WriteStartElement ("meta");
      output.WriteAttributeString ("http-equiv", "Content-type");
      output.WriteAttributeString ("content", "text/html; charset=utf-8");
      output.WriteEndElement ();

      if (output_tpl)
	{
	  output.WriteStartElement ("link");
	  output.WriteAttributeString ("rel", "canonical");
	  output.WriteAttributeString ("href",
				       "http://dia-installer.de/shapes/" +
				       args[args.Length - 1] +
				       "/index.html." + language);
	  output.WriteEndElement ();	// link
	}



      output.WriteStartElement ("meta");
      output.WriteAttributeString ("name", "description");
      if (comes_with_dia)
	output.WriteAttributeString ("content",
				     "{t}Sheet{/t} " + sheetname + ": " +
				     sheetdescription +
				     ". {t}Learn more about these objects from Dia's comprehensive toolbox. See a sample diagram and download it in different formats.{/t}");
      else
	output.WriteAttributeString ("content",
				     "{t}Sheet{/t} " + sheetname + ": " +
				     sheetdescription +
				     ". {t}Learn more about these objects, how they can be added to your Dia toolbox and how you can draw your diagrams with them. See a sample diagram and download it in different formats.{/t}");
      output.WriteEndElement ();

      // CSS sprites
      output.WriteStartElement ("link");
      output.WriteAttributeString ("rel", "stylesheet");
      output.WriteAttributeString ("type", "text/css");
      // @todo timestamp based cache buster
      output.WriteAttributeString ("href",
				   "http://dia-installer.de/shapes/d.css");
      output.WriteEndElement ();	// style
      if (output_tpl)
	{
	  output.WriteRaw
	    ("{include file='body.tpl' folder='/shapes' page='/shapes/" +
	     args[args.Length - 1] + "/index.html' page_title='" + sheetname +
	     "' language='" + language + "'" + noads + "}");
	}
      else
	{
	  output.WriteEndElement ();	// head
	  output.WriteStartElement ("body");
	}

      output.WriteElementString ("h1", sheetname);
      output.WriteStartElement ("div");
      output.WriteString (sheetdescription);
      output.WriteString (". ");
      if (comes_with_dia)
	output.WriteString
	  ("{t}These objects are part of the standard Dia toolbox.{/t}");
      else
	output.WriteString
	  ("{t}These objects can be added to your Dia toolbox.{/t}");
      output.WriteEndElement ();	// div
      string example = "{t}Example{/t}";
      output.WriteElementString ("h2", example);

      output.WriteStartElement ("img");
      output.WriteAttributeString ("alt", sheetname);
      output.WriteAttributeString ("src",
				   "/shapes/" + args[args.Length - 1] +
				   "/images/" + args[args.Length - 1] +
				   ".png");
      output.WriteEndElement ();	// img

      output.WriteElementString ("h2", "{t}Download{/t}");
      output.WriteStartElement ("ul");
      if (!comes_with_dia)
	{
	  output.WriteStartElement ("li");
	  output.WriteStartElement ("a");
	  output.WriteAttributeString ("href",
				       "/shapes/" + args[args.Length - 1] +
				       "/" + args[args.Length - 1] + ".zip");
	  output.WriteAttributeString ("class", "track");
	  output.WriteString (args[args.Length - 1] + ".zip");
	  output.WriteEndElement ();	// a
	  output.WriteString (" ");
	  output.WriteString ("{t}sheet and objects, zipped{/t}");
	  output.WriteEndElement ();	// li
	}
      output.WriteStartElement ("li");
      output.WriteStartElement ("a");
      output.WriteAttributeString ("href",
				   "/shapes/" + args[args.Length - 1] + "/" +
				   args[args.Length - 1] + ".dia");
      output.WriteAttributeString ("class", "track");
      output.WriteString (args[args.Length - 1] + ".dia");
      output.WriteEndElement ();	// a
      output.WriteString (" ");
      output.WriteString ("{t}example diagram in Dia format{/t}");
      if ("" != exampleauthor)
	{
	  output.WriteString (", {t 1='" + exampleauthor +
			      "'}created by %1{/t}");
	}
      if ("" != originalexample)
	{
	  output.WriteString
	    (". {capture name=original_file assign=original_file}");
	  output.WriteStartElement ("a");
	  output.WriteAttributeString ("href", originalexample);
	  output.WriteAttributeString ("target", "_blank");
	  output.WriteAttributeString ("rel", "nofollow");
	  output.WriteString ("{t}original file{/t}");
	  output.WriteEndElement ();	// a
	  output.WriteString
	    ("{/capture}{t escape=no 1=$original_file}See the %1{/t}");
	}
      output.WriteEndElement ();	// li

      output.WriteStartElement ("li");
      output.WriteStartElement ("a");
      output.WriteAttributeString ("href",
				   "/shapes/" + args[args.Length - 1] +
				   "/images/" + args[args.Length - 1] +
				   ".svg");
      output.WriteAttributeString ("class", "track");
      output.WriteString (args[args.Length - 1] + ".svg");
      output.WriteEndElement ();	// a
      output.WriteString (" ");
      output.WriteString ("{t}example diagram in SVG format{/t}");
      output.WriteEndElement ();	// li

      output.WriteEndElement ();	// ul     

      output.WriteElementString ("h2", "{t}Installation{/t}");
      if (comes_with_dia)
	{
	  output.WriteStartElement ("p");
	  output.WriteString
	    ("{t}These objects are part of the standard Dia toolbox.{/t}" +
	     " ");
	  output.WriteString ("{t}To use them simply install Dia:{/t}" + " ");
	  output.WriteStartElement ("a");
	  output.WriteAttributeString ("href", "../../index.html");
	  output.WriteString ("{t}Dia{/t}");
	  output.WriteEndElement ();	// a
	  output.WriteEndElement ();	// p
	}
      else
	{
	  output.WriteStartElement ("ul");
	  output.WriteStartElement ("li");
	  output.WriteString ("{t}Automated, wizard-based installation:{/t}");
	  output.WriteString (" ");
	  output.WriteStartElement ("a");
	  output.WriteAttributeString ("href",
				       "http://dia-installer.de/diashapes/index.html");
	  output.WriteString ("diashapes");
	  output.WriteEndElement ();	// a
	  output.WriteEndElement ();	// li
	  output.WriteStartElement ("li");
	  output.WriteString
	    ("{t}Manual installation: extract the files to your .dia folder and restart Dia.{/t}");
	  output.WriteEndElement ();	// li
	  output.WriteEndElement ();	// ul
	}

      if ("" != author && !comes_with_dia)
	{
	  string authorheader = "{t}Author{/t}";
	  output.WriteElementString ("h2", authorheader);
	  output.WriteElementString ("div", author);
	}

      if ((1 < languages.Count) && (!output_tpl))
	{
	  string languageheader = "{t}Languages{/t}";
	  output.WriteElementString ("h2", languageheader);
	  output.WriteStartElement ("div");
	  output.WriteAttributeString ("id", "flags");
	  foreach (string flag in languages)
	  {
	    if (flag == language)
	      continue;
	    output.WriteStartElement ("a");
	    output.WriteAttributeString ("href", "index.html." + flag);
	    output.WriteStartElement ("img");
	    output.WriteAttributeString ("alt", flag);
	    // @todo: Use CSS sprites
	    output.WriteAttributeString ("src",
					 "../../images/" + flag + ".png");
	    output.WriteEndElement ();	// img
	    output.WriteEndElement ();	// a
	  }
	  output.WriteEndElement ();	// div
	}

      if (output_tpl)
	{
	  output.WriteRaw ("{capture name='col3_content'}");
	}
      else
	{
	  output.WriteEndElement ();	// div col1_content
	  output.WriteEndElement ();	// div col1

	  output.WriteStartElement ("div");
	  output.WriteAttributeString ("id", "col3");
	  output.WriteStartElement ("div");
	  output.WriteAttributeString ("id", "col3_content");
	  output.WriteAttributeString ("class", "clearfix");
	}
      string objectlist = "{t}Object list{/t}";
      output.WriteElementString ("h2", objectlist);

      output.WriteStartElement ("table");

      XPathExpression query =
	nav.Compile ("/dia:sheet/dia:contents/dia:object");
      query.SetContext (manager);
      XPathNodeIterator links = nav.Select (query);
      links = nav.Select (query);

      List < string > objectnames = new List < string > ();
      while (links.MoveNext ())
	{
	  string objectname = links.Current.GetAttribute ("name", "");
	  if (objectnames.Contains (objectname))
	    continue;
	  objectnames.Add (objectname);
	  output.WriteStartElement ("tr");
	  XPathExpression descquery =
	    nav.Compile ("/dia:sheet/dia:contents/dia:object[@name='" +
			 objectname + "']/dia:description");
	  descquery.SetContext (manager);
	  XPathNodeIterator objectdescriptions = nav.Select (descquery);
	  string objectdescription =
	    GetValueI18n (language, objectdescriptions);
	  output.WriteStartElement ("td");
	  output.WriteStartElement ("div");
	  output.WriteAttributeString ("class",
				       "icon " +
				       iconfinder.GetClassForObjectName
				       (objectname));
	  output.WriteString (" ");
	  output.WriteEndElement ();	// div 
	  output.WriteEndElement ();	// td
	  output.WriteElementString ("td", objectdescription);

	  output.WriteEndElement ();	// tr
	}
      output.WriteEndElement ();	// table
      if (output_tpl)
	{
	  output.WriteRaw ("{/capture}");
	  output.WriteRaw
	    ("{include file='footer.tpl' url='dia-installer.de/shapes/" +
	     sheet_path_fragment + "/index.html." + language +
	     "' language='" + language + "'" + noads + "}");
	}
      else
	{
	  output.WriteEndElement ();	// div col3_content
	  output.WriteEndElement ();	// div col3       
	  output.WriteEndElement ();	// div main
	  output.WriteEndElement ();	// div class page
	  output.WriteEndElement ();	// div class page_margins
	  output.WriteEndElement ();	// body
	  output.WriteEndElement ();	// html
	}
      output.Close ();
    }
  }
}

