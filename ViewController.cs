//
// ViewController.cs
//
// Author:
//       Philippe Creytens <philippe@bazookas.be>
//
// Copyright (c) 2016 Philippe Creytens
using System;

using UIKit;
using Foundation;
using SENTTransportDetection;

namespace SDKStarter.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Our AppDelegate broadcasts when sdk auth was successful
			NSNotificationCenter.DefaultCenter.AddObserver (new NSString("SdkAuthenticationSuccess"), notification => refreshStatus());

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			refreshStatus ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		void refreshStatus()
		{
			this.userIdLabel.Text = SENTTransportDetectionSDK.UserId;
		}
	}
}

