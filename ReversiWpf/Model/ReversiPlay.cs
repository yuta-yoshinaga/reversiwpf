////////////////////////////////////////////////////////////////////////////////
///	@file			ReversiPlay.cs
///	@brief			リバーシプレイクラス実装ファイル
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
	///	@class		ReversiPlay
	///	@brief		リバーシプレイクラス
	///
	////////////////////////////////////////////////////////////////////////////////
	public class ReversiPlay
	{
		#region デリゲート
		public delegate void ViewMsgDlg(string title , string msg);
		public delegate void DrawSingle(int y, int x, int sts, int bk, string text);
		public delegate void CurColMsg(string text);
		public delegate void CurStsMsg(string text);
		#endregion

		#region メンバ変数
		private MyReversi _mReversi;							//!< リバーシクラス
		private ReversiSetting _mSetting;						//!< リバーシ設定クラス
		private int _mCurColor = 0;								//!< 現在の色
		private ReversiPoint[] _mCpu;							//!< CPU用ワーク
		private ReversiPoint[] _mEdge;							//!< CPU用角マスワーク
		private int _mPassEnaB = 0;								//!< 黒のパス有効フラグ
		private int _mPassEnaW = 0;								//!< 白のパス有効フラグ
		private int _mGameEndSts = 0;							//!< ゲーム終了ステータス
		private int _mPlayLock = 0;								//!< プレイロック
		private ViewMsgDlg _viewMsgDlg = null;					//!< メッセージコールバック
		private DrawSingle _drawSingle = null;					//!< 描画コールバック
		private CurColMsg _curColMsg = null;					//!< 現在の色メッセージコールバック
		private CurStsMsg _curStsMsg = null;					//!< 現在のステータスメッセージコールバック
		private System.Random r = new System.Random();			//!< 乱数
		#endregion

		#region プロパティ
		public MyReversi mReversi
		{
			get { return _mReversi; }
			set { _mReversi = value; }
		}
		public ReversiSetting mSetting
		{
			get { return _mSetting; }
			set { _mSetting = value; }
		}
		public int mCurColor
		{
			get { return _mCurColor; }
			set { _mCurColor = value; }
		}
		public ReversiPoint[] mCpu
		{
			get { return _mCpu; }
			set { _mCpu = value; }
		}
		public ReversiPoint[] mEdge
		{
			get { return _mEdge; }
			set { _mEdge = value; }
		}
		public int mPassEnaB
		{
			get { return _mPassEnaB; }
			set { _mPassEnaB = value; }
		}
		public int mPassEnaW
		{
			get { return _mPassEnaW; }
			set { _mPassEnaW = value; }
		}
		public int mGameEndSts
		{
			get { return _mGameEndSts; }
			set { _mGameEndSts = value; }
		}
		public int mPlayLock
		{
			get { return _mPlayLock; }
			set { _mPlayLock = value; }
		}
		public ViewMsgDlg viewMsgDlg
		{
			get { return _viewMsgDlg; }
			set { _viewMsgDlg = value; }
		}
		public DrawSingle drawSingle
		{
			get { return _drawSingle; }
			set { _drawSingle = value; }
		}
		public CurColMsg curColMsg
		{
			get { return _curColMsg; }
			set { _curColMsg = value; }
		}
		public CurStsMsg curStsMsg
		{
			get { return _curStsMsg; }
			set { _curStsMsg = value; }
		}
		#endregion

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コンストラクタ
		///	@fn				ReversiPlay()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiPlay()
		{
			this.mReversi	= new MyReversi(ReversiConst.DEF_MASU_CNT_MAX_VAL, ReversiConst.DEF_MASU_CNT_MAX_VAL);
			this.mSetting	= new ReversiSetting();
			this.mCpu		= new ReversiPoint[ReversiConst.DEF_MASU_CNT_MAX_VAL * ReversiConst.DEF_MASU_CNT_MAX_VAL];
			this.mEdge		= new ReversiPoint[ReversiConst.DEF_MASU_CNT_MAX_VAL * ReversiConst.DEF_MASU_CNT_MAX_VAL];
			for (var i = 0; i < (ReversiConst.DEF_MASU_CNT_MAX_VAL * ReversiConst.DEF_MASU_CNT_MAX_VAL); i++) {
				this.mCpu[i]	= new ReversiPoint();
				this.mEdge[i]	= new ReversiPoint();
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			コピー
		///	@fn				ReversiPlay Clone()
		///	@return			オブジェクトコピー
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public ReversiPlay Clone()
		{
			return (ReversiPlay)MemberwiseClone();
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リバーシプレイ
		///	@fn				void reversiPlay(int y, int x)
		///	@param[in]		int y			Y座標
		///	@param[in]		int x			X座標
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void reversiPlay(int y, int x)
		{
			int update = 0;
			int cpuEna = 0;
			int tmpCol = this.mCurColor;
			int pass = 0;

			if(this.mPlayLock == 1) return;
			this.mPlayLock = 1;
			if (this.mReversi.getColorEna(this.mCurColor) == 0) {
				if (this.mReversi.setMasuSts(this.mCurColor, y, x) == 0) {
					if (this.mSetting.mType == ReversiConst.DEF_TYPE_HARD) this.mReversi.AnalysisReversi(this.mPassEnaB, this.mPassEnaW);
					if (this.mSetting.mAssist == ReversiConst.DEF_ASSIST_ON) {
						// *** メッセージ送信 *** //
						this.execMessage(ReversiConst.LC_MSG_ERASE_INFO_ALL, null);
					}
					this.sendDrawMsg(y, x);												// 描画
					this.drawUpdate(ReversiConst.DEF_ASSIST_OFF);						// その他コマ描画
					if (this.mReversi.getGameEndSts() == 0) {
						if (tmpCol == ReversiConst.REVERSI_STS_BLACK) tmpCol = ReversiConst.REVERSI_STS_WHITE;
						else tmpCol = ReversiConst.REVERSI_STS_BLACK;
						if (this.mReversi.getColorEna(tmpCol) == 0) {
							if (this.mSetting.mMode == ReversiConst.DEF_MODE_ONE) {		// CPU対戦
								cpuEna = 1;
							} else {													// 二人対戦
								this.mCurColor = tmpCol;
								this.drawUpdate(this.mSetting.mAssist);					// 次のプレイヤーコマ描画
							}
						} else {
							// *** パスメッセージ *** //
							this.reversiPlayPass(tmpCol);
							pass = 1;
						}
					} else {
						// *** ゲーム終了メッセージ *** //
						this.reversiPlayEnd();
					}
					update = 1;
				} else {
					// *** エラーメッセージ *** //
					this.ViewMsgDlgLocal("エラー", "そのマスには置けません。");
				}
			} else {
				if (this.mReversi.getGameEndSts() == 0) {
					if (tmpCol == ReversiConst.REVERSI_STS_BLACK) tmpCol = ReversiConst.REVERSI_STS_WHITE;
					else tmpCol = ReversiConst.REVERSI_STS_BLACK;
					if (this.mReversi.getColorEna(tmpCol) == 0) {
						if (this.mSetting.mMode == ReversiConst.DEF_MODE_ONE) {			// CPU対戦
							update = 1;
							cpuEna = 1;
						} else {														// 二人対戦
							this.mCurColor = tmpCol;
						}
					} else {
						// *** パスメッセージ *** //
						this.reversiPlayPass(tmpCol);
						pass = 1;
					}
				} else {
					// *** ゲーム終了メッセージ *** //
					this.reversiPlayEnd();
				}
			}
			if (pass == 1) {
				if (this.mSetting.mMode == ReversiConst.DEF_MODE_ONE) {					// CPU対戦
					if (this.mSetting.mAssist == ReversiConst.DEF_ASSIST_ON) {
						// *** メッセージ送信 *** //
						this.execMessage(ReversiConst.LC_MSG_DRAW_INFO_ALL, null);
					}
				}
			}
			if (update == 1) {
				var waitTime = 0;
				if (cpuEna == 1) {
					waitTime = this.mSetting.mPlayCpuInterVal;
				}
				System.Threading.Thread.Sleep(waitTime);
				this.reversiPlaySub(cpuEna, tmpCol);
				this.mPlayLock = 0;
			}else{
				this.mPlayLock = 0;
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リバーシプレイサブ
		///	@fn				void reversiPlaySub(int cpuEna, int tmpCol)
		///	@param[in]		int cpuEna
		///	@param[in]		int tmpCol
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void reversiPlaySub(int cpuEna, int tmpCol)
		{
			int ret;
			for (; ;) {
				ret = this.reversiPlayCpu(tmpCol, cpuEna);
				cpuEna = 0;
				if (ret == 1) {
					if (this.mReversi.getGameEndSts() == 0) {
						if (this.mReversi.getColorEna(this.mCurColor) != 0) {
							// *** パスメッセージ *** //
							this.reversiPlayPass(this.mCurColor);
							cpuEna = 1;
						}
					} else {
						// *** ゲーム終了メッセージ *** //
						this.reversiPlayEnd();
					}
				}
				if (cpuEna == 0) break;
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リバーシプレイ終了
		///	@fn				void reversiPlayEnd()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void reversiPlayEnd()
		{
			if (this.mGameEndSts == 0) {
				this.mGameEndSts = 1;
				var waitTime = this.gameEndAnimExec();					// 終了アニメ実行
				this.mPlayLock = 1;		
				System.Threading.Thread.Sleep(waitTime);
				// *** ゲーム終了メッセージ *** //
				string tmpMsg1, tmpMsg2, msgStr;
				int blk, whi;
				blk = this.mReversi.getBetCnt(ReversiConst.REVERSI_STS_BLACK);
				whi = this.mReversi.getBetCnt(ReversiConst.REVERSI_STS_WHITE);
				tmpMsg1 = "プレイヤー1 = " + blk.ToString() + " プレイヤー2 = " + whi.ToString();
				if (this.mSetting.mMode == ReversiConst.DEF_MODE_ONE) {
					if (whi == blk) tmpMsg2 = "引き分けです。";
					else if (whi < blk) {
						if (this.mCurColor == ReversiConst.REVERSI_STS_BLACK) tmpMsg2 = "あなたの勝ちです。";
						else tmpMsg2 = "あなたの負けです。";
					} else {
						if (this.mCurColor == ReversiConst.REVERSI_STS_WHITE) tmpMsg2 = "あなたの勝ちです。";
						else tmpMsg2 = "あなたの負けです。";
					}
				} else {
					if (whi == blk) tmpMsg2 = "引き分けです。";
					else if (whi < blk) tmpMsg2 = "プレイヤー1の勝ちです。";
					else tmpMsg2 = "プレイヤー2の勝ちです。";
				}
				msgStr = tmpMsg1 + tmpMsg2;
				this.ViewMsgDlgLocal("ゲーム終了", msgStr);

				if (this.mSetting.mEndAnim == ReversiConst.DEF_END_ANIM_ON) {
					// *** メッセージ送信 *** //
					this.execMessage(ReversiConst.LC_MSG_CUR_COL, null);
					// *** メッセージ送信 *** //
					this.execMessage(ReversiConst.LC_MSG_CUR_STS, null);
				}
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リバーシプレイパス
		///	@fn				void reversiPlayPass(int color)
		///	@param[in]		int color		パス色
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void reversiPlayPass(int color)
		{
			// *** パスメッセージ *** //
			if (this.mSetting.mMode == ReversiConst.DEF_MODE_ONE) {
				if (color == this.mCurColor) this.ViewMsgDlgLocal("", "あなたはパスです。");
				else this.ViewMsgDlgLocal("", "CPUはパスです。");
			} else {
				if (color == ReversiConst.REVERSI_STS_BLACK) this.ViewMsgDlgLocal("", "プレイヤー1はパスです。");
				else this.ViewMsgDlgLocal("", "プレイヤー2はパスです。");
			}
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リバーシプレイコンピューター
		///	@fn				int reversiPlayCpu(int color, int cpuEna)
		///	@param[in]		int color		CPU色
		///	@param[in]		int cpuEna		CPU有効フラグ
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int reversiPlayCpu(int color, int cpuEna)
		{
			int update = 0;
			int setY;
			int setX;

			for (; ;) {
				if (cpuEna == 1) {
					cpuEna = 0;
					// *** CPU対戦 *** //
					int pCnt = this.mReversi.getPointCnt(color);
					ReversiPoint pInfo = this.mReversi.getPoint(color, r.Next(pCnt));
					if (pInfo != null) {
						setY = pInfo.y;
						setX = pInfo.x;
						if (this.mSetting.mType != ReversiConst.DEF_TYPE_EASY) {	// 強いコンピューター
							int cpuflg0, cpuflg1, cpuflg2, cpuflg3, mem, mem2, mem3, mem4, rcnt1, rcnt2, kadocnt, loop, pcnt, passCnt, othColor, othBet, ownBet, endZone;
							cpuflg0 = 0;
							cpuflg1 = 0;
							cpuflg2 = 0;
							cpuflg3 = 0;
							mem = -1;
							mem2 = -1;
							mem3 = -1;
							mem4 = -1;
							rcnt1 = 0;
							rcnt2 = 0;
							kadocnt = 0;
							loop = this.mSetting.mMasuCnt * this.mSetting.mMasuCnt;
							pcnt = 0;
							passCnt = 0;
							if (color == ReversiConst.REVERSI_STS_BLACK) othColor = ReversiConst.REVERSI_STS_WHITE;
							else othColor = ReversiConst.REVERSI_STS_BLACK;
							othBet = this.mReversi.getBetCnt(othColor);				// 対戦相手のコマ数
							ownBet = this.mReversi.getBetCnt(color);				// 自分のコマ数
							endZone = 0;
							if ((loop - (othBet + ownBet)) <= 16) endZone = 1;		// ゲーム終盤フラグON
							for (var i = 0; i < loop; i++) {
								this.mCpu[i].x = 0;
								this.mCpu[i].y = 0;
								this.mEdge[i].x = 0;
								this.mEdge[i].y = 0;
							}

							for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
								for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
									if (this.mReversi.getMasuStsEna(color, i, j) != 0) {
										// *** 角の一つ手前なら別のとこに格納 *** //
										if (this.mReversi.getEdgeSideOne(i, j) == 0) {
											this.mEdge[kadocnt].x = j;
											this.mEdge[kadocnt].y = i;
											kadocnt++;
										} else {
											this.mCpu[rcnt1].x = j;
											this.mCpu[rcnt1].y = i;
											rcnt1++;
										}
										if (this.mSetting.mType == ReversiConst.DEF_TYPE_NOR) {
											// *** 角に置けるなら優先的にとらせるため場所を記憶させる *** //
											if (this.mReversi.getEdgeSideZero(i, j) == 0) {
												cpuflg1 = 1;
												rcnt2 = (rcnt1 - 1);
											}
											// *** 角の二つ手前も優先的にとらせるため場所を記憶させる *** //
											if (cpuflg1 == 0) {
												if (this.mReversi.getEdgeSideTwo(i, j) == 0) {
													cpuflg2 = 1;
													rcnt2 = (rcnt1 - 1);
												}
											}
											// *** 角の三つ手前も優先的にとらせるため場所を記憶させる *** //
											if (cpuflg1 == 0 && cpuflg2 == 0) {
												if (this.mReversi.getEdgeSideThree(i, j) == 0) {
													cpuflg0 = 1;
													rcnt2 = (rcnt1 - 1);
												}
											}
										}
										// *** パーフェクトゲームなら *** //
										if (this.mReversi.getMasuStsCnt(color, i, j) == othBet) {
											setY = i;
											setX = j;
											pcnt = 1;
										}
										// *** 相手をパスさせるなら *** //
										if (pcnt == 0) {
											if (this.mReversi.getPassEna(color, i, j) != 0) {
												setY = i;
												setX = j;
												passCnt = 1;
											}
										}
									}
								}
							}

							if (pcnt == 0 && passCnt == 0) {
								var badPoint = -1;
								var goodPoint = -1;
								var pointCnt = -1;
								var ownPointCnt = -1;
								ReversiAnz tmpAnz;
								if (rcnt1 != 0) {
									for (var i = 0; i < rcnt1; i++) {
										if (this.mSetting.mType == ReversiConst.DEF_TYPE_HARD) {
											tmpAnz = this.mReversi.getPointAnz(color, this.mCpu[i].y, this.mCpu[i].x);
											if (tmpAnz != null) {
												if (badPoint == -1) {
													badPoint = tmpAnz.badPoint;
													goodPoint = tmpAnz.goodPoint;
													pointCnt = tmpAnz.pointCnt;
													ownPointCnt = tmpAnz.ownPointCnt;
													mem2 = i;
													mem3 = i;
													mem4 = i;
												} else {
													if (tmpAnz.badPoint < badPoint) {
														badPoint = tmpAnz.badPoint;
														mem2 = i;
													}
													if (goodPoint < tmpAnz.goodPoint) {
														goodPoint = tmpAnz.goodPoint;
														mem3 = i;
													}
													if (tmpAnz.pointCnt < pointCnt) {
														pointCnt = tmpAnz.pointCnt;
														ownPointCnt = tmpAnz.ownPointCnt;
														mem4 = i;
													} else if (tmpAnz.pointCnt == pointCnt) {
														if (ownPointCnt < tmpAnz.ownPointCnt) {
															ownPointCnt = tmpAnz.ownPointCnt;
															mem4 = i;
														}
													}
												}
											}
										}
										if (this.mReversi.getMasuStsEna(color, this.mCpu[i].y, this.mCpu[i].x) == 2) {
											mem = i;
										}
									}
									if (mem2 != -1) {
										if (endZone != 0) {							// 終盤なら枚数重視
											if (mem3 != -1) {
												mem2 = mem3;
											}
										} else {
											if (mem4 != -1) {
												mem2 = mem4;
											}
										}
										mem = mem2;
									}
									if (mem == -1) mem = r.Next(rcnt1);
								} else if (kadocnt != 0) {
									for (var i = 0; i < kadocnt; i++) {
										if (this.mSetting.mType == ReversiConst.DEF_TYPE_HARD) {
											tmpAnz = this.mReversi.getPointAnz(color, this.mEdge[i].y, this.mEdge[i].x);
											if (tmpAnz != null) {
												if (badPoint == -1) {
													badPoint = tmpAnz.badPoint;
													goodPoint = tmpAnz.goodPoint;
													pointCnt = tmpAnz.pointCnt;
													ownPointCnt = tmpAnz.ownPointCnt;
													mem2 = i;
													mem3 = i;
													mem4 = i;
												} else {
													if (tmpAnz.badPoint < badPoint) {
														badPoint = tmpAnz.badPoint;
														mem2 = i;
													}
													if (goodPoint < tmpAnz.goodPoint) {
														goodPoint = tmpAnz.goodPoint;
														mem3 = i;
													}
													if (tmpAnz.pointCnt < pointCnt) {
														pointCnt = tmpAnz.pointCnt;
														ownPointCnt = tmpAnz.ownPointCnt;
														mem4 = i;
													} else if (tmpAnz.pointCnt == pointCnt) {
														if (ownPointCnt < tmpAnz.ownPointCnt) {
															ownPointCnt = tmpAnz.ownPointCnt;
															mem4 = i;
														}
													}
												}
											}
										}
										if (this.mReversi.getMasuStsEna(color, this.mEdge[i].y, this.mEdge[i].x) == 2) {
											mem = i;
										}
									}
									if (mem2 != -1) {
										if (endZone != 0) {							// 終盤なら枚数重視
											if (mem3 != -1) {
												mem2 = mem3;
											}
										} else {
											if (mem4 != -1) {
												mem2 = mem4;
											}
										}
										mem = mem2;
									}
									if (mem == -1) mem = r.Next(kadocnt);
									// *** 置いても平気な角があればそこに置く*** //
									for (var i = 0; i < kadocnt; i++) {
										if (this.mReversi.checkEdge(color, this.mEdge[i].y, this.mEdge[i].x) != 0) {
											if ((cpuflg0 == 0) && (cpuflg1 == 0) && (cpuflg2 == 0)) {
												cpuflg3 = 1;
												rcnt2 = i;
											}
										}
									}
								}
								if ((cpuflg1 == 0) && (cpuflg2 == 0) && (cpuflg0 == 0) && (cpuflg3 == 0)) {
									rcnt2 = mem;
								}
								if (rcnt1 != 0) {
									setY = this.mCpu[rcnt2].y;
									setX = this.mCpu[rcnt2].x;
								} else if (kadocnt != 0) {
									setY = this.mEdge[rcnt2].y;
									setX = this.mEdge[rcnt2].x;
								}
							}
						}

						if (this.mReversi.setMasuSts(color, setY, setX) == 0) {
							if (this.mSetting.mType == ReversiConst.DEF_TYPE_HARD) this.mReversi.AnalysisReversi(this.mPassEnaB, this.mPassEnaW);
							this.sendDrawMsg(setY, setX);							// 描画
							update = 1;
						}
					}
				} else {
					break;
				}
			}
			if (update == 1) {
				this.drawUpdate(ReversiConst.DEF_ASSIST_OFF);
				if (this.mSetting.mAssist == ReversiConst.DEF_ASSIST_ON) {
					// *** メッセージ送信 *** //
					this.execMessage(ReversiConst.LC_MSG_DRAW_INFO_ALL, null);
				}
			}

			return update;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			マス描画更新
		///	@fn				void drawUpdate(int assist)
		///	@param[in]		int assist	アシスト設定
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void drawUpdate(int assist)
		{
			if (assist == ReversiConst.DEF_ASSIST_ON) {
				for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
					for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
						this.sendDrawInfoMsg(i, j);
					}
				}
			}
			var waitTime = this.mSetting.mPlayDrawInterVal;
			for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
				for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
					if(this.mReversi.getMasuSts(i,j) != this.mReversi.getMasuStsOld(i,j)){
						System.Threading.Thread.Sleep(waitTime);
						this.sendDrawMsg(i, j);
					}
				}
			}
			// *** メッセージ送信 *** //
			this.execMessage(ReversiConst.LC_MSG_CUR_COL, null);
			// *** メッセージ送信 *** //
			this.execMessage(ReversiConst.LC_MSG_CUR_STS, null);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			マス描画強制更新
		///	@fn				void drawUpdateForcibly(int assist)
		///	@param[in]		int assist	アシスト設定
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void drawUpdateForcibly(int assist)
		{
			// *** メッセージ送信 *** //
			this.execMessage(ReversiConst.LC_MSG_DRAW_ALL, null);
			if (assist == ReversiConst.DEF_ASSIST_ON) {
				// *** メッセージ送信 *** //
				this.execMessage(ReversiConst.LC_MSG_DRAW_INFO_ALL, null);
			} else {
				// *** メッセージ送信 *** //
				this.execMessage(ReversiConst.LC_MSG_ERASE_INFO_ALL, null);
			}
			// *** メッセージ送信 *** //
			this.execMessage(ReversiConst.LC_MSG_CUR_COL, null);
			// *** メッセージ送信 *** //
			this.execMessage(ReversiConst.LC_MSG_CUR_STS, null);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			リセット処理
		///	@fn				void reset()
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void reset()
		{
			this.mPassEnaB = 0;
			this.mPassEnaW = 0;
			if (this.mSetting.mGameSpd == ReversiConst.DEF_GAME_SPD_FAST) {
				this.mSetting.mPlayDrawInterVal = ReversiConst.DEF_GAME_SPD_FAST_VAL;					// 描画のインターバル(msec)
				this.mSetting.mPlayCpuInterVal = ReversiConst.DEF_GAME_SPD_FAST_VAL2;					// CPU対戦時のインターバル(msec)
			} else if (this.mSetting.mGameSpd == ReversiConst.DEF_GAME_SPD_MID) {
				this.mSetting.mPlayDrawInterVal = ReversiConst.DEF_GAME_SPD_MID_VAL;					// 描画のインターバル(msec)
				this.mSetting.mPlayCpuInterVal = ReversiConst.DEF_GAME_SPD_MID_VAL2;					// CPU対戦時のインターバル(msec)
			} else {
				this.mSetting.mPlayDrawInterVal = ReversiConst.DEF_GAME_SPD_SLOW_VAL;					// 描画のインターバル(msec)
				this.mSetting.mPlayCpuInterVal = ReversiConst.DEF_GAME_SPD_SLOW_VAL2;					// CPU対戦時のインターバル(msec)
			}

			this.mCurColor = this.mSetting.mPlayer;
			if (this.mSetting.mMode == ReversiConst.DEF_MODE_TWO) this.mCurColor = ReversiConst.REVERSI_STS_BLACK;

			this.mReversi.setMasuCnt(this.mSetting.mMasuCnt);											// マスの数設定

			this.mReversi.reset();
			if (this.mSetting.mMode == ReversiConst.DEF_MODE_ONE) {
				if (this.mCurColor == ReversiConst.REVERSI_STS_WHITE) {
					var pCnt = this.mReversi.getPointCnt(ReversiConst.REVERSI_STS_BLACK);
					ReversiPoint pInfo = this.mReversi.getPoint(ReversiConst.REVERSI_STS_BLACK, r.Next(pCnt));
					if (pInfo != null) {
						this.mReversi.setMasuSts(ReversiConst.REVERSI_STS_BLACK, pInfo.y, pInfo.x);
						if (this.mSetting.mType == ReversiConst.DEF_TYPE_HARD) this.mReversi.AnalysisReversi(this.mPassEnaB, this.mPassEnaW);
					}
				}
			}

			this.mPlayLock = 1;
			this.mGameEndSts = 0;

			this.drawUpdateForcibly(this.mSetting.mAssist);

			// *** 終了通知 *** //
			// *** メッセージ送信 *** //
			this.execMessage(ReversiConst.LC_MSG_DRAW_END, null);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			ゲーム終了アニメーション
		///	@fn				int gameEndAnimExec()
		///	@return			ウェイト時間
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public int gameEndAnimExec()
		{
			int bCnt, wCnt;
			int ret = 0;

			if (this.mSetting.mEndAnim == ReversiConst.DEF_END_ANIM_ON) {
				bCnt = this.mReversi.getBetCnt(ReversiConst.REVERSI_STS_BLACK);
				wCnt = this.mReversi.getBetCnt(ReversiConst.REVERSI_STS_WHITE);

				// *** 色、コマ数表示消去 *** //
				// *** メッセージ送信 *** //
				this.execMessage(ReversiConst.LC_MSG_CUR_COL_ERASE, null);
				// *** メッセージ送信 *** //
				this.execMessage(ReversiConst.LC_MSG_CUR_STS_ERASE, null);

				System.Threading.Thread.Sleep(this.mSetting.mEndInterVal);
				// *** マス消去 *** //
				for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
					for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
						this.mReversi.setMasuStsForcibly(ReversiConst.REVERSI_STS_NONE, i, j);
					}
				}
				// *** メッセージ送信 *** //
				this.execMessage(ReversiConst.LC_MSG_ERASE_ALL, null);

				// *** マス描画 *** //
				int bCnt2, wCnt2, bEnd, wEnd;
				bCnt2 = 0;
				wCnt2 = 0;
				bEnd = 0;
				wEnd = 0;
				for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
					for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
						if (bCnt2 < bCnt) {
							bCnt2++;
							this.mReversi.setMasuStsForcibly(ReversiConst.REVERSI_STS_BLACK, i, j);
							this.sendDrawMsg(i, j);
						} else {
							bEnd = 1;
						}
						if (wCnt2 < wCnt) {
							wCnt2++;
							this.mReversi.setMasuStsForcibly(ReversiConst.REVERSI_STS_WHITE, (this.mSetting.mMasuCnt - 1) - i, (this.mSetting.mMasuCnt - 1) - j);
							this.sendDrawMsg((this.mSetting.mMasuCnt - 1) - i, (this.mSetting.mMasuCnt - 1) - j);
						} else {
							wEnd = 1;
						}
						if (bEnd == 1 && wEnd == 1) {
							break;
						}else{
							System.Threading.Thread.Sleep(this.mSetting.mEndDrawInterVal);
						}
					}
				}
				ret = 0;
			}
			return ret;
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			描画メッセージ送信
		///	@fn				void sendDrawMsg(int y, int x)
		///	@param[in]		int y			Y座標
		///	@param[in]		int x			X座標
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void sendDrawMsg(int y, int x)
		{
			ReversiPoint mTmpPoint = new ReversiPoint();
			mTmpPoint.y = y;
			mTmpPoint.x = x;
			// *** メッセージ送信 *** //
			this.execMessage(ReversiConst.LC_MSG_DRAW, mTmpPoint);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			情報描画メッセージ送信
		///	@fn				void sendDrawInfoMsg(int y, int x)
		///	@param[in]		int y			Y座標
		///	@param[in]		int x			X座標
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		public void sendDrawInfoMsg(int y, int x)
		{
			ReversiPoint mTmpPoint = new ReversiPoint();
			mTmpPoint.y = y;
			mTmpPoint.x = x;
			// *** メッセージ送信 *** //
			this.execMessage(ReversiConst.LC_MSG_DRAW_INFO, mTmpPoint);
		}

		////////////////////////////////////////////////////////////////////////////////
		///	@brief			メッセージ
		///	@fn				void execMessage(int what, var obj)
		///	@param[in]		int what
		///	@param[in]		var obj
		///	@return			ありません
		///	@author			Yuta Yoshinaga
		///	@date			2017.10.20
		///
		////////////////////////////////////////////////////////////////////////////////
		private void execMessage(int what, Object obj)
		{
			int dMode, dBack, dCnt;
			if (what == ReversiConst.LC_MSG_DRAW) {
				// *** マス描画 *** //
				ReversiPoint msgPoint = (ReversiPoint)obj;
				dMode = this.mReversi.getMasuSts(msgPoint.y, msgPoint.x);
				dBack = this.mReversi.getMasuStsEna(this.mCurColor, msgPoint.y, msgPoint.x);
				dCnt = this.mReversi.getMasuStsCnt(this.mCurColor, msgPoint.y, msgPoint.x);
				this.DrawSingleLocal(msgPoint.y, msgPoint.x, dMode, dBack, dCnt.ToString());
			} else if (what == ReversiConst.LC_MSG_ERASE) {
				// *** マス消去 *** //
				ReversiPoint msgPoint = (ReversiPoint)obj;
				this.DrawSingleLocal(msgPoint.y, msgPoint.x, 0, 0, "0");
			} else if (what == ReversiConst.LC_MSG_DRAW_INFO) {
				// *** マス情報描画 *** //
				ReversiPoint msgPoint = (ReversiPoint)obj;
				dMode = this.mReversi.getMasuSts(msgPoint.y, msgPoint.x);
				dBack = this.mReversi.getMasuStsEna(this.mCurColor, msgPoint.y, msgPoint.x);
				dCnt = this.mReversi.getMasuStsCnt(this.mCurColor, msgPoint.y, msgPoint.x);
				this.DrawSingleLocal(msgPoint.y, msgPoint.x, dMode, dBack, dCnt.ToString());
			} else if (what == ReversiConst.LC_MSG_ERASE_INFO) {
				// *** マス情報消去 *** //
				ReversiPoint msgPoint = (ReversiPoint)obj;
				dMode = this.mReversi.getMasuSts(msgPoint.y, msgPoint.x);
				this.DrawSingleLocal(msgPoint.y, msgPoint.x, dMode, 0, "0");
			} else if (what == ReversiConst.LC_MSG_DRAW_ALL) {
				// *** 全マス描画 *** //
				for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
					for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
						dMode = this.mReversi.getMasuSts(i, j);
						dBack = this.mReversi.getMasuStsEna(this.mCurColor, i, j);
						dCnt = this.mReversi.getMasuStsCnt(this.mCurColor, i, j);
						this.DrawSingleLocal(i, j, dMode, dBack, dCnt.ToString());
					}
				}
			} else if (what == ReversiConst.LC_MSG_ERASE_ALL) {
				// *** 全マス消去 *** //
				for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
					for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
						this.DrawSingleLocal(i, j, 0, 0, "0");
					}
				}
			} else if (what == ReversiConst.LC_MSG_DRAW_INFO_ALL) {
				// *** 全マス情報描画 *** //
				for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
					for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
						dMode = this.mReversi.getMasuSts(i, j);
						dBack = this.mReversi.getMasuStsEna(this.mCurColor, i, j);
						dCnt = this.mReversi.getMasuStsCnt(this.mCurColor, i, j);
						this.DrawSingleLocal(i, j, dMode, dBack, dCnt.ToString());
					}
				}
			} else if (what == ReversiConst.LC_MSG_ERASE_INFO_ALL) {
				// *** 全マス情報消去 *** //
				for (var i = 0; i < this.mSetting.mMasuCnt; i++) {
					for (var j = 0; j < this.mSetting.mMasuCnt; j++) {
						dMode = this.mReversi.getMasuSts(i, j);
						this.DrawSingleLocal(i, j, dMode, 0, "0");
					}
				}
			} else if (what == ReversiConst.LC_MSG_DRAW_END) {
				this.mPlayLock = 0;
			} else if (what == ReversiConst.LC_MSG_CUR_COL) {
				string tmpStr = "";
				if (this.mSetting.mMode == ReversiConst.DEF_MODE_ONE) {
					if (this.mCurColor == ReversiConst.REVERSI_STS_BLACK) tmpStr = "あなたはプレイヤー1です ";
					else tmpStr = "あなたはプレイヤー2です ";
				} else {
					if (this.mCurColor == ReversiConst.REVERSI_STS_BLACK) tmpStr = "プレイヤー1の番です ";
					else tmpStr = "プレイヤー2の番です ";
				}
				this.CurColMsgLocal(tmpStr);
			} else if (what == ReversiConst.LC_MSG_CUR_COL_ERASE) {
				this.CurColMsgLocal("");
			} else if (what == ReversiConst.LC_MSG_CUR_STS) {
				string tmpStr = "プレイヤー1 = " + this.mReversi.getBetCnt(ReversiConst.REVERSI_STS_BLACK) + " プレイヤー2 = " + this.mReversi.getBetCnt(ReversiConst.REVERSI_STS_WHITE);
				this.CurStsMsgLocal(tmpStr);
			} else if (what == ReversiConst.LC_MSG_CUR_STS_ERASE) {
				this.CurStsMsgLocal("");
			} else if (what == ReversiConst.LC_MSG_MSG_DLG) {
			} else if (what == ReversiConst.LC_MSG_DRAW_ALL_RESET) {
			}
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
		private void ViewMsgDlgLocal(string title , string msg)
		{
			if(this.viewMsgDlg != null) this.viewMsgDlg(title, msg);
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
		private void DrawSingleLocal(int y, int x, int sts, int bk, string text)
		{
			if(this.drawSingle != null) this.drawSingle(y, x, sts, bk, text);
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
		private void CurColMsgLocal(string text)
		{
			if(this.curColMsg != null) this.curColMsg(text);
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
		private void CurStsMsgLocal(string text)
		{
			if(this.curStsMsg != null) this.curStsMsg(text);
		}
	}
}
