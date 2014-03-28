var zmq = require('zmq');
var sub = zmq.socket('sub');
var dealer = zmq.socket('dealer');

sub.on('message', function(msg) {
	console.log(msg.toString());
});

dealer.connect('tcp://127.0.0.1:30002');
dealer.send('channel');
dealer.on('message', function(msg) {
	console.log('Message received: ' + msg);
});
