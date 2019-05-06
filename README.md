
# Sentiance SDK Starter application for Xamarin
A simple single-view application that uses the Sentiance SDK.

## To run this project:
1. Request a developer account by [contacting Sentiance](mailto:support@sentiance.com).
2. Grab your test app credentials from [here](https://insights.sentiance.com/#/apps).
4. [Download the latest Sentiance iOS Xamarin SDK](https://docs.sentiance.com/sdk/appendix/xamarin) and extract it.
5. Place SENTSDK-5.1.7.dll and SENTSDK.bundle in the `Libs` folder.
6. Open the `.sln` file in Visual Studio.
7. Make sure the DLL file is correctly added to the References.
8. Add bundle package to Resources (Right click on Resources -> Add -> Add Existing Folder -> select SENTSDK.bundle -> Open -> Include All -> OK).
9. Make sure you see SENTSDK.bundle as folder in Resources.
10. In `AppDelegate.cs`: fill in the `APP_ID` and `APP_SECRET` variables with the credentials from the application you added in step 2.
11. Using Visual Studio, you can now build and run the application on your device.


## More info
- [Full documentation on the Sentiance SDK](https://docs.sentiance.com/)
