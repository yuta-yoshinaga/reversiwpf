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
			// *** 現在のモード *** //
			if(mSetting.mMode == ReversiConst.DEF_MODE_ONE)
			{
				radioButtonMode1.IsChecked = true;
				radioButtonMode2.IsChecked = false;
			}
			else
			{
				radioButtonMode1.IsChecked = false;
				radioButtonMode2.IsChecked = true;
			}
			// *** 現在のタイプ *** //
			if(mSetting.mType == ReversiConst.DEF_TYPE_EASY)
			{
				radioButtonType1.IsChecked = true;
				radioButtonType2.IsChecked = false;
				radioButtonType3.IsChecked = false;
			}
			else if(mSetting.mType == ReversiConst.DEF_TYPE_NOR)
			{
				radioButtonType1.IsChecked = false;
				radioButtonType2.IsChecked = true;
				radioButtonType3.IsChecked = false;
			}
			else
			{
				radioButtonType1.IsChecked = false;
				radioButtonType2.IsChecked = false;
				radioButtonType3.IsChecked = true;
			}
			// *** プレイヤーの色 *** //
			if(mSetting.mPlayer == ReversiConst.REVERSI_STS_BLACK)
			{
				radioButtonPlayer1.IsChecked = true;
				radioButtonPlayer2.IsChecked = false;
			}
			else
			{
				radioButtonPlayer1.IsChecked = false;
				radioButtonPlayer2.IsChecked = true;
			}
			// *** アシスト *** //
			if(mSetting.mAssist == ReversiConst.DEF_ASSIST_OFF)
			{
				radioButtonAssist1.IsChecked = true;
				radioButtonAssist2.IsChecked = false;
			}
			else
			{
				radioButtonAssist1.IsChecked = false;
				radioButtonAssist2.IsChecked = true;
			}
			// *** ゲームスピード *** //
			if(mSetting.mGameSpd == ReversiConst.DEF_GAME_SPD_FAST)
			{
				radioButtonGameSpd1.IsChecked = true;
				radioButtonGameSpd2.IsChecked = false;
				radioButtonGameSpd3.IsChecked = false;
			}
			else if(mSetting.mGameSpd == ReversiConst.DEF_GAME_SPD_MID)
			{
				radioButtonGameSpd1.IsChecked = false;
				radioButtonGameSpd2.IsChecked = true;
				radioButtonGameSpd3.IsChecked = false;
			}
			else
			{
				radioButtonGameSpd1.IsChecked = false;
				radioButtonGameSpd2.IsChecked = false;
				radioButtonGameSpd3.IsChecked = true;
			}
			// *** ゲーム終了アニメーション *** //
			if(mSetting.mEndAnim == ReversiConst.DEF_END_ANIM_OFF)
			{
				radioButtonEndAnim1.IsChecked = true;
				radioButtonEndAnim2.IsChecked = false;
			}
			else
			{
				radioButtonEndAnim1.IsChecked = false;
				radioButtonEndAnim2.IsChecked = true;
			}
			// *** マスの数 *** //
			if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_6)
			{
				radioButtonMasuCntMenu1.IsChecked = true;
				radioButtonMasuCntMenu2.IsChecked = false;
				radioButtonMasuCntMenu3.IsChecked = false;
				radioButtonMasuCntMenu4.IsChecked = false;
				radioButtonMasuCntMenu5.IsChecked = false;
				radioButtonMasuCntMenu6.IsChecked = false;
				radioButtonMasuCntMenu7.IsChecked = false;
				radioButtonMasuCntMenu8.IsChecked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_8)
			{
				radioButtonMasuCntMenu1.IsChecked = false;
				radioButtonMasuCntMenu2.IsChecked = true;
				radioButtonMasuCntMenu3.IsChecked = false;
				radioButtonMasuCntMenu4.IsChecked = false;
				radioButtonMasuCntMenu5.IsChecked = false;
				radioButtonMasuCntMenu6.IsChecked = false;
				radioButtonMasuCntMenu7.IsChecked = false;
				radioButtonMasuCntMenu8.IsChecked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_10)
			{
				radioButtonMasuCntMenu1.IsChecked = false;
				radioButtonMasuCntMenu2.IsChecked = false;
				radioButtonMasuCntMenu3.IsChecked = true;
				radioButtonMasuCntMenu4.IsChecked = false;
				radioButtonMasuCntMenu5.IsChecked = false;
				radioButtonMasuCntMenu6.IsChecked = false;
				radioButtonMasuCntMenu7.IsChecked = false;
				radioButtonMasuCntMenu8.IsChecked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_12)
			{
				radioButtonMasuCntMenu1.IsChecked = false;
				radioButtonMasuCntMenu2.IsChecked = false;
				radioButtonMasuCntMenu3.IsChecked = false;
				radioButtonMasuCntMenu4.IsChecked = true;
				radioButtonMasuCntMenu5.IsChecked = false;
				radioButtonMasuCntMenu6.IsChecked = false;
				radioButtonMasuCntMenu7.IsChecked = false;
				radioButtonMasuCntMenu8.IsChecked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_14)
			{
				radioButtonMasuCntMenu1.IsChecked = false;
				radioButtonMasuCntMenu2.IsChecked = false;
				radioButtonMasuCntMenu3.IsChecked = false;
				radioButtonMasuCntMenu4.IsChecked = false;
				radioButtonMasuCntMenu5.IsChecked = true;
				radioButtonMasuCntMenu6.IsChecked = false;
				radioButtonMasuCntMenu7.IsChecked = false;
				radioButtonMasuCntMenu8.IsChecked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_16)
			{
				radioButtonMasuCntMenu1.IsChecked = false;
				radioButtonMasuCntMenu2.IsChecked = false;
				radioButtonMasuCntMenu3.IsChecked = false;
				radioButtonMasuCntMenu4.IsChecked = false;
				radioButtonMasuCntMenu5.IsChecked = false;
				radioButtonMasuCntMenu6.IsChecked = true;
				radioButtonMasuCntMenu7.IsChecked = false;
				radioButtonMasuCntMenu8.IsChecked = false;
			}
			else if(mSetting.mMasuCntMenu == ReversiConst.DEF_MASU_CNT_18)
			{
				radioButtonMasuCntMenu1.IsChecked = false;
				radioButtonMasuCntMenu2.IsChecked = false;
				radioButtonMasuCntMenu3.IsChecked = false;
				radioButtonMasuCntMenu4.IsChecked = false;
				radioButtonMasuCntMenu5.IsChecked = false;
				radioButtonMasuCntMenu6.IsChecked = false;
				radioButtonMasuCntMenu7.IsChecked = true;
				radioButtonMasuCntMenu8.IsChecked = false;
			}
			else
			{
				radioButtonMasuCntMenu1.IsChecked = false;
				radioButtonMasuCntMenu2.IsChecked = false;
				radioButtonMasuCntMenu3.IsChecked = false;
				radioButtonMasuCntMenu4.IsChecked = false;
				radioButtonMasuCntMenu5.IsChecked = false;
				radioButtonMasuCntMenu6.IsChecked = false;
				radioButtonMasuCntMenu7.IsChecked = false;
				radioButtonMasuCntMenu8.IsChecked = true;
			}
			pictureBoxPlayerColor1.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(mSetting.mPlayerColor1.A,mSetting.mPlayerColor1.R,mSetting.mPlayerColor1.G,mSetting.mPlayerColor1.B));
			pictureBoxPlayerColor2.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(mSetting.mPlayerColor2.A,mSetting.mPlayerColor2.R,mSetting.mPlayerColor2.G,mSetting.mPlayerColor2.B));
			pictureBoxBackGroundColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(mSetting.mBackGroundColor.A,mSetting.mBackGroundColor.R,mSetting.mBackGroundColor.G,mSetting.mBackGroundColor.B));
			pictureBoxBorderColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(mSetting.mBorderColor.A,mSetting.mBorderColor.R,mSetting.mBorderColor.G,mSetting.mBorderColor.B));
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
			// *** 現在のモード *** //
			if(radioButtonMode1.IsChecked == true)			mSetting.mMode = ReversiConst.DEF_MODE_ONE;
			else											mSetting.mMode = ReversiConst.DEF_MODE_TWO;
			// *** 現在のタイプ *** //
			if(radioButtonType1.IsChecked == true)			mSetting.mType = ReversiConst.DEF_TYPE_EASY;
			else if(radioButtonType2.IsChecked == true)		mSetting.mType = ReversiConst.DEF_TYPE_NOR;
			else											mSetting.mType = ReversiConst.DEF_TYPE_HARD;
			// *** プレイヤーの色 *** //
			if(radioButtonPlayer1.IsChecked == true)			mSetting.mPlayer = ReversiConst.REVERSI_STS_BLACK;
			else											mSetting.mPlayer = ReversiConst.REVERSI_STS_WHITE;
			// *** アシスト *** //
			if(radioButtonAssist1.IsChecked == true)			mSetting.mAssist = ReversiConst.DEF_ASSIST_OFF;
			else											mSetting.mAssist = ReversiConst.DEF_ASSIST_ON;
			// *** ゲームスピード *** //
			if(radioButtonGameSpd1.IsChecked == true)			mSetting.mGameSpd = ReversiConst.DEF_GAME_SPD_FAST;
			else if(radioButtonGameSpd2.IsChecked == true)	mSetting.mGameSpd = ReversiConst.DEF_GAME_SPD_MID;
			else											mSetting.mGameSpd = ReversiConst.DEF_GAME_SPD_SLOW;
			// *** ゲーム終了アニメーション *** //
			if(radioButtonEndAnim1.IsChecked == true)			mSetting.mEndAnim = ReversiConst.DEF_END_ANIM_OFF;
			else											mSetting.mEndAnim = ReversiConst.DEF_END_ANIM_ON;
			// *** マスの数 *** //
			if(radioButtonMasuCntMenu1.IsChecked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_6;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_6_VAL;
			}
			else if(radioButtonMasuCntMenu2.IsChecked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_8;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_8_VAL;
			}
			else if(radioButtonMasuCntMenu3.IsChecked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_10;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_10_VAL;
			}
			else if(radioButtonMasuCntMenu4.IsChecked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_12;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_12_VAL;
			}
			else if(radioButtonMasuCntMenu5.IsChecked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_14;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_14_VAL;
			}
			else if(radioButtonMasuCntMenu6.IsChecked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_16;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_16_VAL;
			}
			else if(radioButtonMasuCntMenu7.IsChecked == true)
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_18;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_18_VAL;
			}
			else
			{
				mSetting.mMasuCntMenu	= ReversiConst.DEF_MASU_CNT_20;
				mSetting.mMasuCnt		= ReversiConst.DEF_MASU_CNT_20_VAL;
			}
			SolidColorBrush work		= (SolidColorBrush)pictureBoxPlayerColor1.Background;
			mSetting.mPlayerColor1		= System.Drawing.Color.FromArgb(work.Color.A,work.Color.R,work.Color.G,work.Color.B);
			work						= (SolidColorBrush)pictureBoxPlayerColor2.Background;
			mSetting.mPlayerColor2		= System.Drawing.Color.FromArgb(work.Color.A,work.Color.R,work.Color.G,work.Color.B);
			work						= (SolidColorBrush)pictureBoxBackGroundColor.Background;
			mSetting.mBackGroundColor	= System.Drawing.Color.FromArgb(work.Color.A,work.Color.R,work.Color.G,work.Color.B);
			work						= (SolidColorBrush)pictureBoxBorderColor.Background;
			mSetting.mBorderColor		= System.Drawing.Color.FromArgb(work.Color.A,work.Color.R,work.Color.G,work.Color.B);
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
			// ColorDialogクラスのインスタンスを作成
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			// はじめに選択されている色を設定
			SolidColorBrush work = (SolidColorBrush)pictureBoxPlayerColor1.Background;
			cd.Color = System.Drawing.Color.FromArgb(work.Color.A,work.Color.R,work.Color.G,work.Color.B);
			// ダイアログを表示する
			if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				// 選択された色の取得
				pictureBoxPlayerColor1.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(cd.Color.A,cd.Color.R,cd.Color.G,cd.Color.B));
			}
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
			// ColorDialogクラスのインスタンスを作成
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			// はじめに選択されている色を設定
			SolidColorBrush work = (SolidColorBrush)pictureBoxPlayerColor2.Background;
			cd.Color = System.Drawing.Color.FromArgb(work.Color.A,work.Color.R,work.Color.G,work.Color.B);
			// ダイアログを表示する
			if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				// 選択された色の取得
				pictureBoxPlayerColor2.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(cd.Color.A,cd.Color.R,cd.Color.G,cd.Color.B));
			}
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
			// ColorDialogクラスのインスタンスを作成
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			// はじめに選択されている色を設定
			SolidColorBrush work = (SolidColorBrush)pictureBoxBackGroundColor.Background;
			cd.Color = System.Drawing.Color.FromArgb(work.Color.A,work.Color.R,work.Color.G,work.Color.B);
			// ダイアログを表示する
			if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				// 選択された色の取得
				pictureBoxBackGroundColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(cd.Color.A,cd.Color.R,cd.Color.G,cd.Color.B));
			}
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
			// ColorDialogクラスのインスタンスを作成
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			// はじめに選択されている色を設定
			SolidColorBrush work = (SolidColorBrush)pictureBoxBorderColor.Background;
			cd.Color = System.Drawing.Color.FromArgb(work.Color.A,work.Color.R,work.Color.G,work.Color.B);
			// ダイアログを表示する
			if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				// 選択された色の取得
				pictureBoxBorderColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(cd.Color.A,cd.Color.R,cd.Color.G,cd.Color.B));
			}
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
			mSetting.reset();
			this.reflectSettingForm();
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
			this.loadSettingForm();
			this.Close();
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
			this.Close();
		}
	}
}
