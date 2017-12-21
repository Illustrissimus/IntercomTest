The program has been built in both debug and release mode.

Debug version can be found in [Solution root]/IntercomConsoleApp/bin/Debug/ folder.
Release version can be found in [Solution root]/IntercomConsoleApp/bin/Release/ folder.
In both cases, the program can be ran by running the IntercomConsoleApp.exe.

Customer file, customers.txt, is copied to the output directory by default and no additional actions are required to read it. However, alternative path
can be specified in the App.settings file by adding a key-value specifying the path. This is done by adding something like this inside <appSettings>:

<add key="customerfile" value="./customers.txt" />.

If the program encounters an error, it will display the help text.

This application was developed using Visual Studio 2017.