using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIREQUEST
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Type in playername");
            string player = Console.ReadLine();
            Console.WriteLine("Type in Realm");
            string realm = Console.ReadLine();
            Console.WriteLine("type in amount of old Achivements to get");
            int am = int.Parse(Console.ReadLine());
            var g = JsonConvert.DeserializeObject<Rootobject>(HttpGet($"https://eu.api.battle.net/wow/character/{realm}/{player}?fields=achievements&locale=en_GB&apikey=################################"));
            List<achivements> ls = new List<achivements>();
            List<int> gs = new List<int>();
            for (int i = 0; i < g.achievements.achievementsCompleted.Count(); i++)
            {
                gs.Add(i);
            }
            Console.WriteLine("Achivements = " + g.achievements.achievementsCompleted.Count());
            int limit = 0;
            Parallel.ForEach(gs, (i) =>
            {

                achivements s = new achivements();
                //s.Name = g.achievements.na[]
                s.id = g.achievements.achievementsCompleted[i];
                Console.WriteLine(s.id);
                s.ticks = g.achievements.achievementsCompletedTimestamp[i];
                ls.Add(s);
                Thread.Sleep(50);
            });
            Console.Clear();
            var h = JsonConvert.SerializeObject(ls);
            foreach (var item in ls)
            {
                item.Time = UnixTimeStampToDateTime(item.ticks);

            }
            int o = 0;
            foreach (var item in ls.OrderBy(x=> x.Time))
            {
                o++;
                string Name = "N/A";
                if (o < am)
                {
                    item.Acivement = JsonConvert.DeserializeObject<achiv>(HttpGet("https://eu.api.battle.net/wow/achievement/" + item.id + "?locale=en_GB&apikey=#######################################"));
                    Name = item.Acivement.title;
                    Console.WriteLine(item.Time + " - " + Name);

                }

            }
            Console.ReadLine();

        }
        class achivements
        {
            public achiv Acivement { get; set; }
            public DateTime Time { get; set; }
            public long ticks { get; set; }
            public int id { get; internal set; }
        }
        public static string HttpGet(string URI)
        {
            achiv gg = new achiv();
            gg.title = "NOT FOUND";
            bool worked = false;
            try
            {

                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                Stream data = client.OpenRead(URI);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                return s;
                worked = true;
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(gg);

            }
            if (!worked)
            {
                try
                {

                    WebClient client = new WebClient();
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    Stream data = client.OpenRead(URI);
                    StreamReader reader = new StreamReader(data);
                    string s = reader.ReadToEnd();
                    data.Close();
                    reader.Close();
                    return s;
                    worked = true;
                }
                catch (Exception)
                {
                    return JsonConvert.SerializeObject(gg);

                }
            }
            if (!worked)
            {
                try
                {

                    WebClient client = new WebClient();
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    Stream data = client.OpenRead(URI);
                    StreamReader reader = new StreamReader(data);
                    string s = reader.ReadToEnd();
                    data.Close();
                    reader.Close();
                    return s;
                    worked = true;
                }
                catch (Exception)
                {
                    return JsonConvert.SerializeObject(gg);

                }
            }
        }


        public class Rootobject
        {
            public long lastModified { get; set; }
            public string name { get; set; }
            public string realm { get; set; }
            public string battlegroup { get; set; }
            public int _class { get; set; }
            public int race { get; set; }
            public int gender { get; set; }
            public int level { get; set; }
            public int achievementPoints { get; set; }
            public string thumbnail { get; set; }
            public string calcClass { get; set; }
            public int faction { get; set; }
            public Achievements achievements { get; set; }
            public int totalHonorableKills { get; set; }
        }

        public class Achievements
        {
            public int[] achievementsCompleted { get; set; }
            public long[] achievementsCompletedTimestamp { get; set; }
            public int[] criteria { get; set; }
            public long[] criteriaQuantity { get; set; }
            public long[] criteriaTimestamp { get; set; }
            public long[] criteriaCreated { get; set; }
        }



        public class achiv
        {
            public int id { get; set; }
            public string title { get; set; }
            public int points { get; set; }
            public string description { get; set; }
            public string reward { get; set; }
            public Rewarditem[] rewardItems { get; set; }
            public string icon { get; set; }
            public Criterion[] criteria { get; set; }
            public bool accountWide { get; set; }
            public int factionId { get; set; }
        }

        public class Rewarditem
        {
            public int id { get; set; }
            public string name { get; set; }
            public string icon { get; set; }
            public int quality { get; set; }
            public int itemLevel { get; set; }
            public Tooltipparams tooltipParams { get; set; }
            public object[] stats { get; set; }
            public int armor { get; set; }
            public string context { get; set; }
            public object[] bonusLists { get; set; }
            public int artifactId { get; set; }
            public int displayInfoId { get; set; }
            public int artifactAppearanceId { get; set; }
            public object[] artifactTraits { get; set; }
            public object[] relics { get; set; }
            public Appearance appearance { get; set; }
        }

        public class Tooltipparams
        {
            public int timewalkerLevel { get; set; }
        }

        public class Appearance
        {
        }

        public class Criterion
        {
            public int id { get; set; }
            public string description { get; set; }
            public int orderIndex { get; set; }
            public int max { get; set; }
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp);

            return dtDateTime;
        }
    }
}
