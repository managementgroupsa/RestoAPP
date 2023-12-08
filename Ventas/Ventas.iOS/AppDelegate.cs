using Syncfusion.SfPdfViewer.XForms.iOS;
using Syncfusion.SfRangeSlider.XForms.iOS;
using Syncfusion.XForms.iOS.PopupLayout;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.SfNumericTextBox.XForms.iOS;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.XForms.iOS.TabView;
using Syncfusion.SfNumericUpDown.XForms.iOS;
using Syncfusion.XForms.Pickers.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.SfDataGrid.XForms.iOS;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace RestoAPP.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHJqVVhjWlpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9jTn5VdkVgXnpYcHNdQA==;Mgo+DSMBPh8sVXJ0S0R+XE9HcFRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xTcUVlWX9dd3FTRmlVVw==;ORg4AjUWIQA/Gnt2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkJjX39ec3NQQGFdV0I=;NjY5NTY3QDMyMzAyZTMyMmUzMEhSWGxZTEt6Tk8vUXMraHhpbm1qQ0hQTXpBTTdrWVI0ODVNbWQ2aUF4ZGc9;NjY5NTY4QDMyMzAyZTMyMmUzMGhqRlF2eVZLdHQzM1BvcThzdEd1RXBZTkdtY3RQeEJ1WDFYUjV2enlvSUU9;NRAiBiAaIQQuGjN/V0Z+Xk9EaFxEVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdEVkWXlecHdSQ2ZdWEBw;NjY5NTcwQDMyMzAyZTMyMmUzMFJYRlg3ck1UQTY4TXh0NkhvSVRRU3ltVGpGVENVcGcrc1hkMjZZNkhUZ2c9;NjY5NTcxQDMyMzAyZTMyMmUzMGFrWVo1TjhnRXhmNHMxTHhTejN1UFpUL3ZWL0g2UjVxRDAvU05IV1Y4dmc9;Mgo+DSMBMAY9C3t2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkJjX39ec3NQQGJaUEI=;NjY5NTczQDMyMzAyZTMyMmUzMFBDaW00WHh5b21IL01CaWpnM1RIK0dsTXppSms3K0pkZFYrMDJsYzFaRHM9;NjY5NTc0QDMyMzAyZTMyMmUzMGE3WFdNZHUxbTBvNmliUjg4QXppVW1zTmxXaEFvY2RjbytRb1ZxQVNLVWM9;NjY5NTc1QDMyMzAyZTMyMmUzMFJYRlg3ck1UQTY4TXh0NkhvSVRRU3ltVGpGVENVcGcrc1hkMjZZNkhUZ2c9");

            global::Xamarin.Forms.Forms.Init();
            SfPdfDocumentViewRenderer.Init();
            SfRangeSliderRenderer.Init();
            SfPopupLayoutRenderer.Init();
            SfComboBoxRenderer.Init();
            SfNumericTextBoxRenderer.Init();
            SfListViewRenderer.Init();
            SfTabViewRenderer.Init();
            SfNumericUpDownRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfDatePickerRenderer.Init();
            SfBusyIndicatorRenderer.Init();
            SfButtonRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfPickerRenderer.Init();
            SfDataGridRenderer.Init();
            PdfSharp.Xamarin.Forms.iOS.Platform.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
