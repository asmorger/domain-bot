// See https://aka.ms/new-console-template for more information

using DBot;

var app = new CommandApp();
app.Configure(AppCommands.Configure);

return app.Run(args);
