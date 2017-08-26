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

		#region public methods
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			pInfoTableView.Source = new TableViewSource();

			// Our AppDelegate broadcasts when the SDK status was updated
			NSNotificationCenter.DefaultCenter.AddObserver(new NSString("SdkStatusUpdated"), new Action<NSNotification>(onStatusUpdated));
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			refreshStatus();
		}
		#endregion

		#region private methods
       	void onStatusUpdated(NSNotification notification)
		{
            refreshStatus();
		}

		void refreshStatus()
		{
			Console.WriteLine("*************** refreshStatus ***************");

            if (!SENTSDK.SharedInstance.IsInitialised) {
                return;
            }

            sdkInfoData = new List<string>();

            sdkInfoData.Add("SDK version: " + SENTSDK.SharedInstance.Version);
            sdkInfoData.Add("User ID: " + SENTSDK.SharedInstance.UserId);

			// You can use the status message to obtain more information

			SENTSDKStatus status = SENTSDK.SharedInstance.SdkStatus;

            sdkInfoData.Add("Start status: " + status.StartStatus);
			sdkInfoData.Add("Can detect: " + status.CanDetect);
            sdkInfoData.Add("Remote enabled: " + status.IsRemoteEnabled);
			sdkInfoData.Add("Location perm granted: " + status.IsLocationPermGranted);

            sdkInfoData.Add(formatQuota("Wi-Fi", status.WifiQuotaStatus, SENTSDK.SharedInstance.WiFiQuotaUsage, SENTSDK.SharedInstance.WifiQuotaLimit));
			sdkInfoData.Add(formatQuota("Mobile data", status.MobileQuotaStatus, SENTSDK.SharedInstance.MobileQuotaUsage, SENTSDK.SharedInstance.MobileQuotaLimit));
            sdkInfoData.Add(formatQuota("Disk", status.DiskQuotaStatus, SENTSDK.SharedInstance.DiskQuotaUsage, SENTSDK.SharedInstance.DiskQuotaLimit));

			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(SENTSDK.SharedInstance.WiFiLastSeenTimestamp/ 1000).ToLocalTime();
			sdkInfoData.Add("Wi-Fi last seen: " + dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

			(pInfoTableView.Source as IUpdateData).UpdateData(sdkInfoData);
			pInfoTableView.ReloadData();

		}

        private string formatQuota(string name, SENTQuotaStatus status, long bytesUsed, long bytesLimit)
		{
			return System.String.Format("{0} quota: {1} / {2} ({3})",
										name,
										bytesUsed,
										bytesLimit,
										status);
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

