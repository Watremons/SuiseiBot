using System.Collections.Generic;

namespace AuctionBot.Code.IO.Config.ConfigClass
{
    internal class BiliSubscription
    {
        public int FlashTime { set; get; }
        public List<GroupSubscription> GroupsConfig { set; get; }
    }

    internal class GroupSubscription
    {
        public List<long> GroupId { set; get; }
        public bool PCR_Subscription { set; get; }
        public List<long> SubscriptionId { set; get; }
    }
}