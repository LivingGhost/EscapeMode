using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace EscapeMode
{
    public partial class NotifyIconWrapper : Component
    {
        public NotifyIconWrapper()
        {
            InitializeComponent();

            // NotifyIconの設定
            this.notifyIcon1.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType().Namespace + "." + "app.ico"));
            this.notifyIcon1.Text = "ノートPCの画面を閉じるとロックする";

            // NotifyIcon コンテキストメニュー「表示」
            ToolStripMenuItem menuItem_Open = new ToolStripMenuItem();
            menuItem_Open.Text = "&表示";
            menuItem_Open.Click += new EventHandler((sender, e) => {
                // MainWindow を表示
                MainWindow.Window.Visibility = Visibility.Visible;
            });

            // NotifyIcon コンテキストメニュー「終了」
            ToolStripMenuItem menuItem_Exit = new ToolStripMenuItem();
            menuItem_Exit.Text = "&終了";
            menuItem_Exit.Click += new EventHandler((sender, e) => {
                // アプリケーション終了
                System.Windows.Application.Current.Shutdown();
            });

            // MenuStripに追加
            ContextMenuStrip menu = new ContextMenuStrip();
            this.notifyIcon1.ContextMenuStrip = menu;
            menu.Items.Add(menuItem_Open);
            menu.Items.Add(menuItem_Exit);
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            // MainWindow を表示
            MainWindow.Window.Visibility = Visibility.Visible;
        }

        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
