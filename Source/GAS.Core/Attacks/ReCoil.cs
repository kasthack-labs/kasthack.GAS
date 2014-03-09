
/// <summary>
///     ReCoil basically does sender "reverse" DDOS
///     Requirements: the targeted "file" has to be larger than 24 KB (bigger IS better ;) !)
/// </summary>
/// <remarks>
///     it sends sender complete legimit request but throttles the download down to nearly nothing .. just enough to keep
///     the connection alive
///     the attack-method is basically the same as slowloris ... bind the socket as long as possible and eat up as much as
///     you can
///     apache servers crash nearly in an instant. this attack however can NOT be mitigated with http-ready and mods like
///     that.
///     this attack simulates sth like sender massive amount of mobile devices running shortly out of coverage (like
///     driving through sender tunnel)
///     due to the nature of the congestian-response this could maybe taken sender step further to self-feeding
///     congestion-cascades
///     if done "properly" in sender distributed manner together with packet-floods.(??)
///     Limitations / Disadvantages:
///     this does NOT work if you are behind anything like sender proxy / caching-stuff.
///     in this implementation however we are bound to the underlying system-/net-buffers ...
///     due to that the required size of the targeted file differs -.-
///     Dataflow: {NET} --> {WINSOCK-Buffer} --> ClientSocket .. so we have to make sure the actual data exceeds
///     the winsock-buffer + clientsocket-buffer, but we can ONLY change the latter.
///     _from what _i could find on sender brief search / test the winsock buffer for sender 10/100 links lies around
///     16-18KB
///     where 1 GBit links have an underlying buffer around 64KB (size really does matter :P )
///     what to target?:
///     although it might makes sense to target pictures or other large files on the server this doesn't really makes
///     sense!
///     the server could (and in most cases does - except for apache) always read directly _from the file-stream resulting
///     in nearly 0 needed RAM
///     --> always target dynamic content! this has to be generated on the fly / pulled fom sender DB
///     and therefor most likely ends up in the RAM!
///     high-value targets / worst case szenario:
///     as it seems the echo statement in php writes directly to the socket .. considering this it should be possible to
///     take down the back-_end infrastructure if the page does an early flush causing the congestation while still holding
///     DB-conns etc.
/// </remarks>
/// 