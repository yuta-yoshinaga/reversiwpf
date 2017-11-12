////////////////////////////////////////////////////////////////////////////////
///	@file			ReversiSetting.cs
///	@brief			アプリ設定クラス
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
using System.Drawing;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
	////////////////////////////////////////////////////////////////////////////////
	///	@class		ReversiSetting
	///	@brief		アプリ設定クラス
	///
	////////////////////////////////////////////////////////////////////////////////
	public class ReversiSetting
	{
		#region メンバ変数
		private int _mMode = ReversiConst.DEF_MODE_ONE;								//!< 現在のモード
		private int _mType = ReversiConst.DEF_TYPE_HARD;							//!< 現在のタイプ
		private int _mPlayer = ReversiConst.REVERSI_STS_BLACK;						//!< プレイヤーの色
		private int _mAssist = ReversiConst.DEF_ASSIST_ON;							//!< アシスト
		private int _mGameSpd = ReversiConst.DEF_GAME_SPD_MID;						//!< ゲームスピード
		private int _mEndAnim = ReversiConst.DEF_END_ANIM_ON;						//!< ゲーム終了アニメーション
		private int _mMasuCntMenu = ReversiConst.DEF_MASU_CNT_8;					//!< マスの数
		private int _mMasuCnt = ReversiConst.DEF_MASU_CNT_8_VAL;					//!< マスの数
		private int _mPlayCpuInterVal = ReversiConst.DEF_GAME_SPD_MID_VAL2;			//!< CPU対戦時のインターバル(msec)
		private int _mPlayDrawInterVal = ReversiConst.DEF_GAME_SPD_MID_VAL;			//!< 描画のインターバル(msec)
		private int _mEndDrawInterVal = 100;										//!< 終了アニメーション描画のインターバル(msec)
		private int _mEndInterVal = 500;											//!< 終了アニメーションのインターバル(msec)
		private Color _mPlayerColor1 = Color.FromArgb(255,0,0,0);					//!< プレイヤー1の色
		private Color _mPlayerColor2 = Color.FromArgb(255,255,255,255);				//!< プレイヤー2の色
		private Color _mBackGroundColor = Color.FromArgb(255,0,255,0);				//!< 背景の色
		private Color _mBorderColor = Color.FromArgb(255,0,0,0);					//!< 枠線の色
		#endregion

		#region プロパティ
		public int mMode
		{
			get { return _mMode; }
			set { _mMode = value; }
		}
		public int mType
		{
			get { return _mType; }
			set { _mType = value; }
		}
		public int mPlayer
		{
			get { return _mPlayer; }
			set { _mPlayer = value; }
		}
		public int mAssist
		{
			get { return _mAssist; }
			set { _mAssist = value; }
		}
		public int mGameSpd
		{
			get { return _mGameSpd; }
			set { _mGameSpd = value; }
		}
		public int mEndAnim
		{
			get { return _mEndAnim; }
			set { _mEndAnim = value; }
		}
		public int mMasuCntMenu
		{
			get { return _mMasuCntMenu; }
			set { _mMasuCntMenu = value; }
		}
		public int mMasuCnt
		{
			get { return _mMasuCnt; }
			set { _mMasuCnt = value; }
		}
		public int mPlayCpuInterVal
		{
			get { return _mPlayCpuInterVal; }
			set { _mPlayCpuInterVal = value; }
		}
		public int mPlayDrawInterVal
		{
			get { return _mPlayDrawInterVal; }
			set { _mPlayDrawInterVal = value; }
		}
		public int mEndDrawInterVal
		{
			get { return _mEndDrawInterVal; }
			set { _mEndDrawInterVal = value; }
		}
		public int mEndInterVal
		{
			get { return _mEndInterVal; }
			set { _mEndInterVal = value; }
		}
		public int mInt32PlayerColor1
		{
			get { return _mPlayerColor1.ToArgb(); }
			set { _mPlayerColor1 = Color.FromArgb(value); }
		}
		public int mInt32PlayerColor2
		{
			get { return _mPlayerColor2.ToArgb(); }
			set { _mPlayerColor2 = Color.FromArgb(value); }
		}
		public int mInt32BackGroundColor
		{
			get { return _mBackGroundColor.ToArgb(); }
			set { _mBackGroundColor = Color.FromArgb(value); }
		}
		public int mInt32BorderColor
		{
			get { return _mBorderColor.ToArgb(); }
			set { _mBorderColor = Color.FromArgb(value); }
		}
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public Color mPlayerColor1
		{
			get { return _mPlayerColor1; }
			set { _mPlayerColor1 = value; }
		}
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Color mPlayerColor2
		{
			get { return _mPlayerColor2; }
			set { _mPlayerColor2 = value; }
		}
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Color mBackGroundColor
		{
			get { return _mBackGroundColor; }
			set { _mBackGroundColor = value; }
		}
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Color mBorderColor
		{
			get { return _mBorderColor; }
			set { _mBorderColor = value; }
		}
		#endregion

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コンストラクタ
		///	@fn				ReversiSetting()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiSetting()
		{
			this.reset();
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
			mMode = ReversiConst.DEF_MODE_ONE;								// 現在のモード
			mType = ReversiConst.DEF_TYPE_HARD;								// 現在のタイプ
			mPlayer = ReversiConst.REVERSI_STS_BLACK;						// プレイヤーの色
			mAssist = ReversiConst.DEF_ASSIST_ON;							// アシスト
			mGameSpd = ReversiConst.DEF_GAME_SPD_MID;						// ゲームスピード
			mEndAnim = ReversiConst.DEF_END_ANIM_ON;						// ゲーム終了アニメーション
			mMasuCntMenu = ReversiConst.DEF_MASU_CNT_8;						// マスの数
			mMasuCnt = ReversiConst.DEF_MASU_CNT_8_VAL;						// マスの数
			mPlayCpuInterVal = ReversiConst.DEF_GAME_SPD_MID_VAL2;			// CPU対戦時のインターバル(msec)
			mPlayDrawInterVal = ReversiConst.DEF_GAME_SPD_MID_VAL;			// 描画のインターバル(msec)
			mEndDrawInterVal = 100;											// 終了アニメーション描画のインターバル(msec)
			mEndInterVal = 500;												// 終了アニメーションのインターバル(msec)
			mPlayerColor1 = Color.FromArgb(255,0,0,0);						// プレイヤー1の色
			mPlayerColor2 = Color.FromArgb(255,255,255,255);				// プレイヤー2の色
			mBackGroundColor = Color.FromArgb(255,0,255,0);					// 背景の色
			mBorderColor = Color.FromArgb(255,0,0,0);						// 枠線の色
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コピー
		///	@fn				ReversiSetting Clone()
		///	@return			オブジェクトコピー
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiSetting Clone()
		{
			return (ReversiSetting)MemberwiseClone();
		}
	}
}
