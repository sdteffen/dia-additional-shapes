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
using System.Collections.Generic;
using System.IO;

// CSS classes for all shapes and objects
public class Shapes2Sprite
{
  public static void Main (string[]args)
  {
    if ((args.Length == 1 && ("-h" == args[0] || "--help" == args[0]))
	|| args.Length > 2)
      {
	Console.Error.WriteLine ("USAGE: shapes2sprite [Options]");
	Console.Error.WriteLine ("Options:");
	Console.Error.
	  WriteLine
	  ("--montage					Output montage command line");
	Console.Error.
	  WriteLine
	  ("--datadir=datadir			Path where sheets and shapes reside");
	Console.Error.
	  WriteLine ("-h, --help                 Display help and exit");
	Console.Error.
	  WriteLine ("-v, --version              Display version and exit");
	return;
      }

    if (1 == args.Length && ("-v" == args[0] || "--version" == args[0]))
      {
	Console.Error.WriteLine ("shapes2sprite 0.2.1");
	Console.Error.WriteLine ("Copyright (c) 2011 Steffen Macke");
	return;
      }

    bool montage = false;

    for (int i = 0; i < args.Length; i++)
      {
	if (10 < args[i].Length && "--datadir=" == args[i].Substring (0, 10))
	  Directory.SetCurrentDirectory (args[i].Substring (10));
	if ("--montage" == args[i])
	  montage = true;
      }

    string montagecmd = "montage -geometry +0+0 -tile ";
    int objectcount = 0;
    string files = "";
    int x = 0;

    if (!montage)
      Console.WriteLine (".icon { width: 22px; height: 22px; }");

    DiaIcons diaicons = new DiaIcons ();
    foreach (KeyValuePair < string, string > icon in diaicons.icons)
    {
      if (DiaIcons.SHEET_SPECIFIC == icon.Value)
	continue;
      files += icon.Value + " ";
      objectcount++;
      if (!montage)
	Console.WriteLine (".d" + icon.Key.Replace (".png", "") +
			   " {background: transparent url(s.png) -" + x +
			   "px 0px no-repeat;}");
      x += 22;
    }
    montagecmd += objectcount + "x1 " + files + " s.png";
    if (montage)
      Console.WriteLine (montagecmd);

  }

}
