using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
namespace HtmlAgilityPackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化网络请求客户端
            HtmlWeb webClient = new HtmlWeb();
            //初始化文档
            HtmlDocument doc = webClient.Load("http://www.cnblogs.com/");
            //查找节点
            HtmlNodeCollection titleNodes = doc.DocumentNode.SelectNodes("//a[@class='post-item-title']");
            if (titleNodes != null)
            {
                foreach (var item in titleNodes)
                {
                    Console.WriteLine(item.InnerText);
                }
            }
            Console.Read();

        }
    }
}
