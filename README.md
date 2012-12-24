#**GAS**
GAS is opensource stress-test tool originally based on IRC-LOIC(github.com/neweracracker/loic)

#TODO
1. Rewrite attack methods using async IO
2. Add support of setting any http field: host/cookie/referrer/etc
3. Optimize RANDOM generator — it's really slow.
4. Add random generator features to attacking classes
5. Add multitargetting
6. Add socks proxy support
7. Add attack exporting (xml serialization)
8. Add universal interface for attacks
9. Add hivemind support - need GOOD code
10. Add support for new interface by Stam

#HELP

#====String generator====

##Random string generator syntax:
	Warning! Expression string will NOT be validated
###Integer
		Validator Regex: \{I:[DH]:[0-9]+:[0-9]+\}
		String format: {I:type:_from:to}
			type
				D:dec
				H- 0x....
			Example:
				{I:D:0:1000}
			Result example
				384
###Character
		Validator Regex: \{C:[0-9]+:[0-9]+\}
		String format: {C:_from:to}
			Example
				{C:1:65535}
			Result example
				Ё
###String
		Validator Regex: \{S:[DHLaRSAU]:[0-9]+:[0-9]+\}
		String format: {S:type:min_length:max_length}
			type
				D,      //0-9
				H,      //0-f
				L,      //a-Z
				a,      //a-z
				R,      //*
				S,      //0-Z
				A,      //A-Z
				U       //full UTF-8
			Example:
				{S:a:3:1000}
			Result example
				gfdfyhtueyrstgdfggfr
###Mutiple generator invocation
		//2+ level expressions are **VERY SLOW**.
		Validator Regex(broken): \{R:\{.*\}:[0-9]+:[0-9]+\}
		String format: {R:{expressions}:min_count:_max_count} 
			Example
				{R:{{S:L:1:5}={S:U:1:50}&}:1:5}
			Result example
				werf=%A1%B3&tjy=%5F%9C%2D%42%A1%B3&ertg=%39%7E%E8%B2&
