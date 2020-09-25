using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Model;
using System;

namespace SuiseiBot.Code.ChatHandle
{
    internal class SurpriseMFKHandle
    {
        #region 属性

        public object Sender { private set; get; }
        public CQGroupMessageEventArgs MFKEventArgs { private set; get; }
        private Group QQGroup { set; get; }

        #endregion 属性

        #region 构造函数

        public SurpriseMFKHandle(object sender, CQGroupMessageEventArgs eventArgs)
        {
            this.MFKEventArgs = eventArgs;
            this.Sender = sender;
        }

        #endregion 构造函数

        #region 消息响应函数

        /// <summary>
        /// 消息接收函数
        /// </summary>
        public void GetChat(WholeMatchCmdType cmdType)
        {
            if (MFKEventArgs == null || Sender == null || CheckInCD.isInCD(MFKEventArgs)) return;
            this.QQGroup = MFKEventArgs.FromGroup;
            switch (cmdType)
            {
                //生成随机数
                case WholeMatchCmdType.SurpriseMFK_Random:
                    RandomNumber();
                    break;
                //随机禁言套餐
                case WholeMatchCmdType.SurpriseMFK_Ban:
                    RandomBan();
                    break;
                //昏睡套餐
                case WholeMatchCmdType.SurpriseMFK_RedTea:
                    RedTea();
                    break;
                //恶臭问答
                //这个是不是多余了（
                case WholeMatchCmdType.SurpriseMFK_24YearsOld:
                    QQGroup.SendGroupMessage("24岁，是学生");
                    break;
            }
        }

        #endregion 消息响应函数

        #region 私有方法

        private void RandomNumber()
        {
            Random randomGen = new Random();
            QQGroup.SendGroupMessage("n=", randomGen.Next(0, 100));
        }

        private void RandomBan()
        {
            Random banTime = new Random();
            TimeSpan banTimeSpan = new TimeSpan(0, banTime.Next(1, 10), 0);
            MFKEventArgs.CQApi.SetGroupMemberBanSpeak(
                                                      QQGroup.Id,
                                                      MFKEventArgs.FromQQ.Id,
                                                      banTimeSpan);
        }

        private void RedTea()
        {
            TimeSpan banTimeSleep = new TimeSpan(8, 0, 0);
            MFKEventArgs.CQApi.SetGroupMemberBanSpeak(
                                                      QQGroup.Id,
                                                      MFKEventArgs.FromQQ.Id,
                                                      banTimeSleep);
        }

        #endregion 私有方法
    }
}