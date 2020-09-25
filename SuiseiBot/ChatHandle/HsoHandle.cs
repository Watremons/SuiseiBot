using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Group = Native.Sdk.Cqp.Model.Group;

namespace SuiseiBot.Code.ChatHandle
{
    internal class HsoHandle
    {
        #region 属性

        public object Sender { private set; get; }
        public Group QQGroup { private set; get; }
        public CQGroupMessageEventArgs HsoEventArgs { private set; get; }

        #endregion 属性

        #region 构造函数

        public HsoHandle(object sender, CQGroupMessageEventArgs e)
        {
            this.HsoEventArgs = e;
            this.Sender = sender;
            this.QQGroup = e.FromGroup;
        }

        #endregion 构造函数

        #region 指令响应分发

        /// <summary>
        /// 用于处理传入指令
        /// </summary>
        /// <param name="cmdType">指令类型</param>
        public async void GetChat(WholeMatchCmdType cmdType)
        {
            Config config = new Config(HsoEventArgs.CQApi.GetLoginQQ().Id);
            switch (cmdType)
            {
                case WholeMatchCmdType.Hso:
                    Hso hso = config.LoadedConfig.HsoConfig;
                    await GiveMeSetu(hso.Source, hso.LoliconToken, hso.YukariToken);
                    break;

                default:
                    break;
            }
        }

        #endregion 指令响应分发

        #region 私有方法

        /// <summary>
        /// <para>从色图源获取色图</para>
        /// <para>不会支持R18的哦</para>
        /// </summary>
        /// <param name="setuSource">源类型</param>
        /// <param name="loliconToken">lolicon token</param>
        /// <param name="yukariToken">yukari token</param>
        private Task GiveMeSetu(SetuSourceType setuSource, string loliconToken = null, string yukariToken = null)
        {
            string localPicPath;
            string response;
            StringBuilder urlBuilder = new StringBuilder();
            ConsoleLog.Debug("源", setuSource);
            //源选择
            switch (setuSource)
            {
                //混合源
                case SetuSourceType.Mix:
                    Random randSource = new Random();
                    if (randSource.Next(1, 100) > 50)
                    {
                        urlBuilder.Append("https://api.lolicon.app/setu/");
                        if (!string.IsNullOrEmpty(loliconToken)) urlBuilder.Append($"?token={loliconToken}");
                        ConsoleLog.Debug("色图源", "Lolicon");
                    }
                    else
                    {
                        urlBuilder.Append("https://api.yukari.one/setu/");
                        if (!string.IsNullOrEmpty(yukariToken)) urlBuilder.Append($"?token={yukariToken}");
                        ConsoleLog.Debug("色图源", "Yukari");
                    }
                    break;
                //lolicon
                case SetuSourceType.Lolicon:
                    urlBuilder.Append("https://api.lolicon.app/setu/");
                    if (!string.IsNullOrEmpty(loliconToken)) urlBuilder.Append($"?token={loliconToken}");
                    ConsoleLog.Debug("色图源", "Lolicon");
                    break;
                //Yukari
                case SetuSourceType.Yukari:
                    urlBuilder.Append("https://api.yukari.one/setu/");
                    if (!string.IsNullOrEmpty(yukariToken)) urlBuilder.Append($"?token={yukariToken}");
                    ConsoleLog.Debug("色图源", "Yukari");
                    break;

                case SetuSourceType.Local:
                    string[] picNames = Directory.GetFiles(IOUtils.GetHsoPath());
                    Random randFile = new Random();
                    localPicPath = $"{picNames[randFile.Next(0, picNames.Length - 1)]}";
                    ConsoleLog.Debug("发送图片", localPicPath);
                    QQGroup.SendGroupMessage(CQApi.CQCode_Image(localPicPath));
                    return Task.CompletedTask;
            }
            //网络部分
            try
            {
                ConsoleLog.Info("NET", "尝试获取色图");
                QQGroup.SendGroupMessage("正在获取色图中...");
                response = HTTPUtils.GetHttpResponse(urlBuilder.ToString());
                ConsoleLog.Debug("Get Json", response);
                if (string.IsNullOrEmpty(response))//没有获取到任何返回
                {
                    ConsoleLog.Error("网络错误", "获取到的响应数据为空");
                    HsoEventArgs.FromGroup.SendGroupMessage("哇哦~发生了网络错误，请联系机器人所在服务器管理员");
                    return Task.CompletedTask;
                }
            }
            catch (Exception e)
            {
                //网络错误
                QQGroup.SendGroupMessage("哇哦~发生了网络错误，请联系机器人所在服务器管理员");
                ConsoleLog.Error("网络发生错误", ConsoleLog.ErrorLogBuilder(e));
                return Task.CompletedTask;
            }
            //json处理
            try
            {
                JObject picJson = JObject.Parse(response);
                if ((int)picJson["code"] == 0)
                {
                    //图片链接
                    string picUrl = picJson["data"]?[0]?["url"]?.ToString() ?? "";
                    ConsoleLog.Debug("获取到图片", picUrl);
                    //本地图片存储路径
                    localPicPath = $"{IOUtils.GetHsoPath()}/{Path.GetFileName(picUrl)}";
                    if (File.Exists(localPicPath)) //检查是否已缓存过图片
                    {
                        QQGroup.SendGroupMessage(CQApi.CQCode_Image(localPicPath));
                    }
                    else
                    {
                        //文件名处理(mirai发送网络图片时pixivcat会返回403暂时无法使用代理发送图片
                        //QQGroup.SendGroupMessage(CQApi.Mirai_UrlImage(picUrl));
                        DownloadFileFromURL(picUrl, localPicPath);
                    }
                    return Task.CompletedTask;
                }
                if (((int)picJson["code"] == 401 || (int)picJson["code"] == 429) &&
                    setuSource == SetuSourceType.Lolicon)
                    ConsoleLog.Warning("API Token 失效", $"code:{picJson["code"]}");
                else
                    ConsoleLog.Warning("没有找到图片信息", "服务器拒绝提供信息");
                QQGroup.SendGroupMessage("哇奧色图不见了\n请联系机器人服务器管理员");
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                ConsoleLog.Error("色图下载失败", $"网络下载数据错误\n{ConsoleLog.ErrorLogBuilder(e)}");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// 下载图片保存到本地
        /// </summary>
        /// <param name="url">目标URL</param>
        /// <param name="receivePath">接收文件的地址</param>
        private void DownloadFileFromURL(string url, string receivePath)
        {
            try
            {
                int progressPercentage = 0;
                long bytesReceived = 0;
                DateTime flashTime = DateTime.Now;
                Console.WriteLine("开始从网络下载文件");
                WebClient client = new WebClient();
                //文件下载
                client.DownloadProgressChanged += (sender, args) =>
                                                  {
                                                      if (progressPercentage != args.ProgressPercentage)
                                                      {
                                                          progressPercentage = args.ProgressPercentage;
                                                          ConsoleLog
                                                              .Debug("Download Pic", $"Downloading {args.ProgressPercentage}% " +
                                                                         $"({(args.BytesReceived - bytesReceived) / 1024.0 / (DateTime.Now - flashTime).TotalSeconds}KB/s) ");
                                                          flashTime = DateTime.Now;
                                                          bytesReceived = args.BytesReceived;
                                                      }
                                                  };
                //文件下载完成
                client.DownloadFileCompleted += (sender, args) =>
                                                {
                                                    ConsoleLog.Info("Hso", "下载数据成功,发送图片");
                                                    QQGroup.SendGroupMessage(CQApi.CQCode_Image(receivePath));
                                                    ConsoleLog.Debug("file", Path.GetFileName(receivePath));
                                                };
                client.DownloadFileAsync(new Uri(url), receivePath);
            }
            catch (Exception e)
            {
                ConsoleLog.Error("色图下载失败", $"网络下载数据错误\n{ConsoleLog.ErrorLogBuilder(e)}");
            }
        }

        #endregion 私有方法
    }
}