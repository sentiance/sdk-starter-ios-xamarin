// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SDKStarter.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UILabel userIdLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (userIdLabel != null) {
				userIdLabel.Dispose ();
				userIdLabel = null;
			}
		}
	}
}
