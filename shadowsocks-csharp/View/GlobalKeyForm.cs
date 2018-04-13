
using Shadowsocks.Controller;
using System;
using System.Windows.Forms;

namespace Shadowsocks.View
{
    public partial class GlobalKeyForm : Form
    {
        HotKeys h = new HotKeys();

        ShadowsocksController controller;

        public GlobalKeyForm(ShadowsocksController controller)
        {
            InitializeComponent();

            this.controller = controller;


            h.Regist(this.Handle, (int)HotKeys.HotkeyModifiers.Control, Keys.F12, CallBack);
            //MessageBox.Show("注册成功");


        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            //这里注册了Ctrl+Alt+E 快捷键
            h.Regist(this.Handle, (int)HotKeys.HotkeyModifiers.Control + (int)HotKeys.HotkeyModifiers.Alt, Keys.E, CallBack);
            MessageBox.Show("注册成功");
        }

        private void btnUnregist_Click(object sender, EventArgs e)
        {
            h.UnRegist(this.Handle, CallBack);
            MessageBox.Show("卸载成功");
        }

        protected override void WndProc(ref Message m)
        {
            //窗口消息处理函数
            h.ProcessHotKey(m);
            base.WndProc(ref m);
        }

        //按下快捷键时被调用的方法
        public void CallBack()
        {
            //MessageBox.Show("快捷键被调用！,Switch Mode");


            if (controller.GetCurrentConfiguration().sysProxyMode == (int)ProxyMode.Global)
            {
                controller.ToggleMode(ProxyMode.Direct);
            }
            else if (controller.GetCurrentConfiguration().sysProxyMode == (int)ProxyMode.Direct)
            {
                controller.ToggleMode(ProxyMode.Pac);
            }
            else if(controller.GetCurrentConfiguration().sysProxyMode == (int)ProxyMode.Pac)
            {
                controller.ToggleMode(ProxyMode.Global);
            }


        }
    }
}
