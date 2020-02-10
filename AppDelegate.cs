using System;
using Foundation;
using UIKit;
using SENTIANCE;

namespace SDKStarter
{
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		const string APP_ID = "";
		const string APP_SECRET = "";

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{

			Console.WriteLine("finished launching");

			SENTConfig config = new SENTConfig(APP_ID, APP_SECRET, launchOptions ?? new NSDictionary());
			config.DidReceiveSdkStatusUpdate = new Action<SENTSDKStatus>(onStatusUpdate);

			SENTSDK.SharedInstance.InitWithConfig(config, new Action(onInitSuccess), new Action<SENTInitIssue>(onInitFailure));

			return true;
		}

		private void onInitSuccess()
		{
			Console.WriteLine("Sentiance SDK initialized, version: {0}", SENTSDK.SharedInstance.Version);

			SENTSDK.SharedInstance.Start(new Action<SENTSDKStatus>(onStartFinished));
		}

		private void onInitFailure(SENTInitIssue issue)
		{
			Console.WriteLine("Could not initialize SDK, error={0}", issue);

			switch (issue)
			{
				case SENTInitIssue.InvalidCredentials:
					Console.WriteLine("Make sure APP_ID and APP_SECRET are set correctly.");
					break;
				case SENTInitIssue.ChangedCredentials:
					Console.WriteLine("The app ID and secret have changed; this is not supported. If you meant to change the app credentials, please uninstall the app and try again.");
					break;
				case SENTInitIssue.ServiceUnreachable:
					Console.WriteLine("The Sentiance API could not be reached. Double-check your internet connection and try again.");
					break;
			}
		}

		private void onStartFinished(SENTSDKStatus status)
		{
			Console.WriteLine("SDK start finished with status: {0}", status.StartStatus);
		}

		private void onStatusUpdate(SENTSDKStatus status)
		{
			Console.WriteLine("SDK status updated");
			NSNotificationCenter.DefaultCenter.PostNotificationName("SdkStatusUpdated", status);
		}
	}
}
