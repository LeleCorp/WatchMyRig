using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            submitButton.Touch += OnTouch;

            // Fill Pool Spinner items from Ressource 
            poolSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(PoolSpinner_ItemSelected);
            poolSpinner.Adapter = FillSpinner(Resource.Array.pool_array);

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
            int itemPosition = e.Position;

            if (itemPosition != 0)
            {
                string textToast = string.Format("You have selected {0} as pool server", spinner.GetItemAtPosition(itemPosition));
                Toast.MakeText(this, textToast, ToastLength.Long).Show();

                switch (spinner.GetItemAtPosition(e.Position).ToString())
                {
                    case "Nanopool":
                        coinSpinner.Adapter = FillSpinner(Resource.Array.nanopool_arrayCoin);
                        break;

                    case "Ethermine":
                        coinSpinner.Adapter = FillSpinner(Resource.Array.etherMine_arrayCoin);
                        break;
                }
            }
        }

        private void CoinSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            int itemPosition = e.Position;
            if (e.Position != 0)
            {
                string textToast = string.Format("You have selected {0} as mine coin", spinner.GetItemAtPosition(itemPosition));
                Toast.MakeText(this, textToast, ToastLength.Long).Show();
            }
        }

        /// <summary>
        /// Fill the content of a spinner
        /// </summary>
        /// <param name="_ArrayCoin">Resource.Array.arrayCoinNeeded</param>
        /// <returns>Return ArrayAdapter</returns>
        private ArrayAdapter FillSpinner(int _ArrayCoin)
        {
            var adapter = ArrayAdapter.CreateFromResource(this, _ArrayCoin, Android.Resource.Layout.SimpleDropDownItem1Line);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            return adapter;
        }
        #endregion
        private void OnTouch(object sender, EventArgs ea)
        {
            string textToast = string.Format("Nice");
            Toast.MakeText(this, textToast, ToastLength.Short).Show();
        }
        #endregion
    }
}