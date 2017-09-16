using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace watchmyrig
{
    [Activity(Label = "Add address", MainLauncher = true, Icon = "@drawable/icon")]
    public class AddWallet : Activity
    {
        private EditText adrEditText;
        private Spinner poolSpinner;
        private Spinner coinSpinner;
        private Button submitButton;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AddWallet);

            // Declare UI elements
            adrEditText = FindViewById<EditText>(Resource.Id.editTextPublicAddress);
            poolSpinner = FindViewById<Spinner>(Resource.Id.spinnerPool);
            coinSpinner = FindViewById<Spinner>(Resource.Id.spinnerCoin);
            submitButton = FindViewById<Button>(Resource.Id.submitButton);

            //submitButton.SetOnTouchListener(this);

            // Fill Pool Spinner items from Ressource 
            poolSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(PoolSpinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.pool_array, Android.Resource.Layout.SimpleDropDownItem1Line);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            poolSpinner.Adapter = adapter;

            // Fill coin spinner switch POOLSPINNER
            coinSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(CoinSpinner_ItemSelected);
        }

        protected override void OnStart()
        {
            base.OnStart();
        }



#region ClickOnElement
    #region Spinner
        private void PoolSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string textToast = string.Format("You have selected {0} as pool server", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, textToast, ToastLength.Long).Show();

            switch (spinner.GetItemAtPosition(e.Position).ToString())
            {
                case "Nanopool":
                    var adapterNanopoolCoin = ArrayAdapter.CreateFromResource(this, Resource.Array.nanopool_arrayCoin, Android.Resource.Layout.SimpleDropDownItem1Line);
                    adapterNanopoolCoin.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
                    coinSpinner.Adapter = adapterNanopoolCoin;
                    break;

                case "Ethermine":
                    var adapterEthermine = ArrayAdapter.CreateFromResource(this, Resource.Array.etherMine_arrayCoin, Android.Resource.Layout.SimpleDropDownItem1Line);
                    adapterEthermine.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
                    coinSpinner.Adapter = adapterEthermine;
                    break;
            }
        }

        private void CoinSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string textToast = string.Format("You have selected {0} as mine coin", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, textToast, ToastLength.Long).Show();
        }
        #endregion
        private void OnTouch()
        {
            string textToast = string.Format("Nice");
            Toast.MakeText(this, textToast, ToastLength.Short).Show();
        }
        #endregion
    }
}