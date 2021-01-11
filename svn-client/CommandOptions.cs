using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;

namespace svn_client
{
    public class CommandOptions
    {

        [Option('u', "uri", Required = false, HelpText = "リポジトリのURI(例：http://orangeright/svnroot/OR/branches/Custom/NIS/or_group/OR/BedSide/System/Prog)")]
        public string Uri { get; set; }

        [Option('l', "limit", Required = false, HelpText = "ログを取得する件数の上限", Default = 200)]
        public int Limit { get; set; }
    }
}
