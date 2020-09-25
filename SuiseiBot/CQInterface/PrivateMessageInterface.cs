using Native.Sdk.Cqp.EventArgs;

namespace SuiseiBot.Code.CQInterface
{
    public static class PrivateMessageInterface
    {
        public static void PrivateMessage(object sender, CQPrivateMessageEventArgs eventArgs)
        {
            ConsoleLog.Info($"收到信息[私信:{eventArgs.FromQQ.Id}]", $"{(eventArgs.Message.Text).Replace("\r\n", "\\r\\n")}\n{eventArgs.Message.Id}");
            if (eventArgs.Message.Text.Equals("suisei"))
            {
                eventArgs.FromQQ.SendPrivateMessage("すいちゃんは——今日もかわいい！");
            }
            eventArgs.Handler = true;

            //读取配置文件
            Config config = new Config(eventArgs.CQApi.GetLoginQQ().Id, false);

            //以*开头的消息全部交给Auction模块处理
            if (eventArgs.Message.Text.Trim().StartsWith("*") && //检查指令开头
                config.LoadConfig()                              //加载配置文件
            )
            {
                //检查模块使能
                if (!config.LoadedConfig.ModuleSwitch.AuctionBot)
                {
                    eventArgs.FromQQ.SendPrivateMessage("此模块未启用");
                    return;
                }
                PrivateAuctionHandle auctionGuild = new PrivateAuctionHandle(sender, eventArgs);
                auctionGuild.GetChat();
                return;
            }
        }
    }
}