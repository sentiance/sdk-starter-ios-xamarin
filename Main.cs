//
// Main.cs
//
// Author:
//       Philippe Creytens <philippe@bazookas.be>
//
// Copyright (c) 2016 Philippe Creytens
using UIKit;

namespace SDKStarter.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
