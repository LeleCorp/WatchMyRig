using Android.App;
using Android.OS;
using watchmyrig.Class;

namespace watchmyrig
{
    [Activity(Label = "Watch My Rig", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Wallet walletEthermine;
        Wallet walletNanopool;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);



            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        protected override void OnStart()
        {
            base.OnStart();

            walletEthermine = new Wallet("8aec081e391d275dc0fb8e4697fc252359d353f8", "https://api.ethermine.org", "eth");
            walletNanopool = new Wallet("0xb9e33a4a1dba378925b7f20d21b5ab2d78ad58f0", "https://api.nanopool.org", "eth");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}

