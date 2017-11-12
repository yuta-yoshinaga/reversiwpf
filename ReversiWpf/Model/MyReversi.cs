////////////////////////////////////////////////////////////////////////////////
///	@file			MyReversi.cs
///	@brief			リバーシクラス実装ファイル
///	@author			Yuta Yoshinaga
///	@date			2017.10.20
///	$Version:		$
///	$Revision:		$
///
/// Copyright (c) 2017 Yuta Yoshinaga. All Rights reserved.
///
/// - 本ソフトウェアの一部又は全てを無断で複写複製（コピー）することは、
///   著作権侵害にあたりますので、これを禁止します。
/// - 本製品の使用に起因する侵害または特許権その他権利の侵害に関しては
///   当社は一切その責任を負いません。
///
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
	////////////////////////////////////////////////////////////////////////////////
	///	@class		MyReversi
	///	@brief		リバーシクラス
	///
	////////////////////////////////////////////////////////////////////////////////
	public class MyReversi
	{
		#region メンバ変数
		private int[,] _mMasuSts;										//!< マスの状態
		private int[,] _mMasuStsOld;									//!< 以前のマスの状態
		private int[,] _mMasuStsEnaB;									//!< 黒の置ける場所
		private int[,] _mMasuStsCntB;									//!< 黒の獲得コマ数
		private int[,] _mMasuStsPassB;									//!< 黒が相手をパスさせる場所
		private ReversiAnz[,] _mMasuStsAnzB;							//!< 黒がその場所に置いた場合の解析結果
		private ReversiPoint[] _mMasuPointB;							//!< 黒の置ける場所座標一覧
		private int _mMasuPointCntB;									//!< 黒の置ける場所座標一覧数
		private int _mMasuBetCntB;										//!< 黒コマ数
		private int[,] _mMasuStsEnaW;									//!< 白の置ける場所
		private int[,] _mMasuStsCntW;									//!< 白の獲得コマ数
		private int[,] _mMasuStsPassW;									//!< 白が相手をパスさせる場所
		private ReversiAnz[,] _mMasuStsAnzW;							//!< 白がその場所に置いた場合の解析結果
		private ReversiPoint[] _mMasuPointW;							//!< 白の置ける場所座標一覧
		private int _mMasuPointCntW;									//!< 白の置ける場所座標一覧数
		private int _mMasuBetCntW;										//!< 白コマ数
		private int _mMasuCnt;											//!< 縦横マス数
		private int _mMasuCntMax;										//!< 縦横マス最大数
		private int _mMasuHistCur;										//!< 履歴現在位置
		private ReversiHistory[] _mMasuHist;							//!< 履歴
		#endregion

		#region プロパティ
		public int[,] mMasuSts
		{
			get { return _mMasuSts; }
			set { _mMasuSts = value; }
		}
		public int[,] mMasuStsOld
		{
			get { return _mMasuStsOld; }
			set { _mMasuStsOld = value; }
		}
		public int[,] mMasuStsEnaB
		{
			get { return _mMasuStsEnaB; }
			set { _mMasuStsEnaB = value; }
		}
		public int[,] mMasuStsCntB
		{
			get { return _mMasuStsCntB; }
			set { _mMasuStsCntB = value; }
		}
		public int[,] mMasuStsPassB
		{
			get { return _mMasuStsPassB; }
			set { _mMasuStsPassB = value; }
		}
		public ReversiAnz[,] mMasuStsAnzB
		{
			get { return _mMasuStsAnzB; }
			set { _mMasuStsAnzB = value; }
		}
		public ReversiPoint[] mMasuPointB
		{
			get { return _mMasuPointB; }
			set { _mMasuPointB = value; }
		}
		public int mMasuPointCntB
		{
			get { return _mMasuPointCntB; }
			set { _mMasuPointCntB = value; }
		}
		public int mMasuBetCntB
		{
			get { return _mMasuBetCntB; }
			set { _mMasuBetCntB = value; }
		}
		public int[,] mMasuStsEnaW
		{
			get { return _mMasuStsEnaW; }
			set { _mMasuStsEnaW = value; }
		}
		public int[,] mMasuStsCntW
		{
			get { return _mMasuStsCntW; }
			set { _mMasuStsCntW = value; }
		}
		public int[,] mMasuStsPassW
		{
			get { return _mMasuStsPassW; }
			set { _mMasuStsPassW = value; }
		}
		public ReversiAnz[,] mMasuStsAnzW
		{
			get { return _mMasuStsAnzW; }
			set { _mMasuStsAnzW = value; }
		}
		public ReversiPoint[] mMasuPointW
		{
			get { return _mMasuPointW; }
			set { _mMasuPointW = value; }
		}
		public int mMasuPointCntW
		{
			get { return _mMasuPointCntW; }
			set { _mMasuPointCntW = value; }
		}
		public int mMasuBetCntW
		{
			get { return _mMasuBetCntW; }
			set { _mMasuBetCntW = value; }
		}
		public int mMasuCnt
		{
			get { return _mMasuCnt; }
			set { _mMasuCnt = value; }
		}
		public int mMasuCntMax
		{
			get { return _mMasuCntMax; }
			set { _mMasuCntMax = value; }
		}
		public int mMasuHistCur
		{
			get { return _mMasuHistCur; }
			set { _mMasuHistCur = value; }
		}
		public ReversiHistory[] mMasuHist
		{
			get { return _mMasuHist; }
			set { _mMasuHist = value; }
		}
		#endregion

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コンストラクタ
		///	@fn				MyReversi(int masuCnt, int masuMax)
		///	@param[in]		int masuCnt		縦横マス数
		///	@param[in]		int masuMax		縦横マス最大数
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public MyReversi(int masuCnt, int masuMax)
		{
			this.mMasuCnt = masuCnt;
			this.mMasuCntMax = masuMax;

			this.mMasuSts = new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mMasuStsOld = new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];

			this.mMasuStsEnaB	= new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mMasuStsCntB	= new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mMasuStsPassB	= new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mMasuStsAnzB	= new ReversiAnz[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];

			this.mMasuStsEnaW	= new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mMasuStsCntW	= new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mMasuStsPassW	= new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mMasuStsAnzW	= new ReversiAnz[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];

			for (var i = 0; i < this.mMasuCntMax; i++) {
				for (var j = 0; j < this.mMasuCntMax; j++) {
					this.mMasuSts[i,j]			= ReversiConst.REVERSI_STS_NONE;

					this.mMasuStsEnaB[i,j]		= ReversiConst.REVERSI_STS_NONE;
					this.mMasuStsCntB[i,j]		= ReversiConst.REVERSI_STS_NONE;
					this.mMasuStsPassB[i,j]		= ReversiConst.REVERSI_STS_NONE;
					this.mMasuStsAnzB[i,j]		= new ReversiAnz();

					this.mMasuStsEnaW[i,j]		= ReversiConst.REVERSI_STS_NONE;
					this.mMasuStsCntW[i,j]		= ReversiConst.REVERSI_STS_NONE;
					this.mMasuStsPassW[i,j]		= ReversiConst.REVERSI_STS_NONE;
					this.mMasuStsAnzW[i,j]		= new ReversiAnz();
				}
			}
			this.mMasuPointB = new ReversiPoint[ReversiConst.DEF_MASU_CNT_MAX_VAL * ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mMasuPointW = new ReversiPoint[ReversiConst.DEF_MASU_CNT_MAX_VAL * ReversiConst.DEF_MASU_CNT_MAX_VAL];
			for (var i = 0; i < (this.mMasuCntMax * this.mMasuCntMax); i++) {
				this.mMasuPointB[i] = new ReversiPoint();
				this.mMasuPointW[i] = new ReversiPoint();
			}
			this.mMasuPointCntB = 0;
			this.mMasuPointCntW = 0;

			this.mMasuBetCntB = 0;
			this.mMasuBetCntW = 0;

			this.mMasuHist = new ReversiHistory[ReversiConst.DEF_MASU_CNT_MAX_VAL * ReversiConst.DEF_MASU_CNT_MAX_VAL];
			for (var i = 0; i < (this.mMasuCntMax * this.mMasuCntMax); i++) {
				this.mMasuHist[i] = new ReversiHistory();
			}
			this.mMasuHistCur = 0;
			Array.Copy(this.mMasuSts, this.mMasuStsOld, this.mMasuSts.Length);
			this.reset();
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コピー
		///	@fn				MyReversi Clone()
		///	@return			オブジェクトコピー
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public MyReversi Clone()
		{
			return (MyReversi)MemberwiseClone();
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リセット
		///	@fn				void reset()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void reset()
		{
			for (var i = 0; i < this.mMasuCnt; i++) {
				for (var j = 0; j < this.mMasuCnt; j++) {
					this.mMasuSts[i,j]			= ReversiConst.REVERSI_STS_NONE;
					this.mMasuStsPassB[i,j]	= 0;
					this.mMasuStsAnzB[i,j].reset();
					this.mMasuStsPassW[i,j]	= 0;
					this.mMasuStsAnzW[i,j].reset();
				}
			}
			this.mMasuSts[(this.mMasuCnt >> 1) - 1,(this.mMasuCnt >> 1) - 1]	= ReversiConst.REVERSI_STS_BLACK;
			this.mMasuSts[(this.mMasuCnt >> 1) - 1,(this.mMasuCnt >> 1)]		= ReversiConst.REVERSI_STS_WHITE;
			this.mMasuSts[(this.mMasuCnt >> 1),(this.mMasuCnt >> 1) - 1]		= ReversiConst.REVERSI_STS_WHITE;
			this.mMasuSts[(this.mMasuCnt >> 1),(this.mMasuCnt >> 1)]			= ReversiConst.REVERSI_STS_BLACK;
			this.makeMasuSts(ReversiConst.REVERSI_STS_BLACK);
			this.makeMasuSts(ReversiConst.REVERSI_STS_WHITE);
			this.mMasuHistCur = 0;
			Array.Copy(this.mMasuSts, this.mMasuStsOld, this.mMasuSts.Length);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			各コマの置ける場所等のステータス作成
		///	@fn				int makeMasuSts(int color)
		///	@param[in]		int color		ステータスを作成するコマ
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private int makeMasuSts(int color)
		{
			int flg;
			int okflg = 0;
			int cnt1;
			int cnt2;
			int count1;
			int count2 = 0;
			int ret = -1;
			int countMax = 0;
			int loop;

			for (var i = 0; i < this.mMasuCnt; i++) {								// 初期化
				for (var j = 0; j < this.mMasuCnt; j++) {
					if (color == ReversiConst.REVERSI_STS_BLACK) {
						this.mMasuStsEnaB[i,j] = 0;
						this.mMasuStsCntB[i,j] = 0;
					} else {
						this.mMasuStsEnaW[i,j] = 0;
						this.mMasuStsCntW[i,j] = 0;
					}
				}
			}

			loop = this.mMasuCnt * this.mMasuCnt;
			for (var i = 0; i < loop; i++) {										// 初期化
				if (color == ReversiConst.REVERSI_STS_BLACK) {
					this.mMasuPointB[i].x = 0;
					this.mMasuPointB[i].y = 0;
				} else {
					this.mMasuPointW[i].x = 0;
					this.mMasuPointW[i].y = 0;
				}
			}
			if (color == ReversiConst.REVERSI_STS_BLACK) {
				this.mMasuPointCntB = 0;
			} else {
				this.mMasuPointCntW = 0;
			}
			this.mMasuBetCntB = 0;
			this.mMasuBetCntW = 0;

			for (var i = 0; i < this.mMasuCnt; i++) {
				for (var j = 0; j < this.mMasuCnt; j++) {
					okflg = 0;
					count2 = 0;
					if (this.mMasuSts[i,j] == ReversiConst.REVERSI_STS_NONE) {		// 何も置かれていないマスなら
						cnt1 = i;
						count1 = flg = 0;
						// *** 上方向を調べる *** //
						while ((cnt1 > 0) && (this.mMasuSts[cnt1 - 1,j] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt1 - 1,j] != color)) {
							flg = 1;
							cnt1--;
							count1++;
						}
						if ((cnt1 > 0) && (flg == 1) && (this.mMasuSts[cnt1 - 1,j] == color)) {
							okflg = 1;
							count2 += count1;
						}
						cnt1 = i;
						count1 = flg = 0;
						// *** 下方向を調べる *** //
						while ((cnt1 < (this.mMasuCnt - 1)) && (this.mMasuSts[cnt1 + 1,j] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt1 + 1,j] != color)) {
							flg = 1;
							cnt1++;
							count1++;
						}
						if ((cnt1 < (this.mMasuCnt - 1)) && (flg == 1) && (this.mMasuSts[cnt1 + 1,j] == color)) {
							okflg = 1;
							count2 += count1;
						}
						cnt2 = j;
						count1 = flg = 0;
						// *** 右方向を調べる *** //
						while ((cnt2 < (this.mMasuCnt - 1)) && (this.mMasuSts[i,cnt2 + 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[i,cnt2 + 1] != color)) {
							flg = 1;
							cnt2++;
							count1++;
						}
						if ((cnt2 < (this.mMasuCnt - 1)) && (flg == 1) && (this.mMasuSts[i,cnt2 + 1] == color)) {
							okflg = 1;
							count2 += count1;
						}
						cnt2 = j;
						count1 = flg = 0;
						// *** 左方向を調べる *** //
						while ((cnt2 > 0) && (this.mMasuSts[i,cnt2 - 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[i,cnt2 - 1] != color)) {
							flg = 1;
							cnt2--;
							count1++;
						}
						if ((cnt2 > 0) && (flg == 1) && (this.mMasuSts[i,cnt2 - 1] == color)) {
							okflg = 1;
							count2 += count1;
						}
						cnt2 = j;
						cnt1 = i;
						count1 = flg = 0;
						// *** 右上方向を調べる *** //
						while (((cnt2 < (this.mMasuCnt - 1)) && (cnt1 > 0)) && (this.mMasuSts[cnt1 - 1,cnt2 + 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt1 - 1,cnt2 + 1] != color)) {
							flg = 1;
							cnt1--;
							cnt2++;
							count1++;
						}
						if (((cnt2 < (this.mMasuCnt - 1)) && (cnt1 > 0)) && (flg == 1) && (this.mMasuSts[cnt1 - 1,cnt2 + 1] == color)) {
							okflg = 1;
							count2 += count1;
						}
						cnt2 = j;
						cnt1 = i;
						count1 = flg = 0;
						// *** 左上方向を調べる *** //
						while (((cnt2 > 0) && (cnt1 > 0)) && (this.mMasuSts[cnt1 - 1,cnt2 - 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt1 - 1,cnt2 - 1] != color)) {
							flg = 1;
							cnt1--;
							cnt2--;
							count1++;
						}
						if (((cnt2 > 0) && (cnt1 > 0)) && (flg == 1) && (this.mMasuSts[cnt1 - 1,cnt2 - 1] == color)) {
							okflg = 1;
							count2 += count1;
						}
						cnt2 = j;
						cnt1 = i;
						count1 = flg = 0;
						// *** 右下方向を調べる *** //
						while (((cnt2 < (this.mMasuCnt - 1)) && (cnt1 < (this.mMasuCnt - 1))) && (this.mMasuSts[cnt1 + 1,cnt2 + 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt1 + 1,cnt2 + 1] != color)) {
							flg = 1;
							cnt1++;
							cnt2++;
							count1++;
						}
						if (((cnt2 < (this.mMasuCnt - 1)) && (cnt1 < (this.mMasuCnt - 1))) && (flg == 1) && (this.mMasuSts[cnt1 + 1,cnt2 + 1] == color)) {
							okflg = 1;
							count2 += count1;
						}
						cnt2 = j;
						cnt1 = i;
						count1 = flg = 0;
						// *** 左下方向を調べる *** //
						while (((cnt2 > 0) && (cnt1 < (this.mMasuCnt - 1))) && (this.mMasuSts[cnt1 + 1,cnt2 - 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt1 + 1,cnt2 - 1] != color)) {
							flg = 1;
							cnt1++;
							cnt2--;
							count1++;
						}
						if (((cnt2 > 0) && (cnt1 < (this.mMasuCnt - 1))) && (flg == 1) && (this.mMasuSts[cnt1 + 1,cnt2 - 1] == color)) {
							okflg = 1;
							count2 += count1;
						}
						if (okflg == 1) {
							if (color == ReversiConst.REVERSI_STS_BLACK) {
								this.mMasuStsEnaB[i,j] = 1;
								this.mMasuStsCntB[i,j] = count2;
								// *** 置ける場所をリニアに保存、置けるポイント数も保存 *** //
								this.mMasuPointB[this.mMasuPointCntB].y = i;
								this.mMasuPointB[this.mMasuPointCntB].x = j;
								this.mMasuPointCntB++;
							} else {
								this.mMasuStsEnaW[i,j] = 1;
								this.mMasuStsCntW[i,j] = count2;
								// *** 置ける場所をリニアに保存、置けるポイント数も保存 *** //
								this.mMasuPointW[this.mMasuPointCntW].y = i;
								this.mMasuPointW[this.mMasuPointCntW].x = j;
								this.mMasuPointCntW++;
							}
							ret = 0;
							if (countMax < count2) countMax = count2;
						}
					} else if (this.mMasuSts[i,j] == ReversiConst.REVERSI_STS_BLACK) {
						this.mMasuBetCntB++;
					} else if (this.mMasuSts[i,j] == ReversiConst.REVERSI_STS_WHITE) {
						this.mMasuBetCntW++;
					}
				}
			}

			// *** 一番枚数を獲得できるマスを設定 *** //
			for (var i = 0; i < this.mMasuCnt; i++) {
				for (var j = 0; j < this.mMasuCnt; j++) {
					if (color == ReversiConst.REVERSI_STS_BLACK) {
						if (this.mMasuStsEnaB[i,j] != 0 && this.mMasuStsCntB[i,j] == countMax) {
							this.mMasuStsEnaB[i,j] = 2;
						}
					} else {
						if (this.mMasuStsEnaW[i,j] != 0 && this.mMasuStsCntW[i,j] == countMax) {
							this.mMasuStsEnaW[i,j] = 2;
						}
					}
				}
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コマをひっくり返す
		///	@fn				void revMasuSts(int color, int y, int x)
		///	@param[in]		int color		ひっくり返す元コマ
		///	@param[in]		int y			ひっくり返す元コマのY座標
		///	@param[in]		int x			ひっくり返す元コマのX座標
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void revMasuSts(int color, int y, int x)
		{
			int cnt1;
			int cnt2;
			int rcnt1;
			int rcnt2;
			int flg = 0;

			// *** 左方向にある駒を調べる *** //
			for (flg = 0, cnt1 = x, cnt2 = y; cnt1 > 0;) {
				if (this.mMasuSts[cnt2,cnt1 - 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt2,cnt1 - 1] != color) {
					// *** プレイヤー以外の色の駒があるなら *** //
					cnt1--;
				} else if (this.mMasuSts[cnt2,cnt1 - 1] == color) {
					flg = 1;
					break;
				} else if (this.mMasuSts[cnt2,cnt1 - 1] == ReversiConst.REVERSI_STS_NONE) {
					break;
				}
			}
			if (flg == 1) {
				// *** 駒をひっくり返す *** //
				for (rcnt1 = cnt1; rcnt1 < x; rcnt1++) {
					this.mMasuSts[cnt2,rcnt1] = color;
				}
			}

			// *** 右方向にある駒を調べる *** //
			for (flg = 0, cnt1 = x, cnt2 = y; cnt1 < (this.mMasuCnt - 1);) {
				if (this.mMasuSts[cnt2,cnt1 + 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt2,cnt1 + 1] != color) {
					// *** プレイヤー以外の色の駒があるなら *** //
					cnt1++;
				} else if (this.mMasuSts[cnt2,cnt1 + 1] == color) {
					flg = 1;
					break;
				} else if (this.mMasuSts[cnt2,cnt1 + 1] == ReversiConst.REVERSI_STS_NONE) {
					break;
				}
			}
			if (flg == 1) {
				// *** 駒をひっくり返す *** //
				for (rcnt1 = cnt1; rcnt1 > x; rcnt1--) {
					this.mMasuSts[cnt2,rcnt1] = color;
				}
			}

			// *** 上方向にある駒を調べる *** //
			for (flg = 0, cnt1 = x, cnt2 = y; cnt2 > 0;) {
				if (this.mMasuSts[cnt2 - 1,cnt1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt2 - 1,cnt1] != color) {
					// *** プレイヤー以外の色の駒があるなら *** //
					cnt2--;
				} else if (this.mMasuSts[cnt2 - 1,cnt1] == color) {
					flg = 1;
					break;
				} else if (this.mMasuSts[cnt2 - 1,cnt1] == ReversiConst.REVERSI_STS_NONE) {
					break;
				}
			}
			if (flg == 1) {
				// *** 駒をひっくり返す *** //
				for (rcnt1 = cnt2; rcnt1 < y; rcnt1++) {
					this.mMasuSts[rcnt1,cnt1] = color;
				}
			}

			// *** 下方向にある駒を調べる *** //
			for (flg = 0, cnt1 = x, cnt2 = y; cnt2 < (this.mMasuCnt - 1);) {
				if (this.mMasuSts[cnt2 + 1,cnt1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt2 + 1,cnt1] != color) {
					// *** プレイヤー以外の色の駒があるなら *** //
					cnt2++;
				} else if (this.mMasuSts[cnt2 + 1,cnt1] == color) {
					flg = 1;
					break;
				} else if (this.mMasuSts[cnt2 + 1,cnt1] == ReversiConst.REVERSI_STS_NONE) {
					break;
				}
			}
			if (flg == 1) {
				// *** 駒をひっくり返す *** //
				for (rcnt1 = cnt2; rcnt1 > y; rcnt1--) {
					this.mMasuSts[rcnt1,cnt1] = color;
				}
			}

			// *** 左上方向にある駒を調べる *** //
			for (flg = 0, cnt1 = x, cnt2 = y; cnt2 > 0 && cnt1 > 0;) {
				if (this.mMasuSts[cnt2 - 1,cnt1 - 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt2 - 1,cnt1 - 1] != color) {
					// *** プレイヤー以外の色の駒があるなら *** //
					cnt2--;
					cnt1--;
				} else if (this.mMasuSts[cnt2 - 1,cnt1 - 1] == color) {
					flg = 1;
					break;
				} else if (this.mMasuSts[cnt2 - 1,cnt1 - 1] == ReversiConst.REVERSI_STS_NONE) {
					break;
				}
			}
			if (flg == 1) {
				// *** 駒をひっくり返す *** //
				for (rcnt1 = cnt2, rcnt2 = cnt1; (rcnt1 < y) && (rcnt2 < x); rcnt1++ , rcnt2++) {
					this.mMasuSts[rcnt1,rcnt2] = color;
				}
			}

			// *** 左下方向にある駒を調べる *** //
			for (flg = 0, cnt1 = x, cnt2 = y; cnt2 < (this.mMasuCnt - 1) && cnt1 > 0;) {
				if (this.mMasuSts[cnt2 + 1,cnt1 - 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt2 + 1,cnt1 - 1] != color) {
					// *** プレイヤー以外の色の駒があるなら *** //
					cnt2++;
					cnt1--;
				} else if (this.mMasuSts[cnt2 + 1,cnt1 - 1] == color) {
					flg = 1;
					break;
				} else if (this.mMasuSts[cnt2 + 1,cnt1 - 1] == ReversiConst.REVERSI_STS_NONE) {
					break;
				}
			}
			if (flg == 1) {
				// *** 駒をひっくり返す *** //
				for (rcnt1 = cnt2, rcnt2 = cnt1; (rcnt1 > y) && (rcnt2 < x); rcnt1-- , rcnt2++) {
					this.mMasuSts[rcnt1,rcnt2] = color;
				}
			}

			// *** 右上方向にある駒を調べる *** //
			for (flg = 0, cnt1 = x, cnt2 = y; cnt2 > 0 && cnt1 < (this.mMasuCnt - 1);) {
				if (this.mMasuSts[cnt2 - 1,cnt1 + 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt2 - 1,cnt1 + 1] != color) {
					// *** プレイヤー以外の色の駒があるなら *** //
					cnt2--;
					cnt1++;
				} else if (this.mMasuSts[cnt2 - 1,cnt1 + 1] == color) {
					flg = 1;
					break;
				} else if (this.mMasuSts[cnt2 - 1,cnt1 + 1] == ReversiConst.REVERSI_STS_NONE) {
					break;
				}
			}
			if (flg == 1) {
				// *** 駒をひっくり返す *** //
				for (rcnt1 = cnt2, rcnt2 = cnt1; (rcnt1 < y) && (rcnt2 > x); rcnt1++ , rcnt2--) {
					this.mMasuSts[rcnt1,rcnt2] = color;
				}
			}

			// *** 右下方向にある駒を調べる *** //
			for (flg = 0, cnt1 = x, cnt2 = y; cnt2 < (this.mMasuCnt - 1) && cnt1 < (this.mMasuCnt - 1);) {
				if (this.mMasuSts[cnt2 + 1,cnt1 + 1] != ReversiConst.REVERSI_STS_NONE && this.mMasuSts[cnt2 + 1,cnt1 + 1] != color) {
					// *** プレイヤー以外の色の駒があるなら *** //
					cnt2++;
					cnt1++;
				} else if (this.mMasuSts[cnt2 + 1,cnt1 + 1] == color) {
					flg = 1;
					break;
				} else if (this.mMasuSts[cnt2 + 1,cnt1 + 1] == ReversiConst.REVERSI_STS_NONE) {
					break;
				}
			}
			if (flg == 1) {
				// *** 駒をひっくり返す *** //
				for (rcnt1 = cnt2, rcnt2 = cnt1; (rcnt1 > y) && (rcnt2 > x); rcnt1-- , rcnt2--) {
					this.mMasuSts[rcnt1,rcnt2] = color;
				}
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			パラメーター範囲チェック
		///	@fn				int checkPara(int para, int min, int max)
		///	@param[in]		int para 	チェックパラメーター
		///	@param[in]		int min		パラメーター最小値
		///	@param[in]		int max		パラメーター最大値
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private int checkPara(int para, int min, int max)
		{
			int ret = -1;
			if (min <= para && para <= max) ret = 0;
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			解析を行う(黒)
		///	@fn				void AnalysisReversiBlack()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void AnalysisReversiBlack()
		{
			int tmpX;
			int tmpY;
			int sum;
			int sumOwn;
			int tmpGoodPoint;
			int tmpBadPoint;
			int tmpD1;
			int tmpD2;
			for (var cnt = 0; cnt < this.mMasuPointCntB; cnt++) {
				// *** 現在ステータスを一旦コピー *** //
				int[,] tmpMasu = new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
				int[,] tmpMasuEnaB = new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
				int[,] tmpMasuEnaW = new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
				Array.Copy(this.mMasuSts, tmpMasu, this.mMasuSts.Length);
				Array.Copy(this.mMasuStsEnaB, tmpMasuEnaB, this.mMasuStsEnaB.Length);
				Array.Copy(this.mMasuStsEnaW, tmpMasuEnaW, this.mMasuStsEnaW.Length);

				tmpY = this.mMasuPointB[cnt].y;
				tmpX = this.mMasuPointB[cnt].x;
				this.mMasuSts[tmpY,tmpX] = ReversiConst.REVERSI_STS_BLACK;					// 仮に置く
				this.revMasuSts(ReversiConst.REVERSI_STS_BLACK, tmpY, tmpX);				// 仮にひっくり返す
				this.makeMasuSts(ReversiConst.REVERSI_STS_BLACK);
				this.makeMasuSts(ReversiConst.REVERSI_STS_WHITE);
				// *** このマスに置いた場合の解析を行う *** //
				if (this.getColorEna(ReversiConst.REVERSI_STS_WHITE) != 0) {				// 相手がパス
					this.mMasuStsPassB[tmpY,tmpX] = 1;
				}
				if (this.getEdgeSideZero(tmpY, tmpX) == 0) {								// 置いた場所が角
					this.mMasuStsAnzB[tmpY,tmpX].ownEdgeCnt++;
					this.mMasuStsAnzB[tmpY,tmpX].goodPoint += 10000 * this.mMasuStsCntB[tmpY,tmpX];
				} else if (this.getEdgeSideOne(tmpY, tmpX) == 0) {							// 置いた場所が角の一つ手前
					this.mMasuStsAnzB[tmpY,tmpX].ownEdgeSideOneCnt++;
					if (this.checkEdge(ReversiConst.REVERSI_STS_BLACK, tmpY, tmpX) != 0) {	// 角を取られない
						this.mMasuStsAnzB[tmpY,tmpX].goodPoint += 10 * this.mMasuStsCntB[tmpY,tmpX];
					} else {																// 角を取られる
						this.mMasuStsAnzB[tmpY,tmpX].badPoint += 100000;
					}
				} else if (this.getEdgeSideTwo(tmpY, tmpX) == 0) {							// 置いた場所が角の二つ手前
					this.mMasuStsAnzB[tmpY,tmpX].ownEdgeSideTwoCnt++;
					this.mMasuStsAnzB[tmpY,tmpX].goodPoint += 1000 * this.mMasuStsCntB[tmpY,tmpX];
				} else if (this.getEdgeSideThree(tmpY, tmpX) == 0) {						// 置いた場所が角の三つ手前
					this.mMasuStsAnzB[tmpY,tmpX].ownEdgeSideThreeCnt++;
					this.mMasuStsAnzB[tmpY,tmpX].goodPoint += 100 * this.mMasuStsCntB[tmpY,tmpX];
				} else {																	// 置いた場所がその他
					this.mMasuStsAnzB[tmpY,tmpX].ownEdgeSideOtherCnt++;
					this.mMasuStsAnzB[tmpY,tmpX].goodPoint += 10 * this.mMasuStsCntB[tmpY,tmpX];
				}
				sum = 0;
				sumOwn = 0;
				for (var i = 0; i < this.mMasuCnt; i++) {
					for (var j = 0; j < this.mMasuCnt; j++) {
						tmpBadPoint = 0;
						tmpGoodPoint = 0;
						if (this.getMasuStsEna(ReversiConst.REVERSI_STS_WHITE, i, j) != 0) {
							sum += this.mMasuStsCntW[i,j];									// 相手の獲得予定枚数
							// *** 相手の獲得予定の最大数保持 *** //
							if (this.mMasuStsAnzB[tmpY,tmpX].max < this.mMasuStsCntW[i,j]) this.mMasuStsAnzB[tmpY,tmpX].max = this.mMasuStsCntW[i,j];
							// *** 相手の獲得予定の最小数保持 *** //
							if (this.mMasuStsCntW[i,j] < this.mMasuStsAnzB[tmpY,tmpX].min) this.mMasuStsAnzB[tmpY,tmpX].min = this.mMasuStsCntW[i,j];
							this.mMasuStsAnzB[tmpY,tmpX].pointCnt++;						// 相手の置ける場所の数
							if (this.getEdgeSideZero(i, j) == 0) {							// 置く場所が角
								this.mMasuStsAnzB[tmpY,tmpX].edgeCnt++;
								tmpBadPoint = 100000 * this.mMasuStsCntW[i,j];
							} else if (this.getEdgeSideOne(i, j) == 0) {					// 置く場所が角の一つ手前
								this.mMasuStsAnzB[tmpY,tmpX].edgeSideOneCnt++;
								tmpBadPoint = 0;
							} else if (this.getEdgeSideTwo(i, j) == 0) {					// 置く場所が角の二つ手前
								this.mMasuStsAnzB[tmpY,tmpX].edgeSideTwoCnt++;
								tmpBadPoint = 1 * this.mMasuStsCntW[i,j];
							} else if (this.getEdgeSideThree(i, j) == 0) {					// 置く場所が角の三つ手前
								this.mMasuStsAnzB[tmpY,tmpX].edgeSideThreeCnt++;
								tmpBadPoint = 1 * this.mMasuStsCntW[i,j];
							} else {														// 置く場所がその他
								this.mMasuStsAnzB[tmpY,tmpX].edgeSideOtherCnt++;
								tmpBadPoint = 1 * this.mMasuStsCntW[i,j];
							}
							if (tmpMasuEnaW[i,j] != 0) tmpBadPoint = 0;					// ステータス変化していないなら
						}
						if (this.getMasuStsEna(ReversiConst.REVERSI_STS_BLACK, i, j) != 0) {
							sumOwn += this.mMasuStsCntB[i,j];								// 自分の獲得予定枚数
							// *** 自分の獲得予定の最大数保持 *** //
							if (this.mMasuStsAnzB[tmpY,tmpX].ownMax < this.mMasuStsCntB[i,j]) this.mMasuStsAnzB[tmpY,tmpX].ownMax = this.mMasuStsCntB[i,j];
							// *** 自分の獲得予定の最小数保持 *** //
							if (this.mMasuStsCntB[i,j] < this.mMasuStsAnzB[tmpY,tmpX].ownMin) this.mMasuStsAnzB[tmpY,tmpX].ownMin = this.mMasuStsCntB[i,j];
							this.mMasuStsAnzB[tmpY,tmpX].ownPointCnt++;					// 自分の置ける場所の数
							if (this.getEdgeSideZero(i, j) == 0) {							// 置く場所が角
								this.mMasuStsAnzB[tmpY,tmpX].ownEdgeCnt++;
								tmpGoodPoint = 100 * this.mMasuStsCntB[i,j];
							} else if (this.getEdgeSideOne(i, j) == 0) {					// 置く場所が角の一つ手前
								this.mMasuStsAnzB[tmpY,tmpX].ownEdgeSideOneCnt++;
								tmpGoodPoint = 0;
							} else if (this.getEdgeSideTwo(i, j) == 0) {					// 置く場所が角の二つ手前
								this.mMasuStsAnzB[tmpY,tmpX].ownEdgeSideTwoCnt++;
								tmpGoodPoint = 3 * this.mMasuStsCntB[i,j];
							} else if (this.getEdgeSideThree(i, j) == 0) {					// 置く場所が角の三つ手前
								this.mMasuStsAnzB[tmpY,tmpX].ownEdgeSideThreeCnt++;
								tmpGoodPoint = 2 * this.mMasuStsCntB[i,j];
							} else {														// 置く場所がその他
								this.mMasuStsAnzB[tmpY,tmpX].ownEdgeSideOtherCnt++;
								tmpGoodPoint = 1 * this.mMasuStsCntB[i,j];
							}
							if (tmpMasuEnaB[i,j] != 0) tmpGoodPoint = 0;					// ステータス変化していないなら
						}
						if (tmpBadPoint != 0) this.mMasuStsAnzB[tmpY,tmpX].badPoint += tmpBadPoint;
						if (tmpGoodPoint != 0) this.mMasuStsAnzB[tmpY,tmpX].goodPoint += tmpGoodPoint;
					}
				}
				// *** 相手に取られる平均コマ数 *** //
				if (this.getPointCnt(ReversiConst.REVERSI_STS_WHITE) != 0) {
					tmpD1 = sum;
					tmpD2 = this.getPointCnt(ReversiConst.REVERSI_STS_WHITE);
					this.mMasuStsAnzB[tmpY,tmpX].avg = tmpD1 / tmpD2;
				}

				// *** 自分が取れる平均コマ数 *** //
				if (this.getPointCnt(ReversiConst.REVERSI_STS_BLACK) != 0) {
					tmpD1 = sumOwn;
					tmpD2 = this.getPointCnt(ReversiConst.REVERSI_STS_BLACK);
					this.mMasuStsAnzB[tmpY,tmpX].ownAvg = tmpD1 / tmpD2;
				}

				// *** 元に戻す *** //
				Array.Copy(tmpMasu, this.mMasuSts, tmpMasu.Length);
				this.makeMasuSts(ReversiConst.REVERSI_STS_BLACK);
				this.makeMasuSts(ReversiConst.REVERSI_STS_WHITE);
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			解析を行う(白)
		///	@fn				void AnalysisReversiWhite()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void AnalysisReversiWhite()
		{
			int tmpX;
			int tmpY;
			int sum;
			int sumOwn;
			int tmpGoodPoint;
			int tmpBadPoint;
			int tmpD1;
			int tmpD2;
			for (var cnt = 0; cnt < this.mMasuPointCntW; cnt++) {
				// *** 現在ステータスを一旦コピー *** //
				int[,] tmpMasu = new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
				int[,] tmpMasuEnaB = new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
				int[,] tmpMasuEnaW = new int[ReversiConst.DEF_MASU_CNT_MAX_VAL,ReversiConst.DEF_MASU_CNT_MAX_VAL];
				Array.Copy(this.mMasuSts, tmpMasu, this.mMasuSts.Length);
				Array.Copy(this.mMasuStsEnaB, tmpMasuEnaB, this.mMasuStsEnaB.Length);
				Array.Copy(this.mMasuStsEnaW, tmpMasuEnaW, this.mMasuStsEnaW.Length);

				tmpY = this.mMasuPointW[cnt].y;
				tmpX = this.mMasuPointW[cnt].x;
				this.mMasuSts[tmpY,tmpX] = ReversiConst.REVERSI_STS_WHITE;					// 仮に置く
				this.revMasuSts(ReversiConst.REVERSI_STS_WHITE, tmpY, tmpX);				// 仮にひっくり返す
				this.makeMasuSts(ReversiConst.REVERSI_STS_BLACK);
				this.makeMasuSts(ReversiConst.REVERSI_STS_WHITE);
				// *** このマスに置いた場合の解析を行う *** //
				if (this.getColorEna(ReversiConst.REVERSI_STS_BLACK) != 0) {				// 相手がパス
					this.mMasuStsPassW[tmpY,tmpX] = 1;
				}
				if (this.getEdgeSideZero(tmpY, tmpX) == 0) {								// 置いた場所が角
					this.mMasuStsAnzW[tmpY,tmpX].ownEdgeCnt++;
					this.mMasuStsAnzW[tmpY,tmpX].goodPoint += 10000 * this.mMasuStsCntW[tmpY,tmpX];
				} else if (this.getEdgeSideOne(tmpY, tmpX) == 0) {							// 置いた場所が角の一つ手前
					this.mMasuStsAnzW[tmpY,tmpX].ownEdgeSideOneCnt++;
					if (this.checkEdge(ReversiConst.REVERSI_STS_WHITE, tmpY, tmpX) != 0) {	// 角を取られない
						this.mMasuStsAnzW[tmpY,tmpX].goodPoint += 10 * this.mMasuStsCntW[tmpY,tmpX];
					} else {																// 角を取られる
						this.mMasuStsAnzW[tmpY,tmpX].badPoint += 100000;
					}
				} else if (this.getEdgeSideTwo(tmpY, tmpX) == 0) {							// 置いた場所が角の二つ手前
					this.mMasuStsAnzW[tmpY,tmpX].ownEdgeSideTwoCnt++;
					this.mMasuStsAnzW[tmpY,tmpX].goodPoint += 1000 * this.mMasuStsCntW[tmpY,tmpX];
				} else if (this.getEdgeSideThree(tmpY, tmpX) == 0) {						// 置いた場所が角の三つ手前
					this.mMasuStsAnzW[tmpY,tmpX].ownEdgeSideThreeCnt++;
					this.mMasuStsAnzW[tmpY,tmpX].goodPoint += 100 * this.mMasuStsCntW[tmpY,tmpX];
				} else {																	// 置いた場所がその他
					this.mMasuStsAnzW[tmpY,tmpX].ownEdgeSideOtherCnt++;
					this.mMasuStsAnzW[tmpY,tmpX].goodPoint += 10 * this.mMasuStsCntW[tmpY,tmpX];
				}
				sum = 0;
				sumOwn = 0;
				for (var i = 0; i < this.mMasuCnt; i++) {
					for (var j = 0; j < this.mMasuCnt; j++) {
						tmpBadPoint = 0;
						tmpGoodPoint = 0;
						if (this.getMasuStsEna(ReversiConst.REVERSI_STS_BLACK, i, j) != 0) {
							sum += this.mMasuStsCntB[i,j];									// 相手の獲得予定枚数
							// *** 相手の獲得予定の最大数保持 *** //
							if (this.mMasuStsAnzW[tmpY,tmpX].max < this.mMasuStsCntB[i,j]) this.mMasuStsAnzW[tmpY,tmpX].max = this.mMasuStsCntB[i,j];
							// *** 相手の獲得予定の最小数保持 *** //
							if (this.mMasuStsCntB[i,j] < this.mMasuStsAnzW[tmpY,tmpX].min) this.mMasuStsAnzW[tmpY,tmpX].min = this.mMasuStsCntB[i,j];
							this.mMasuStsAnzW[tmpY,tmpX].pointCnt++;						// 相手の置ける場所の数
							if (this.getEdgeSideZero(i, j) == 0) {							// 置く場所が角
								this.mMasuStsAnzW[tmpY,tmpX].edgeCnt++;
								tmpBadPoint = 100000 * this.mMasuStsCntB[i,j];
							} else if (this.getEdgeSideOne(i, j) == 0) {					// 置く場所が角の一つ手前
								this.mMasuStsAnzW[tmpY,tmpX].edgeSideOneCnt++;
								tmpBadPoint = 0;
							} else if (this.getEdgeSideTwo(i, j) == 0) {					// 置く場所が角の二つ手前
								this.mMasuStsAnzW[tmpY,tmpX].edgeSideTwoCnt++;
								tmpBadPoint = 1 * this.mMasuStsCntB[i,j];
							} else if (this.getEdgeSideThree(i, j) == 0) {					// 置く場所が角の三つ手前
								this.mMasuStsAnzW[tmpY,tmpX].edgeSideThreeCnt++;
								tmpBadPoint = 1 * this.mMasuStsCntB[i,j];
							} else {														// 置く場所がその他
								this.mMasuStsAnzW[tmpY,tmpX].edgeSideOtherCnt++;
								tmpBadPoint = 1 * this.mMasuStsCntB[i,j];
							}
							if (tmpMasuEnaB[i,j] != 0) tmpBadPoint = 0;					// ステータス変化していないなら
						}
						if (this.getMasuStsEna(ReversiConst.REVERSI_STS_WHITE, i, j) != 0) {
							sumOwn += this.mMasuStsCntW[i,j];								// 自分の獲得予定枚数
							// *** 自分の獲得予定の最大数保持 *** //
							if (this.mMasuStsAnzW[tmpY,tmpX].ownMax < this.mMasuStsCntW[i,j]) this.mMasuStsAnzW[tmpY,tmpX].ownMax = this.mMasuStsCntW[i,j];
							// *** 自分の獲得予定の最小数保持 *** //
							if (this.mMasuStsCntW[i,j] < this.mMasuStsAnzW[tmpY,tmpX].ownMin) this.mMasuStsAnzW[tmpY,tmpX].ownMin = this.mMasuStsCntW[i,j];
							this.mMasuStsAnzW[tmpY,tmpX].ownPointCnt++;					// 自分の置ける場所の数
							if (this.getEdgeSideZero(i, j) == 0) {							// 置く場所が角
								this.mMasuStsAnzW[tmpY,tmpX].ownEdgeCnt++;
								tmpGoodPoint = 100 * this.mMasuStsCntW[i,j];
							} else if (this.getEdgeSideOne(i, j) == 0) {					// 置く場所が角の一つ手前
								this.mMasuStsAnzW[tmpY,tmpX].ownEdgeSideOneCnt++;
								tmpGoodPoint = 0;
							} else if (this.getEdgeSideTwo(i, j) == 0) {					// 置く場所が角の二つ手前
								this.mMasuStsAnzW[tmpY,tmpX].ownEdgeSideTwoCnt++;
								tmpGoodPoint = 3 * this.mMasuStsCntW[i,j];
							} else if (this.getEdgeSideThree(i, j) == 0) {					// 置く場所が角の三つ手前
								this.mMasuStsAnzW[tmpY,tmpX].ownEdgeSideThreeCnt++;
								tmpGoodPoint = 2 * this.mMasuStsCntW[i,j];
							} else {														// 置く場所がその他
								this.mMasuStsAnzW[tmpY,tmpX].ownEdgeSideOtherCnt++;
								tmpGoodPoint = 1 * this.mMasuStsCntW[i,j];
							}
							if (tmpMasuEnaW[i,j] != 0) tmpGoodPoint = 0;					// ステータス変化していないなら
						}
						if (tmpBadPoint != 0) this.mMasuStsAnzW[tmpY,tmpX].badPoint += tmpBadPoint;
						if (tmpGoodPoint != 0) this.mMasuStsAnzW[tmpY,tmpX].goodPoint += tmpGoodPoint;
					}
				}
				// *** 相手に取られる平均コマ数 *** //
				if (this.getPointCnt(ReversiConst.REVERSI_STS_BLACK) != 0) {
					tmpD1 = sum;
					tmpD2 = this.getPointCnt(ReversiConst.REVERSI_STS_BLACK);
					this.mMasuStsAnzW[tmpY,tmpX].avg = tmpD1 / tmpD2;
				}

				// *** 自分が取れる平均コマ数 *** //
				if (this.getPointCnt(ReversiConst.REVERSI_STS_WHITE) != 0) {
					tmpD1 = sumOwn;
					tmpD2 = this.getPointCnt(ReversiConst.REVERSI_STS_WHITE);
					this.mMasuStsAnzW[tmpY,tmpX].ownAvg = tmpD1 / tmpD2;
				}

				// *** 元に戻す *** //
				Array.Copy(tmpMasu, this.mMasuSts, tmpMasu.Length);
				this.makeMasuSts(ReversiConst.REVERSI_STS_BLACK);
				this.makeMasuSts(ReversiConst.REVERSI_STS_WHITE);
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			解析を行う
		///	@fn				void AnalysisReversi(int bPassEna, int wPassEna)
		///	@param[in]		int bPassEna		1=黒パス有効
		///	@param[in]		int wPassEna		1=白パス有効
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void AnalysisReversi(int bPassEna, int wPassEna)
		{
			// *** 相手をパスさせることができるマス検索 *** //
			for (var i = 0; i < this.mMasuCnt; i++) {					// 初期化
				for (var j = 0; j < this.mMasuCnt; j++) {
					this.mMasuStsPassB[i,j] = 0;
					this.mMasuStsAnzB[i,j].reset();
					this.mMasuStsPassW[i,j] = 0;
					this.mMasuStsAnzW[i,j].reset();
				}
			}
			this.AnalysisReversiBlack();								// 黒解析
			this.AnalysisReversiWhite();								// 白解析

			this.makeMasuSts(ReversiConst.REVERSI_STS_BLACK);
			this.makeMasuSts(ReversiConst.REVERSI_STS_WHITE);

			// *** パスマスを取得 *** //
			for (var i = 0; i < this.mMasuCnt; i++) {
				for (var j = 0; j < this.mMasuCnt; j++) {
					if (this.mMasuStsPassB[i,j] != 0) {
						if (bPassEna != 0) this.mMasuStsEnaB[i,j] = 3;
					}
					if (this.mMasuStsPassW[i,j] != 0) {
						if (wPassEna != 0) this.mMasuStsEnaW[i,j] = 3;
					}
				}
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			マスステータスを取得
		///	@fn				int getMasuSts(int y, int x)
		///	@param[in]		int y			取得するマスのY座標
		///	@param[in]		int x			取得するマスのX座標
		///	@return			-1 : 失敗 それ以外 : マスステータス
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getMasuSts(int y, int x)
		{
			int ret = -1;
			if (this.checkPara(y, 0, this.mMasuCnt) == 0 && this.checkPara(x, 0, this.mMasuCnt) == 0) ret = this.mMasuSts[y,x];
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			以前のマスステータスを取得
		///	@fn				int getMasuStsOld(int y, int x)
		///	@param[in]		int y			取得するマスのY座標
		///	@param[in]		int x			取得するマスのX座標
		///	@return			-1 : 失敗 それ以外 : マスステータス
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getMasuStsOld(int y, int x)
		{
			int ret = -1;
			if (this.checkPara(y, 0, this.mMasuCnt) == 0 && this.checkPara(x, 0, this.mMasuCnt) == 0) ret = this.mMasuStsOld[y,x];
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定座標に指定色を置けるかチェック
		///	@fn				int getMasuStsEna(int color, int y, int x)
		///	@param[in]		int color		コマ色
		///	@param[in]		int y			マスのY座標
		///	@param[in]		int x			マスのX座標
		///	@return			1 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getMasuStsEna(int color, int y, int x)
		{
			int ret = 0;
			if (this.checkPara(y, 0, this.mMasuCnt) == 0 && this.checkPara(x, 0, this.mMasuCnt) == 0) {
				if (color == ReversiConst.REVERSI_STS_BLACK) ret = this.mMasuStsEnaB[y,x];
				else ret = this.mMasuStsEnaW[y,x];
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定座標の獲得コマ数取得
		///	@fn				int getMasuStsCnt(int color, int y, int x)
		///	@param[in]		int color		コマ色
		///	@param[in]		int y			マスのY座標
		///	@param[in]		int x			マスのX座標
		///	@return			-1 : 失敗 それ以外 : 獲得コマ数
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getMasuStsCnt(int color, int y, int x)
		{
			int ret = -1;
			if (this.checkPara(y, 0, this.mMasuCnt) == 0 && this.checkPara(x, 0, this.mMasuCnt) == 0) {
				if (color == ReversiConst.REVERSI_STS_BLACK) ret = this.mMasuStsCntB[y,x];
				else ret = this.mMasuStsCntW[y,x];
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定色が現在置ける場所があるかチェック
		///	@fn				int getColorEna(int color)
		///	@param[in]		int color		コマ色
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getColorEna(int color)
		{
			int ret = -1;
			for (var i = 0; i < this.mMasuCnt; i++) {
				for (var j = 0; j < this.mMasuCnt; j++) {
					if (this.getMasuStsEna(color, i, j) != 0) {
						ret = 0;
						break;
					}
				}
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			ゲーム終了かチェック
		///	@fn				int getGameEndSts()
		///	@return			0 : 続行 それ以外 : ゲーム終了
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getGameEndSts()
		{
			int ret = 1;
			if (this.getColorEna(ReversiConst.REVERSI_STS_BLACK) == 0) ret = 0;
			if (this.getColorEna(ReversiConst.REVERSI_STS_WHITE) == 0) ret = 0;
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定座標にコマを置く
		///	@fn				int setMasuSts(int color, int y, int x)
		///	@param[in]		int color		コマ色
		///	@param[in]		int y			マスのY座標
		///	@param[in]		int x			マスのX座標
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int setMasuSts(int color, int y, int x)
		{
			int ret = -1;
			if (this.getMasuStsEna(color, y, x) != 0) {
				ret = 0;
				Array.Copy(this.mMasuSts, this.mMasuStsOld, this.mMasuSts.Length);
				this.mMasuSts[y,x] = color;
				this.revMasuSts(color, y, x);
				this.makeMasuSts(ReversiConst.REVERSI_STS_BLACK);
				this.makeMasuSts(ReversiConst.REVERSI_STS_WHITE);
				// *** 履歴保存 *** //
				if (this.mMasuHistCur < (this.mMasuCntMax * this.mMasuCntMax)) {
					this.mMasuHist[this.mMasuHistCur].color = color;
					this.mMasuHist[this.mMasuHistCur].point.y = y;
					this.mMasuHist[this.mMasuHistCur].point.x = x;
					this.mMasuHistCur++;
				}
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定座標にコマを強制的に置く
		///	@fn				int setMasuStsForcibly(int color, int y, int x)
		///	@param[in]		int color		コマ色
		///	@param[in]		int y			マスのY座標
		///	@param[in]		int x			マスのX座標
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int setMasuStsForcibly(int color, int y, int x)
		{
			int ret = -1;
			ret = 0;
			Array.Copy(this.mMasuSts, this.mMasuStsOld, this.mMasuSts.Length);
			this.mMasuSts[y,x] = color;
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			マスの数変更
		///	@fn				int setMasuCnt(int cnt)
		///	@param[in]		int cnt		マスの数
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int setMasuCnt(int cnt)
		{
			int ret = -1;
			int chg = 0;

			if (this.checkPara(cnt, 0, this.mMasuCntMax) == 0) {
				if (this.mMasuCnt != cnt) chg = 1;
				this.mMasuCnt = cnt;
				ret = 0;
				if (chg == 1) this.reset();
			}

			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			ポイント座標取得
		///	@fn				ReversiPoint getPoint(int color, int num)
		///	@param[in]		int color		コマ色
		///	@param[in]		int num			ポイント
		///	@return			ポイント座標 null : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiPoint getPoint(int color, int num)
		{
			ReversiPoint ret = null;
			if (this.checkPara(num, 0, (this.mMasuCnt * this.mMasuCnt)) == 0) {
				if (color == ReversiConst.REVERSI_STS_BLACK) ret = this.mMasuPointB[num];
				else ret = this.mMasuPointW[num];
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			ポイント座標数取得
		///	@fn				int getPointCnt(int color)
		///	@param[in]		int color		コマ色
		///	@return			ポイント座標数取得
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getPointCnt(int color)
		{
			int ret = 0;
			if (color == ReversiConst.REVERSI_STS_BLACK) ret = this.mMasuPointCntB;
			else ret = this.mMasuPointCntW;
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コマ数取得
		///	@fn				int getBetCnt(int color)
		///	@param[in]		int color		コマ色
		///	@return			コマ数取得
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getBetCnt(int color)
		{
			int ret = 0;
			if (color == ReversiConst.REVERSI_STS_BLACK) ret = this.mMasuBetCntB;
			else ret = this.mMasuBetCntW;
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			パス判定
		///	@fn				int getPassEna(int color, int y, int x)
		///	@param[in]		int color		コマ色
		///	@param[in]		int y			マスのY座標
		///	@param[in]		int x			マスのX座標
		///	@return			パス判定
		///					- 0 : NOT PASS
		///					- 1 : PASS
		///
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getPassEna(int color, int y, int x)
		{
			int ret = 0;
			if (this.checkPara(y, 0, this.mMasuCnt) == 0 && this.checkPara(x, 0, this.mMasuCnt) == 0) {
				if (color == ReversiConst.REVERSI_STS_BLACK) ret = this.mMasuStsPassB[y,x];
				else ret = this.mMasuStsPassW[y,x];
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			履歴取得
		///	@fn				ReversiHistory getHistory(int num)
		///	@param[in]		int num		ポイント
		///	@return			履歴 null : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiHistory getHistory(int num)
		{
			ReversiHistory ret = null;
			if (this.checkPara(num, 0, (this.mMasuCnt * this.mMasuCnt)) == 0) {
				ret = this.mMasuHist[num];
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			履歴数取得
		///	@fn				int getHistoryCnt()
		///	@return			履歴数
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getHistoryCnt()
		{
			int ret = 0;
			ret = this.mMasuHistCur;
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			ポイント座標解析取得
		///	@fn				ReversiAnz getPointAnz(int color, int y, int x)
		///	@param[in]		int color		コマ色
		///	@param[in]		int y			マスのY座標
		///	@param[in]		int x			マスのX座標
		///	@return			ポイント座標解析 null : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiAnz getPointAnz(int color, int y, int x)
		{
			ReversiAnz ret = null;
			if (this.checkPara(y, 0, this.mMasuCnt) == 0 && this.checkPara(x, 0, this.mMasuCnt) == 0) {
				if (color == ReversiConst.REVERSI_STS_BLACK) ret = this.mMasuStsAnzB[y,x];
				else ret = this.mMasuStsAnzW[y,x];
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			角の隣に置いても角を取られないマス検索
		///	@fn				int checkEdge(int color, int y, int x)
		///	@param[in]		int color		コマ色
		///	@param[in]		int y			マスのY座標
		///	@param[in]		int x			マスのX座標
		///	@return			0 : 取られる それ以外 : 取られない
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int checkEdge(int color, int y, int x)
		{
			int style = 0;
			int flg1 = 0;
			int flg2 = 0;
			int loop;
			int loop2;

			if (y == 0 && x == 1) {
				for (loop = x, flg1 = 0, flg2 = 0; loop < this.mMasuCnt; loop++) {
					if (this.getMasuSts(y, loop) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(y, loop) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(y, loop) != color) && (this.getMasuSts(y, loop) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == 1 && x == 0) {
				for (loop = y, flg1 = 0, flg2 = 0; loop < this.mMasuCnt; loop++) {
					if (this.getMasuSts(loop, x) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(loop, x) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(loop, x) != color) && (this.getMasuSts(loop, x) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == 1 && x == 1) {
				for (loop = y, flg1 = 0, flg2 = 0; loop < this.mMasuCnt; loop++) {
					if (this.getMasuSts(loop, loop) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(loop, loop) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(loop, loop) != color) && (this.getMasuSts(loop, loop) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == 0 && x == (this.mMasuCnt - 2)) {
				for (loop = x, flg1 = 0, flg2 = 0; loop > 0; loop--) {
					if (this.getMasuSts(y, loop) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(y, loop) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(y, loop) != color) && (this.getMasuSts(y, loop) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == 1 && x == (this.mMasuCnt - 1)) {
				for (loop = y, flg1 = 0, flg2 = 0; loop < this.mMasuCnt; loop++) {
					if (this.getMasuSts(loop, x) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(loop, x) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(loop, x) != color) && (this.getMasuSts(loop, x) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == 1 && x == (this.mMasuCnt - 2)) {
				for (loop = y, loop2 = x, flg1 = 0, flg2 = 0; loop < this.mMasuCnt; loop++ , loop2--) {
					if (this.getMasuSts(loop, loop2) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(loop, loop2) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(loop, loop2) != color) && (this.getMasuSts(loop, loop2) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == (this.mMasuCnt - 2) && x == 0) {
				for (loop = y, flg1 = 0, flg2 = 0; loop > 0; loop--) {
					if (this.getMasuSts(loop, x) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(loop, x) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(loop, x) != color) && (this.getMasuSts(loop, x) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == (this.mMasuCnt - 1) && x == 1) {
				for (loop = x, flg1 = 0, flg2 = 0; loop < this.mMasuCnt; loop++) {
					if (this.getMasuSts(y, loop) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(y, loop) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(y, loop) != color) && (this.getMasuSts(y, loop) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == (this.mMasuCnt - 2) && x == 1) {
				for (loop = y, loop2 = x, flg1 = 0, flg2 = 0; loop > 0; loop-- , loop2++) {
					if (this.getMasuSts(loop, loop2) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(loop, loop2) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(loop, loop2) != color) && (this.getMasuSts(loop, loop2) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == (this.mMasuCnt - 2) && x == (this.mMasuCnt - 1)) {
				for (loop = y, flg1 = 0, flg2 = 0; loop > 0; loop--) {
					if (this.getMasuSts(loop, x) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(loop, x) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(loop, x) != color) && (this.getMasuSts(loop, x) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == (this.mMasuCnt - 1) && x == (this.mMasuCnt - 2)) {
				for (loop = x, flg1 = 0, flg2 = 0; loop > 0; loop--) {
					if (this.getMasuSts(y, loop) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(y, loop) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(y, loop) != color) && (this.getMasuSts(y, loop) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}
			if (y == (this.mMasuCnt - 2) && x == (this.mMasuCnt - 2)) {
				for (loop = y, loop2 = x, flg1 = 0, flg2 = 0; loop > 0; loop-- , loop2--) {
					if (this.getMasuSts(loop, loop2) == color) flg1 = 1;
					if (flg1 == 1 && this.getMasuSts(loop, loop2) == ReversiConst.REVERSI_STS_NONE) break;
					if ((flg1 == 1) && (this.getMasuSts(loop, loop2) != color) && (this.getMasuSts(loop, loop2) != ReversiConst.REVERSI_STS_NONE)) flg2 = 1;
				}
				if ((flg1 == 1) && (flg2 == 0)) style = 1;
			}

			return style;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定座標が角か取得
		///	@fn				int getEdgeSideZero(int y, int x)
		///	@param[in]		int y			Y座標
		///	@param[in]		int x			X座標
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getEdgeSideZero(int y, int x)
		{
			int ret = -1;
			if (
				(y == 0 && x == 0)
				|| (y == 0 && x == (this.mMasuCnt - 1))
				|| (y == (this.mMasuCnt - 1) && x == 0)
				|| (y == (this.mMasuCnt - 1) && x == (this.mMasuCnt - 1))
			) {
				ret = 0;
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定座標が角の一つ手前か取得
		///	@fn				int getEdgeSideOne(int y, int x)
		///	@param[in]		int y			Y座標
		///	@param[in]		int x			X座標
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getEdgeSideOne(int y, int x)
		{
			int ret = -1;
			if (
				(y == 0 && x == 1)
				|| (y == 0 && x == (this.mMasuCnt - 2))
				|| (y == 1 && x == 0)
				|| (y == 1 && x == 1)
				|| (y == 1 && x == (this.mMasuCnt - 2))
				|| (y == 1 && x == (this.mMasuCnt - 1))
				|| (y == (this.mMasuCnt - 2) && x == 0)
				|| (y == (this.mMasuCnt - 2) && x == 1)
				|| (y == (this.mMasuCnt - 2) && x == (this.mMasuCnt - 2))
				|| (y == (this.mMasuCnt - 2) && x == (this.mMasuCnt - 1))
				|| (y == (this.mMasuCnt - 1) && x == 1)
				|| (y == (this.mMasuCnt - 1) && x == (this.mMasuCnt - 2))
			) {
				ret = 0;
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定座標が角の二つ手前か取得
		///	@fn				int getEdgeSideTwo(int y, int x)
		///	@param[in]		int y			Y座標
		///	@param[in]		int x			X座標
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getEdgeSideTwo(int y, int x)
		{
			int ret = -1;
			if (
				(y == 0 && x == 2)
				|| (y == 0 && x == (this.mMasuCnt - 3))
				|| (y == 2 && x == 0)
				|| (y == 2 && x == 2)
				|| (y == 2 && x == (this.mMasuCnt - 3))
				|| (y == 2 && x == (this.mMasuCnt - 1))
				|| (y == (this.mMasuCnt - 3) && x == 0)
				|| (y == (this.mMasuCnt - 3) && x == 2)
				|| (y == (this.mMasuCnt - 3) && x == (this.mMasuCnt - 3))
				|| (y == (this.mMasuCnt - 3) && x == (this.mMasuCnt - 1))
				|| (y == (this.mMasuCnt - 1) && x == 2)
				|| (y == (this.mMasuCnt - 1) && x == (this.mMasuCnt - 3))
			) {
				ret = 0;
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			指定座標が角の三つ以上手前か取得
		///	@fn				int getEdgeSideThree(int y, int x)
		///	@param[in]		int y			Y座標
		///	@param[in]		int x			X座標
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int getEdgeSideThree(int y, int x)
		{
			int ret = -1;
			if (
				(y == 0 && (3 <= x && x <= (this.mMasuCnt - 4)))
				|| ((3 <= y && y <= (this.mMasuCnt - 4)) && x == 0)
				|| (y == (this.mMasuCnt - 1) && (3 <= x && x <= (this.mMasuCnt - 4)))
				|| ((3 <= y && y <= (this.mMasuCnt - 4)) && x == (this.mMasuCnt - 1))
			) {
				ret = 0;
			}
			return ret;
		}
	}
}
