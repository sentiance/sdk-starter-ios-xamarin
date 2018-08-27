
# Sentiance SDK Starter application for Xamarin
A simple single-view application that uses the Sentiance SDK.

## To run this project:
1. Clone this repository and `cd` into it.
2. [Create a developer account here](https://audience.sentiance.com/developers).
3. [Register a Sentiance application here](https://audience.sentiance.com/apps) to obtain an application ID and secret.
4. [Download the Sentiance iOS Xamarin SDK](https://sentiance-sdk.s3.amazonaws.com/ios/xamarin/sentiance-ios-sdk-5.1.0.zip) and extract
5. Place SENTSDK-5.1.0.dll and SENTSDK.bundle in the `Libs` folder.
6. Open the `.sln` file in Visual Studio.
7. Make sure the DLL file is correctly added to the References.
8. Add bundle package to Resources (Right click on Resources -> Add -> Add Existing Folder -> select SENTSDK.bundle -> Open -> Include All -> OK).
9. Make sure you see SENTSDK.bundle as folder in Resources.
10. In `AppDelegate.cs`: fill in the `APP_ID` and `APP_SECRET` variables with the credentials from the application you added in step 3.
11. Using Visual Studio, you can now build and run the application on your device.


## More info
- [Full documentation on the Sentiance SDK](https://developers.sentiance.com/docs)
- [Developer signup](https://developers.sentiance.com)
