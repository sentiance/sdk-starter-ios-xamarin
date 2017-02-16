using System;

using UIKit;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;
using SENTTransportDetection;

namespace SDKStarter
{
	public partial class ViewController : UIViewController
	{
		IList<string> sdkInfoData;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			pInfoTableView.Source = new TableViewSource();

			// Our AppDelegate broadcasts when sdk auth was successful
			NSNotificationCenter.DefaultCenter.AddObserver(new NSString("SdkAuthenticationSuccess"), notification => refreshStatus());

			// Perform any additional setup after loading the view, typically from a nib.
			NSTimer.CreateRepeatingScheduledTimer(5, obj => refreshStatus());
		}

		#region public methods
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			refreshStatus();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		#endregion

		#region private methods

		void refreshStatus()
		{
			// Refresh the staus of the sdk in the table data source.
			// Every item in this data source is displayed in the table view.
			// You can use the status message for more information

			SENTStatusMessage sdkStatus = (UIApplication.SharedApplication.Delegate as AppDelegate).SentianceSDK.StatusMessage;
			string flavor = SENTTransportDetectionSDK.SDKFlavor;

			sdkInfoData = new List<string>();
			sdkInfoData.Add("SDK Flavor : " + flavor);
			sdkInfoData.Add("SDK Version : " + SENTTransportDetectionSDK.SDKVersion);
			sdkInfoData.Add("User ID : " + SENTTransportDetectionSDK.UserId);


			//    [sdkInfoData addObject:[NSString stringWithFormat:@"User Token : %@", [SENTTransportDetectionSDK getUserToken]]];


			string[] sdkIssuesArray = new string[]{
				"SDK_LocationPermissionIssue",
				"SDK_SensorPermissionIssue",
				"SDK_NetworkPermissionIssue",
				"SDK_BGAccessPermissionIssue",
				"SDK_DiskUsePermissionIssue",
				"SDK_GpsIssue",
				"SDK_GyroscopeIssue",
				"SDK_MotionsensorIssue",
				"SDK_AccelerometerIssue",
				"SDK_PlatformIssue",
				"SDK_ManufacturerIssue",
				"SDK_ModelIssue",
				"SDK_OSIssue",
				"SDK_SDKVersionIssue",
				"SDK_DiskQuotaExceeded",
				"SDK_WifiQuotaExceeded",
				"SDK_MobileQuotaExceeded"};

			sdkInfoData.Add("Status : " + (sdkStatus.Mode == SDKMode.Supported ? "SUPPORTED" : "UNSUPPORTED"));

			if (sdkStatus.Mode == SDKMode.Unsupported && sdkStatus.Issues.Count > 0)
			{

				for (nuint i = 0; i < sdkStatus.Issues.Count; i++)
				{
					var issue = sdkStatus.Issues.GetItem<SDKIssue>(i);
					sdkInfoData.Add("Issue : " + sdkIssuesArray[issue.Type.GetHashCode() - 1]);
				}
			}

			sdkInfoData.Add(string.Format("WIFIData : {0}/{1}", sdkStatus.WifiQuotaUsed, sdkStatus.WifiQuotaLimit));
			sdkInfoData.Add(string.Format("MobileData : {0}/{1}", sdkStatus.MobileQuotaUsed, sdkStatus.MobileQuotaLimit));
			sdkInfoData.Add(string.Format("WIFIData : {0}/{1}", sdkStatus.DiskQuotaUsed, sdkStatus.DiskQuotaLimit));

			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(sdkStatus.WifiLastSeenTimestamp / 1000).ToLocalTime();
			sdkInfoData.Add("Last WIFI Access: " + dtDateTime.ToString("yyyy-MM-dd HH:mm:ss"));

			(pInfoTableView.Source as IUpdateData).UpdateData(sdkInfoData);

			pInfoTableView.ReloadData();

		}
		#endregion

		#region internal class
		internal class TableViewSource : UITableViewSource, IUpdateData
		{

			const string reuseIdentifier = "StatusCell";
			IList<string> sdkInfoData;

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				if (sdkInfoData != null)
					return sdkInfoData.Count;
				return 0;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell(reuseIdentifier);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, reuseIdentifier);
					cell.TextLabel.MinimumScaleFactor = 0.8f;
					cell.TextLabel.AdjustsFontSizeToFitWidth = true;
				}
				cell.TextLabel.Text = sdkInfoData[indexPath.Row];
				return cell;
			}

			public void UpdateData(IList<string> data)
			{
				this.sdkInfoData = data;
			}
		}

		internal interface IUpdateData
		{
			void UpdateData(IList<string> data);
		}

		internal interface ISetHeaderView
		{
			void SetHeaderView(UIView header);
		}
		#endregion
	}
}

