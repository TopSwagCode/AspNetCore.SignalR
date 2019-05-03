 # TopSwagCode.SignalR

Small work in progress for showcasing stuff to do with WebSockets and how simple it is getting started :)

Contains 4 small sample usages:

* Chat -> Small simple chat between all users.
* Graph -> Shows some numbers to all users. Eg. Admin dashboard.
* Process -> Shows how you could offload some work but still keep your user informed on progress.
* Stocks -> Show how to broadcast complex models using json.

Run app locally:

Now run the Dotnet project by running:

```console
$ dotnet run
```

Or you could run the server with docker ;)

```console
$ docker run --name topswagcode -p 51133:80 kiksen1987/signalr
```

Now open your browser of chouse at http://localhost:4000 and you should have links to the 4 demo apps as shown below:

# Chat

![Chat App](chatapp.gif "Chat App")

# Graph

![Graph App](graphapp.gif "Graph App")

# Processing

![Processing App](processingapp.gif "Processing App")

# Stock

![Stock App](stockapp.gif "Stock App")