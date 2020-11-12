using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace BaiduAPI
{
    public partial class Form1 : Form
    {
        string q = "米老鼠";
        string from = "zh";
        string to = "en";
        string appid = "20201111000614330";
        string key = "bibJGOC4AqVR34w7YEUL";
        int salt = 1435660288;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string result = GetResult();
        }

        public string sign
        {
            get { return string.Format("{0}{1}{2}{3}", appid, q, salt, key); }
        }
        string getMd5()
        {
            var md5 = new MD5CryptoServiceProvider();
            var result = Encoding.UTF8.GetBytes(sign);
            var output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }

        public string GetJson()
        {
            var client = new RestClient("http://api.fanyi.baidu.com");
            var request = new RestRequest("/api/trans/vip/translate", Method.GET);
            request.AddParameter("q", q);
            request.AddParameter("from", from);
            request.AddParameter("to", to);
            request.AddParameter("appid", appid);
            request.AddParameter("salt", salt);
            request.AddParameter("sign", getMd5());
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string GetResult()
        {
            var lst = new List<string>();
            var content = GetJson();
            dynamic json = JsonConvert.DeserializeObject(content);
            foreach (var item in json.trans_result)
            {
                lst.Add(item.dst.ToString());
            }
            MessageBox.Show(string.Join(";", lst));
            return string.Join(";", lst);
        }

    }
}
