using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpSvn;

namespace svn_client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SvnClient client = new SvnClient())
            {
                //サーバー上のリポジトリ位置の設定 
                string uri = "リポジトリのURL";
                SvnUriTarget repos = new SvnUriTarget(new Uri(uri));


                SvnInfoEventArgs serverInfo;
                if (! client.GetInfo(repos, out serverInfo)) {
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
