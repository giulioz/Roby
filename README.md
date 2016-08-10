# Roby :pencil2::page_facing_up:
The "Reliable and Optimistic Blackboard Ynterface", a multi-platform software for interactive whiteboard designed for lazy teachers. Dedicated to Roberto Schiavon.

Works under **Windows** (with .NET 4.0) and **GNU/Linux** (installing the *mono-complete* 4.0 package).

It's translated to Italian :it: and English :uk:.

![alt tag](http://i65.tinypic.com/juxxrs.jpg)

# Compiling and Running on Ubuntu/Debian
```
sudo apt install mono-complete mono-devel git
git clone https://github.com/giulioz/Roby.git
cd Roby
xbuild /p:Configuration=Release
cd roby/bin/Release
./roby.exe
```

# roby.conf
You can specify the double monitor settings under the file *roby.conf* (executable folder on Windows and */home/(user)/roby.conf* on Linux).
You need to write on every line of this file: the monitor number, monitors resolutions and visibility on the taskbar.

Example:
```
1
1680
1050
1024
768
false
```