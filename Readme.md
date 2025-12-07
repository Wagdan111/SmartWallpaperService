# SmartWallpaperService

SmartWallpaperService is a lightweight Windows background service designed to automatically rotate desktop wallpapers from a specified folder.  
It remembers the last used image, logs activity, and works smoothly with Task Scheduler or as a standalone Windows Service.

---

## Features

- Automatically changes desktop wallpaper
- Rotates images from a selected folder
- Saves the current image index (does not repeat after restart)
- Creates log files for tracking
- Works with Task Scheduler or Windows Service
- Lightweight and fast

---

## Requirements

- Windows
- .NET Framework 4.8+
- A folder containing images (jpg / png / bmp)

---

## How it works

1. The service reads all images from the specified folder
2. It selects the next image using a rotating index
3. Sets the image as the desktop wallpaper
4. Saves the index for the next run
5. Logs all operations

---

## Configuration

Edit the `App.config` file:

```xml
<add key="FolderImagesPath" value="C:\Your\Image\Folder\Here" />
<add key="Logs" value="C:\Your\Logs\Here" />
 Usage with Task Scheduler

You can schedule it to run every 12 hours:

Open Task Scheduler

Create a new task

Set trigger: Daily / Every 12 hours

Set action: Start your .exe file
Save

Example Log
2025-05-08 16:42:01 ==> Service started successfully
2025-05-08 16:42:01 ==> Wallpaper changed to: image_4.jpg
2025-05-08 16:55:10 ==> Service stopped successfully