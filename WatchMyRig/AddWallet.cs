
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace watchmyrig
{
    [Activity(Label = "Add address", MainLauncher = true, Icon = "@drawable/icon")]
    public class AddWallet : Activity
    {
        private Spinner poolSpinner;
        private Spinner coinSpinner;
        private object adapterEthermineCoin;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AddWallet);

            // Declare Pool Spinner items
            poolSpinner = FindViewById<Spinner>(Resource.Id.spinnerPool);
            coinSpinner = FindViewById<Spinner>(Resource.Id.spinnerCoin);

        }

        protected override void OnStart()
        {
            base.OnStart();

            // Fill Pool Spinner items from Ressource 
            poolSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(poolSpinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.pool_array, Android.Resource.Layout.SimpleDropDownItem1Line);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            poolSpinner.Adapter = adapter;

            // Fill coin spinner switch POOLSPINNER
            coinSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(coinSpinner_ItemSelected);
        }

        private void poolSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
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

        private void coinSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string textToast = string.Format("You have selected {0} as mine coin", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, textToast, ToastLength.Long).Show();
        }
    }
}