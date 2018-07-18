# TopSwagCode.SignalR

Small work in progress for showcasing what to do with WebSockets and how simple it is to get started :)

Contains 3 small sample usages:

* Chat -> Small simple chat between all users.
* Graph -> Shows Server numbers to all users. Eg. Admin site or stock tradeing.
* Process -> Shows how you could offload some work but still keep your user uptodate on progress before its done.

Made graph with SQS to start of with, but switched over to FakeSQS for easier startup. So people without AWS could try the sample.

Will make a guide in future of how to run this Project