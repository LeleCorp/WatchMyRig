using Android.App;
using Android.OS;
using watchmyrig.Class;
using System.Collections.Generic;
using Android.Widget;
using Android.Views;
using static Android.Resource;

namespace watchmyrig
{
    [Activity(Label = "Watch My Rig", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        List<Wallet> arrayWallet = new List<Wallet>();
        Wallet walletEthermine;
        Wallet walletNanopool;
        private LinearLayout relativeLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            relativeLayout = FindViewById<LinearLayout>(Resource.Id.MainLayout);

            walletEthermine = new Wallet("e7d55118ef16d4c854e6f8fc139a7596f915201b", "Ethermine", "eth");
            arrayWallet.Add(walletEthermine);

            walletNanopool = new Wallet("0x28c0e919482c843ebad611be8451e20017d81716", "Nanopool", "eth");
            arrayWallet.Add(walletNanopool);

            var param = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent);


            foreach (Wallet wallet in arrayWallet)
            {
                TextView tv = new TextView(this)
                {
                    Text = wallet.GetPool() + " " + wallet.GetCoin() + " " + wallet.stats.GetAverageHashrate()
                };
                tv.SetTextColor(GetColorStateList(Color.Black));
                relativeLayout.AddView(tv, param);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

        }
    }
}

