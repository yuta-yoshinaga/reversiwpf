////////////////////////////////////////////////////////////////////////////////
///	@file			ReversiHistory.cs
///	@brief			リバーシ履歴クラス実装ファイル
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
	///	@class		ReversiHistory
	///	@brief		リバーシ履歴クラス
	///
	////////////////////////////////////////////////////////////////////////////////
	public class ReversiHistory
	{
		#region メンバ変数
		private ReversiPoint _point;					//!< ポイント
		private int _color;								//!< 色
		#endregion

		#region プロパティ
		public ReversiPoint point
		{
			get { return _point; }
			set { _point = value; }
		}
		public int color
		{
			get { return _color; }
			set { _color = value; }
		}
		#endregion

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コンストラクタ
		///	@fn				ReversiHistory()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiHistory()
		{
			this.point = new ReversiPoint();
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
			this.point.x	= -1;
			this.point.y	= -1;
			this.color		= -1;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コピー
		///	@fn				ReversiHistory Clone()
		///	@return			オブジェクトコピー
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiHistory Clone()
		{
			return (ReversiHistory)MemberwiseClone();
		}
	}
}
