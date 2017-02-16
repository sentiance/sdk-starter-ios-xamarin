// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SDKStarter
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UITableView pInfoTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (pInfoTableView != null) {
				pInfoTableView.Dispose ();
				pInfoTableView = null;
			}
		}
	}
}
