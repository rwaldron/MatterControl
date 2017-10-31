﻿/*
Copyright (c) 2017, Lars Brubaker, John Lewin
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those
of the authors and should not be interpreted as representing official policies,
either expressed or implied, of the FreeBSD Project.
*/

using MatterHackers.Agg;
using MatterHackers.Agg.UI;
using MatterHackers.Agg.VertexSource;

namespace MatterHackers.MatterControl.PartPreviewWindow
{
	public class PopupMenuButton : PopupButton
	{
		public PopupMenuButton(GuiWidget viewWidget)
			: base(viewWidget)
		{
		}

		private bool _drawArrow = false;
		public bool DrawArrow
		{
			get => _drawArrow;
			set
			{
				if (_drawArrow != value)
				{
					_drawArrow = value;

					if (_drawArrow)
					{
						this.Padding = new BorderDouble(this.Padding.Left, this.Padding.Bottom, 20, this.Padding.Top);
					}
				}
			}
		}

		private VertexStorage dropArrow = DropArrow.DownArrow;

		public static BorderDouble MenuPadding { get; set; } = new BorderDouble(40, 8, 20, 8);

		public static MenuItem CreateHorizontalLine()
		{
			return new MenuItem(new GuiWidget()
			{
				HAnchor = HAnchor.Stretch,
				Height = 1,
				BackgroundColor = Color.LightGray,
				Margin = new BorderDouble(10, 1),
				VAnchor = VAnchor.Center,
			}, "HorizontalLine");
		}

		public static MenuItem CreateMenuItem(string name, string value = null, double pointSize = 12)
		{
			var menuStatesView = new MenuItemColorStatesView(name)
			{
				NormalBackgroundColor = Color.White,
				OverBackgroundColor = Color.Gray,
				NormalTextColor = Color.Black,
				OverTextColor = Color.Black,
				DisabledTextColor = Color.Gray,
				PointSize = pointSize,
				Padding = MenuPadding,
			};

			return new MenuItem(menuStatesView, value ?? name)
			{
				Text = name,
				Name = name + " Menu Item"
			};
		}

		public override void OnDraw(Graphics2D graphics2D)
		{
			base.OnDraw(graphics2D);

			if (this.DrawArrow)
			{
				// Draw directional arrow
				graphics2D.Render(dropArrow, LocalBounds.Right - DropArrow.ArrowHeight * 2 - 2, LocalBounds.Center.Y + DropArrow.ArrowHeight / 2, ActiveTheme.Instance.SecondaryTextColor);
			}
		}

		protected override void BeforeShowPopup()
		{
			if (this.PopupContent.BackgroundColor == Color.Transparent)
			{
				this.PopupContent.BackgroundColor = Color.White;
			}
		}
	}
}