#GAS
GAS is opensource stress-test tool originally based on IRC-LOIC(neweracracker/loic)
Some features may be broken. If you want to perform TCP/UDP flood try net_flooder(kasthack/net_flooder)
#TODO
1. Rewrite attack methods using async IO
2. Add support of setting any http field: host/cookie/referrer/etc
3. Optimize RANDOM generator — it's really slow.	[_V_] moved to separate project: [kasthack/RandomStringGenerator](//github.com/kasthack/RandomStringGenerator)
4. Add random generator features to attacking classes
5. Add multitargetting
6. Add socks proxy support
7. Add attack exporting (xml serialization)
8. Add universal interface for attacks
9. Add hivemind support - need GOOD code
10. Add support for new interface by Stam
