using System;

namespace GAS.Core
{
    public class Functions
    {
        enum StringFormat
        {
            UpperCase,
            LowerCase,
            Digits,
            Punctuation,
            Special,
            Random,
            Urlencode
        }
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
        /// <summary>
        /// Generate random string
        /// syntax:
        ///     {#i:from:to:type#} - integer
        ///         default:dec
        ///         hex - 0x....
        ///     Example:
        ///         {#int:0:1000:dec#}
        ///     {#c:from:to#} -character
        ///     Example
        ///         {#char:1:65535#}
        ///     {#s:type:min_length:max_length#} -string
        ///         type(may be combined)
        ///             A - uppercase ASCII
        ///             a - lowercase ASCII
        ///             0 - digits
        ///             s - punctuation
        ///             S - special
        ///             Z - random unicode
        ///             U - urlencode string
        ///         Example:
        ///         {#string:Aa0s:3:1000#}
        /// Warning! string will NOT be validated
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RandomFormattedString(string input)
        {
            return input;
        }
        ushort [] offsets={0,0,33,48,65,97};
        ushort[] lengs = { 65535, 32, 32, 10, 26, 26 };
        static char[] __random_string_gen(StringFormat f, int MinLen, int MaxLen)
        {/*
            int cnt = random.Next(MinLen, MaxLen + 1),c_len=1;
            bool urlencode = ((f & StringFormat.Urlencode) == StringFormat.Urlencode);
            char[] c =new char[ cnt * ( urlencode? 3 : 1)];

            for (int i = 0; i < cnt; i++)
            {
                random.Next(random.Next(0,c_len);
            }*/
            return null;
        }
        static int quintparse(char[] input)
        {
            return quintparse(input, 0, input.Length);
        }
        static int quintparse(char[] input, int from, int count)
        {
            int sum = 0, cnt=0;
            while(cnt<count){
                sum*=10;
                sum+=((int)input[(cnt++)+from])-48;
            }
            return sum;
        }
    }
}
