////////////////////////////////////////////////////////////////////////////////
///	@file			HslColor.cs
///	@brief			HslColorクラス
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
using System.Drawing;

namespace ReversiWpf
{
	////////////////////////////////////////////////////////////////////////////////
	///	@class		HslColor
	///	@brief		HSL (HLS) カラーを表す
	///
	////////////////////////////////////////////////////////////////////////////////
	public class HslColor
	{
		private float _h;
		/// <summary>
		/// 色相 (Hue)
		/// </summary>
		public float H
		{
			get { return this._h; }
		}

		private float _s;
		/// <summary>
		/// 彩度 (Saturation)
		/// </summary>
		public float S
		{
			get { return this._s; }
		}

		private float _l;
		/// <summary>
		/// 輝度 (Lightness)
		/// </summary>
		public float L
		{
			get { return this._l; }
		}

		public HslColor(float hue, float saturation, float lightness)
		{
			if (hue < 0f || 360f <= hue)
			{
				throw new ArgumentException("hueは0以上360未満の値です。", "hue");
			}
			if (saturation < 0f || 1f < saturation)
			{
				throw new ArgumentException("saturationは0以上1以下の値です。", "saturation");
			}
			if (lightness < 0f || 1f < lightness)
			{
				throw new ArgumentException("lightnessは0以上1以下の値です。", "lightness");
			}

			this._h = hue;
			this._s = saturation;
			this._l = lightness;
		}

		/// <summary>
		/// 指定したColorからHslColorを作成する
		/// </summary>
		/// <param name="rgb">Color</param>
		/// <returns>HslColor</returns>
		public static HslColor FromRgb(Color rgb)
		{
			float r = (float)rgb.R / 255f;
			float g = (float)rgb.G / 255f;
			float b = (float)rgb.B / 255f;

			float max = Math.Max(r, Math.Max(g, b));
			float min = Math.Min(r, Math.Min(g, b));

			float lightness = (max + min) / 2f;

			float hue, saturation;
			if (max == min)
			{
				//undefined
				hue = 0f;
				saturation = 0f;
			}
			else
			{
				float c = max - min;

				if (max == r)
				{
					hue = (g - b) / c;
				}
				else if (max == g)
				{
					hue = (b - r) / c + 2f;
				}
				else
				{
					hue = (r - g) / c + 4f;
				}
					hue *= 60f;
				if (hue < 0f)
				{
					hue += 360f;
				}

				//saturation = c / (1f - Math.Abs(2f * lightness - 1f));
				if (lightness < 0.5f)
				{
					saturation = c / (max + min);
				}
				else
				{
					saturation = c / (2f - max - min);
				}
			}

			return new HslColor(hue, saturation, lightness);
		}

		/// <summary>
		/// 指定したHslColorからColorを作成する
		/// </summary>
		/// <param name="hsl">HslColor</param>
		/// <returns>Color</returns>
		public static Color ToRgb(HslColor hsl)
		{
			float s = hsl.S;
			float l = hsl.L;

			float r1, g1, b1;
			if (s == 0)
			{
				r1 = l;
				g1 = l;
				b1 = l;
			}
			else
			{
				float h = hsl.H / 60f;
				int i = (int)Math.Floor(h);
				float f = h - i;
				//float c = (1f - Math.Abs(2f * l - 1f)) * s;
				float c;
				if (l < 0.5f)
				{
					c = 2f * s * l;
				}
				else
				{
					c = 2f * s * (1f - l);
				}
				float m = l - c / 2f;
				float p = c + m;
				//float x = c * (1f - Math.Abs(h % 2f - 1f));
				float q; // q = x + m
				if (i % 2 == 0)
				{
					q = l + c * (f - 0.5f);
				}
				else
				{
					q = l - c * (f - 0.5f);
				}

				switch (i)
				{
					case 0:
						r1 = p;
						g1 = q;
						b1 = m;
						break;
					case 1:
						r1 = q;
						g1 = p;
						b1 = m;
						break;
					case 2:
						r1 = m;
						g1 = p;
						b1 = q;
						break;
					case 3:
						r1 = m;
						g1 = q;
						b1 = p;
						break;
					case 4:
						r1 = q;
						g1 = m;
						b1 = p;
						break;
					case 5:
						r1 = p;
						g1 = m;
						b1 = q;
						break;
					default:
						throw new ArgumentException("色相の値が不正です。", "hsl");
				}
			}

			return Color.FromArgb(
			(int)Math.Round(r1 * 255f),
			(int)Math.Round(g1 * 255f),
			(int)Math.Round(b1 * 255f));
		}
	}
}