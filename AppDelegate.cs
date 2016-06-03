//
// AppDelegate.cs
//
// Author:
//       Philippe Creytens <philippe@bazookas.be>
//
// Copyright (c) 2016 Philippe Creytens
using System;
using Foundation;
using UIKit;
using SENTTransportDetection;

namespace SDKStarter.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		const string APP_ID = "YOUR_APP_ID";
		const string APP_SECRET = "YOUR_APP_SECRET";

		// class-level declarations

		public SENTTransportDetectionSDK SentianceSDK {
			get;
			private set;
		}

		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			NSMutableDictionary config = new NSMutableDictionary ();
			config.Add (new NSString("appid"), new NSString(APP_ID));
			config.Add (new NSString("secret"), new NSString(APP_SECRET));
			config.Add (new NSString ("appLaunchOptions"), launchOptions ?? new NSDictionary ());

			this.SentianceSDK = new SENTTransportDetectionSDK (config);
			this.SentianceSDK.Delegate = new SENTTransportDelegate ();

			return true;
		}

		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}

	public class SENTTransportDelegate : SENTTransportDetectionSDKDelegate {
		
		public override void DidAuthenticationFailed (NSError error)
		{
			Console.WriteLine("Error launching Sentiance SDK: "+error); 

			// Here you should wait, inform the user to ensure an internet connection and retry initializeSentianceSdk


			// Some SDK starter specific help
			if( error.Code == 400 ) {
				Console.Write("You should create a developer account on https://audience.sentiance.com/developers and afterwards register a Sentiance application on https://audience.sentiance.com/apps");
				Console.Write("This will give you an application ID and secret which you can use to replace YOUR_APP_ID and YOUR_APP_SECRET in AppDelegate.m");
			}
		}

		public override void DidAuthenticationSuccess ()
		{
			Console.WriteLine("==== Sentiance SDK started, version: "+SENTTransportDetectionSDK.SDKVersion);
			Console.WriteLine("==== Sentiance platform user id for this install: "+SENTTransportDetectionSDK.UserId);
			Console.WriteLine("==== Authorization token that can be used to query the HTTP API: Bearer "+SENTTransportDetectionSDK.UserToken);

			NSNotificationCenter.DefaultCenter.PostNotificationName ("SdkAuthenticationSuccess", null);
		}

		public override void OnSdkIssue (SDKIssue issue, SENTStatusMessage message)
		{
			Console.Write ("Sentiance SDK Isuse" + issue.Type);
		}
	}
}


