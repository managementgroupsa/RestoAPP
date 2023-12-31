﻿using RestoPLUS.Services.Identity;
using RestoPLUS.Services.Routing;
using Splat;
using System;
using RestoPLUS.Services;
using RestoPLUS.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestoPLUS.ViewModels;


namespace RestoPLUS
{
    public partial class App : Application
    {

        public App()
        {
            
            InitializeDi();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHJqVVhjWlpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9jTn5VdkVgXnpYcHNdQA==;Mgo+DSMBPh8sVXJ0S0R+XE9HcFRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xTcUVlWX9dd3FTRmlVVw==;ORg4AjUWIQA/Gnt2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkJjX39ec3NQQGFdV0I=;NjY5NTY3QDMyMzAyZTMyMmUzMEhSWGxZTEt6Tk8vUXMraHhpbm1qQ0hQTXpBTTdrWVI0ODVNbWQ2aUF4ZGc9;NjY5NTY4QDMyMzAyZTMyMmUzMGhqRlF2eVZLdHQzM1BvcThzdEd1RXBZTkdtY3RQeEJ1WDFYUjV2enlvSUU9;NRAiBiAaIQQuGjN/V0Z+Xk9EaFxEVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdEVkWXlecHdSQ2ZdWEBw;NjY5NTcwQDMyMzAyZTMyMmUzMFJYRlg3ck1UQTY4TXh0NkhvSVRRU3ltVGpGVENVcGcrc1hkMjZZNkhUZ2c9;NjY5NTcxQDMyMzAyZTMyMmUzMGFrWVo1TjhnRXhmNHMxTHhTejN1UFpUL3ZWL0g2UjVxRDAvU05IV1Y4dmc9;Mgo+DSMBMAY9C3t2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkJjX39ec3NQQGJaUEI=;NjY5NTczQDMyMzAyZTMyMmUzMFBDaW00WHh5b21IL01CaWpnM1RIK0dsTXppSms3K0pkZFYrMDJsYzFaRHM9;NjY5NTc0QDMyMzAyZTMyMmUzMGE3WFdNZHUxbTBvNmliUjg4QXppVW1zTmxXaEFvY2RjbytRb1ZxQVNLVWM9;NjY5NTc1QDMyMzAyZTMyMmUzMFJYRlg3ck1UQTY4TXh0NkhvSVRRU3ltVGpGVENVcGcrc1hkMjZZNkhUZ2c9");
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        private void InitializeDi()
        {
            // Services
            Locator.CurrentMutable.RegisterLazySingleton<IRoutingService>(() => new ShellRoutingService());
            Locator.CurrentMutable.RegisterLazySingleton<IIdentityService>(() => new IdentityServiceStub());

            // ViewModels
            Locator.CurrentMutable.Register(() => new LoadingViewModel());
            Locator.CurrentMutable.Register(() => new LoginViewModel());
            //Locator.CurrentMutable.Register(() => new RegistrationViewModel());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
