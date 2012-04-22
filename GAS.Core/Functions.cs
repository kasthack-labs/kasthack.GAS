using System;

namespace GAS.Core
{
    public static class Functions
    {
        static Random random = new Random();
        public static string RandomString()
        {
            char[] ch = new char[6];
            for (int i = 0; i < 6; i++)
                ch[i] = Convert.ToChar(random.Next(65, 90));
            return new string(ch);
        }
        public static string RandomUserAgent()
        {
            Random random = new Random();
            String[] osversions = { "5.1", "6.0", "6.1" };
            String[] oslanguages = { "en-GB", "en-US", "es-ES", "pt-BR", "pt-PT", "sv-SE" };
            String version = osversions[random.Next(0, osversions.Length - 1)];
            String language = oslanguages[random.Next(0, oslanguages.Length - 1)];
            String useragent = String.Format("Mozilla/5.0 (Windows; U; Windows NT {0}; {1}; rv:1.9.2.17) Gecko/20110420 Firefox/3.6.17", version, language);
            return useragent;
        }
    }
}
