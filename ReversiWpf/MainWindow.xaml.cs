////////////////////////////////////////////////////////////////////////////////
///	@file			MainWindow.xaml.cs
///	@brief			MainWindow.xamlクラス
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
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ReversiWpf
{
	////////////////////////////////////////////////////////////////////////////////
	///	@class		MainWindow
	///	@brief		MainWindow.xaml の相互作用ロジッククラス
	///
	////////////////////////////////////////////////////////////////////////////////
	public partial class MainWindow : Window
	{
		delegate void ViewMsgDlgDelegate(string title , string msg);
		delegate void DrawSingleDelegate(int y, int x, int sts, int bk, string text);
		delegate void CurColMsgDelegate(string text);
		delegate void CurStsMsgDelegate(string text);
		delegate void Reversi_ResizeEndDelegate(object sender, EventArgs e);

		public ReversiSetting m_AppSettings;								//!< アプリ設定
		public ReversiPlay m_ReversiPlay;									//!< リバーシ本体
		private static System.Timers.Timer aTimer;							//!< タイマー
		private System.Drawing.Size oldSize;								//!< リサイズ前のサイズ

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);				//!< gdi32.dllのDeleteObjectメソッドの使用を宣言する。

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コンストラクタ
		///	@fn				MainWindow()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public MainWindow()
		{
			InitializeComponent();
			oldSize.Width = 0;
			oldSize.Height = 0;

			System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Y.Y Magic\\ReversiWpf");
			string setPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Y.Y Magic\\ReversiWpf\\" + "AppSetting.xml";
			try
			{
				this.m_AppSettings = this.LoadSettingXml(setPath);
				if(this.m_AppSettings == null)
				{
					this.m_AppSettings = new ReversiSetting();
					this.SaveSettingXml(setPath,ref this.m_AppSettings);
				}
				this.m_ReversiPlay = new ReversiPlay();
				this.m_ReversiPlay.mSetting = this.m_AppSettings;
				this.m_ReversiPlay.viewMsgDlg = this.ViewMsgDlg;
				this.m_ReversiPlay.drawSingle = this.DrawSingle;
				this.m_ReversiPlay.curColMsg = this.CurColMsg;
				this.m_ReversiPlay.curStsMsg = this.CurStsMsg;
				this.appInit();
				Task newTask = new Task( () => { this.m_ReversiPlay.reset(); } );
				newTask.Start();
			}
			catch (Exception ex)
			{
				System.Console.WriteLine("MainWindow(1) : " + ex.Message);
				System.Console.WriteLine("MainWindow(1) : " + ex.StackTrace);
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			BITMAPかImageSourceに変換
		///	@fn				ImageSource ToImageSource(Bitmap bmp)
		///	@param[in]		Bitmap bmp
		///	@return			ImageSource
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ImageSource ToImageSource(Bitmap bmp)
		{
			var handle = bmp.GetHbitmap();
			try
			{
				return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			}
			finally { DeleteObject(handle); }
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			設定XMLファイルロード
		///	@fn				ReversiSetting LoadSettingXml(string path)
		///	@param[in]		string path		設定XMLファイルパス
		///	@return			ReversiSettingオブジェクトインスタンス
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiSetting LoadSettingXml(string path)
		{
			ReversiSetting ret = null;
			try
			{
				// *** XMLをReversiSettingオブジェクトに読み込む *** //
				XmlSerializer serializer = new XmlSerializer(typeof(ReversiSetting));
				ret = new ReversiSetting();

				FileStream fsr = new FileStream(path, FileMode.Open);

				// *** XMLファイルを読み込み、逆シリアル化（復元）する *** //
				ret = (ReversiSetting)serializer.Deserialize(fsr);
				fsr.Close();
			}
			catch (Exception ex)
			{
				System.Console.WriteLine("LoadSettingXml() : " + ex.Message);
				System.Console.WriteLine("LoadSettingXml() : " + ex.StackTrace);
				ret = null;
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			設定XMLファイルセーブ
		///	@fn				int SaveSettingXml(string path,ReversiSetting appSet)
		///	@param[in]		string path			設定XMLファイルパス
		///	@param[out]		ReversiSetting appSet	設定XMLファイルオブジェクト
		///	@return			0 : 成功 それ以外 : 失敗
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int SaveSettingXml(string path,ref ReversiSetting appSet)
		{
			int ret = 0;
			try
			{
				// *** XMLをReversiSettingオブジェクトに読み込む *** //
				XmlSerializer serializer = new XmlSerializer(typeof(ReversiSetting));

				// *** カレントディレクトリに"AppSetting.xml"というファイルで書き出す *** //
				FileStream fsw = new FileStream(path, FileMode.Create);

				// *** オブジェクトをシリアル化してXMLファイルに書き込む *** //
				serializer.Serialize(fsw, appSet);
				fsw.Close();
			}
			catch (Exception exl)
			{
				System.Console.WriteLine("SaveSettingXml() : " + exl.Message);
				System.Console.WriteLine("SaveSettingXml() : " + exl.StackTrace);
				ret = -1;
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			アプリ初期化
		///	@fn				void appInit()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void appInit()
		{
			// *** tableLayoutPanelの最適化 *** //
			System.Windows.Size formSize = new System.Windows.Size();
			formSize.Width = this.ActualWidth;
			formSize.Height = this.ActualHeight;
			System.Windows.Size tblSize = new System.Windows.Size();
			tblSize.Width = this.grid_reversi_fields.ActualWidth;
			tblSize.Height = this.grid_reversi_fields.ActualHeight;
/*
			int startX = 45;
			int startY = 45;
            // *** 各種オフセットを設定 *** //
            System.Windows.Point pt1 = this.PointToScreen(new System.Windows.Point(0.0d, 0.0d));
            System.Windows.Point pt2 = this.label_sts1.PointToScreen(new System.Windows.Point(0.0d, 0.0d));
            formSize.Height  = (int)pt2.Y - pt1.Y;
			formSize.Height -= startX << 1;
			formSize.Width  -= startY << 1;
			int refSize = (int)formSize.Height;
			if (formSize.Width < refSize) refSize = (int)formSize.Width;
			double tmpD = (double)refSize / this.m_AppSettings.mMasuCnt;
			refSize = (int)Math.Ceiling(tmpD);
			refSize *= this.m_AppSettings.mMasuCnt;

//			this.grid_reversi_fields.Top = startX;
//			this.grid_reversi_fields.Left = ( ( (int)formSize.Width + ( startY << 1 ) ) - refSize ) >> 1;
			Thickness tk = new Thickness(startX, ( ( (int)formSize.Width + ( startY << 1 ) ) - refSize ) >> 1, refSize, refSize); 
			this.grid_reversi_fields.Margin = tk;
			tblSize.Height = refSize;
			tblSize.Width = refSize;
			this.grid_reversi_fields.Height = refSize;
			this.grid_reversi_fields.Width = refSize;
*/
			System.Windows.Size curSize = tblSize;
			float cellSizeAll = (float)curSize.Height;
			if (curSize.Width < cellSizeAll) cellSizeAll = (float)curSize.Width;
			float cellSize = cellSizeAll / (float)this.m_AppSettings.mMasuCnt;
			float per = cellSize / cellSizeAll * 100F;
			this.grid_reversi_fields.Visibility = Visibility.Collapsed;
			for (int i = 0; i < ReversiConst.DEF_MASU_CNT_MAX_VAL;i++)
			{
				for (int j = 0; j < ReversiConst.DEF_MASU_CNT_MAX_VAL;j++)
				{
					UIElement c = (UIElement)this.grid_reversi_fields.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j);
					if (c != null)
					{
						if( i < this.m_AppSettings.mMasuCnt && j < this.m_AppSettings.mMasuCnt)
						{
							c.Visibility = Visibility.Visible;
						}
						else
						{
							c.Visibility = Visibility.Collapsed;
						}
					}
					// *** テーブルの列サイズを調整 *** //
					if(j < this.m_AppSettings.mMasuCnt)
					{
						this.grid_reversi_fields.ColumnDefinitions[j].Width = new GridLength(2.0, GridUnitType.Star);
					}
					else
					{
						this.grid_reversi_fields.ColumnDefinitions[j].Width = new GridLength(0.0, GridUnitType.Star);
					}
				}
				// *** テーブルの行サイズを調整 *** //
				if(i < this.m_AppSettings.mMasuCnt)
				{
					this.grid_reversi_fields.RowDefinitions[i].Height = new GridLength(2.0, GridUnitType.Star);
				}
				else
				{
					this.grid_reversi_fields.RowDefinitions[i].Height = new GridLength(0.0, GridUnitType.Star);
				}
			}
			this.grid_reversi_fields.Visibility = Visibility.Visible;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			メッセージダイアログ
		///	@fn				void ViewMsgDlg(string title , string msg)
		///	@param[in]		string title	タイトル
		///	@param[in]		string msg		メッセージ
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void ViewMsgDlg(string title , string msg)
		{
            Dispatcher.Invoke(new ViewMsgDlgDelegate(ViewMsgDlgLocal), title, msg);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			メッセージダイアログ
		///	@fn				void ViewMsgDlgLocal(string title , string msg)
		///	@param[in]		string title	タイトル
		///	@param[in]		string msg		メッセージ
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void ViewMsgDlgLocal(string title , string msg)
		{
			MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Information);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			1マス描画
		///	@fn				void DrawSingle(int y, int x, int sts, int bk, string text)
		///	@param[in]		int y		Y座標
		///	@param[in]		int x		X座標
		///	@param[in]		int sts		ステータス
		///	@param[in]		int bk		背景
		///	@param[in]		string text	テキスト
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void DrawSingle(int y, int x, int sts, int bk, string text)
		{
            Dispatcher.Invoke(new DrawSingleDelegate(DrawSingleLocal), y, x, sts, bk, text);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			1マス描画
		///	@fn				void DrawSingleLocal(int y, int x, int sts, int bk, string text)
		///	@param[in]		int y		Y座標
		///	@param[in]		int x		X座標
		///	@param[in]		int sts		ステータス
		///	@param[in]		int bk		背景
		///	@param[in]		string text	テキスト
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void DrawSingleLocal(int y, int x, int sts, int bk, string text)
		{
			Canvas curPict = (Canvas) this.grid_reversi_fields.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == y && Grid.GetColumn(e) == x);
			if(curPict != null && !Double.IsNaN(curPict.ActualWidth) && !Double.IsNaN(curPict.ActualHeight))
			{
				// 描画先とするImageオブジェクトを作成する
				Bitmap canvas = new Bitmap((int)curPict.ActualWidth, (int)curPict.ActualHeight);
				// ImageオブジェクトのGraphicsオブジェクトを作成する
				Graphics g = Graphics.FromImage(canvas);
				g.SmoothingMode = SmoothingMode.HighQuality;
                System.Drawing.Pen curPen1 = new System.Drawing.Pen(m_AppSettings.mBorderColor,2);
				// Brushオブジェクトの作成
				SolidBrush curBru1 = null;
				SolidBrush curBru2 = null;
				SolidBrush curBru3 = null;
                System.Drawing.Color curBkColor = m_AppSettings.mBackGroundColor;
				byte tmpA = curBkColor.A;
				byte tmpR = curBkColor.R;
				byte tmpG = curBkColor.G;
				byte tmpB = curBkColor.B;
                System.Drawing.Color curBkColorRev;

				if (bk == 1) {
					// *** cell_back_blue *** //
					tmpG -= 127;
					if (tmpG < 0) tmpG += 255;
					tmpB += 255;
					if (255 < tmpB) tmpB -= 255;
					curBkColor = System.Drawing.Color.FromArgb(tmpA, tmpR, tmpG, tmpB);
				}
				else if (bk == 2) {
					// *** cell_back_red *** //
					tmpR += 255;
					if(255 < tmpR) tmpR -= 255;
					tmpG -= 127;
					if(tmpG < 0) tmpG += 255;
					tmpB -= 127;
					if(tmpB < 0) tmpB += 255;
					curBkColor = System.Drawing.Color.FromArgb(tmpA, tmpR, tmpG, tmpB);
			    } else if (bk == 3) {
					// *** cell_back_magenta *** //
					tmpR += 255;
					if(255 < tmpR) tmpR -= 255;
					tmpB += 255;
					if(255 < tmpB) tmpB -= 255;
					curBkColor = System.Drawing.Color.FromArgb(tmpA, tmpR, tmpG, tmpB);
			    } else {
					// *** cell_back_green *** //
					curBkColor = System.Drawing.Color.FromArgb(tmpA, tmpR, tmpG, tmpB);
			    }
				curBru1 = new SolidBrush(curBkColor);
				HslColor workCol = HslColor.FromRgb(curBkColor);
				float h = workCol.H + 180F;
				if (359F < h) h -= 360F;
				curBkColorRev = HslColor.ToRgb(new HslColor(h, workCol.S, workCol.L));
				curBru3 = new SolidBrush(curBkColorRev);

				if (sts == ReversiConst.REVERSI_STS_NONE)
				{
				}
				else if (sts == ReversiConst.REVERSI_STS_BLACK)
				{
					curBru2 = new SolidBrush(m_AppSettings.mPlayerColor1);
				}
				else if (sts == ReversiConst.REVERSI_STS_WHITE)
				{
					curBru2 = new SolidBrush(m_AppSettings.mPlayerColor2);
				}

				// 位置(x, y)にActualWidth x ActualHeightの四角を描く
				g.FillRectangle(curBru1, 0, 0, (float)curPict.ActualWidth, (float)curPict.ActualHeight);
				// 位置(x, y)にActualWidth x ActualHeightの四角を描く
				g.DrawRectangle(curPen1, 0, 0, (float)curPict.ActualWidth, (float)curPict.ActualHeight);
				// 先に描いた四角に内接する楕円を黒で描く
				if(curBru2 != null) g.FillEllipse(curBru2, 2, 2, (float)curPict.ActualWidth - 4, (float)curPict.ActualHeight - 4);

				if (text != null && text.Length != 0 && text != "0")
				{
					// フォントオブジェクトの作成
					int fntSize = (int)curPict.ActualWidth;
					if(curPict.ActualHeight < fntSize) fntSize = (int)curPict.ActualHeight;
					fntSize = (int)((double)fntSize * 0.75);
					fntSize /= text.Length;
					if (fntSize < 8) fntSize = 8;
					Font fnt = new Font("MS UI Gothic", fntSize);
                    System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(0, 0, (int)curPict.ActualWidth, (int)curPict.ActualHeight);
					StringFormat stringFormat = new StringFormat();
					stringFormat.Alignment = StringAlignment.Center;
					stringFormat.LineAlignment = StringAlignment.Center;
					g.DrawString(text, fnt, curBru3, rect1, stringFormat);
					//リソースを解放する
					fnt.Dispose();
				}
				// リソースを解放する
				g.Dispose();
				// curPictに表示する
				System.Windows.Controls.Image img = new System.Windows.Controls.Image();
				img.Source = this.ToImageSource(canvas);
				img.Width = canvas.Width;
				img.Height = canvas.Height;
				Canvas.SetLeft(img, 0);
				Canvas.SetTop(img, 0);
				curPict.Children.Add(img);
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			現在の色メッセージ
		///	@fn				void CurColMsg(string text)
		///	@param[in]		string text	テキスト
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void CurColMsg(string text)
		{
            Dispatcher.Invoke(new CurColMsgDelegate(CurColMsgLocal), text);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			現在の色メッセージ
		///	@fn				void CurColMsgLocal(string text)
		///	@param[in]		string text	テキスト
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void CurColMsgLocal(string text)
		{
			this.label_sts1.Content = text;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			現在のステータスメッセージ
		///	@fn				void CurStsMsg(string text)
		///	@param[in]		string text	テキスト
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void CurStsMsg(string text)
		{
            Dispatcher.Invoke(new CurStsMsgDelegate(CurStsMsgLocal), text);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			現在のステータスメッセージ
		///	@fn				void CurStsMsgLocal(string text)
		///	@param[in]		string text	テキスト
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void CurStsMsgLocal(string text)
		{
			this.label_sts2.Content = text;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			マスクリック
		///	@fn				void pictureBox_Click(object sender, MouseButtonEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		MouseButtonEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void pictureBox_Click(object sender, MouseButtonEventArgs e)
		{

			int Column = Grid.GetColumn((UIElement)sender);
			int Row = Grid.GetRow((UIElement)sender);

			Console.WriteLine("click y=" + Row + " x=" + Column);

			Task newTask = new Task( () => { this.m_ReversiPlay.reversiPlay(Row, Column); } );
			newTask.Start();

		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リセットクリック
		///	@fn				void btn_reset_Click(object sender, RoutedEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		RoutedEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void btn_reset_Click(object sender, RoutedEventArgs e)
		{
			this.appInit();
			Task newTask = new Task( () => { this.m_ReversiPlay.reset(); } );
			newTask.Start();
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			セッティングクリック
		///	@fn				void button2_Click(object sender, RoutedEventArgs e)
		///	@param[in]		object sender
		///	@param[in]		RoutedEventArgs e
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void btn_setting_Click(object sender, RoutedEventArgs e)
		{
			System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Y.Y Magic\\ReversiWpf");
			string setPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Y.Y Magic\\ReversiWpf\\" + "AppSetting.xml";

			SettingWindow form = new SettingWindow(this.m_AppSettings);
			form.ShowDialog();

			this.m_AppSettings = form.mSetting;
			SaveSettingXml(setPath,ref this.m_AppSettings);

			// *** フォームが必要なくなったところで、Disposeを呼び出す *** //
			this.m_ReversiPlay.mSetting = this.m_AppSettings;
			this.appInit();
			Task newTask = new Task( () => { this.m_ReversiPlay.reset(); } );
			newTask.Start();
		}

/*
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //using System.Runtime.InteropServices;
        const double fixedRate = (double)800 / 700;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            IntPtr handle = (new WindowInteropHelper(this)).Handle;
            HwndSource hwndSource =
                            (HwndSource)HwndSource.FromVisual(this);
            hwndSource.AddHook(WndHookProc);
        }

        const int WM_SIZING = 0x214;
        const int WMSZ_LEFT = 1;
        const int WMSZ_RIGHT = 2;
        const int WMSZ_TOP = 3;
        const int WMSZ_TOPLEFT = 4;
        const int WMSZ_TOPRIGHT = 5;
        const int WMSZ_BOTTOM = 6;
        const int WMSZ_BOTTOMLEFT = 7;
        const int WMSZ_BOTTOMRIGHT = 8;


        private IntPtr WndHookProc(
            IntPtr hwnd, int msg, IntPtr wParam,
            IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SIZING)
            {
                RECT r = (RECT)Marshal.PtrToStructure(
                                          lParam, typeof(RECT));
                RECT recCopy = r;
                int w = r.right - r.left;
                int h = r.bottom - r.top;
                int dw;
                int dh;
                dw = (int)(h * fixedRate + 0.5) - w;
                dh = (int)(w / fixedRate + 0.5) - h;

                switch (wParam.ToInt32())
                {
                    case WMSZ_TOP:
                    case WMSZ_BOTTOM:
                        r.right += dw;
                        break;
                    case WMSZ_LEFT:
                    case WMSZ_RIGHT:
                        r.bottom += dh;
                        break;
                    case WMSZ_TOPLEFT:
                        if (dw > 0) r.left -= dw;
                        else r.top -= dh;
                        break;
                    case WMSZ_TOPRIGHT:
                        if (dw > 0) r.right += dw;
                        else r.top -= dh;
                        break;
                    case WMSZ_BOTTOMLEFT:
                        if (dw > 0) r.left -= dw;
                        else r.bottom += dh;
                        break;
                    case WMSZ_BOTTOMRIGHT:
                        if (dw > 0) r.right += dw;
                        else r.bottom += dh;
                        break;
                }
                Marshal.StructureToPtr(r, lParam, false);
            }
            return IntPtr.Zero;
        }
*/
    }
}
