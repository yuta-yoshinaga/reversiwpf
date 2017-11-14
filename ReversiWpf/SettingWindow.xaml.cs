////////////////////////////////////////////////////////////////////////////////
///	@file			SettingWindow.xaml.cs
///	@brief			SettingWindowクラス
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReversiWpf
{
	////////////////////////////////////////////////////////////////////////////////
	///	@class		SettingWindow
	///	@brief		SettingWindow.xaml の相互作用ロジッククラス
	///
	////////////////////////////////////////////////////////////////////////////////
	public partial class SettingWindow : Window
	{
		#region メンバ変数
		private ReversiSetting _mSetting;						//!< リバーシ設定クラス
		#endregion

		#region プロパティ
		public ReversiSetting mSetting
		{
			get { return _mSetting; }
			set { _mSetting = value; }
		}
		#endregion

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コンストラクタ
		///	@fn				SettingWindow(ReversiSetting mSetting)
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public SettingWindow(ReversiSetting mSetting)
		{
			InitializeComponent();
			this.mSetting = mSetting;
			this.reflectSettingForm();
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			フォームに設定を反映
		///	@fn				void reflectSettingForm()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void reflectSettingForm()
		{
/*
			// *** 現在のモード *** //
			if(mSetting.mMode == ReversiConst.DEF_MODE_ONE)
			{
				radioButtonMode1.Checked = true;
				radioButtonMode2.Checked = false;
			}
			else
			{
				radioButtonMode1.Checked = false;
				radioButtonMode2.Checked = true;
			}
			// *** 現在のタイプ *** //
			if(mSetting.mType == ReversiConst.DEF_TYPE_EASY)
			{
				radioButtonType1.Checked = true;
				radioButtonType2.Checked = false;
				radioButtonType3.Checked = false;
			}
			else if(mSetting.mType == ReversiConst.DEF_TYPE_NOR)
			{
				radioButtonType1.Checked = false;
				radioButtonType2.Checked = true;
				radioButtonType3.Checked = false;
			}
			else
			{
				radioButtonType1.Checked = false;
				radioButtonType2.Checked = false;
				radioButtonType3.Checked = true;
			}
			// *** プレイヤーの色 *** //
			if(mSetting.mPlayer == ReversiConst.REVERSI_STS_BLACK)
			{
				radioButtonPlayer1.Checked = true;
				radioButtonPlayer2.Checked = false;
			}
			else
			{
				radioButtonPlayer1.Checked = false;
				radioButtonPlayer2.Checked = true;
			}
			// *** アシスト *** //
			if(mSetting.mAssist == ReversiConst.DEF_ASSIST_OFF)
			{
				radioButtonAssist1.Checked = true;
				radioButtonAssist2.Checked = false;
			}
			else
			{
				radioButtonAssist1.Checked = false;
				radioButtonAssist2.Checked = true;
			}
			// *** ゲームスピード *** //
			if(mSetting.mGameSpd == ReversiConst.DEF_GAME_SPD_FAST)
			{
				radioButtonGameSpd1.Checked = true;
				radioButtonGameSpd2.Checked = false;
				radioButtonGameSpd3.Checked = false;
			}
			else if(mSetting.mGameSpd == ReversiConst.DEF_GAME_SPD_MID)
			{
				radioButtonGameSpd1.Checked = false;
				radioButtonGameSpd2.Checked = true;
				radioButtonGameSpd3.Checked = false;
			}
			else
			{
				radioButtonGameSpd1.Checked = false;
				radioButtonGameSpd2.Checked = false;
				radioButtonGameSpd3.Checked = true;
			}
			// *** ゲーム終了アニメーション *** //
			if(mSetting.mEndAnim == ReversiConst.DEF_END_ANIM_OFF)
			{
				radioButtonEndAnim1.Checked = true;
				radioButtonEndAnim2.Checked = false;
			}
			else
			{
				radioButtonEndAnim1.Checked = false;
				radioButtonEndAnim2.Checked = true;
			}
			// *** マスの数 *** //
			if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_6)
			{
				radioButtonMasuCntMenu1.Checked = true;
				radioButtonMasuCntMenu2.Checked = false;
				radioButtonMasuCntMenu3.Checked = false;
				radioButtonMasuCntMenu4.Checked = false;
				radioButtonMasuCntMenu5.Checked = false;
				radioButtonMasuCntMenu6.Checked = false;
				radioButtonMasuCntMenu7.Checked = false;
				radioButtonMasuCntMenu8.Checked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_8)
			{
				radioButtonMasuCntMenu1.Checked = false;
				radioButtonMasuCntMenu2.Checked = true;
				radioButtonMasuCntMenu3.Checked = false;
				radioButtonMasuCntMenu4.Checked = false;
				radioButtonMasuCntMenu5.Checked = false;
				radioButtonMasuCntMenu6.Checked = false;
				radioButtonMasuCntMenu7.Checked = false;
				radioButtonMasuCntMenu8.Checked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_10)
			{
				radioButtonMasuCntMenu1.Checked = false;
				radioButtonMasuCntMenu2.Checked = false;
				radioButtonMasuCntMenu3.Checked = true;
				radioButtonMasuCntMenu4.Checked = false;
				radioButtonMasuCntMenu5.Checked = false;
				radioButtonMasuCntMenu6.Checked = false;
				radioButtonMasuCntMenu7.Checked = false;
				radioButtonMasuCntMenu8.Checked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_12)
			{
				radioButtonMasuCntMenu1.Checked = false;
				radioButtonMasuCntMenu2.Checked = false;
				radioButtonMasuCntMenu3.Checked = false;
				radioButtonMasuCntMenu4.Checked = true;
				radioButtonMasuCntMenu5.Checked = false;
				radioButtonMasuCntMenu6.Checked = false;
				radioButtonMasuCntMenu7.Checked = false;
				radioButtonMasuCntMenu8.Checked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_14)
			{
				radioButtonMasuCntMenu1.Checked = false;
				radioButtonMasuCntMenu2.Checked = false;
				radioButtonMasuCntMenu3.Checked = false;
				radioButtonMasuCntMenu4.Checked = false;
				radioButtonMasuCntMenu5.Checked = true;
				radioButtonMasuCntMenu6.Checked = false;
				radioButtonMasuCntMenu7.Checked = false;
				radioButtonMasuCntMenu8.Checked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_16)
			{
				radioButtonMasuCntMenu1.Checked = false;
				radioButtonMasuCntMenu2.Checked = false;
				radioButtonMasuCntMenu3.Checked = false;
				radioButtonMasuCntMenu4.Checked = false;
				radioButtonMasuCntMenu5.Checked = false;
				radioButtonMasuCntMenu6.Checked = true;
				radioButtonMasuCntMenu7.Checked = false;
				radioButtonMasuCntMenu8.Checked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_18)
			{
				radioButtonMasuCntMenu1.Checked = false;
				radioButtonMasuCntMenu2.Checked = false;
				radioButtonMasuCntMenu3.Checked = false;
				radioButtonMasuCntMenu4.Checked = false;
				radioButtonMasuCntMenu5.Checked = false;
				radioButtonMasuCntMenu6.Checked = false;
				radioButtonMasuCntMenu7.Checked = true;
				radioButtonMasuCntMenu8.Checked = false;
			}
			else
			{
				radioButtonMasuCntMenu1.Checked = false;
				radioButtonMasuCntMenu2.Checked = false;
				radioButtonMasuCntMenu3.Checked = false;
				radioButtonMasuCntMenu4.Checked = false;
				radioButtonMasuCntMenu5.Checked = false;
				radioButtonMasuCntMenu6.Checked = false;
				radioButtonMasuCntMenu7.Checked = false;
				radioButtonMasuCntMenu8.Checked = true;
			}
			pictureBoxPlayerColor1.BackColor = mSetting.mPlayerColor1;
			pictureBoxPlayerColor2.BackColor = mSetting.mPlayerColor2;
			pictureBoxBackGroundColor.BackColor = mSetting.mBackGroundColor;
			pictureBoxBorderColor.BackColor = mSetting.mBorderColor;
*/
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			フォームから設定を読み込み
		///	@fn				void reflectSettingForm()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void loadSettingForm()
		{
/*
			// *** 現在のモード *** //
			if(radioButtonMode1.Checked == true)			mSetting.mMode = ReversiConst.DEF_MODE_ONE;
			else											mSetting.mMode = ReversiConst.DEF_MODE_TWO;
			// *** 現在のタイプ *** //
			if(radioButtonType1.Checked == true)			mSetting.mType = ReversiConst.DEF_TYPE_EASY;
			else if(radioButtonType2.Checked == true)		mSetting.mType = ReversiConst.DEF_TYPE_NOR;
			else											mSetting.mType = ReversiConst.DEF_TYPE_HARD;
			// *** プレイヤーの色 *** //
			if(radioButtonPlayer1.Checked == true)			mSetting.mPlayer = ReversiConst.REVERSI_STS_BLACK;
			else											mSetting.mPlayer = ReversiConst.REVERSI_STS_WHITE;
			// *** アシスト *** //
			if(radioButtonAssist1.Checked == true)			mSetting.mAssist = ReversiConst.DEF_ASSIST_OFF;
			else											mSetting.mAssist = ReversiConst.DEF_ASSIST_ON;
			// *** ゲームスピード *** //
			if(radioButtonGameSpd1.Checked == true)			mSetting.mGameSpd = ReversiConst.DEF_GAME_SPD_FAST;
			else if(radioButtonGameSpd2.Checked == true)	mSetting.mGameSpd = ReversiConst.DEF_GAME_SPD_MID;
			else											mSetting.mGameSpd = ReversiConst.DEF_GAME_SPD_SLOW;
			// *** ゲーム終了アニメーション *** //
			if(radioButtonEndAnim1.Checked == true)			mSetting.mEndAnim = ReversiConst.DEF_END_ANIM_OFF;
			else											mSetting.mEndAnim = ReversiConst.DEF_END_ANIM_ON;
			// *** マスの数 *** //
			if(radioButtonMasuCntMenu1.Checked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_6;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_6_VAL;
			}
			else if(radioButtonMasuCntMenu2.Checked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_8;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_8_VAL;
			}
			else if(radioButtonMasuCntMenu3.Checked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_10;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_10_VAL;
			}
			else if(radioButtonMasuCntMenu4.Checked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_12;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_12_VAL;
			}
			else if(radioButtonMasuCntMenu5.Checked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_14;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_14_VAL;
			}
			else if(radioButtonMasuCntMenu6.Checked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_16;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_16_VAL;
			}
			else if(radioButtonMasuCntMenu7.Checked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_18;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_18_VAL;
			}
			else
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_20;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_20_VAL;
			}
			mSetting.mPlayerColor1		= pictureBoxPlayerColor1.BackColor;
			mSetting.mPlayerColor2		= pictureBoxPlayerColor2.BackColor;
			mSetting.mBackGroundColor	= pictureBoxBackGroundColor.BackColor;
			mSetting.mBorderColor		= pictureBoxBorderColor.BackColor;
*/
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			プレイヤー1の色クリック
		///	@fn				void pictureBoxPlayerColor1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		MouseButtonEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void pictureBoxPlayerColor1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			プレイヤー2の色クリック
		///	@fn				void pictureBoxPlayerColor2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		MouseButtonEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void pictureBoxPlayerColor2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			背景の色クリック
		///	@fn				void pictureBoxBackGroundColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		MouseButtonEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void pictureBoxBackGroundColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			枠線の色クリック
		///	@fn				void pictureBoxBorderColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		MouseButtonEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void pictureBoxBorderColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			デフォルト設定に戻すボタンクリック
		///	@fn				void buttonDefault_Click(object sender, RoutedEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		RoutedEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void buttonDefault_Click(object sender, RoutedEventArgs e)
		{

		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			保存ボタンクリック
		///	@fn				void buttonSave_Click(object sender, RoutedEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		RoutedEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void buttonSave_Click(object sender, RoutedEventArgs e)
		{

		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			キャンセルボタンクリック
		///	@fn				void buttonCancel_Click(object sender, RoutedEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		RoutedEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
