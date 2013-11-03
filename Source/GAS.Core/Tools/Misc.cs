using RandomStringGenerator.Helpers;

namespace GAS.Core.Tools {
    static class Misc {
        internal static string RS() {
            return new string(Generators.RandomASCII(4,10));
        }
    }
}
