using Native.Sdk.Cqp.EventArgs;
using System;

namespace AuctionBot.Code.ChatHandle
{
    internal class DefaultHandle
    {
        #region 属性

        public object Sender { private set; get; }
        public CQGroupMessageEventArgs DebugEventArgs { private set; get; }

        #endregion 属性

        #region 构造函数

        public DefaultHandle(object sender, CQGroupMessageEventArgs e)
        {
            this.DebugEventArgs = e;
            this.Sender = sender;
        }

        #endregion 构造函数

        #region 消息响应函数

        /// <summary>
        /// 消息接收函数
        /// 并匹配相应指令
        /// </summary>
        public void GetChat() //消息接收并判断是否响应
        {
            if (DebugEventArgs == null || Sender == null) return;
            if (DebugEventArgs.Message.Text.StartsWith("echo"))
            {
                Echo();
            }
            else
            {
                Test();
            }
        }

        #endregion 消息响应函数

        #region DEBUG

        /// <summary>
        /// echo打印函数
        /// </summary>
        private void Echo()
        {
            if (DebugEventArgs.Message.Text.Length > 5)
            {
                DebugEventArgs.FromGroup.SendGroupMessage(DebugEventArgs.Message.Text.Substring(5));
            }
        }

        /// <summary>
        /// 响应函数
        /// </summary>
        private void Test() //功能响应
        {
            //此区域代码均只用于测试
#if DEBUG
            //用于异常捕获测试
            DebugEventArgs.FromGroup.SendGroupMessage("将会抛出一个异常");
            throw new Exception("wow");
#else
            DebugEventArgs.FromGroup.SendGroupMessage("哇哦");
#endif
        }

        #endregion DEBUG
    }
}