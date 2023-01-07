﻿using Microsoft.Win32;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>
	/// Interface that exposes an <c>Enabled</c> property for an item supplied to <see cref="DisabledItemComboBox"/>.
	/// </summary>
	public interface IEnableable
	{
		/// <summary>Gets a value indicating whether an item is enabled.</summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		bool Enabled { get; }
	}

	/// <summary>
	/// A version of <see cref="ComboBox"/> that allows for disabled items.
	/// </summary>
	[ToolboxBitmap(typeof(Microsoft.Win32.TaskScheduler.TaskEditDialog), "Control")]
	public class DisabledItemComboBox : ComboBox
	{
		private const TextFormatFlags tff = TextFormatFlags.Default | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;
		private bool animationsNeedCleanup;
		private readonly bool BufferedPaintSupported = false;
		private ComboBoxState currentState = ComboBoxState.Normal, newState = ComboBoxState.Normal;
		private LBNativeWindow dropDownWindow;
		private readonly AnimationTransition<ComboBoxState>[] transitions;

		/// <summary>
		/// Initializes a new instance of the <see cref="DisabledItemComboBox"/> class.
		/// </summary>
		public DisabledItemComboBox()
		{
			SetStyle(/*ControlStyles.Opaque |*/ ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			DrawMode = DrawMode.OwnerDrawFixed;
			DropDownStyle = ComboBoxStyle.DropDownList;
			if (Environment.OSVersion.Version.Major >= 6 && VisualStyleRenderer.IsSupported && Application.RenderWithVisualStyles)
			{
				BufferedPaintSupported = true;
				var vsr = new VisualStyleRenderer("COMBOBOX", 5, 0);
				transitions = new[] {
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Normal, ComboBoxState.Hot),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Normal, ComboBoxState.Pressed),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Normal, ComboBoxState.Disabled),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Hot, ComboBoxState.Normal),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Hot, ComboBoxState.Pressed),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Hot, ComboBoxState.Disabled),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Pressed, ComboBoxState.Normal),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Pressed, ComboBoxState.Hot),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Disabled, ComboBoxState.Normal)
				};
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether your code or the operating system will handle drawing of elements in the list.
		/// </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DrawMode" /> enumeration values. The default is <see cref="F:System.Windows.Forms.DrawMode.Normal" />.</returns>
		///   <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   </PermissionSet>
		[DefaultValue(DrawMode.OwnerDrawFixed), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new DrawMode DrawMode
		{
			get { return base.DrawMode; }
			set { base.DrawMode = value; }
		}

		/// <summary>
		/// Gets or sets a value specifying the style of the combo box.
		/// </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. The default is DropDown.</returns>
		///   <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   </PermissionSet>
		[DefaultValue(ComboBoxStyle.DropDownList), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new ComboBoxStyle DropDownStyle
		{
			get { return base.DropDownStyle; }
			set { base.DropDownStyle = value; }
		}

		/// <summary>
		/// Gets or sets the state of the combobox.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		private ComboBoxState State
		{
			get
			{
				return currentState;
			}
			set
			{
				var diff = !Equals(currentState, value);
				newState = value;
				if (diff)
				{
					if (animationsNeedCleanup && IsHandleCreated) NativeMethods.BufferedPaintStopAllAnimations(Handle);
					Invalidate();
				}
			}
		}

		/// <summary>
		/// Determines whether an item is enabled.
		/// </summary>
		/// <param name="idx">The index of the item.</param>
		/// <returns>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </returns>
		public bool IsItemEnabled(int idx) => !(idx > -1 && idx < Items.Count && Items[idx] is IEnableable && !((IEnableable)Items[idx]).Enabled);

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ComboBox" /> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (animationsNeedCleanup)
			{
				NativeMethods.BufferedPaintUnInit();
				animationsNeedCleanup = false;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ComboBox.DrawItem" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data.</param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			var itemString = e.Index >= 0 ? GetItemText(Items[e.Index]) : string.Empty;

			if ((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit)
			{
				if (e.Index >= 0)
				{
					var iEnabled = IsItemEnabled(e.Index);
					if (iEnabled)
					{
						e.DrawBackground();
						e.DrawFocusRectangle();
					}
					else
					{
						using (var bb = new SolidBrush(e.BackColor))
							e.Graphics.FillRectangle(bb, e.Bounds);
					}
					TextRenderer.DrawText(e.Graphics, itemString, e.Font, Rectangle.Inflate(e.Bounds, -2, 0), iEnabled ? e.ForeColor : SystemColors.GrayText, tff);
				}
			}
			base.OnDrawItem(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDown" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnDropDown(EventArgs e)
		{
			base.OnDropDown(e);
			State = ComboBoxState.Pressed;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownClosed" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnDropDownClosed(EventArgs e)
		{
			base.OnDropDownClosed(e);
			State = ComboBoxState.Normal;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (BufferedPaintSupported)
			{
				NativeMethods.BufferedPaintInit();
				animationsNeedCleanup = true;
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnHandleDestroyed(EventArgs e)
		{
			dropDownWindow?.DestroyHandle();
			base.OnHandleDestroyed(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			var idx = FindEnabledString(e.KeyChar.ToString(), SelectedIndex);
			if (idx == -1 || idx == SelectedIndex)
				e.Handled = true;
			base.OnKeyPress(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			Invalidate();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			State = ComboBoxState.Pressed;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			State = ComboBoxState.Hot;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (State != ComboBoxState.Pressed)
				State = ComboBoxState.Normal;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (State != ComboBoxState.Pressed)
				State = ComboBoxState.Hot;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (BufferedPaintSupported)
			{
				var stateChanged = !Equals(currentState, newState);

				using (var hdc = new SafeGDIHandle(e.Graphics))
				{
					if (hdc.IsInvalid || NativeMethods.BufferedPaintRenderAnimation(Handle, hdc)) return;

					var animParams = new NativeMethods.BufferedPaintAnimationParams(NativeMethods.BufferedPaintAnimationStyle.Linear);

					// get appropriate animation time depending on state transition (or 0 if unchanged)
					if (stateChanged)
					{
						foreach (var item in transitions)
							if (item.currentState == currentState && item.newState == newState)
							{
								animParams.Duration = item.duration;
								break;
							}
					}

					var rc = ClientRectangle;
					IntPtr hdcFrom, hdcTo;
					var hbpAnimation = NativeMethods.BeginBufferedAnimation(Handle, hdc, ref rc, NativeMethods.BufferedPaintBufferFormat.CompatibleBitmap, IntPtr.Zero, ref animParams, out hdcFrom, out hdcTo);
					if (hbpAnimation != IntPtr.Zero)
					{
						if (hdcFrom != IntPtr.Zero)
						{
							using (var gfxFrom = Graphics.FromHdc(hdcFrom))
								PaintControl(new PaintEventArgs(gfxFrom, e.ClipRectangle));
						}
						currentState = newState;
						if (hdcTo != IntPtr.Zero)
						{
							using (var gfxTo = Graphics.FromHdc(hdcTo))
								PaintControl(new PaintEventArgs(gfxTo, e.ClipRectangle));
						}
						NativeMethods.EndBufferedAnimation(hbpAnimation, true);
					}
					else
					{
						hdc.Dispose();
						currentState = newState;
						PaintControl(e);
					}
				}
			}
			else
			{
				// buffered painting not supported, just paint using the current state
				currentState = newState;
				PaintControl(e);
			}
		}

		/// <summary>
		/// Paints the background of the control.
		/// </summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			// don't paint the control's background
		}

		/// <summary>
		/// Paints the control.
		/// </summary>
		/// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
		protected virtual void PaintControl(PaintEventArgs e)
		{
			var cbi = NativeMethods.COMBOBOXINFO.FromComboBox(this);

			var itemText = SelectedIndex >= 0 ? GetItemText(SelectedItem) : string.Empty;
			var state = Enabled ? currentState : ComboBoxState.Disabled;
			Rectangle tr = cbi.rcItem;
			/*Rectangle tr = this.ClientRectangle;
			tr.Width -= (SystemInformation.VerticalScrollBarWidth + 2);
			tr.Inflate(0, -2);
			tr.Offset(1, 0);*/
			Rectangle br = cbi.rcButton;
			var vsSuccess = false;
			if (VisualStyleRenderer.IsSupported && Application.RenderWithVisualStyles)
			{
				/*Rectangle r = Rectangle.Inflate(this.ClientRectangle, 1, 1);
				if (this.DropDownStyle != ComboBoxStyle.DropDownList)
				{
					e.Graphics.Clear(this.BackColor);
					ComboBoxRenderer.DrawTextBox(e.Graphics, r, itemText, this.Font, tr, tff, state);
					ComboBoxRenderer.DrawDropDownButton(e.Graphics, br, state);
				}
				else*/
				{
					try
					{
						var vr = new VisualStyleRenderer("Combobox", DropDownStyle == ComboBoxStyle.DropDownList ? 5 : 4, (int)state);
						vr.DrawParentBackground(e.Graphics, ClientRectangle, this);
						vr.DrawBackground(e.Graphics, ClientRectangle);
						if (DropDownStyle != ComboBoxStyle.DropDownList) br.Inflate(1, 1);
						var cr = DropDownStyle == ComboBoxStyle.DropDownList ? Rectangle.Inflate(br, -1, -1) : br;
						vr.SetParameters("Combobox", 7, (int)(br.Contains(PointToClient(Cursor.Position)) ? state : ComboBoxState.Normal));
						vr.DrawBackground(e.Graphics, br, cr);
						if (Focused && State != ComboBoxState.Pressed)
						{
							var sz = TextRenderer.MeasureText(e.Graphics, "Wg", Font, tr.Size, TextFormatFlags.Default);
							var fr = Rectangle.Inflate(tr, 0, (sz.Height - tr.Height) / 2 + 1);
							ControlPaint.DrawFocusRectangle(e.Graphics, fr);
						}
						var fgc = Enabled ? ForeColor : SystemColors.GrayText;
						TextRenderer.DrawText(e.Graphics, itemText, Font, tr, fgc, tff);
						vsSuccess = true;
					}
					catch { }
				}
			}

			if (!vsSuccess)
			{
				Diagnostics.Debug.WriteLine($"CR:{ClientRectangle};ClR:{e.ClipRectangle};Foc:{Focused};St:{state};Tx:{itemText}");
				var focusedNotPressed = Focused && state != ComboBoxState.Pressed;
				var bgc = Enabled ? (focusedNotPressed ? SystemColors.Highlight : BackColor) : SystemColors.Control;
				var fgc = Enabled ? (focusedNotPressed ? SystemColors.HighlightText : ForeColor) : SystemColors.GrayText;
				e.Graphics.Clear(bgc);
				ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Sunken);
				ControlPaint.DrawComboButton(e.Graphics, br, Enabled ? (state == ComboBoxState.Pressed ? ButtonState.Pushed : ButtonState.Normal) : ButtonState.Inactive);
				tr = new Rectangle(tr.X + 3, tr.Y + 1, tr.Width - 4, tr.Height - 2);
				TextRenderer.DrawText(e.Graphics, itemText, Font, tr, fgc, tff);
				if (focusedNotPressed)
				{
					var fr = Rectangle.Inflate(cbi.rcItem, -1, -1);
					fr.Height++;
					fr.Width++;
					ControlPaint.DrawFocusRectangle(e.Graphics, fr, fgc, bgc);
					e.Graphics.DrawRectangle(SystemPens.Window, cbi.rcItem);
				}
			}
		}

		/// <summary>
		/// Processes a command key.
		/// </summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		/// true if the character was processed by the control; otherwise, false.
		/// </returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			var visItems = DropDownHeight / ItemHeight;
			switch (keyData)
			{
				case Keys.Down:
				case Keys.Right:
					SelectedIndex = GetNextEnabledItemIndex(SelectedIndex, true);
					return true;

				case Keys.Up:
				case Keys.Left:
					SelectedIndex = GetNextEnabledItemIndex(SelectedIndex, false);
					return true;

				case Keys.PageDown:
					if (SelectedIndex + visItems > Items.Count)
						SelectedIndex = GetNextEnabledItemIndex(Items.Count, false);
					else
						SelectedIndex = GetNextEnabledItemIndex(SelectedIndex + visItems, true);
					return true;

				case Keys.PageUp:
					if (SelectedIndex - visItems < 0)
						SelectedIndex = GetNextEnabledItemIndex(-1, true);
					else
						SelectedIndex = GetNextEnabledItemIndex(SelectedIndex - visItems, false);
					return true;

				case Keys.Home:
					SelectedIndex = GetNextEnabledItemIndex(-1, true);
					return true;

				case Keys.End:
					SelectedIndex = GetNextEnabledItemIndex(Items.Count, false);
					return true;

				case Keys.Enter:
					var pt = dropDownWindow.MapPointToClient(Cursor.Position);
					var idx = dropDownWindow.IndexFromPoint(pt.X, pt.Y);
					if (idx >= 0 && IsItemEnabled(idx))
						return false;
					DroppedDown = false;
					return true;

				case Keys.Escape:
					DroppedDown = false;
					return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if ((int)(long)m.WParam == 0x3e80001)
			{
				dropDownWindow = new LBNativeWindow(m.LParam, this);
			}
		}

		private int FindEnabledString(string str, int startIndex)
		{
			if (str != null)
			{
				if (startIndex < -1 || startIndex >= Items.Count)
					return -1;
				var length = str.Length;
				var num2 = 0;
				for (var i = (startIndex + 1) % Items.Count; num2 < Items.Count; i = (i + 1) % Items.Count)
				{
					num2++;
					if (IsItemEnabled(i) && string.Compare(str, 0, GetItemText(Items[i]), 0, length, true, Globalization.CultureInfo.CurrentUICulture) == 0)
						return i;
				}
			}
			return -1;
		}

		private int GetNextEnabledItemIndex(int startIndex, bool forward = true)
		{
			if (forward)
			{
				for (var i = startIndex + 1; i < Items.Count; i++)
				{
					if (IsItemEnabled(i))
						return i;
				}
				return startIndex;
			}
			else
			{
				for (var i = startIndex - 1; i >= 0; i--)
				{
					if (IsItemEnabled(i))
						return i;
				}
				return startIndex;
			}
		}

		private struct AnimationTransition<T>
		{
			public readonly T currentState;
			public readonly uint duration;
			public readonly T newState;

			public AnimationTransition(VisualStyleRenderer rnd, T fromState, T toState)
			{
				if (rnd.State != Convert.ToInt32(fromState))
					rnd.SetParameters(rnd.Class, rnd.Part, Convert.ToInt32(fromState));
				currentState = fromState;
				newState = toState;
				duration = rnd.GetTransitionDuration(Convert.ToInt32(toState));
			}
		}

		private class LBNativeWindow : NativeWindow
		{
			private readonly DisabledItemComboBox Parent;

			public LBNativeWindow(IntPtr handle, DisabledItemComboBox parent)
			{
				Parent = parent;
				AssignHandle(handle);
			}

			public int IndexFromPoint(int x, int y)
			{
				var n = NativeMethods.SendMessage(Handle, 0x1a9 /* LB_ITEMFROMPOINT */, IntPtr.Zero, NativeMethods.Util.MAKELPARAM(x, y)).ToInt32();
				if (NativeMethods.Util.HIWORD(n) == 0)
					return NativeMethods.Util.LOWORD(n);
				return -1;
			}

			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 0x0202 || m.Msg == 0x0201 || m.Msg == 0x0203) /* WM_LBUTTONUP or WM_LBUTTONDOWN or WM_LBUTTONDBLCLK */
				{
					var idx = IndexFromPoint(NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
					if (idx >= 0 && !Parent.IsItemEnabled(idx))
						return;
				}
				base.WndProc(ref m);
			}
		}
	}
}