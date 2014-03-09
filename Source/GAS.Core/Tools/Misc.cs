using RandomStringGenerator.Helpers;

namespace GAS.Core.Tools {
    internal static class Misc {
        internal static string RS() {
            return new string(
                Generators.RandomAscii(
                    4,
                    10 ) );
        }
    }
}