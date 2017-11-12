////////////////////////////////////////////////////////////////////////////////
///	@file			ReversiAnz.cs
///	@brief			リバーシ解析クラス実装ファイル
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
	///	@class		ReversiAnz
	///	@brief		リバーシ解析クラス
	///
	////////////////////////////////////////////////////////////////////////////////
	public class ReversiAnz
	{
		#region メンバ変数
		private int _min;													//!< 最小値
		private int _max;													//!< 最大値
		private double _avg;												//!< 平均
		private int _pointCnt;												//!< 置けるポイント数
		private int _edgeCnt;												//!< 角を取れるポイント数
		private int _edgeSideOneCnt;										//!< 角一つ前を取れるポイント数
		private int _edgeSideTwoCnt;										//!< 角二つ前を取れるポイント数
		private int _edgeSideThreeCnt;										//!< 角三つ前を取れるポイント数
		private int _edgeSideOtherCnt;										//!< それ以外を取れるポイント数
		private int _ownMin;												//!< 最小値
		private int _ownMax;												//!< 最大値
		private double _ownAvg;												//!< 平均
		private int _ownPointCnt;											//!< 置けるポイント数
		private int _ownEdgeCnt;											//!< 角を取れるポイント数
		private int _ownEdgeSideOneCnt;										//!< 角一つ前を取れるポイント数
		private int _ownEdgeSideTwoCnt;										//!< 角二つ前を取れるポイント数
		private int _ownEdgeSideThreeCnt;									//!< 角三つ前を取れるポイント数
		private int _ownEdgeSideOtherCnt;									//!< それ以外を取れるポイント数
		private int _badPoint;												//!< 悪手ポイント
		private int _goodPoint;												//!< 良手ポイント
		#endregion

		#region プロパティ
		public int min
		{
			get { return _min; }
			set { _min = value; }
		}
		public int max
		{
			get { return _max; }
			set { _max = value; }
		}
		public double avg
		{
			get { return _avg; }
			set { _avg = value; }
		}
		public int pointCnt
		{
			get { return _pointCnt; }
			set { _pointCnt = value; }
		}
		public int edgeCnt
		{
			get { return _edgeCnt; }
			set { _edgeCnt = value; }
		}
		public int edgeSideOneCnt
		{
			get { return _edgeSideOneCnt; }
			set { _edgeSideOneCnt = value; }
		}
		public int edgeSideTwoCnt
		{
			get { return _edgeSideTwoCnt; }
			set { _edgeSideTwoCnt = value; }
		}
		public int edgeSideThreeCnt
		{
			get { return _edgeSideThreeCnt; }
			set { _edgeSideThreeCnt = value; }
		}
		public int edgeSideOtherCnt
		{
			get { return _edgeSideOtherCnt; }
			set { _edgeSideOtherCnt = value; }
		}
		public int ownMin
		{
			get { return _ownMin; }
			set { _ownMin = value; }
		}
		public int ownMax
		{
			get { return _ownMax; }
			set { _ownMax = value; }
		}
		public double ownAvg
		{
			get { return _ownAvg; }
			set { _ownAvg = value; }
		}
		public int ownPointCnt
		{
			get { return _ownPointCnt; }
			set { _ownPointCnt = value; }
		}
		public int ownEdgeCnt
		{
			get { return _ownEdgeCnt; }
			set { _ownEdgeCnt = value; }
		}
		public int ownEdgeSideOneCnt
		{
			get { return _ownEdgeSideOneCnt; }
			set { _ownEdgeSideOneCnt = value; }
		}
		public int ownEdgeSideTwoCnt
		{
			get { return _ownEdgeSideTwoCnt; }
			set { _ownEdgeSideTwoCnt = value; }
		}
		public int ownEdgeSideThreeCnt
		{
			get { return _ownEdgeSideThreeCnt; }
			set { _ownEdgeSideThreeCnt = value; }
		}
		public int ownEdgeSideOtherCnt
		{
			get { return _ownEdgeSideOtherCnt; }
			set { _ownEdgeSideOtherCnt = value; }
		}
		public int badPoint
		{
			get { return _badPoint; }
			set { _badPoint = value; }
		}
		public int goodPoint
		{
			get { return _goodPoint; }
			set { _goodPoint = value; }
		}
		#endregion

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コンストラクタ
		///	@fn				ReversiAnz()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiAnz()
		{
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コピー
		///	@fn				ReversiAnz Clone()
		///	@return			オブジェクトコピー
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiAnz Clone()
		{
			return (ReversiAnz)MemberwiseClone();
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リセット
		///	@fn				public void reset()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void reset()
		{
			this.min					= 0;
			this.max					= 0;
			this.avg					= 0.0;
			this.pointCnt				= 0;
			this.edgeCnt				= 0;
			this.edgeSideOneCnt			= 0;
			this.edgeSideTwoCnt			= 0;
			this.edgeSideThreeCnt		= 0;
			this.edgeSideOtherCnt		= 0;
			this.ownMin					= 0;
			this.ownMax					= 0;
			this.ownAvg					= 0.0;
			this.ownPointCnt			= 0;
			this.ownEdgeCnt				= 0;
			this.ownEdgeSideOneCnt		= 0;
			this.ownEdgeSideTwoCnt		= 0;
			this.ownEdgeSideThreeCnt	= 0;
			this.ownEdgeSideOtherCnt	= 0;
			this.badPoint				= 0;
			this.goodPoint				= 0;
		}
	}
}
