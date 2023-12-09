using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace RestoAPP.Droid
{
    [Activity(Label = "RestoPLUS", Icon = "@mipmap/iconstore", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHJqVVhjWlpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9jTn5VdkVgXnpYcHNdQA==;Mgo+DSMBPh8sVXJ0S0R+XE9HcFRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xTcUVlWX9dd3FTRmlVVw==;ORg4AjUWIQA/Gnt2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkJjX39ec3NQQGFdV0I=;NjY5NTY3QDMyMzAyZTMyMmUzMEhSWGxZTEt6Tk8vUXMraHhpbm1qQ0hQTXpBTTdrWVI0ODVNbWQ2aUF4ZGc9;NjY5NTY4QDMyMzAyZTMyMmUzMGhqRlF2eVZLdHQzM1BvcThzdEd1RXBZTkdtY3RQeEJ1WDFYUjV2enlvSUU9;NRAiBiAaIQQuGjN/V0Z+Xk9EaFxEVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdEVkWXlecHdSQ2ZdWEBw;NjY5NTcwQDMyMzAyZTMyMmUzMFJYRlg3ck1UQTY4TXh0NkhvSVRRU3ltVGpGVENVcGcrc1hkMjZZNkhUZ2c9;NjY5NTcxQDMyMzAyZTMyMmUzMGFrWVo1TjhnRXhmNHMxTHhTejN1UFpUL3ZWL0g2UjVxRDAvU05IV1Y4dmc9;Mgo+DSMBMAY9C3t2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkJjX39ec3NQQGJaUEI=;NjY5NTczQDMyMzAyZTMyMmUzMFBDaW00WHh5b21IL01CaWpnM1RIK0dsTXppSms3K0pkZFYrMDJsYzFaRHM9;NjY5NTc0QDMyMzAyZTMyMmUzMGE3WFdNZHUxbTBvNmliUjg4QXppVW1zTmxXaEFvY2RjbytRb1ZxQVNLVWM9;NjY5NTc1QDMyMzAyZTMyMmUzMFJYRlg3ck1UQTY4TXh0NkhvSVRRU3ltVGpGVENVcGcrc1hkMjZZNkhUZ2c9");
            base.OnCreate(savedInstanceState);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


        }




    }
}