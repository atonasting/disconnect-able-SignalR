# Disconnectable SignalR demo

SignalR does not support disconnect client from server. This is a demo project to solve the problem.

It bulids a web chatroom, each browser tab open a new WebSocket connection. Everyone could send message to others and kick them out.

WebSocket connections and their contexts are saved in a Dictionary. Connection manager class uses dependency injection into the hub.

Framework: .net 5