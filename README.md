# Sentiance SDK Starter Application for Xamarin
It is a simple single-view application that uses the Sentiance SDK and displays only a user ID of the logged in user on interface.

Steps applied: [Sentiance iOS SDK docs](https://audience.sentiance.com/docs/sdk/ios/integration)

## To run this project:
1.  Clone this repository
2.  Download and extract the latest framwork file from the [sentiance ios xamarin sdk](https://s3-eu-west-1.amazonaws.com/sentiance-sdk/ios/xamarin/SENTTransportDetectionSDK.iOS.dll)
3.  Place the downloaded dll file in the repository root path or in lib folder.
4.  [Create a developer account here](https://audience.sentiance.com/developers)
5.  [Register a Sentiance application here](https://audience.sentiance.com/apps) to obtain an application ID and secret
6.  Import the copied dll file in your xamarin project as a new reference. After doing this make sure to compile project for once to see if the added reference is working well or not till this point.
7.  In `AppDelegate.cs`: replace `YOUR_APP_ID` and `YOUR_APP_SECRET` with the credentials from the application you added in step 5.
8.  Clean, build and run the application on your device/simulator


## More info
- [Full documentation on the Sentiance SDK](https://audience.sentiance.com/docs)
- [Developer signup](https://audience.sentiance.com/developers)