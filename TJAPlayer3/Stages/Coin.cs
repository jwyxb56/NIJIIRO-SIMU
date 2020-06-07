using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using SlimDX.DirectInput;
using FDK;
using System.Reflection;

namespace TJAPlayer3.Stages
{
    class Coin
    {
        public void コイン数のセットアップ()
        {
            this.pfCoin = new CPrivateFastFont(new FontFamily(TJAPlayer3.ConfigIni.FontName), 16, FontStyle.Bold);

            using (var bmpStageText = pfCoin.DrawPrivateFont(NowCoinCount.ToString(), Color.White, Color.Black))     //20
            {
                this.txCoin = TJAPlayer3.tテクスチャの生成(bmpStageText, false);
            }
        }

        public void コイン数の管理()
        {
            if (nPastCoin != NowCoinCount)                            //もしメモリリーク用の変数と現在のコイン数が違ったら
            {
                TJAPlayer3.tテクスチャの解放(ref txCoin);　　　　　　　　　　　　　　　　　//txCoinのテクスチャを開放する!

                using (var bmpStageText = pfCoin.DrawPrivateFont(NowCoinCount.ToString(), Color.White, Color.Black))  //20
                {
                    this.txCoin = TJAPlayer3.tテクスチャの生成(bmpStageText, false);　　//テスクチャ製作所
                }
                nPastCoin = NowCoinCount;　　//必要なコイン数
            }

            if (TJAPlayer3.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.PageUp) || TJAPlayer3.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.NumberPad1))
            {
                NowCoinCount += 1;　
                TJAPlayer3.Skin.Coin.t再生する();
            }
            else if (TJAPlayer3.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.PageDown) || TJAPlayer3.Input管理.Keyboard.bキーが押された((int)SlimDX.DirectInput.Key.NumberPad2))
            {
                if (NowCoinCount > 0) //コインの数が-1以下にならないようにする
                {
                    NowCoinCount -= 1;
                }
            }

            TJAPlayer3.Tx.Coin.t2D描画(TJAPlayer3.app.Device, 0, 0);

            if (txCoin != null)
            {
                txCoin.vc拡大縮小倍率.Y = 0.97f;
                txCoin.t2D描画(TJAPlayer3.app.Device, 680, 680);
            }
        }


 
        #region [ private ]
        private CPrivateFastFont pfCoin;　//コインフォント
        private CTexture txCoin;　//コインテスクチャ　　
        private int NowCoinCount;
        private double nPastCoin;
        #endregion
    }
}
