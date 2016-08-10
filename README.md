# Roby :pencil2::page_facing_up:
The **"Reliable and Optimistic Blackboard Ynterface"** is a very simple, multi-platform and free software for drawing and screen management *(upcoming)* on interactive whiteboards. *Dedicated to Roberto Schiavon.*

It works under **Windows** (with *.NET 4.0*) and **GNU/Linux** (installing the *mono-complete* package).

It's translated to Italian :it: and English :uk:.

![alt tag](http://i65.tinypic.com/juxxrs.jpg)

:floppy_disk: **Compiled download link!** :floppy_disk:

https://drive.google.com/open?id=0B1eKs0rkHHF4UjJxVklJVDlqVzQ

# Compiling and Running on Ubuntu/Debian
```
sudo apt install mono-complete mono-devel git
git clone https://github.com/giulioz/Roby.git
cd Roby
xbuild /p:Configuration=Release
cd roby/bin/Release
./roby.exe
```

# Compiling and Running on windows
Download the zip on Github, extract it, open a command prompt and type:
```
"%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" Roby-master\roby.sln
```
Then type:
```
Roby-master\roby\bin\Debug\roby.exe
```
