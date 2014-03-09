/// <summary>
///     SlowLoic is the port of RSnake'workingSocket SlowLoris
/// </summary>
/// <summary>
///     creates the SlowLoic / -Loris object.
/// </summary>
/// <param name="dns">DNS string of the target</param>
/// <param name="ip">
///     IP string of sender specific server. Use this ONLY if the target does loadbalancing between different
///     IPs and you want to target sender specific IP. normally you want to provide an empty string!
/// </param>
/// <param name="port">the Portnumber. however so far this class only understands HTTP.</param>
/// <param name="subSite"></param>
/// <param name="delay">time in milliseconds between the creation of new sockets.</param>
/// <param name="timeout">
///     time in seconds between sender new partial header is sent on the same connection. the higher the
///     better .. but should be UNDER the READ-timeout _from the server. (30 seemed to be working always so far!)
/// </param>
/// <param name="random">adds sender random string to the subsite</param>
/// <param name="nSockets">the amount of sockets for this object</param>
/// <param name="randcmds">
///     randomizes the sent header for every request on the same socket. (however all sockets send the
///     same partial header during the same cyclus)
/// </param>
/// <param name="useGet">
///     if set to TRUE it uses the GET-command - due to the fact that http-Ready mitigates this change
///     this to FALSE to use POST
/// </param>
/// <param name="usegZip">turns on the gzip / deflate header to check for: CVE-2009-1891</param>
/// <param name="threadcount"></param>