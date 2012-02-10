using System;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;	// DllImport
using System.Text;						// StringBuilder

namespace casper {

	public class frmMain : System.Windows.Forms.Form {
		[DllImport("user32.dll")] private static extern int GetWindowText(int hWnd, StringBuilder title, int size);
		[DllImport("user32.dll")] private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
		[DllImport("user32.dll")] private static extern int EnumWindows(EnumWindowsProc ewp, int lParam); 
		[DllImport("user32.dll")] private static extern bool IsWindowVisible(int hWnd);

		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.NotifyIcon notifyIcon2;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.Timer timer2;

		public delegate bool EnumWindowsProc(int hWnd, int lParam);
		
		private ArrayList wins = new ArrayList();
		private ArrayList checkedWins = new ArrayList();
		ArrayList allWin = new ArrayList();

		private const int SW_HIDE = 0;
		private const int SW_SHOWNORMAL = 1;
		public bool IEVIEW = false;				
		
		public frmMain() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		// Clean up any resources being used.
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifyIcon2 = new System.Windows.Forms.NotifyIcon(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem5,
																						 this.menuItem2});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// menuItem1
			// 
			this.menuItem1.DefaultItem = true;
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "SensePost";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "E&xit";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "&Refresh";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "IEView";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "-";
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "SensePost Hidden Window Enumerator";
			this.notifyIcon1.Visible = true;
			// 
			// notifyIcon2
			// 
			this.notifyIcon2.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon2.Icon")));
			this.notifyIcon2.Text = "New Invisible App Running";
			// 
			// timer1
			// 
			this.timer1.Interval = 10000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// timer2
			// 
			this.timer2.Interval = 30000;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 262);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMain";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.Text = "Casper";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new frmMain());
		}

		// not sure how this should be done, using this function to redraw the context menu
		public void re_menu() {
			// clear the old menu, and set up us the default!
			contextMenu1.MenuItems.Clear();
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem5,
																						 this.menuItem2});

			// menuItem1
			this.menuItem1.DefaultItem = true;
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "SensePost";

			// menuItem3
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Exit";

			// menuItem4
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "&Refresh!";

			// menuItem5
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "IEView";
			
			// menuItem2
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "-";
		}

		// SensePost Button
		private void menuItem1_Click(object sender, System.EventArgs e) {
			MessageBox.Show("SensePost Hidden Windows Enumerator\n\nhttp://www.sensepost.com\n");
		}

		// Exit button
		private void menuItem3_Click(object sender, System.EventArgs e) {
			// lets cleanup
			this.Dispose(true);
			Application.Exit();
		}

		// selected window from dynamic list
		private void menuItem_Click(object sender, System.EventArgs e) {
			// going to see if i can cast sendere to type menuitem
			// i really shld read up on this visual stuff
			MenuItem test = (MenuItem)sender;

			// lets convert the Array to intPtr and call showWindowAsync
			// the -5 is to account for the menu-items that are permanent
			int x = (int)allWin[test.Index-5];
			IntPtr myWnd = new IntPtr(x);

			// first we check if this item is already checked or not
			// if it is.. we make it invisible, else we make it visible
			if(test.Checked) {
				// lets hide the window
				ShowWindowAsync(myWnd, SW_HIDE);
				// lets remove the item from our array of hidden-by-us windows
				checkedWins.RemoveAt(checkedWins.IndexOf((int)myWnd));
				// lets uncheck the menu
				test.Checked = false;
				return;
			}
			else {
				// lets check the item so we know we playing with it
				test.Checked=true;
				// lets remove the item from the context menu
				contextMenu1.MenuItems.RemoveAt(test.Index);
				// lets make the item visible
				ShowWindowAsync(myWnd, SW_SHOWNORMAL);
				// lets add the window to an array of checked-items
				checkedWins.Add((int)myWnd);			
			}
		}

		// Enum Windows
		private void menuItem4_Click(object sender, System.EventArgs e) {
			enumWindows();
		}

		// i kludge changing the icon by using swapIcon(true) or swapIcon(false)
		// again, im sure there must be an official way to do this that i dont know about
		private void swapIcon(bool x) {
			this.notifyIcon2.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Visible	= !x;
			this.notifyIcon2.Visible	= x;
		}

		// we first clear the menu, redraw it (ie. clear the old list)
		// then we enum the new windows
		private void enumWindows() {
			// clear the array of windows		
			allWin.Clear();

			// add the checked windows to the array we going to 
			// store our discoverd windows in
			for(int y=0; y<checkedWins.Count; y++) {
				allWin.Add(checkedWins[y]);
			}

			// lets (re)add the new menu
			this.re_menu();

			// kay lets go with the enumWindows
			EnumWindowsProc ewp = new EnumWindowsProc(EvalWindow);
			//Enumerate all Windows
			EnumWindows(ewp, 0);
			
			// foreach element in array, add a menu item
			for(int x=0; x<allWin.Count; x++) {
				MenuItem menuItem = new MenuItem();
				
				// kay, we have an array of windows,
				// lets extract the title of each
				StringBuilder title = new StringBuilder(256);
				GetWindowText((int)allWin[x], title, 256);
				
				menuItem.Text = "[" + x + "] " + title.ToString(); 
				menuItem.Index = this.contextMenu1.MenuItems.Count;
				menuItem.Click += new System.EventHandler(this.menuItem_Click);
					
				if(x < checkedWins.Count) menuItem.Checked = true;

				contextMenu1.MenuItems.Add(menuItem);
			}
		}

		// run everytime the context menu is right clicked
		private void contextMenu1_Popup(object sender, System.EventArgs e) {
			// disable the timer1 for 30 seconds
			timer1.Enabled = false;
			timer2.Enabled = true;

			enumWindows();
			// set the icon back to default
			swapIcon(false);
		}

		private void timer1_Tick(object sender, System.EventArgs e) {
			try { 
				enumWindows(); 
			} 
			catch(Exception f) {
				MessageBox.Show("Catch All Error message\nPlease mail the error to haroon@sensepost.com\n");
				MessageBox.Show(f.StackTrace.ToString());
			}
		}

		//EnumWindows CALLBACK function
		private bool EvalWindow(int hWnd, int lParam) {
			StringBuilder title = new StringBuilder(256);
			GetWindowText(hWnd, title, 256);

			if(!IsWindowVisible(hWnd)&&title.Length>1) {
				// Check if we in ieview
				// if we are, make the win visible
				// else on as usual
				if((IEVIEW) && (title.ToString().StartsWith("Microsoft Internet Explorer"))) { 
					swapIcon(true);
					//MessageBox.Show("Hidden Internet Explorer Detected!\n\nSetting to Visible\n");
					IntPtr y = new IntPtr(hWnd);
					ShowWindowAsync(y, SW_SHOWNORMAL); 
				}
				else { allWin.Add(hWnd); }
			}
			return(true);
		}

		// IEView button
		private void menuItem5_Click(object sender, System.EventArgs e) {
			if(menuItem5.Checked) {
				MessageBox.Show("IEView Cancelled!");
				this.menuItem5.Checked=false;
				IEVIEW = false;
			}
			else {
				this.menuItem5.Checked = true;
				MessageBox.Show("IEView is enabled:\n\nAll instances of Internet Explorer will be set to Visible");
				IEVIEW = true;
			}
		}

		private void Form1_Load(object sender, System.EventArgs e) {
			timer1.Enabled = true;
		}

		// this timer is used to re-enable the other
		// (mainly cause i cant find an event for
		// the contextmenu minimising
		private void timer2_Tick(object sender, System.EventArgs e) {
			timer1.Enabled = true;
			// lets shut ourselves off
			timer2.Enabled = false;
		}

	}
}
