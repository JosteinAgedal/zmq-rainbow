var zmq = require('zmq');
var dealer = zmq.socket('dealer');
var pub = zmq.socket('pub');
var router = zmq.socket('router');

dealer.bind('tcp://*:30000', function(err) {
	if (err) console.log(err);
});

pub.bind('tcp://*:30001', function(err) {
	if (err) console.log(err);
});

router.bind('tcp://*:30002', function(err) {
	if (err) console.log(err);
});

var subscribers = {};

router.on('message', function(id, msg) {
	subscribers[id.toString('base64')] = msg;
});

dealer.on('message', function(msg) {
	pub.send(msg);

	Object.keys(subscribers).forEach(function(id) {
		router.send(new Buffer(id, 'base64'), zmq.ZMQ_SNDMORE);
		router.send(msg);
	});
});
