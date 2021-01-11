using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using SharpSvn;

namespace svn_client
{
    class Program
    {

        private static string _URI;
        private static int _Limit;

        static void Main(string[] args)
        {
            if (!ParseArgs(args))
            {
                Console.WriteLine("ログを取得するURIを入力してください");
                _URI = Console.ReadLine();
            }
            if (String.IsNullOrEmpty(_URI)) { return; }
            OutputSvnLog();
            Console.WriteLine("終了しました");
            Console.ReadKey();
        }

        private static bool ParseArgs(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<CommandOptions>(args);
            if (result.Tag != ParserResultType.Parsed)
            {
                // コマンドの解析に失敗したり、不明なオプションがあったりHelp押されたりしたとき
                return false;
            }

            var parsed = (Parsed<CommandOptions>)result;
            _URI = parsed.Value.Uri;
            _Limit = parsed.Value.Limit;

            if (String.IsNullOrEmpty(_URI)) { return false; }
            return true;
        }

        private static void OutputSvnLog()
        {
            using (SvnClient client = new SvnClient())
            {
                Console.WriteLine("ログ取得を開始します");
                // SVN リポジトリのパス
                string path = _URI;
                Uri uri = null;
                try
                {
                    uri = new Uri(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
               
                SvnUriTarget repos = new SvnUriTarget(uri);
                SvnLogArgs logArgs = new SvnLogArgs()
                {
                    Limit = _Limit,
                };

                Collection<SvnLogEventArgs> logs;
                client.GetLog(uri, logArgs, out logs);

                Encoding enc = Encoding.GetEncoding("shift_jis");
                string outputPath = @"result.txt";
                using (StreamWriter writer = new StreamWriter(outputPath, true))
                {
                    foreach (var log in logs)
                    {
                        string rev = log.Revision.ToString();
                        string message = log.LogMessage.Replace("\r\n", " ").Trim();
                        int pos = message.LastIndexOf('#');
                        string id = (pos > 0) ? message.Substring(pos) : ""; 
                        DateTime time = log.Time;

                        writer.WriteLine($"{rev}	{time.ToString("yyyy/MM/dd")}	{id}	{message}");
                    }
                }
            }
        }

        private void TestCheckout()
        {
            using (SvnClient client = new SvnClient())
            {
                //サーバー上のリポジトリ位置の設定 
                string uri = "リポジトリのURL";
                SvnUriTarget repos = new SvnUriTarget(new Uri(uri));


                SvnInfoEventArgs serverInfo;
                if (!client.GetInfo(repos, out serverInfo))
                {
                    Console.WriteLine("error");
                }


                //System.Collections.ObjectModel.Collection<SvnListEventArgs> list;
                ////一覧の取得を実行 
                //if (!client.GetList(repos, out list))
                //{
                //    Console.WriteLine("error");
                //}


                ////処理結果の表示  
                //foreach (var node in list)
                //{
                //    if (node.Entry.NodeKind == SvnNodeKind.Directory) {
                //        continue;
                //    }

                //    Console.WriteLine("File:{0}", node.Uri.AbsoluteUri);
                //}

                // 空でチェックアウト
                SvnCheckOutArgs checkoutconf = new SvnCheckOutArgs();
                checkoutconf.Depth = SvnDepth.Empty;
                string path = @"チェックアウトするローカルパス";
                client.CheckOut(repos, path, checkoutconf);

                // 指定したファイルだけ落とす
                SvnUpdateArgs updateconfig = new SvnUpdateArgs();
                updateconfig.Depth = SvnDepth.Files;
                string filename = "";
                // 配列でもOK
                client.Update(path + filename, updateconfig);

            }
        }
    }
}
